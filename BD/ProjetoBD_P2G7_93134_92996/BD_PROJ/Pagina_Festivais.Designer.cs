
namespace BD_PROJ
{
    partial class Pagina_Festivais
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
            this.label1 = new System.Windows.Forms.Label();
            this.TbReference = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbName = new System.Windows.Forms.TextBox();
            this.Nome = new System.Windows.Forms.Label();
            this.TbHora = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.TbVenue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TbGenero = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbData = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.BtPatrocinios = new System.Windows.Forms.Button();
            this.BtStaff = new System.Windows.Forms.Button();
            this.BtArtist = new System.Windows.Forms.Button();
            this.listView4 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.TbEndDate = new System.Windows.Forms.TextBox();
            this.TbEndTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TbPreco = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(549, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Novo Festival";
            // 
            // TbReference
            // 
            this.TbReference.Location = new System.Drawing.Point(37, 129);
            this.TbReference.Name = "TbReference";
            this.TbReference.Size = new System.Drawing.Size(163, 23);
            this.TbReference.TabIndex = 1;
            this.TbReference.TextChanged += new System.EventHandler(this.TbReference_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Referencia (9 digitos)";
            // 
            // TbName
            // 
            this.TbName.Location = new System.Drawing.Point(37, 193);
            this.TbName.Name = "TbName";
            this.TbName.Size = new System.Drawing.Size(344, 23);
            this.TbName.TabIndex = 3;
            this.TbName.TextChanged += new System.EventHandler(this.TbName_TextChanged);
            // 
            // Nome
            // 
            this.Nome.AutoSize = true;
            this.Nome.Location = new System.Drawing.Point(37, 175);
            this.Nome.Name = "Nome";
            this.Nome.Size = new System.Drawing.Size(40, 15);
            this.Nome.TabIndex = 4;
            this.Nome.Text = "Nome";
            // 
            // TbHora
            // 
            this.TbHora.Location = new System.Drawing.Point(273, 250);
            this.TbHora.Name = "TbHora";
            this.TbHora.Size = new System.Drawing.Size(108, 23);
            this.TbHora.TabIndex = 6;
            this.TbHora.TextChanged += new System.EventHandler(this.TbHora_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Data Inicio (ano-dia-mes) ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(273, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = " Inicio (hh:mm:ss)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(39, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(344, 47);
            this.button1.TabIndex = 9;
            this.button1.Text = "Arrendar Local";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 694);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(344, 48);
            this.button2.TabIndex = 10;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TbVenue
            // 
            this.TbVenue.Location = new System.Drawing.Point(39, 392);
            this.TbVenue.Name = "TbVenue";
            this.TbVenue.ReadOnly = true;
            this.TbVenue.Size = new System.Drawing.Size(344, 23);
            this.TbVenue.TabIndex = 11;
            this.TbVenue.TextChanged += new System.EventHandler(this.TbVenue_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 365);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Local";
            // 
            // TbGenero
            // 
            this.TbGenero.Location = new System.Drawing.Point(216, 129);
            this.TbGenero.Name = "TbGenero";
            this.TbGenero.Size = new System.Drawing.Size(165, 23);
            this.TbGenero.TabIndex = 13;
            this.TbGenero.TextChanged += new System.EventHandler(this.TbGenero_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(216, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Genero Musical";
            // 
            // TbData
            // 
            this.TbData.Location = new System.Drawing.Point(39, 250);
            this.TbData.Name = "TbData";
            this.TbData.Size = new System.Drawing.Size(216, 23);
            this.TbData.TabIndex = 15;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(449, 129);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(628, 95);
            this.listView1.TabIndex = 16;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sponsor";
            this.columnHeader1.Width = 620;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(449, 286);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(628, 95);
            this.listView2.TabIndex = 17;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Staff";
            this.columnHeader2.Width = 620;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6});
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(449, 471);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(628, 95);
            this.listView3.TabIndex = 18;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Concerto";
            this.columnHeader6.Width = 620;
            // 
            // BtPatrocinios
            // 
            this.BtPatrocinios.Location = new System.Drawing.Point(1083, 129);
            this.BtPatrocinios.Name = "BtPatrocinios";
            this.BtPatrocinios.Size = new System.Drawing.Size(109, 95);
            this.BtPatrocinios.TabIndex = 19;
            this.BtPatrocinios.Text = "Adicionar Patrocinios";
            this.BtPatrocinios.UseVisualStyleBackColor = true;
            this.BtPatrocinios.Click += new System.EventHandler(this.BtPatrocinios_Click);
            // 
            // BtStaff
            // 
            this.BtStaff.Location = new System.Drawing.Point(1083, 286);
            this.BtStaff.Name = "BtStaff";
            this.BtStaff.Size = new System.Drawing.Size(109, 95);
            this.BtStaff.TabIndex = 20;
            this.BtStaff.Text = "Adicionar Staff";
            this.BtStaff.UseVisualStyleBackColor = true;
            this.BtStaff.Click += new System.EventHandler(this.BtStaff_Click);
            // 
            // BtArtist
            // 
            this.BtArtist.Location = new System.Drawing.Point(1083, 471);
            this.BtArtist.Name = "BtArtist";
            this.BtArtist.Size = new System.Drawing.Size(109, 95);
            this.BtArtist.TabIndex = 21;
            this.BtArtist.Text = "Criar Concerto";
            this.BtArtist.UseVisualStyleBackColor = true;
            this.BtArtist.Click += new System.EventHandler(this.BtArtist_Click);
            // 
            // listView4
            // 
            this.listView4.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView4.Enabled = false;
            this.listView4.HideSelection = false;
            this.listView4.Location = new System.Drawing.Point(39, 536);
            this.listView4.Name = "listView4";
            this.listView4.Size = new System.Drawing.Size(344, 136);
            this.listView4.TabIndex = 22;
            this.listView4.UseCompatibleStateImageBehavior = false;
            this.listView4.View = System.Windows.Forms.View.Details;
            this.listView4.SelectedIndexChanged += new System.EventHandler(this.listView4_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Id Palco";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Nome";
            this.columnHeader4.Width = 200;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Capacidade";
            this.columnHeader5.Width = 80;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 511);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Palcos";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // TbEndDate
            // 
            this.TbEndDate.Location = new System.Drawing.Point(39, 323);
            this.TbEndDate.Name = "TbEndDate";
            this.TbEndDate.Size = new System.Drawing.Size(216, 23);
            this.TbEndDate.TabIndex = 24;
            // 
            // TbEndTime
            // 
            this.TbEndTime.Location = new System.Drawing.Point(273, 323);
            this.TbEndTime.Name = "TbEndTime";
            this.TbEndTime.Size = new System.Drawing.Size(108, 23);
            this.TbEndTime.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 302);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 15);
            this.label8.TabIndex = 26;
            this.label8.Text = "Data Fim (ano-dia-mes)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(273, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 15);
            this.label9.TabIndex = 27;
            this.label9.Text = "Fim (hh:mm:ss)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(922, 695);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 15);
            this.label10.TabIndex = 28;
            this.label10.Text = "Preço total";
            // 
            // TbPreco
            // 
            this.TbPreco.Enabled = false;
            this.TbPreco.Location = new System.Drawing.Point(922, 718);
            this.TbPreco.Name = "TbPreco";
            this.TbPreco.Size = new System.Drawing.Size(181, 23);
            this.TbPreco.TabIndex = 29;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1109, 717);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 30;
            this.button3.Text = "Atualizar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(449, 694);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(450, 47);
            this.button4.TabIndex = 31;
            this.button4.Text = "Ver Cartaz Atual";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Pagina_Festivais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 755);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TbPreco);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TbEndTime);
            this.Controls.Add(this.TbEndDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listView4);
            this.Controls.Add(this.BtArtist);
            this.Controls.Add(this.BtStaff);
            this.Controls.Add(this.BtPatrocinios);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.TbData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TbGenero);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TbVenue);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TbHora);
            this.Controls.Add(this.Nome);
            this.Controls.Add(this.TbName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbReference);
            this.Controls.Add(this.label1);
            this.Name = "Pagina_Festivais";
            this.Text = "Pagina_Festivais";
            this.Load += new System.EventHandler(this.Pagina_Festivais_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TbReference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbName;
        private System.Windows.Forms.Label Nome;
        private System.Windows.Forms.TextBox TbHora;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox TbVenue;
        private System.Windows.Forms.TextBox TbGenero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TbData;
        private System.Windows.Forms.Button BtPatrocinios;
        private System.Windows.Forms.Button BtStaff;
        private System.Windows.Forms.Button BtArtist;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ListView listView4;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TbEndDate;
        private System.Windows.Forms.TextBox TbEndTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TbPreco;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        public System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.Button button4;
    }
}