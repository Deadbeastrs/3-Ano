
namespace BD_PROJ
{
    partial class Página_organizador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Página_organizador));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtContStaff = new System.Windows.Forms.Button();
            this.BtContratarArtista = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TbName = new System.Windows.Forms.TextBox();
            this.TbTele = new System.Windows.Forms.TextBox();
            this.TbMail = new System.Windows.Forms.TextBox();
            this.BtEdit = new System.Windows.Forms.Button();
            this.BtOk = new System.Windows.Forms.Button();
            this.BtFestivais = new System.Windows.Forms.Button();
            this.BtAddPat = new System.Windows.Forms.Button();
            this.BtApagarFestival = new System.Windows.Forms.Button();
            this.BtCartaz = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(54, 158);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 308);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BtContStaff
            // 
            this.BtContStaff.Location = new System.Drawing.Point(869, 103);
            this.BtContStaff.Name = "BtContStaff";
            this.BtContStaff.Size = new System.Drawing.Size(133, 58);
            this.BtContStaff.TabIndex = 3;
            this.BtContStaff.Text = "Contratar Staff";
            this.BtContStaff.UseVisualStyleBackColor = true;
            this.BtContStaff.Click += new System.EventHandler(this.BtContStaff_Click);
            // 
            // BtContratarArtista
            // 
            this.BtContratarArtista.Location = new System.Drawing.Point(869, 187);
            this.BtContratarArtista.Name = "BtContratarArtista";
            this.BtContratarArtista.Size = new System.Drawing.Size(133, 58);
            this.BtContratarArtista.TabIndex = 4;
            this.BtContratarArtista.Text = "Contratar Artista";
            this.BtContratarArtista.UseVisualStyleBackColor = true;
            this.BtContratarArtista.Click += new System.EventHandler(this.BtContratarArtista_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(442, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(442, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Número de telefóne";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(442, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "email";
            // 
            // TbName
            // 
            this.TbName.Location = new System.Drawing.Point(442, 187);
            this.TbName.Name = "TbName";
            this.TbName.Size = new System.Drawing.Size(346, 23);
            this.TbName.TabIndex = 12;
            this.TbName.TextChanged += new System.EventHandler(this.TbName_TextChanged);
            // 
            // TbTele
            // 
            this.TbTele.Location = new System.Drawing.Point(442, 253);
            this.TbTele.Name = "TbTele";
            this.TbTele.Size = new System.Drawing.Size(346, 23);
            this.TbTele.TabIndex = 13;
            this.TbTele.TextChanged += new System.EventHandler(this.TbTele_TextChanged);
            // 
            // TbMail
            // 
            this.TbMail.Location = new System.Drawing.Point(442, 316);
            this.TbMail.Name = "TbMail";
            this.TbMail.Size = new System.Drawing.Size(348, 23);
            this.TbMail.TabIndex = 14;
            this.TbMail.TextChanged += new System.EventHandler(this.TbMail_TextChanged);
            // 
            // BtEdit
            // 
            this.BtEdit.Location = new System.Drawing.Point(442, 376);
            this.BtEdit.Name = "BtEdit";
            this.BtEdit.Size = new System.Drawing.Size(113, 54);
            this.BtEdit.TabIndex = 15;
            this.BtEdit.Text = "Editar";
            this.BtEdit.UseVisualStyleBackColor = true;
            this.BtEdit.Click += new System.EventHandler(this.BtEdit_Click);
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(679, 375);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(111, 55);
            this.BtOk.TabIndex = 16;
            this.BtOk.Text = "Ok";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Visible = false;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // BtFestivais
            // 
            this.BtFestivais.Location = new System.Drawing.Point(869, 27);
            this.BtFestivais.Name = "BtFestivais";
            this.BtFestivais.Size = new System.Drawing.Size(133, 54);
            this.BtFestivais.TabIndex = 17;
            this.BtFestivais.Text = "Novo Festival";
            this.BtFestivais.UseVisualStyleBackColor = true;
            this.BtFestivais.Click += new System.EventHandler(this.TbFestivais_Click);
            // 
            // BtAddPat
            // 
            this.BtAddPat.Location = new System.Drawing.Point(869, 272);
            this.BtAddPat.Name = "BtAddPat";
            this.BtAddPat.Size = new System.Drawing.Size(133, 58);
            this.BtAddPat.TabIndex = 18;
            this.BtAddPat.Text = "Adicionar Patrocionador";
            this.BtAddPat.UseVisualStyleBackColor = true;
            this.BtAddPat.Click += new System.EventHandler(this.BtAddPat_Click);
            // 
            // BtApagarFestival
            // 
            this.BtApagarFestival.Location = new System.Drawing.Point(869, 358);
            this.BtApagarFestival.Name = "BtApagarFestival";
            this.BtApagarFestival.Size = new System.Drawing.Size(133, 58);
            this.BtApagarFestival.TabIndex = 19;
            this.BtApagarFestival.Text = "Apagar um festival";
            this.BtApagarFestival.UseVisualStyleBackColor = true;
            this.BtApagarFestival.Click += new System.EventHandler(this.BtApagarFestival_Click);
            // 
            // BtCartaz
            // 
            this.BtCartaz.Location = new System.Drawing.Point(870, 436);
            this.BtCartaz.Name = "BtCartaz";
            this.BtCartaz.Size = new System.Drawing.Size(132, 58);
            this.BtCartaz.TabIndex = 20;
            this.BtCartaz.Text = "Ver cartaz de um festival";
            this.BtCartaz.UseVisualStyleBackColor = true;
            this.BtCartaz.Click += new System.EventHandler(this.BtCartaz_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(869, 519);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 58);
            this.button1.TabIndex = 21;
            this.button1.Text = "Obter Informações sobre os festivais";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Página_organizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 589);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtCartaz);
            this.Controls.Add(this.BtApagarFestival);
            this.Controls.Add(this.BtAddPat);
            this.Controls.Add(this.BtFestivais);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.BtEdit);
            this.Controls.Add(this.TbMail);
            this.Controls.Add(this.TbTele);
            this.Controls.Add(this.TbName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtContratarArtista);
            this.Controls.Add(this.BtContStaff);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Página_organizador";
            this.Text = "Página_organizador";
            this.Load += new System.EventHandler(this.Página_organizador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtContStaff;
        private System.Windows.Forms.Button BtContratarArtista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbName;
        private System.Windows.Forms.TextBox TbTele;
        private System.Windows.Forms.TextBox TbMail;
        private System.Windows.Forms.Button BtEdit;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.Button BtFestivais;
        private System.Windows.Forms.Button BtAddPat;
        private System.Windows.Forms.Button BtApagarFestival;
        private System.Windows.Forms.Button BtCartaz;
        private System.Windows.Forms.Button button1;
    }
}