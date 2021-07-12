
namespace BD_PROJ
{
    partial class Pagina_Adicionar_Concerto
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
            this.LvArtista = new System.Windows.Forms.ListView();
            this.NIF = new System.Windows.Forms.ColumnHeader();
            this.NomeArtista = new System.Windows.Forms.ColumnHeader();
            this.LvMusic = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.TbIdPalco = new System.Windows.Forms.TextBox();
            this.TbIdPreco = new System.Windows.Forms.TextBox();
            this.TbIdDuracao = new System.Windows.Forms.TextBox();
            this.BtAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LvArtista
            // 
            this.LvArtista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NIF,
            this.NomeArtista});
            this.LvArtista.FullRowSelect = true;
            this.LvArtista.HideSelection = false;
            this.LvArtista.Location = new System.Drawing.Point(12, 12);
            this.LvArtista.MultiSelect = false;
            this.LvArtista.Name = "LvArtista";
            this.LvArtista.Size = new System.Drawing.Size(346, 251);
            this.LvArtista.TabIndex = 0;
            this.LvArtista.UseCompatibleStateImageBehavior = false;
            this.LvArtista.View = System.Windows.Forms.View.Details;
            this.LvArtista.SelectedIndexChanged += new System.EventHandler(this.LvArtista_SelectedIndexChanged);
            // 
            // NIF
            // 
            this.NIF.Text = "NIF";
            this.NIF.Width = 140;
            // 
            // NomeArtista
            // 
            this.NomeArtista.Text = "Nome do artista";
            this.NomeArtista.Width = 200;
            // 
            // LvMusic
            // 
            this.LvMusic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.LvMusic.Enabled = false;
            this.LvMusic.HideSelection = false;
            this.LvMusic.Location = new System.Drawing.Point(411, 12);
            this.LvMusic.Name = "LvMusic";
            this.LvMusic.Size = new System.Drawing.Size(346, 251);
            this.LvMusic.TabIndex = 1;
            this.LvMusic.UseCompatibleStateImageBehavior = false;
            this.LvMusic.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nome";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Duração";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Genero";
            this.columnHeader3.Width = 70;
            // 
            // TbIdPalco
            // 
            this.TbIdPalco.Location = new System.Drawing.Point(119, 310);
            this.TbIdPalco.Name = "TbIdPalco";
            this.TbIdPalco.Size = new System.Drawing.Size(534, 23);
            this.TbIdPalco.TabIndex = 3;
            // 
            // TbIdPreco
            // 
            this.TbIdPreco.Location = new System.Drawing.Point(119, 399);
            this.TbIdPreco.Name = "TbIdPreco";
            this.TbIdPreco.Size = new System.Drawing.Size(251, 23);
            this.TbIdPreco.TabIndex = 4;
            // 
            // TbIdDuracao
            // 
            this.TbIdDuracao.Location = new System.Drawing.Point(399, 399);
            this.TbIdDuracao.Name = "TbIdDuracao";
            this.TbIdDuracao.Size = new System.Drawing.Size(254, 23);
            this.TbIdDuracao.TabIndex = 5;
            // 
            // BtAdd
            // 
            this.BtAdd.Location = new System.Drawing.Point(119, 450);
            this.BtAdd.Name = "BtAdd";
            this.BtAdd.Size = new System.Drawing.Size(534, 23);
            this.BtAdd.TabIndex = 6;
            this.BtAdd.Text = "Adicionar concerto";
            this.BtAdd.UseVisualStyleBackColor = true;
            this.BtAdd.Click += new System.EventHandler(this.BtAdd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Id do Palco";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Preço";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(399, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Duração (hh:mm:ss)";
            // 
            // Pagina_Adicionar_Concerto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 504);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtAdd);
            this.Controls.Add(this.TbIdDuracao);
            this.Controls.Add(this.TbIdPreco);
            this.Controls.Add(this.TbIdPalco);
            this.Controls.Add(this.LvMusic);
            this.Controls.Add(this.LvArtista);
            this.Name = "Pagina_Adicionar_Concerto";
            this.Text = "Pagina_Adicionar_Concerto";
            this.Load += new System.EventHandler(this.Pagina_Adicionar_Concerto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LvArtista;
        private System.Windows.Forms.ListView LvMusic;
        private System.Windows.Forms.TextBox TbIdPalco;
        private System.Windows.Forms.TextBox TbIdPreco;
        private System.Windows.Forms.TextBox TbIdDuracao;
        private System.Windows.Forms.Button BtAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader NIF;
        private System.Windows.Forms.ColumnHeader NomeArtista;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}