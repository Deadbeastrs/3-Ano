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
    public partial class Pagina_Adicionar_Patrocinios : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        Pagina_Festivais pag;
        string Festival_Reference;

        public Pagina_Adicionar_Patrocinios(string OrganizerNif, Pagina_Festivais pg, string Fest)
        {
            OrganizadorNIF = OrganizerNif;
            pag = pg;
            Festival_Reference = Fest;
            InitializeComponent();
        }

        private void Pagina_Adicionar_Patrocinios_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM dbo.SearchSponsor(@NIF,@Reference);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@NIF", OrganizadorNIF);
            cmd.Parameters.AddWithValue("@Reference", Festival_Reference);
            

            cmd.Connection = cn;

            SqlDataReader reader = cmd.ExecuteReader();

            listView1.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["SPONSOR_ID"].ToString());
                item.SubItems.Add(reader["NAME"].ToString());
                item.SubItems.Add(reader["DESCRIPTION"].ToString());
                listView1.Items.Add(item);
            }
            reader.Close();
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
        
            SqlCommand cmd = new SqlCommand("SponsorFestival", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Sponsor_ID", listView1.SelectedItems[0].SubItems[0].Text);
            cmd.Parameters.AddWithValue("@Festival_Reference", Festival_Reference);
            cmd.Parameters.AddWithValue("@Investment", TbPreco.Text);
            
            try
            {
                cmd.ExecuteNonQuery();
                ListViewItem item = listView1.SelectedItems[0];
                string sp = "Sponsor ID: " + item.SubItems[0].Text + ", Nome: " + item.SubItems[1].Text + " Descrição: " + item.SubItems[2].Text;
                string cleaned = sp.Replace("\n", "");
                pag.listView1.Items.Add(sp);
                System.Windows.Forms.MessageBox.Show("Patrocinador adicionado");
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados invalidos:\n" + ex.Message);
                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
