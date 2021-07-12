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
    public partial class Pagina_Festivais : Form
    {
        private string OrganizadorNIF;
        private SqlConnection cn;

        public Pagina_Festivais(string OrganizadorNif)
        {
            OrganizadorNIF = OrganizadorNif;
            InitializeComponent();

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

        private void Pagina_Festivais_Load(object sender, EventArgs e)
        {
            listView1.Enabled = false;
            listView2.Enabled = false;
            listView3.Enabled = false;
            BtArtist.Enabled = false;
            BtPatrocinios.Enabled = false;
            BtStaff.Enabled = false;
            button4.Enabled = false;
        }


        private void TbReference_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbVenue_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pagina_Venues pg = new Pagina_Venues(OrganizadorNIF,this);
            pg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertFestival", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            
            cmd.Parameters.AddWithValue("@Reference", TbReference.Text);
            cmd.Parameters.AddWithValue("@Name", TbName.Text);
            cmd.Parameters.AddWithValue("@Genre",TbGenero.Text);
            cmd.Parameters.AddWithValue("@Start_Date", TbData.Text + ' ' + TbHora.Text);
            cmd.Parameters.AddWithValue("@End_Date", TbEndDate.Text + ' ' + TbEndTime.Text);
            cmd.Parameters.AddWithValue("@Venue", TbVenue.Text);
            cmd.Parameters.AddWithValue("@Nif", OrganizadorNIF);
            cmd.Connection = cn;
            

            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Festival criado com sucesso");
                TbReference.Enabled = false;
                TbHora.Enabled = false;
                TbGenero.Enabled = false;
                TbName.Enabled = false;
                TbData.Enabled = false;
                button2.Enabled = false;
                listView1.Enabled = true;
                listView2.Enabled = true;
                listView3.Enabled = true;
                BtArtist.Enabled = true;
                BtPatrocinios.Enabled = true;
                TbEndTime.Enabled = false;
                TbEndDate.Enabled = false;
                BtStaff.Enabled = true;
                button1.Enabled = false;
                button4.Enabled = true;

            }

            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados do Festival Inválidos " + ex.Message);
                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }

            finally
            {
                cn.Close();
            }

        }

        private void TbGenero_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtArtist_Click(object sender, EventArgs e)
        {
            Pagina_Adicionar_Concerto pg = new Pagina_Adicionar_Concerto(OrganizadorNIF, TbReference.Text, this);
            pg.ShowDialog();
        }

        private void BtStaff_Click(object sender, EventArgs e)
        {
            Pagina_Adicionar_Staff pg = new Pagina_Adicionar_Staff(OrganizadorNIF, this,TbReference.Text);
            pg.ShowDialog();
        }

        private void BtPatrocinios_Click(object sender, EventArgs e)
        {
            
            Pagina_Adicionar_Patrocinios pg = new Pagina_Adicionar_Patrocinios(OrganizadorNIF, this, TbReference.Text);
            pg.ShowDialog();

        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("Select dbo.GetPriceAll(@FestivalReference)", cn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FestivalReference", TbReference.Text);
            cmd.ExecuteNonQuery();
            int rtn = (int) cmd.ExecuteScalar();
            TbPreco.Text = rtn.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Cartaz pg = new Cartaz(Convert.ToInt32(TbReference.Text));
            pg.ShowDialog();
        }
    }
}
