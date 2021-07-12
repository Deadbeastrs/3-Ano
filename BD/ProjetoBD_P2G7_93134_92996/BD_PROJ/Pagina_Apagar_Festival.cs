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
    public partial class Pagina_Apagar_Festival : Form
    {
        private SqlConnection cn;
        private string OrganizadorNif;
        public Pagina_Apagar_Festival(string OrgNif)
        {
            OrganizadorNif = OrgNif;
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("DeleteFestival", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Reference", comboBox1.SelectedItem.ToString().Split(":")[0]);
                cmd.Parameters.AddWithValue("@OrganizerNIF", OrganizadorNif);
                cmd.Connection = cn;
                try
                {
                    cmd.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("Festival Apagado Com sucesso");

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Dados Inválidos");

                    throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
                }
                finally
                {
                    cn.Close();
                }
                comboBox1.Items.Remove(comboBox1.SelectedItem);
            }
            

            
        }

        private void Pagina_Apagar_Festival_Load(object sender, EventArgs e)
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
    }
}
