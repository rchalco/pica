namespace NMDManagement.Administration
{
    partial class FrmCalibrateDispenser
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
            this.btnCalibrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCalibrar
            // 
            this.btnCalibrar.Location = new System.Drawing.Point(35, 384);
            this.btnCalibrar.Name = "btnCalibrar";
            this.btnCalibrar.Size = new System.Drawing.Size(340, 64);
            this.btnCalibrar.TabIndex = 0;
            this.btnCalibrar.Text = "Calibrar";
            this.btnCalibrar.UseVisualStyleBackColor = true;
            this.btnCalibrar.Click += new System.EventHandler(this.btnCalibrar_Click);
            // 
            // FrmCalibrateDispenser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 478);
            this.Controls.Add(this.btnCalibrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCalibrateDispenser";
            this.Text = "Calibrar Dispensador";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCalibrar;
    }
}