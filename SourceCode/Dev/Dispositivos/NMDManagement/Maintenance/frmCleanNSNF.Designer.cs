namespace NMDManagement.Maintenance
{
    partial class frmCleanNSNF
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNF4 = new System.Windows.Forms.Button();
            this.btnNF3 = new System.Windows.Forms.Button();
            this.btnNF2 = new System.Windows.Forms.Button();
            this.btnNF1 = new System.Windows.Forms.Button();
            this.btnLimpiarNS = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNF4);
            this.groupBox1.Controls.Add(this.btnNF3);
            this.groupBox1.Controls.Add(this.btnNF2);
            this.groupBox1.Controls.Add(this.btnNF1);
            this.groupBox1.Controls.Add(this.btnLimpiarNS);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Limpiar:";
            // 
            // btnNF4
            // 
            this.btnNF4.Location = new System.Drawing.Point(309, 61);
            this.btnNF4.Name = "btnNF4";
            this.btnNF4.Size = new System.Drawing.Size(81, 31);
            this.btnNF4.TabIndex = 6;
            this.btnNF4.Text = "NF 4 Rollers";
            this.btnNF4.UseVisualStyleBackColor = true;
            this.btnNF4.Click += new System.EventHandler(this.btnNF4_Click);
            // 
            // btnNF3
            // 
            this.btnNF3.Location = new System.Drawing.Point(208, 61);
            this.btnNF3.Name = "btnNF3";
            this.btnNF3.Size = new System.Drawing.Size(81, 31);
            this.btnNF3.TabIndex = 5;
            this.btnNF3.Text = "NF 3 Rollers";
            this.btnNF3.UseVisualStyleBackColor = true;
            this.btnNF3.Click += new System.EventHandler(this.btnNF3_Click);
            // 
            // btnNF2
            // 
            this.btnNF2.Location = new System.Drawing.Point(102, 61);
            this.btnNF2.Name = "btnNF2";
            this.btnNF2.Size = new System.Drawing.Size(81, 31);
            this.btnNF2.TabIndex = 4;
            this.btnNF2.Text = "NF 2 Rollers";
            this.btnNF2.UseVisualStyleBackColor = true;
            this.btnNF2.Click += new System.EventHandler(this.btnNF2_Click);
            // 
            // btnNF1
            // 
            this.btnNF1.Location = new System.Drawing.Point(6, 61);
            this.btnNF1.Name = "btnNF1";
            this.btnNF1.Size = new System.Drawing.Size(81, 31);
            this.btnNF1.TabIndex = 3;
            this.btnNF1.Text = "NF 1 Rollers";
            this.btnNF1.UseVisualStyleBackColor = true;
            this.btnNF1.Click += new System.EventHandler(this.btnNF1_Click);
            // 
            // btnLimpiarNS
            // 
            this.btnLimpiarNS.Location = new System.Drawing.Point(6, 26);
            this.btnLimpiarNS.Name = "btnLimpiarNS";
            this.btnLimpiarNS.Size = new System.Drawing.Size(384, 29);
            this.btnLimpiarNS.TabIndex = 2;
            this.btnLimpiarNS.Text = "NS Rollers";
            this.btnLimpiarNS.UseVisualStyleBackColor = true;
            this.btnLimpiarNS.Click += new System.EventHandler(this.btnLimpiarNS_Click);
            // 
            // frmCleanNSNF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 143);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCleanNSNF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Limpiar NS y NF\'s";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNF4;
        private System.Windows.Forms.Button btnNF3;
        private System.Windows.Forms.Button btnNF2;
        private System.Windows.Forms.Button btnNF1;
        private System.Windows.Forms.Button btnLimpiarNS;
    }
}