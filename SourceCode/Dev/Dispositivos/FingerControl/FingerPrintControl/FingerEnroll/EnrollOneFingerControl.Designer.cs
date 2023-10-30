namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    partial class EnrollOneFingerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel7 = new System.Windows.Forms.Panel();
            this.N4 = new System.Windows.Forms.Panel();
            this.N3 = new System.Windows.Forms.Panel();
            this.N2 = new System.Windows.Forms.Panel();
            this.N1 = new System.Windows.Forms.Panel();
            this.pbFingerprint = new System.Windows.Forms.PictureBox();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).BeginInit();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackgroundImage = global::Prodem.Fingerprint.FingerPrintControl.Properties.Resources.Numeros;
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.N4);
            this.panel7.Controls.Add(this.N3);
            this.panel7.Controls.Add(this.N2);
            this.panel7.Controls.Add(this.N1);
            this.panel7.Location = new System.Drawing.Point(7, 6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(250, 104);
            this.panel7.TabIndex = 17;
            // 
            // N4
            // 
            this.N4.BackColor = System.Drawing.Color.Transparent;
            this.N4.Location = new System.Drawing.Point(188, 3);
            this.N4.Name = "N4";
            this.N4.Size = new System.Drawing.Size(57, 90);
            this.N4.TabIndex = 2;
            // 
            // N3
            // 
            this.N3.BackColor = System.Drawing.Color.Transparent;
            this.N3.Location = new System.Drawing.Point(122, 3);
            this.N3.Name = "N3";
            this.N3.Size = new System.Drawing.Size(60, 90);
            this.N3.TabIndex = 2;
            // 
            // N2
            // 
            this.N2.BackColor = System.Drawing.Color.Transparent;
            this.N2.Location = new System.Drawing.Point(67, 3);
            this.N2.Name = "N2";
            this.N2.Size = new System.Drawing.Size(49, 90);
            this.N2.TabIndex = 2;
            // 
            // N1
            // 
            this.N1.BackColor = System.Drawing.Color.Transparent;
            this.N1.Location = new System.Drawing.Point(3, 3);
            this.N1.Name = "N1";
            this.N1.Size = new System.Drawing.Size(58, 90);
            this.N1.TabIndex = 2;
            // 
            // pbFingerprint
            // 
            this.pbFingerprint.Location = new System.Drawing.Point(263, 6);
            this.pbFingerprint.Name = "pbFingerprint";
            this.pbFingerprint.Size = new System.Drawing.Size(110, 104);
            this.pbFingerprint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbFingerprint.TabIndex = 18;
            this.pbFingerprint.TabStop = false;
            // 
            // EnrollOneFingerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.pbFingerprint);
            this.Name = "EnrollOneFingerControl";
            this.Size = new System.Drawing.Size(380, 117);
            this.Load += new System.EventHandler(this.EnrollOneFingerControl_Load);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbFingerprint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel N4;
        private System.Windows.Forms.Panel N3;
        private System.Windows.Forms.Panel N2;
        private System.Windows.Forms.Panel N1;
        private System.Windows.Forms.PictureBox pbFingerprint;
    }
}
