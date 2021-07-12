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
    public partial class Contratar_Staff : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        public Contratar_Staff(string OrgNif)
        {
            OrganizadorNIF = OrgNif;
            InitializeComponent();
        }

        private void Contratar_Staff_Load(object sender, EventArgs e)
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

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertStaff", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", TbNome.Text);
            cmd.Parameters.AddWithValue("@Phone", TbTele.Text);
            cmd.Parameters.AddWithValue("@NIF", TbNIF.Text);
            cmd.Parameters.AddWithValue("@Price", TbPrice.Text);
            cmd.Parameters.AddWithValue("@Staff_number", TbNumeroStaff.Text);
            cmd.Parameters.AddWithValue("@NIF_Organizer",OrganizadorNIF);
            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Staff Contratado com sucesso");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados Inválidos \n ERROR: " + ex.Message);

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }
    }
}
