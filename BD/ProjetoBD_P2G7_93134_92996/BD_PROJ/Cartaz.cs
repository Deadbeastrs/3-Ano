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
    public partial class Cartaz : Form
    {
        private int FestivalReference;
        private SqlConnection cn;
        public Cartaz(int Fes)
        {
            FestivalReference = Fes;
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
        private void Cartaz_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT NAME, START_DATE, END_DATE, LOCATION_VENUE FROM FESTIVAL WHERE REFERENCE = @Reference";
            cmd.Parameters.AddWithValue("@Reference", FestivalReference);
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                label1.Text = "Festival " + reader["NAME"].ToString();
                label3.Text = "De dia " + reader["START_DATE"].ToString() + " até " + reader["END_DATE"].ToString();
                label2.Text = "Em " + reader["LOCATION_VENUE"].ToString();
            }

            reader.Close();

            
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM dbo.GetFestivalMusics(@FestivalReference)", cn);
            cmd2.Parameters.Clear();
            cmd2.Parameters.AddWithValue("@FestivalReference", FestivalReference);
            SqlDataReader reader2 = cmd2.ExecuteReader();
            string Musics = "";
            while (reader2.Read())
            {
                Musics = Musics + reader2["NAME"].ToString() + "\n";
            }
            reader2.Close();

            label4.Text = Musics;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
