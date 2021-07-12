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
    public partial class Pagina_Festival_Info : Form
    {
        private SqlConnection cn;
        private string OrganizadorNif;
        public Pagina_Festival_Info(string OrgNIF)
        {
            OrganizadorNif = OrgNIF;
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

        private void Pagina_Festival_Info_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.SearchFestival(@NIF)", cn);
            cmd.Parameters.AddWithValue("@NIF", OrganizadorNif);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                string Reference = reader["REFERENCE"].ToString();
                string Name = reader["NAME"].ToString();
                comboBox1.Items.Add(Reference + ":" + Name);
            }
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand("Select * FROM dbo.GetFestivalSponsors(@FestivalReference)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem("NOME: " + reader["NAME"].ToString());
                    ListViewItem item1 = new ListViewItem("Descrição: " + reader["DESCRIPTION"].ToString());
                   
                    ListViewItem item4 = new ListViewItem("\n");
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                   
                    listView2.Items.Add(item4);
                }
                reader.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM dbo.GetFestivalMainInfo(@Reference)";
                cmd.Parameters.AddWithValue("@Reference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem("NOME: " + reader["NAME"].ToString());
                    ListViewItem item1 = new ListViewItem("De dia " + reader["START_DATE"].ToString() + " até " + reader["END_DATE"].ToString());
                    ListViewItem item2 = new ListViewItem("Em " + reader["LOCATION_VENUE"].ToString());
     
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                    listView2.Items.Add(item2);
                }
                reader.Close();

                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd2 = new SqlCommand("Select dbo.GetPriceAll(@FestivalReference)", cn);
                cmd2.Parameters.Clear();
                cmd2.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.ExecuteNonQuery();
                int rtn = (int)cmd2.ExecuteScalar();
                ListViewItem item3 = new ListViewItem("Preço Total " + rtn.ToString());
                listView2.Items.Add(item3);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand("Select * FROM dbo.FestivalGetAllStaff(@OrganizerNIF,@FestivalReference)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OrganizerNIF", OrganizadorNif);
                cmd.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem("NOME: " + reader["NAME"].ToString());
                    ListViewItem item1 = new ListViewItem("NIF: " + reader["NIF"].ToString());
                    ListViewItem item2 = new ListViewItem("Telemovel: " + reader["PHONE_NUMBER"].ToString());
                    ListViewItem item3 = new ListViewItem("Preço: " + reader["PRICE"].ToString());
                    ListViewItem item4 = new ListViewItem("\n");
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                    listView2.Items.Add(item2);
                    listView2.Items.Add(item3);
                    listView2.Items.Add(item4);
                }
                reader.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand("Select * FROM dbo.FestivalGetAllArtists(@FestivalReference)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    ListViewItem item1 = new ListViewItem("NIF: " + reader["NIF"].ToString());
                    ListViewItem item = new ListViewItem("NOME: " + reader["ARTIST_NAME"].ToString());
                    ListViewItem item4 = new ListViewItem("\n");
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                    listView2.Items.Add(item4);

                }
                reader.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand("Select * FROM dbo.GetFestivalEquipment(@FestivalReference)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ListViewItem item1 = new ListViewItem("Custo: " + reader["COST"].ToString());
                    ListViewItem item = new ListViewItem("NOME: " + reader["NAME"].ToString());
                    ListViewItem item2 = new ListViewItem("Tipo de equipamento: " + reader["TYPE_NAME"].ToString());
                    ListViewItem item4 = new ListViewItem("\n");
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                    listView2.Items.Add(item2);
                    listView2.Items.Add(item4);

                }
                reader.Close();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                listView2.Items.Clear();
                if (!verifySGBDConnection())
                {
                    return;
                }

                SqlCommand cmd = new SqlCommand("Select * FROM dbo.GetFestivalStages(@FestivalReference)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@FestivalReference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ListViewItem item1 = new ListViewItem("ID: " + reader["STAGE_ID"].ToString());
                    ListViewItem item = new ListViewItem("NOME: " + reader["NAME"].ToString());
                    ListViewItem item2 = new ListViewItem("Capacidade: " + reader["CAPCITY"].ToString());
                    ListViewItem item4 = new ListViewItem("\n");
                    listView2.Items.Add(item);
                    listView2.Items.Add(item1);
                    listView2.Items.Add(item2);
                    listView2.Items.Add(item4);

                }
                reader.Close();

            }

        }
    }
}
