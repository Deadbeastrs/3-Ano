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
    public partial class Pagina_Inicial : Form
    {
        private SqlConnection cn;
        
        public Pagina_Inicial()
        {
            InitializeComponent();   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Organizador inválido");
            }
            else {
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("Select dbo.CheckCredentials(@NIF,@Password)", cn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Password", textBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@NIF", comboBox1.Text.Split(":")[0]);
                cmd.ExecuteNonQuery();
                int rtn = (int)cmd.ExecuteScalar();
                
                if (rtn == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Palavra pass errada");
                    return;
                }
                else 
                {
                    Página_organizador pg = new Página_organizador(comboBox1.Text.Split(":")[0]);
                    pg.ShowDialog();
                }
                       
            }   
            
        }

        private void Pagina_Inicial_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM SearchOrganizer", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Organizer O = new Organizer();
                O.Telemovel = Convert.ToInt32(reader["PHONE_NUMBER"]);
                O.Email = reader["MAIL_ADDRESS"].ToString();
                O.NIF = reader["NIF"].ToString();
                O.Name= reader["NAME"].ToString();
                comboBox1.Items.Add(O.ToString());
            }
            reader.Close();
        }

        private SqlConnection getSGBDConnection()
        {
            Class1 cls = new Class1();
            string connection = cls.getConnection();
            return new SqlConnection(connection);
            //return new SqlConnection(connection);
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pagina_Introduzir_Organizador pg = new Pagina_Introduzir_Organizador();
            pg.ShowDialog();
        }
    }
}
