namespace GeneradorLotes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtArchivoEmbozo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ultimaTarjetaBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ultimoIndiceBox = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nLP = new System.Windows.Forms.NumericUpDown();
            this.nCBB = new System.Windows.Forms.NumericUpDown();
            this.nSC = new System.Windows.Forms.NumericUpDown();
            this.nBEPN = new System.Windows.Forms.NumericUpDown();
            this.nPO = new System.Windows.Forms.NumericUpDown();
            this.nTJ = new System.Windows.Forms.NumericUpDown();
            this.nSU = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ultimaTarjetaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultimoIndiceBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nLP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCBB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBEPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTJ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSU)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Archivo actual de embozo";
            // 
            // txtArchivoEmbozo
            // 
            this.txtArchivoEmbozo.Location = new System.Drawing.Point(169, 30);
            this.txtArchivoEmbozo.Name = "txtArchivoEmbozo";
            this.txtArchivoEmbozo.Size = new System.Drawing.Size(332, 20);
            this.txtArchivoEmbozo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ultima Tarjeta Distribuida";
            // 
            // ultimaTarjetaBox
            // 
            this.ultimaTarjetaBox.Location = new System.Drawing.Point(181, 61);
            this.ultimaTarjetaBox.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.ultimaTarjetaBox.Name = "ultimaTarjetaBox";
            this.ultimaTarjetaBox.Size = new System.Drawing.Size(320, 20);
            this.ultimaTarjetaBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ultima Indice Generado";
            // 
            // ultimoIndiceBox
            // 
            this.ultimoIndiceBox.Location = new System.Drawing.Point(181, 87);
            this.ultimoIndiceBox.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.ultimoIndiceBox.Name = "ultimoIndiceBox";
            this.ultimoIndiceBox.Size = new System.Drawing.Size(320, 20);
            this.ultimoIndiceBox.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(38, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nSU);
            this.groupBox1.Controls.Add(this.nTJ);
            this.groupBox1.Controls.Add(this.nPO);
            this.groupBox1.Controls.Add(this.nBEPN);
            this.groupBox1.Controls.Add(this.nSC);
            this.groupBox1.Controls.Add(this.nCBB);
            this.groupBox1.Controls.Add(this.nLP);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(38, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 217);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distribucion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "La Paz";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Cochabamba";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Santa Cruz";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Beni - Pando";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Potosi";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Tarija";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Sucre";
            // 
            // nLP
            // 
            this.nLP.Location = new System.Drawing.Point(98, 20);
            this.nLP.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nLP.Name = "nLP";
            this.nLP.Size = new System.Drawing.Size(120, 20);
            this.nLP.TabIndex = 1;
            // 
            // nCBB
            // 
            this.nCBB.Location = new System.Drawing.Point(98, 47);
            this.nCBB.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nCBB.Name = "nCBB";
            this.nCBB.Size = new System.Drawing.Size(120, 20);
            this.nCBB.TabIndex = 1;
            // 
            // nSC
            // 
            this.nSC.Location = new System.Drawing.Point(98, 76);
            this.nSC.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nSC.Name = "nSC";
            this.nSC.Size = new System.Drawing.Size(120, 20);
            this.nSC.TabIndex = 1;
            // 
            // nBEPN
            // 
            this.nBEPN.Location = new System.Drawing.Point(98, 105);
            this.nBEPN.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nBEPN.Name = "nBEPN";
            this.nBEPN.Size = new System.Drawing.Size(120, 20);
            this.nBEPN.TabIndex = 1;
            // 
            // nPO
            // 
            this.nPO.Location = new System.Drawing.Point(98, 132);
            this.nPO.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nPO.Name = "nPO";
            this.nPO.Size = new System.Drawing.Size(120, 20);
            this.nPO.TabIndex = 1;
            // 
            // nTJ
            // 
            this.nTJ.Location = new System.Drawing.Point(98, 155);
            this.nTJ.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nTJ.Name = "nTJ";
            this.nTJ.Size = new System.Drawing.Size(120, 20);
            this.nTJ.TabIndex = 1;
            // 
            // nSU
            // 
            this.nSU.Location = new System.Drawing.Point(98, 181);
            this.nSU.Maximum = new decimal(new int[] {
            -825053522,
            2157224,
            0,
            0});
            this.nSU.Name = "nSU";
            this.nSU.Size = new System.Drawing.Size(120, 20);
            this.nSU.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 387);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ultimoIndiceBox);
            this.Controls.Add(this.ultimaTarjetaBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtArchivoEmbozo);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "GENERADOR DE ARCHIVOS";
            ((System.ComponentModel.ISupportInitialize)(this.ultimaTarjetaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultimoIndiceBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nLP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nCBB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nBEPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nPO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nTJ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSU)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtArchivoEmbozo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ultimaTarjetaBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown ultimoIndiceBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nSU;
        private System.Windows.Forms.NumericUpDown nTJ;
        private System.Windows.Forms.NumericUpDown nPO;
        private System.Windows.Forms.NumericUpDown nBEPN;
        private System.Windows.Forms.NumericUpDown nSC;
        private System.Windows.Forms.NumericUpDown nCBB;
        private System.Windows.Forms.NumericUpDown nLP;
    }
}

