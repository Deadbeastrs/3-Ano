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
    public partial class Página_organizador : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        public Página_organizador(string NIF)
        {
            OrganizadorNIF = NIF;
            InitializeComponent();
            TbName.Enabled = false;
            TbMail.Enabled = false;
            TbTele.Enabled = false;
            
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


        private void TbName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void TbTele_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbMail_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertOrganizer",cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", TbName.Text);
            cmd.Parameters.AddWithValue("@Phone", Convert.ToInt32(TbTele.Text));
            cmd.Parameters.AddWithValue("@NIF", OrganizadorNIF);
            cmd.Parameters.AddWithValue("@Mail", TbMail.Text);
            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
                TbName.Enabled = false;
                TbMail.Enabled = false;
                TbTele.Enabled = false;
                BtOk.Visible = false;
                BtEdit.Visible = true;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados Inválidos: \n" + ex.Message);

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void BtEdit_Click(object sender, EventArgs e)
        {
            TbName.Enabled = true;
            TbMail.Enabled = true;
            TbTele.Enabled = true;
            BtOk.Visible = true;
            BtEdit.Visible = false;

        }


        private void Página_organizador_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            Console.WriteLine(OrganizadorNIF);
            SqlCommand cmd = new SqlCommand("Select * FROM dbo.GetOrganizerInfo(@OrganizerNIF)", cn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrganizerNIF", OrganizadorNIF);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Organizer O = new Organizer();
                O.Telemovel = Convert.ToInt32(reader["PHONE_NUMBER"]);
                O.Email = reader["MAIL_ADDRESS"].ToString();
                O.NIF = reader["NIF"].ToString();
                O.Name = reader["NAME"].ToString();
                TbMail.Text = O.Email.ToString();
                TbName.Text = O.Name.ToString();
                TbTele.Text = O.Telemovel.ToString();

            }
            reader.Close();

        }

        private void TbFestivais_Click(object sender, EventArgs e)
        {
            Pagina_Festivais pg = new Pagina_Festivais(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void BtContStaff_Click(object sender, EventArgs e)
        {
            Contratar_Staff pg = new Contratar_Staff(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void BtContratarArtista_Click(object sender, EventArgs e)
        {
            Contratar_Artista pg = new Contratar_Artista(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void BtAddPat_Click(object sender, EventArgs e)
        {
            Contactar_Sponsor pg = new Contactar_Sponsor(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void BtApagarFestival_Click(object sender, EventArgs e)
        {
            Pagina_Apagar_Festival pg = new Pagina_Apagar_Festival(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void BtCartaz_Click(object sender, EventArgs e)
        {
            Escolher_Festival pg = new Escolher_Festival(OrganizadorNIF);

            pg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pagina_Festival_Info pg = new Pagina_Festival_Info(OrganizadorNIF);

            pg.ShowDialog();
        }
    }
}
