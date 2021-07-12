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
    public partial class Pagina_Adicionar_Concerto : Form
    {
        private SqlConnection cn;
        private string OrganizerNIF;
        private string Festival;
        private Pagina_Festivais pag;
        public Pagina_Adicionar_Concerto(string OrgNif,string Fest, Pagina_Festivais pg)
        {
            OrganizerNIF = OrgNif;
            Festival = Fest;
            pag = pg;
            InitializeComponent();
        }

        private void LvArtista_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection album = this.LvArtista.SelectedItems;
            if (album.Count == 0)
                return;
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM dbo.GetArtistMusic(@ArtistID);";
            cmd.Parameters.Clear();
            
            cmd.Parameters.AddWithValue("@ArtistID", LvArtista.SelectedItems[0].Text.ToString());

            cmd.Connection = cn;

            SqlDataReader reader = cmd.ExecuteReader();

            LvMusic.Items.Clear();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["NAME"].ToString());
                item.SubItems.Add(reader["DURATION"].ToString());
                item.SubItems.Add(reader["GENRE"].ToString());
                LvMusic.Items.Add(item);
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

        private void Pagina_Adicionar_Concerto_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM dbo.GetArtists(@OrganizerNIF,@Reference);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrganizerNIF", OrganizerNIF);
            cmd.Parameters.AddWithValue("@Reference", Festival);


            cmd.Connection = cn;

            SqlDataReader reader = cmd.ExecuteReader();

            LvArtista.Items.Clear();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["NIF"].ToString());
                item.SubItems.Add(reader["ARTIST_NAME"].ToString());
                LvArtista.Items.Add(item);
            }
            reader.Close();
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("AddConcert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FestivalReference", Festival);
            cmd.Parameters.AddWithValue("@ArtistNIF", LvArtista.SelectedItems[0].Text.ToString());
            cmd.Parameters.AddWithValue("@IdStage", Convert.ToInt32((TbIdPalco.Text.ToString())));
            cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(TbIdPreco.Text.ToString()));
            cmd.Parameters.AddWithValue("@Duration", TbIdDuracao.Text.ToString());
            
            cmd.Connection = cn;


            try
            {

                cmd.ExecuteNonQuery();
                ListViewItem item = new ListViewItem("Artista: " + LvArtista.SelectedItems[0].Text.ToString() + " Preço: " + TbIdPreco.Text.ToString() + " Duração: " +TbIdDuracao.Text.ToString() + " Palco: " + TbIdPalco.Text.ToString());
                System.Windows.Forms.MessageBox.Show("Concerto Criado Com successo");
                pag.listView3.Items.Add(item);


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Dados do Concerto Inválidos: \n" + ex.Message);
                //throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
