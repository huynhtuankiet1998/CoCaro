namespace cocaro
{
    partial class Form1
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
            this.btnThoat = new System.Windows.Forms.Button();
            this.pnlBanco = new System.Windows.Forms.Panel();
            this.btnstart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(281, 521);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 28);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // pnlBanco
            // 
            this.pnlBanco.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlBanco.ForeColor = System.Drawing.Color.DarkSalmon;
            this.pnlBanco.Location = new System.Drawing.Point(11, 11);
            this.pnlBanco.Margin = new System.Windows.Forms.Padding(2);
            this.pnlBanco.Name = "pnlBanco";
            this.pnlBanco.Size = new System.Drawing.Size(503, 506);
            this.pnlBanco.TabIndex = 4;
            this.pnlBanco.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBanco_Paint);
            this.pnlBanco.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlBanco_MouseClick);
            // 
            // btnstart
            // 
            this.btnstart.Location = new System.Drawing.Point(153, 522);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(90, 28);
            this.btnstart.TabIndex = 5;
            this.btnstart.Text = "Bắt Đầu";
            this.btnstart.UseVisualStyleBackColor = true;
            this.btnstart.Click += new System.EventHandler(this.btnstart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 559);
            this.Controls.Add(this.btnstart);
            this.Controls.Add(this.pnlBanco);
            this.Controls.Add(this.btnThoat);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Caro Game";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel pnlBanco;
        private System.Windows.Forms.Button btnstart;
    }
}

