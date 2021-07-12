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
    public partial class Pagina_Venues : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        Pagina_Festivais pag;
        public Pagina_Venues(string OrganizadorNif,Pagina_Festivais pg)
        {
            OrganizadorNIF = OrganizadorNif;
            pag = pg;
            InitializeComponent();
        }

        private void Pagina_Venues_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SearchVenue(@NIF)",cn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@NIF", OrganizadorNIF);
            SqlDataReader reader = cmd.ExecuteReader();

            listView1.Items.Clear();
            while (reader.Read()) {
                ListViewItem item = new ListViewItem(reader["LOCATION"].ToString());
                item.SubItems.Add(reader["NAME"].ToString());
                item.SubItems.Add(reader["CAPACITY"].ToString());
                item.SubItems.Add(reader["NUMBER_OF_STAGES"].ToString());
                item.SubItems.Add(reader["PRICE"].ToString());
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection album = this.listView1.SelectedItems;
            if (album.Count == 0)
                return;

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.GetStages(@Venue) ", cn);
            cmd2.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@Venue", listView1.SelectedItems[0].Text.ToString());

            SqlDataReader reader = cmd2.ExecuteReader();

            listView2.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["STAGE_ID"].ToString());
                item.SubItems.Add(reader["NAME"].ToString());
                item.SubItems.Add(reader["CAPCITY"].ToString());
                listView2.Items.Add(item);
            }
            reader.Close();
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            
            SqlCommand cmd = new SqlCommand("UpdateVenue", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Location", listView1.SelectedItems[0].Text.ToString());
            cmd.Parameters.AddWithValue("@organizerNIF", OrganizadorNIF);
            cmd.Connection = cn;

            SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.GetStages(@Venue)", cn);
            cmd2.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@Venue", listView1.SelectedItems[0].Text.ToString());

            SqlDataReader reader = cmd2.ExecuteReader();

            pag.listView4.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["STAGE_ID"].ToString());
                item.SubItems.Add(reader["NAME"].ToString());
                item.SubItems.Add(reader["CAPCITY"].ToString());
                pag.listView4.Items.Add(item);
            }
            reader.Close();


            try
            {

                cmd.ExecuteNonQuery();
                pag.TbVenue.Text = listView1.SelectedItems[0].Text.ToString();
                pag.button1.Enabled = false;



            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados Inválidos: \n" + ex.Message);

               // throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {












        }
    }
}
