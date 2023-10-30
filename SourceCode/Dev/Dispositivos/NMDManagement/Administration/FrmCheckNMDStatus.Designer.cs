namespace NMDManagement.Administration
{
    partial class FrmCheckNMDStatus
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
            this.e = new System.Windows.Forms.TabControl();
            this.Dispensador = new System.Windows.Forms.TabPage();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnFinger = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnEject = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnInicializa = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Nro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tarea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.e.SuspendLayout();
            this.Dispensador.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // e
            // 
            this.e.Controls.Add(this.Dispensador);
            this.e.Controls.Add(this.tabPage2);
            this.e.Controls.Add(this.tabPage3);
            this.e.Location = new System.Drawing.Point(12, 12);
            this.e.Name = "e";
            this.e.SelectedIndex = 0;
            this.e.Size = new System.Drawing.Size(411, 331);
            this.e.TabIndex = 2;
            // 
            // Dispensador
            // 
            this.Dispensador.Controls.Add(this.btnVerificar);
            this.Dispensador.Controls.Add(this.dataGridView1);
            this.Dispensador.Location = new System.Drawing.Point(4, 22);
            this.Dispensador.Name = "Dispensador";
            this.Dispensador.Padding = new System.Windows.Forms.Padding(3);
            this.Dispensador.Size = new System.Drawing.Size(403, 305);
            this.Dispensador.TabIndex = 0;
            this.Dispensador.Text = "Dispensador";
            this.Dispensador.UseVisualStyleBackColor = true;
            // 
            // btnVerificar
            // 
            this.btnVerificar.Location = new System.Drawing.Point(65, 260);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(264, 36);
            this.btnVerificar.TabIndex = 2;
            this.btnVerificar.Text = "Verificar";
            this.btnVerificar.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nro,
            this.Tarea,
            this.Proceso});
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(389, 248);
            this.dataGridView1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnFinger);
            this.tabPage2.Controls.Add(this.btnReset);
            this.tabPage2.Controls.Add(this.btnEject);
            this.tabPage2.Controls.Add(this.btnRead);
            this.tabPage2.Controls.Add(this.btnInicializa);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tarjeta y Huella";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnFinger
            // 
            this.btnFinger.Location = new System.Drawing.Point(23, 181);
            this.btnFinger.Name = "btnFinger";
            this.btnFinger.Size = new System.Drawing.Size(170, 49);
            this.btnFinger.TabIndex = 4;
            this.btnFinger.Text = "Captura huella";
            this.btnFinger.UseVisualStyleBackColor = true;
            this.btnFinger.Click += new System.EventHandler(this.btnFinger_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(210, 107);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(170, 49);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnEject
            // 
            this.btnEject.Location = new System.Drawing.Point(23, 107);
            this.btnEject.Name = "btnEject";
            this.btnEject.Size = new System.Drawing.Size(170, 49);
            this.btnEject.TabIndex = 2;
            this.btnEject.Text = "Expulsar";
            this.btnEject.UseVisualStyleBackColor = true;
            this.btnEject.Click += new System.EventHandler(this.btnEject_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(210, 34);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(170, 49);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Leer tarjeta";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnInicializa
            // 
            this.btnInicializa.Location = new System.Drawing.Point(23, 34);
            this.btnInicializa.Name = "btnInicializa";
            this.btnInicializa.Size = new System.Drawing.Size(170, 49);
            this.btnInicializa.TabIndex = 0;
            this.btnInicializa.Text = "Inicializa";
            this.btnInicializa.UseVisualStyleBackColor = true;
            this.btnInicializa.Click += new System.EventHandler(this.btnInicializa_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(403, 305);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Receptor";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Nro
            // 
            this.Nro.DataPropertyName = "Nro";
            this.Nro.HeaderText = "Nro.";
            this.Nro.Name = "Nro";
            this.Nro.ReadOnly = true;
            this.Nro.Width = 30;
            // 
            // Tarea
            // 
            this.Tarea.DataPropertyName = "ProccessName";
            this.Tarea.HeaderText = "Tarea";
            this.Tarea.Name = "Tarea";
            this.Tarea.ReadOnly = true;
            this.Tarea.Width = 180;
            // 
            // Proceso
            // 
            this.Proceso.DataPropertyName = "StatusProccess";
            this.Proceso.HeaderText = "Proceso";
            this.Proceso.Name = "Proceso";
            this.Proceso.ReadOnly = true;
            this.Proceso.Width = 80;
            // 
            // FrmCheckNMDStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 357);
            this.Controls.Add(this.e);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCheckNMDStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Verifica estado de dispensador";
            this.e.ResumeLayout(false);
            this.Dispensador.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl e;
        private System.Windows.Forms.TabPage Dispensador;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnFinger;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEject;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnInicializa;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nro;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tarea;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proceso;
    }
}