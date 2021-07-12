
namespace BD_PROJ
{
    partial class Pagina_Adicionar_Staff
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.TbPalcoId = new System.Windows.Forms.TextBox();
            this.CbTarefa = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(612, 210);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "NIF";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Numero de telemovel";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nome";
            this.columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Preço";
            this.columnHeader4.Width = 100;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 321);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 61);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // TbPalcoId
            // 
            this.TbPalcoId.Location = new System.Drawing.Point(12, 264);
            this.TbPalcoId.Name = "TbPalcoId";
            this.TbPalcoId.Size = new System.Drawing.Size(300, 23);
            this.TbPalcoId.TabIndex = 2;
            this.TbPalcoId.TextChanged += new System.EventHandler(this.TbPalcoId_TextChanged);
            // 
            // CbTarefa
            // 
            this.CbTarefa.FormattingEnabled = true;
            this.CbTarefa.Location = new System.Drawing.Point(332, 264);
            this.CbTarefa.Name = "CbTarefa";
            this.CbTarefa.Size = new System.Drawing.Size(292, 23);
            this.CbTarefa.TabIndex = 3;
            this.CbTarefa.SelectedIndexChanged += new System.EventHandler(this.CbTarefa_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Id do Palco";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(332, 242);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tarefa";
            // 
            // Pagina_Adicionar_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 403);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CbTarefa);
            this.Controls.Add(this.TbPalcoId);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Name = "Pagina_Adicionar_Staff";
            this.Text = "Pagina_Adicionar_Staff";
            this.Load += new System.EventHandler(this.Pagina_Adicionar_Staff_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox TbPalcoId;
        private System.Windows.Forms.ComboBox CbTarefa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}