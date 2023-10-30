namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    partial class GetFingerControl
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
            this.pbFinger = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFinger)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFinger
            // 
            this.pbFinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbFinger.Location = new System.Drawing.Point(0, 0);
            this.pbFinger.Name = "pbFinger";
            this.pbFinger.Size = new System.Drawing.Size(206, 247);
            this.pbFinger.TabIndex = 1;
            this.pbFinger.TabStop = false;
            // 
            // GetFingerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbFinger);
            this.Name = "GetFingerControl";
            this.Size = new System.Drawing.Size(206, 247);
            this.Load += new System.EventHandler(this.GetFingerControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFinger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFinger;
    }
}
