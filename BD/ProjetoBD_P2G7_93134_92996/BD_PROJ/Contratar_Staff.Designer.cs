
namespace BD_PROJ
{
    partial class Contratar_Staff
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
            this.TbNome = new System.Windows.Forms.TextBox();
            this.TbTele = new System.Windows.Forms.TextBox();
            this.TbNIF = new System.Windows.Forms.TextBox();
            this.TbPrice = new System.Windows.Forms.TextBox();
            this.TbNumeroStaff = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TbNome
            // 
            this.TbNome.Location = new System.Drawing.Point(44, 92);
            this.TbNome.Name = "TbNome";
            this.TbNome.Size = new System.Drawing.Size(501, 23);
            this.TbNome.TabIndex = 0;
            // 
            // TbTele
            // 
            this.TbTele.Location = new System.Drawing.Point(44, 161);
            this.TbTele.Name = "TbTele";
            this.TbTele.Size = new System.Drawing.Size(501, 23);
            this.TbTele.TabIndex = 1;
            // 
            // TbNIF
            // 
            this.TbNIF.Location = new System.Drawing.Point(44, 233);
            this.TbNIF.Name = "TbNIF";
            this.TbNIF.Size = new System.Drawing.Size(501, 23);
            this.TbNIF.TabIndex = 2;
            // 
            // TbPrice
            // 
            this.TbPrice.Location = new System.Drawing.Point(44, 305);
            this.TbPrice.Name = "TbPrice";
            this.TbPrice.Size = new System.Drawing.Size(238, 23);
            this.TbPrice.TabIndex = 3;
            // 
            // TbNumeroStaff
            // 
            this.TbNumeroStaff.Location = new System.Drawing.Point(289, 305);
            this.TbNumeroStaff.Name = "TbNumeroStaff";
            this.TbNumeroStaff.Size = new System.Drawing.Size(256, 23);
            this.TbNumeroStaff.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Telemovel (9 digitos)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "NIF (9 digitos)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Preço";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(289, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Número de Staff";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(201, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 32);
            this.label6.TabIndex = 10;
            this.label6.Text = "Contratar Staff";
            // 
            // BtOk
            // 
            this.BtOk.Location = new System.Drawing.Point(201, 368);
            this.BtOk.Name = "BtOk";
            this.BtOk.Size = new System.Drawing.Size(168, 57);
            this.BtOk.TabIndex = 11;
            this.BtOk.Text = "OK";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // Contratar_Staff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 450);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TbNumeroStaff);
            this.Controls.Add(this.TbPrice);
            this.Controls.Add(this.TbNIF);
            this.Controls.Add(this.TbTele);
            this.Controls.Add(this.TbNome);
            this.Name = "Contratar_Staff";
            this.Text = "Contratar_Staff";
            this.Load += new System.EventHandler(this.Contratar_Staff_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbNome;
        private System.Windows.Forms.TextBox TbTele;
        private System.Windows.Forms.TextBox TbNIF;
        private System.Windows.Forms.TextBox TbPrice;
        private System.Windows.Forms.TextBox TbNumeroStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtOk;
    }
}