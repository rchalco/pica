namespace FingerPrintControl
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.enrollToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getOneFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elimnarComponentesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.escribirLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levantarServicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enrollToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(618, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // enrollToolStripMenuItem
            // 
            this.enrollToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enrollToolStripMenuItem1,
            this.compareToolStripMenuItem,
            this.compareFingerToolStripMenuItem,
            this.getOneFingerToolStripMenuItem,
            this.elimnarComponentesToolStripMenuItem,
            this.escribirLogToolStripMenuItem,
            this.levantarServicioToolStripMenuItem});
            this.enrollToolStripMenuItem.Name = "enrollToolStripMenuItem";
            this.enrollToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.enrollToolStripMenuItem.Text = "Menu";
            // 
            // enrollToolStripMenuItem1
            // 
            this.enrollToolStripMenuItem1.Name = "enrollToolStripMenuItem1";
            this.enrollToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.enrollToolStripMenuItem1.Text = "Enroll";
            this.enrollToolStripMenuItem1.Click += new System.EventHandler(this.enrollToolStripMenuItem1_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.compareToolStripMenuItem.Text = "Enroll One Finger";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // compareFingerToolStripMenuItem
            // 
            this.compareFingerToolStripMenuItem.Enabled = false;
            this.compareFingerToolStripMenuItem.Name = "compareFingerToolStripMenuItem";
            this.compareFingerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.compareFingerToolStripMenuItem.Text = "Compare Finger";
            this.compareFingerToolStripMenuItem.Click += new System.EventHandler(this.compareFingerToolStripMenuItem_Click);
            // 
            // getOneFingerToolStripMenuItem
            // 
            this.getOneFingerToolStripMenuItem.Name = "getOneFingerToolStripMenuItem";
            this.getOneFingerToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.getOneFingerToolStripMenuItem.Text = "Get One Finger";
            this.getOneFingerToolStripMenuItem.Click += new System.EventHandler(this.getOneFingerToolStripMenuItem_Click);
            // 
            // elimnarComponentesToolStripMenuItem
            // 
            this.elimnarComponentesToolStripMenuItem.Name = "elimnarComponentesToolStripMenuItem";
            this.elimnarComponentesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.elimnarComponentesToolStripMenuItem.Text = "Eliminar Componentes";
            this.elimnarComponentesToolStripMenuItem.Click += new System.EventHandler(this.elimnarComponentesToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(618, 503);
            this.panel1.TabIndex = 1;
            // 
            // escribirLogToolStripMenuItem
            // 
            this.escribirLogToolStripMenuItem.Name = "escribirLogToolStripMenuItem";
            this.escribirLogToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.escribirLogToolStripMenuItem.Text = "Escribir Log";
            this.escribirLogToolStripMenuItem.Click += new System.EventHandler(this.escribirLogToolStripMenuItem_Click);
            // 
            // levantarServicioToolStripMenuItem
            // 
            this.levantarServicioToolStripMenuItem.Name = "levantarServicioToolStripMenuItem";
            this.levantarServicioToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.levantarServicioToolStripMenuItem.Text = "Levantar Servicio";
            this.levantarServicioToolStripMenuItem.Click += new System.EventHandler(this.levantarServicioToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 527);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Test Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem enrollToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem compareFingerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getOneFingerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elimnarComponentesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem escribirLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levantarServicioToolStripMenuItem;
    }
}

