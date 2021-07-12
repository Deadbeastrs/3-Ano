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
    public partial class Contactar_Sponsor : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        public Contactar_Sponsor(string OrgNif)
        {
            OrganizadorNIF = OrgNif;
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

        private void Contactar_Sponsor_Load(object sender, EventArgs e)
        {

        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertSponsor", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            
            cmd.Parameters.AddWithValue("@NIF_Organizer", OrganizadorNIF);
            cmd.Parameters.AddWithValue("@Description", TbDescription.Text);
            cmd.Parameters.AddWithValue("@Name", TbNome.Text);
            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Patrocionador Adicionado com sucesso");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados do Patrocionador Inválidos");

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
