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
    public partial class Contratar_Artista : Form
    {
        private SqlConnection cn;
        private string OrganizadorNIF;
        public Contratar_Artista(string OrgNif)
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

        private void TbNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertArtist", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Name", TbNome.Text);
            cmd.Parameters.AddWithValue("@Phone", TbTele.Text);
            cmd.Parameters.AddWithValue("@NIF", TbNIF.Text);
            cmd.Parameters.AddWithValue("@Artist_Name", TbNomeArtist.Text);
            cmd.Parameters.AddWithValue("@NIF_Organizer", OrganizadorNIF);
            cmd.Connection = cn;
            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Artista Adicionado com sucesso");
                TbNome.Enabled = false;
                TbTele.Enabled = false;
                TbNIF.Enabled = false;
                TbNomeArtist.Enabled = false;
                BtOK.Enabled = false;
                TbNomeEquip.Enabled = true;
                TbRef.Enabled = true;
                TbCost.Enabled = true;
                CbType.Enabled = true;
                BtOkEq.Enabled = true;
                TbNomeMusic.Enabled = true;
                TbGenMusic.Enabled = true;
                TbDuration.Enabled = true;
                TbAddMusic.Enabled = true;


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados do Artista Inválidos: \n" + ex.Message);

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void BtOkEq_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertEquipment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ArtistNif", TbNIF.Text);
            cmd.Parameters.AddWithValue("@Reference", Convert.ToInt32(TbRef.Text));
            cmd.Parameters.AddWithValue("@Cost", TbCost.Text);
            cmd.Parameters.AddWithValue("@Name", TbNomeEquip.Text);
            cmd.Parameters.AddWithValue("@Type_ID", Convert.ToInt32((CbType.SelectedItem.ToString().Split(":")[0])));
            cmd.Connection = cn;

            
            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Equipamento Adicionada com successo");
                
                TbRef.Text = "";
                TbCost.Text = "";
                TbNomeEquip.Text = "";

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados do Equipamento Inválidos: \n" + ex.Message);

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void CbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Contratar_Artista_Load(object sender, EventArgs e)
        {
            TbNomeEquip.Enabled = false;
            TbRef.Enabled = false;
            TbCost.Enabled = false;
            CbType.Enabled = false;
            BtOkEq.Enabled = false;
            TbNomeMusic.Enabled = false;
            TbGenMusic.Enabled = false;
            TbDuration.Enabled = false;
            TbAddMusic.Enabled = false;
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM SearchEquipmentType", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = reader["TYPE_ID"].ToString();
                string typeName = reader["TYPE_NAME"].ToString();
                CbType.Items.Add(id + ':' + typeName);
            }
            reader.Close();
        }

        private void TbAddMusic_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("InsertMusic", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ArtistNif", TbNIF.Text);
            cmd.Parameters.AddWithValue("@Music_Name", TbNomeMusic.Text);
            cmd.Parameters.AddWithValue("@Music_Genre", TbGenMusic.Text);
            cmd.Parameters.AddWithValue("@Music_DURATION", TbDuration.Text);
            cmd.Connection = cn;

            

            try
            {
                cmd.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Musica Adicionada com successo");
                TbNomeMusic.Text = "";
                TbGenMusic.Text = "";
                TbDuration.Text = "";

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados da Música Inválidos: \n" + ex.Message);

                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
