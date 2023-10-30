namespace NPM_GUI
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
            this.btnIniciar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFinDev = new System.Windows.Forms.Button();
            this.txtElectron = new System.Windows.Forms.TextBox();
            this.txtServe = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGenerar = new System.Windows.Forms.TextBox();
            this.txtNombreComponente = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFinProd = new System.Windows.Forms.Button();
            this.txtOblyElectron = new System.Windows.Forms.TextBox();
            this.bntElectron = new System.Windows.Forms.Button();
            this.btnServicio = new System.Windows.Forms.Button();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.txtServicioResul = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(34, 93);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(181, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar Electron en modo DEV";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(30, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Comandos  NPM";
            // 
            // btnGenerar
            // 
            this.btnGenerar.ForeColor = System.Drawing.Color.Black;
            this.btnGenerar.Location = new System.Drawing.Point(15, 97);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(216, 23);
            this.btnGenerar.TabIndex = 2;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = true;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFinDev);
            this.groupBox1.Controls.Add(this.txtElectron);
            this.groupBox1.Controls.Add(this.txtServe);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 394);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MODO DEV";
            // 
            // btnFinDev
            // 
            this.btnFinDev.ForeColor = System.Drawing.Color.Black;
            this.btnFinDev.Location = new System.Drawing.Point(22, 335);
            this.btnFinDev.Name = "btnFinDev";
            this.btnFinDev.Size = new System.Drawing.Size(181, 23);
            this.btnFinDev.TabIndex = 1;
            this.btnFinDev.Text = "Finalzar proceso";
            this.btnFinDev.UseVisualStyleBackColor = true;
            this.btnFinDev.Click += new System.EventHandler(this.btnFinDev_Click);
            // 
            // txtElectron
            // 
            this.txtElectron.BackColor = System.Drawing.Color.Gray;
            this.txtElectron.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtElectron.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtElectron.Location = new System.Drawing.Point(22, 185);
            this.txtElectron.Multiline = true;
            this.txtElectron.Name = "txtElectron";
            this.txtElectron.ReadOnly = true;
            this.txtElectron.Size = new System.Drawing.Size(243, 129);
            this.txtElectron.TabIndex = 0;
            // 
            // txtServe
            // 
            this.txtServe.BackColor = System.Drawing.Color.Gray;
            this.txtServe.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtServe.Location = new System.Drawing.Point(22, 71);
            this.txtServe.Multiline = true;
            this.txtServe.Name = "txtServe";
            this.txtServe.ReadOnly = true;
            this.txtServe.Size = new System.Drawing.Size(243, 108);
            this.txtServe.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtServicioResul);
            this.groupBox2.Controls.Add(this.txtServicio);
            this.groupBox2.Controls.Add(this.txtGenerar);
            this.groupBox2.Controls.Add(this.btnServicio);
            this.groupBox2.Controls.Add(this.txtNombreComponente);
            this.groupBox2.Controls.Add(this.btnGenerar);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(332, 69);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(248, 394);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generar Componente y Servicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre del Componente";
            // 
            // txtGenerar
            // 
            this.txtGenerar.BackColor = System.Drawing.Color.Gray;
            this.txtGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGenerar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtGenerar.Location = new System.Drawing.Point(15, 127);
            this.txtGenerar.Multiline = true;
            this.txtGenerar.Name = "txtGenerar";
            this.txtGenerar.ReadOnly = true;
            this.txtGenerar.Size = new System.Drawing.Size(216, 52);
            this.txtGenerar.TabIndex = 3;
            // 
            // txtNombreComponente
            // 
            this.txtNombreComponente.Location = new System.Drawing.Point(15, 71);
            this.txtNombreComponente.Name = "txtNombreComponente";
            this.txtNombreComponente.Size = new System.Drawing.Size(216, 20);
            this.txtNombreComponente.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFinProd);
            this.groupBox3.Controls.Add(this.txtOblyElectron);
            this.groupBox3.Controls.Add(this.bntElectron);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(601, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(283, 394);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MODO DEV";
            // 
            // btnFinProd
            // 
            this.btnFinProd.ForeColor = System.Drawing.Color.Black;
            this.btnFinProd.Location = new System.Drawing.Point(22, 339);
            this.btnFinProd.Name = "btnFinProd";
            this.btnFinProd.Size = new System.Drawing.Size(181, 23);
            this.btnFinProd.TabIndex = 1;
            this.btnFinProd.Text = "Finalzar proceso";
            this.btnFinProd.UseVisualStyleBackColor = true;
            this.btnFinProd.Click += new System.EventHandler(this.btnFinProd_Click);
            // 
            // txtOblyElectron
            // 
            this.txtOblyElectron.BackColor = System.Drawing.Color.Gray;
            this.txtOblyElectron.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOblyElectron.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtOblyElectron.Location = new System.Drawing.Point(22, 71);
            this.txtOblyElectron.Multiline = true;
            this.txtOblyElectron.Name = "txtOblyElectron";
            this.txtOblyElectron.ReadOnly = true;
            this.txtOblyElectron.Size = new System.Drawing.Size(243, 243);
            this.txtOblyElectron.TabIndex = 0;
            // 
            // bntElectron
            // 
            this.bntElectron.ForeColor = System.Drawing.Color.Black;
            this.bntElectron.Location = new System.Drawing.Point(22, 24);
            this.bntElectron.Name = "bntElectron";
            this.bntElectron.Size = new System.Drawing.Size(181, 23);
            this.bntElectron.TabIndex = 0;
            this.bntElectron.Text = "Iniciar solo Electron ";
            this.bntElectron.UseVisualStyleBackColor = true;
            this.bntElectron.Click += new System.EventHandler(this.bntElectron_Click);
            // 
            // btnServicio
            // 
            this.btnServicio.ForeColor = System.Drawing.Color.Black;
            this.btnServicio.Location = new System.Drawing.Point(15, 232);
            this.btnServicio.Name = "btnServicio";
            this.btnServicio.Size = new System.Drawing.Size(216, 23);
            this.btnServicio.TabIndex = 2;
            this.btnServicio.Text = "Generar";
            this.btnServicio.UseVisualStyleBackColor = true;
            this.btnServicio.Click += new System.EventHandler(this.btnServicio_Click);
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(15, 206);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.Size = new System.Drawing.Size(216, 20);
            this.txtServicio.TabIndex = 0;
            // 
            // txtServicioResul
            // 
            this.txtServicioResul.BackColor = System.Drawing.Color.Gray;
            this.txtServicioResul.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServicioResul.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.txtServicioResul.Location = new System.Drawing.Point(15, 262);
            this.txtServicioResul.Multiline = true;
            this.txtServicioResul.Name = "txtServicioResul";
            this.txtServicioResul.ReadOnly = true;
            this.txtServicioResul.Size = new System.Drawing.Size(216, 52);
            this.txtServicioResul.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Nombre del Servicio";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(897, 475);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "NPM TOOL";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtElectron;
        private System.Windows.Forms.TextBox txtServe;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtNombreComponente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGenerar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOblyElectron;
        private System.Windows.Forms.Button bntElectron;
        private System.Windows.Forms.Button btnFinDev;
        private System.Windows.Forms.Button btnFinProd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServicioResul;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Button btnServicio;
    }
}

