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
    public partial class Pagina_Introduzir_Organizador : Form
    {
        private SqlConnection cn;
        public Pagina_Introduzir_Organizador()
        {
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
        private void Pagina_Introduzir_Organizador_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("AddOrganizer", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@NIF", textBox1.Text.ToString());
            cmd.Parameters.AddWithValue("@Phone_Number",textBox2.Text);
            cmd.Parameters.AddWithValue("@Name", textBox3.Text.ToString());
            cmd.Parameters.AddWithValue("@Email", textBox4.Text.ToString());
            cmd.Parameters.AddWithValue("@Password", textBox5.Text.ToString());
            cmd.Connection = cn;

            try
            {
                cmd.ExecuteNonQuery();
                Página_organizador pg = new Página_organizador(textBox1.Text.ToString());
                pg.ShowDialog();
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados Inválidos" + ex.Message);
                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }
    }
}
