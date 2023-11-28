namespace Agent.Device
{
    partial class frmMain
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
            btnHuella = new Button();
            SuspendLayout();
            // 
            // btnHuella
            // 
            btnHuella.Location = new Point(259, 12);
            btnHuella.Name = "btnHuella";
            btnHuella.Size = new Size(107, 23);
            btnHuella.TabIndex = 0;
            btnHuella.Text = "Capturar Huella";
            btnHuella.UseVisualStyleBackColor = true;
            btnHuella.Click += btnHuella_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.tuercas;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(378, 174);
            Controls.Add(btnHuella);
            Name = "frmMain";
            Text = "Agente";
            Load += frmMain_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnHuella;
    }
}