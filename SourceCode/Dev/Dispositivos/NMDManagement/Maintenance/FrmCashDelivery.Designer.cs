
namespace NMDManagement
{
    partial class FrmCashDelivery
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHabilitar = new System.Windows.Forms.Button();
            this.btnCerrarVentana = new System.Windows.Forms.Button();
            this.btnEntrega = new System.Windows.Forms.Button();
            this.txtBandeja4 = new System.Windows.Forms.TextBox();
            this.txtBandeja3 = new System.Windows.Forms.TextBox();
            this.txtBandeja2 = new System.Windows.Forms.TextBox();
            this.txtBandeja1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLiberaBandeja = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnLiberaBandeja);
            this.panel1.Controls.Add(this.btnHabilitar);
            this.panel1.Controls.Add(this.btnCerrarVentana);
            this.panel1.Controls.Add(this.btnEntrega);
            this.panel1.Controls.Add(this.txtBandeja4);
            this.panel1.Controls.Add(this.txtBandeja3);
            this.panel1.Controls.Add(this.txtBandeja2);
            this.panel1.Controls.Add(this.txtBandeja1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 274);
            this.panel1.TabIndex = 0;
            // 
            // btnHabilitar
            // 
            this.btnHabilitar.Location = new System.Drawing.Point(22, 3);
            this.btnHabilitar.Name = "btnHabilitar";
            this.btnHabilitar.Size = new System.Drawing.Size(170, 25);
            this.btnHabilitar.TabIndex = 10;
            this.btnHabilitar.Text = "Abrir y Leer ID Bandejas";
            this.btnHabilitar.UseVisualStyleBackColor = true;
            this.btnHabilitar.Click += new System.EventHandler(this.btnHabilitar_Click);
            // 
            // btnCerrarVentana
            // 
            this.btnCerrarVentana.Location = new System.Drawing.Point(22, 236);
            this.btnCerrarVentana.Name = "btnCerrarVentana";
            this.btnCerrarVentana.Size = new System.Drawing.Size(171, 29);
            this.btnCerrarVentana.TabIndex = 9;
            this.btnCerrarVentana.Text = "Cerrar Ventana";
            this.btnCerrarVentana.UseVisualStyleBackColor = true;
            this.btnCerrarVentana.Click += new System.EventHandler(this.btnCerrarVentana_Click);
            // 
            // btnEntrega
            // 
            this.btnEntrega.Location = new System.Drawing.Point(22, 165);
            this.btnEntrega.Name = "btnEntrega";
            this.btnEntrega.Size = new System.Drawing.Size(171, 29);
            this.btnEntrega.TabIndex = 8;
            this.btnEntrega.Text = "Entregar";
            this.btnEntrega.UseVisualStyleBackColor = true;
            this.btnEntrega.Click += new System.EventHandler(this.btnEntrega_Click);
            // 
            // txtBandeja4
            // 
            this.txtBandeja4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBandeja4.Location = new System.Drawing.Point(122, 131);
            this.txtBandeja4.MaxLength = 2;
            this.txtBandeja4.Name = "txtBandeja4";
            this.txtBandeja4.Size = new System.Drawing.Size(57, 23);
            this.txtBandeja4.TabIndex = 7;
            this.txtBandeja4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeja4_KeyPress);
            // 
            // txtBandeja3
            // 
            this.txtBandeja3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBandeja3.Location = new System.Drawing.Point(122, 102);
            this.txtBandeja3.MaxLength = 2;
            this.txtBandeja3.Name = "txtBandeja3";
            this.txtBandeja3.Size = new System.Drawing.Size(57, 23);
            this.txtBandeja3.TabIndex = 6;
            this.txtBandeja3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeja3_KeyPress);
            // 
            // txtBandeja2
            // 
            this.txtBandeja2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBandeja2.Location = new System.Drawing.Point(122, 72);
            this.txtBandeja2.MaxLength = 2;
            this.txtBandeja2.Name = "txtBandeja2";
            this.txtBandeja2.Size = new System.Drawing.Size(57, 23);
            this.txtBandeja2.TabIndex = 5;
            this.txtBandeja2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeja2_KeyPress);
            // 
            // txtBandeja1
            // 
            this.txtBandeja1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBandeja1.Location = new System.Drawing.Point(122, 43);
            this.txtBandeja1.MaxLength = 2;
            this.txtBandeja1.Name = "txtBandeja1";
            this.txtBandeja1.Size = new System.Drawing.Size(57, 23);
            this.txtBandeja1.TabIndex = 4;
            this.txtBandeja1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBandeja1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(28, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bandeja 4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Bandeja 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bandeja 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bandeja 1";
            // 
            // btnLiberaBandeja
            // 
            this.btnLiberaBandeja.Location = new System.Drawing.Point(22, 200);
            this.btnLiberaBandeja.Name = "btnLiberaBandeja";
            this.btnLiberaBandeja.Size = new System.Drawing.Size(171, 29);
            this.btnLiberaBandeja.TabIndex = 11;
            this.btnLiberaBandeja.Text = "Liberar bandejas";
            this.btnLiberaBandeja.UseVisualStyleBackColor = true;
            this.btnLiberaBandeja.Click += new System.EventHandler(this.btnLiberaBandeja_Click);
            // 
            // FrmCashDelivery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 274);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCashDelivery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Entrega de Efectivo";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCerrarVentana;
        private System.Windows.Forms.Button btnEntrega;
        private System.Windows.Forms.TextBox txtBandeja4;
        private System.Windows.Forms.TextBox txtBandeja3;
        private System.Windows.Forms.TextBox txtBandeja2;
        private System.Windows.Forms.TextBox txtBandeja1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHabilitar;
        private System.Windows.Forms.Button btnLiberaBandeja;
    }
}