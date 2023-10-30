namespace NMDManagement.Administration
{
    partial class FrmDispencerAccountingBalance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBandejas = new System.Windows.Forms.DataGridView();
            this.Bandeja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Corte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arqueo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRegistraArqueo = new System.Windows.Forms.Button();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCloseCassette = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejas)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtObs);
            this.groupBox1.Controls.Add(this.btnRegistraArqueo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dgvBandejas);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 282);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dgvBandejas
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBandejas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBandejas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBandejas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bandeja,
            this.IdCount,
            this.IdMoney,
            this.Corte,
            this.Saldo,
            this.Arqueo});
            this.dgvBandejas.Location = new System.Drawing.Point(6, 19);
            this.dgvBandejas.Name = "dgvBandejas";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBandejas.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvBandejas.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBandejas.Size = new System.Drawing.Size(470, 171);
            this.dgvBandejas.TabIndex = 2;
            // 
            // Bandeja
            // 
            this.Bandeja.DataPropertyName = "NroCassette";
            this.Bandeja.HeaderText = "Bandeja";
            this.Bandeja.Name = "Bandeja";
            this.Bandeja.ReadOnly = true;
            this.Bandeja.Width = 80;
            // 
            // IdCount
            // 
            this.IdCount.DataPropertyName = "IdCount";
            this.IdCount.HeaderText = "IdCount";
            this.IdCount.Name = "IdCount";
            this.IdCount.Visible = false;
            // 
            // IdMoney
            // 
            this.IdMoney.DataPropertyName = "IdMoney";
            this.IdMoney.HeaderText = "IdMoney";
            this.IdMoney.Name = "IdMoney";
            this.IdMoney.Visible = false;
            // 
            // Corte
            // 
            this.Corte.DataPropertyName = "Denomination";
            this.Corte.HeaderText = "Corte";
            this.Corte.Name = "Corte";
            this.Corte.ReadOnly = true;
            this.Corte.Width = 80;
            // 
            // Saldo
            // 
            this.Saldo.DataPropertyName = "SystemBalance";
            this.Saldo.HeaderText = "Saldo";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Visible = false;
            // 
            // Arqueo
            // 
            this.Arqueo.DataPropertyName = "RegisterBalance";
            this.Arqueo.HeaderText = "Arqueo";
            this.Arqueo.Name = "Arqueo";
            this.Arqueo.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Observación:";
            // 
            // btnRegistraArqueo
            // 
            this.btnRegistraArqueo.Location = new System.Drawing.Point(108, 233);
            this.btnRegistraArqueo.Name = "btnRegistraArqueo";
            this.btnRegistraArqueo.Size = new System.Drawing.Size(231, 43);
            this.btnRegistraArqueo.TabIndex = 5;
            this.btnRegistraArqueo.Text = "Registrar Arqueo";
            this.btnRegistraArqueo.UseVisualStyleBackColor = true;
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(78, 198);
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(398, 20);
            this.txtObs.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnCloseCassette);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(487, 282);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnCloseCassette
            // 
            this.btnCloseCassette.Location = new System.Drawing.Point(112, 208);
            this.btnCloseCassette.Name = "btnCloseCassette";
            this.btnCloseCassette.Size = new System.Drawing.Size(231, 43);
            this.btnCloseCassette.TabIndex = 5;
            this.btnCloseCassette.Text = "Cerrar bandeja";
            this.btnCloseCassette.UseVisualStyleBackColor = true;
            this.btnCloseCassette.Click += new System.EventHandler(this.btnCloseCassette_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(401, 121);
            this.label2.TabIndex = 6;
            this.label2.Text = "Finalizado el arqueo, vuelva a ingresar las bandejas  en el mismo sitio y presion" +
    "e el botón <Cerrar bandejas>";
            // 
            // FrmDispencerAccountingBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 304);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmDispencerAccountingBalance";
            this.Text = "Arqueo de dispensador";
            this.Load += new System.EventHandler(this.FrmDispencerAccountingBalance_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCloseCassette;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.Button btnRegistraArqueo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBandejas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bandeja;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn Corte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arqueo;
    }
}