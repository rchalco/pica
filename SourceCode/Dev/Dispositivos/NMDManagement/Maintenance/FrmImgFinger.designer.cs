
namespace NMDManagement
{
    partial class FrmImgFinger
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
            this.picFinger = new System.Windows.Forms.PictureBox();
            this.picFondo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFondo)).BeginInit();
            this.SuspendLayout();
            // 
            // picFinger
            // 
            this.picFinger.Image = global::NMDManagement.Properties.Resources.huella;
            this.picFinger.Location = new System.Drawing.Point(34, 74);
            this.picFinger.Name = "picFinger";
            this.picFinger.Size = new System.Drawing.Size(183, 260);
            this.picFinger.TabIndex = 1;
            this.picFinger.TabStop = false;
            this.picFinger.Visible = false;
            // 
            // picFondo
            // 
            this.picFondo.Image = global::NMDManagement.Properties.Resources.ImgHuella;
            this.picFondo.Location = new System.Drawing.Point(5, 1);
            this.picFondo.Name = "picFondo";
            this.picFondo.Size = new System.Drawing.Size(253, 373);
            this.picFondo.TabIndex = 0;
            this.picFondo.TabStop = false;
            // 
            // FrmImgFinger
            // 
            this.ClientSize = new System.Drawing.Size(262, 377);
            this.Controls.Add(this.picFinger);
            this.Controls.Add(this.picFondo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmImgFinger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.FrmImgFinger_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFondo)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.PictureBox picFondo;
        private System.Windows.Forms.PictureBox picFinger;
    }
}