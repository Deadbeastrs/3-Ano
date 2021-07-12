using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BD_PROJ
{
    public partial class Pagina_Adicionar_Staff : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        Pagina_Festivais pag;
        string Fes;
        public Pagina_Adicionar_Staff(string OrganizerNif, Pagina_Festivais pg, string FesReference)
        {
            OrganizadorNIF = OrganizerNif;
            pag = pg;
            Fes = FesReference;
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private SqlConnection getSGBDConnection()
        {
            Class1 cls = new Class1();
            string connection = cls.getConnection();
            return new SqlConnection(connection);
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("StaffWorksOn", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@Nif", listView1.SelectedItems[0].Text.ToString());
            cmd.Parameters.AddWithValue("@STAGE_ID", TbPalcoId.Text);
            cmd.Parameters.AddWithValue("@ROLE_ID", CbTarefa.SelectedItem.ToString().Split(":")[0]);
            cmd.Parameters.AddWithValue("@Reference", Fes);

            cmd.Connection = cn;

            try
            {

                cmd.ExecuteNonQuery();
                ListViewItem item = listView1.SelectedItems[0];
                string sp = "NIF: " + item.SubItems[0].Text + ", Telemóvel: " + item.SubItems[1].Text + ", Nome: " + item.SubItems[2].Text + ", Preço: " + item.SubItems[3].Text + " Palco: " + TbPalcoId.Text + " Tarefa: " + CbTarefa.SelectedItem.ToString().Split(":")[1];
                string cleaned = sp.Replace("\n", "");
                pag.listView2.Items.Add(sp);
                System.Windows.Forms.MessageBox.Show("Staff adicionado");

            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("Dados do Staff Inválidos" + " ERRO: " + ex.Message);
                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void Pagina_Adicionar_Staff_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM dbo.GetStaffInfo(@OrganizerNIF);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrganizerNIF", OrganizadorNIF);
            cmd.Connection = cn;

            SqlDataReader reader = cmd.ExecuteReader();

            listView1.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["NIF"].ToString());
                item.SubItems.Add(reader["PHONE_NUMBER"].ToString());
                item.SubItems.Add(reader["NAME"].ToString());
                item.SubItems.Add(reader["PRICE"].ToString());
                listView1.Items.Add(item);
            }
            reader.Close();

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.SearchRole", cn);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                int roleID = Convert.ToInt32(reader2["ROLE_ID"].ToString());
                string Description = reader2["DESCRIPTION"].ToString();
                CbTarefa.Items.Add(roleID.ToString() + ":" + Description);
            }
            reader2.Close();
        }

        private void TbPalcoId_TextChanged(object sender, EventArgs e)
        {

        }

        private void CbTarefa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
