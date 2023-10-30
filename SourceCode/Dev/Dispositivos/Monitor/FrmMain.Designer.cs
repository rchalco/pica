namespace Monitor
{
    partial class FrmMain
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
            this.rtHuella = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.rtCardReader = new System.Windows.Forms.RichTextBox();
            this.cmbTipoLector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.dgPublisher = new System.Windows.Forms.DataGridView();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPublisher)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtHuella);
            this.groupBox1.Location = new System.Drawing.Point(17, 93);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 124);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data de la Huella";
            // 
            // rtHuella
            // 
            this.rtHuella.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtHuella.Location = new System.Drawing.Point(3, 16);
            this.rtHuella.Name = "rtHuella";
            this.rtHuella.Size = new System.Drawing.Size(477, 105);
            this.rtHuella.TabIndex = 0;
            this.rtHuella.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.rtCardReader);
            this.groupBox2.Controls.Add(this.cmbTipoLector);
            this.groupBox2.Location = new System.Drawing.Point(20, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(480, 125);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Tarjeta";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Verificar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtCardReader
            // 
            this.rtCardReader.Location = new System.Drawing.Point(7, 48);
            this.rtCardReader.Name = "rtCardReader";
            this.rtCardReader.Size = new System.Drawing.Size(470, 77);
            this.rtCardReader.TabIndex = 1;
            this.rtCardReader.Text = "";
            // 
            // cmbTipoLector
            // 
            this.cmbTipoLector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoLector.FormattingEnabled = true;
            this.cmbTipoLector.Items.AddRange(new object[] {
            "CREATOR - ATM",
            "GEMPLUS - WORKSTATION"});
            this.cmbTipoLector.Location = new System.Drawing.Point(7, 20);
            this.cmbTipoLector.Name = "cmbTipoLector";
            this.cmbTipoLector.Size = new System.Drawing.Size(231, 21);
            this.cmbTipoLector.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "INGRESE EL IP O EL SERIAL: ";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(31, 39);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(343, 20);
            this.txtIP.TabIndex = 5;
            // 
            // btnVerificar
            // 
            this.btnVerificar.Location = new System.Drawing.Point(396, 39);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(75, 23);
            this.btnVerificar.TabIndex = 6;
            this.btnVerificar.Text = "VERIFICAR";
            this.btnVerificar.UseVisualStyleBackColor = true;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // dgPublisher
            // 
            this.dgPublisher.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPublisher.Location = new System.Drawing.Point(17, 10);
            this.dgPublisher.Name = "dgPublisher";
            this.dgPublisher.Size = new System.Drawing.Size(483, 77);
            this.dgPublisher.TabIndex = 7;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgPublisher);
            this.pnlMain.Controls.Add(this.groupBox2);
            this.pnlMain.Controls.Add(this.groupBox1);
            this.pnlMain.Location = new System.Drawing.Point(14, 86);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(517, 360);
            this.pnlMain.TabIndex = 8;
            this.pnlMain.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(318, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Leer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(399, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "expulsar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Expulsar);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 459);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnVerificar);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.label1);
            this.Name = "FrmMain";
            this.Text = "Monitor PICA";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPublisher)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.DataGridView dgPublisher;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.RichTextBox rtHuella;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtCardReader;
        private System.Windows.Forms.ComboBox cmbTipoLector;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}