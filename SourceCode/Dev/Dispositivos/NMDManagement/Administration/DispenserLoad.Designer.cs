namespace NMDManagement.Administration
{
    partial class DispenserLoad
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBandejas = new System.Windows.Forms.DataGridView();
            this.Bandeja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Corte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Asignado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBandejas
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBandejas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBandejas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBandejas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Bandeja,
            this.Corte,
            this.Saldo,
            this.Asignado,
            this.IdCount,
            this.IdMoney});
            this.dgvBandejas.Location = new System.Drawing.Point(3, 1);
            this.dgvBandejas.Name = "dgvBandejas";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBandejas.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBandejas.Size = new System.Drawing.Size(470, 171);
            this.dgvBandejas.TabIndex = 0;
            // 
            // Bandeja
            // 
            this.Bandeja.DataPropertyName = "NroCassette";
            this.Bandeja.HeaderText = "Bandeja";
            this.Bandeja.Name = "Bandeja";
            this.Bandeja.ReadOnly = true;
            this.Bandeja.Width = 60;
            // 
            // Corte
            // 
            this.Corte.DataPropertyName = "Denomination";
            this.Corte.HeaderText = "Corte";
            this.Corte.Name = "Corte";
            this.Corte.ReadOnly = true;
            this.Corte.Width = 60;
            // 
            // Saldo
            // 
            this.Saldo.HeaderText = "Saldo";
            this.Saldo.Name = "Saldo";
            this.Saldo.ReadOnly = true;
            this.Saldo.Visible = false;
            // 
            // Asignado
            // 
            this.Asignado.DataPropertyName = "RegisterBalance";
            this.Asignado.HeaderText = "Asignado";
            this.Asignado.Name = "Asignado";
            this.Asignado.ReadOnly = true;
            // 
            // IdCount
            // 
            this.IdCount.DataPropertyName = "IdCount";
            this.IdCount.HeaderText = "IdCount";
            this.IdCount.Name = "IdCount";
            this.IdCount.ReadOnly = true;
            this.IdCount.Visible = false;
            // 
            // IdMoney
            // 
            this.IdMoney.DataPropertyName = "IdMoney";
            this.IdMoney.HeaderText = "IdMoney";
            this.IdMoney.Name = "IdMoney";
            this.IdMoney.ReadOnly = true;
            this.IdMoney.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de Policía:";
            this.label1.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(119, 183);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(353, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(84, 219);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(150, 37);
            this.btnTest.TabIndex = 4;
            this.btnTest.Text = "Validar carga";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(248, 219);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 37);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DispenserLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 268);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvBandejas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DispenserLoad";
            this.ShowIcon = false;
            this.Text = "Realizar carga de dispensador";
            this.Load += new System.EventHandler(this.DispenserLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBandejas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBandejas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnCancel;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bandeja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Corte;
        private System.Windows.Forms.DataGridViewTextBoxColumn Saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Asignado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdMoney;
    }
}