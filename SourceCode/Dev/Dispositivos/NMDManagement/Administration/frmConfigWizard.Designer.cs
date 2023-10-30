namespace NMDManagement.Administration
{
    partial class frmConfigWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigWizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.cmbATMs = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbTypeDispenser = new System.Windows.Forms.ComboBox();
            this.cbxPorts = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvCassette = new System.Windows.Forms.DataGridView();
            this.Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrencyCourt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNCassettes = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblIdOffice = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblNameATM = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butFindATM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbDesReceptor = new System.Windows.Forms.RadioButton();
            this.rbActReceptor = new System.Windows.Forms.RadioButton();
            this.cmbPuertoReceptor = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTipoReceptor = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbCardReader = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbDesFinger = new System.Windows.Forms.RadioButton();
            this.rbActFinger = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.obDescCardReader = new System.Windows.Forms.RadioButton();
            this.rbActCardReader = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCassette)).BeginInit();
            this.wizardPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wizardPage1);
            this.wizardControl1.Pages.Add(this.wizardPage2);
            this.wizardControl1.Size = new System.Drawing.Size(739, 410);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "Parametrizar ATM";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            this.wizardControl1.Finished += new System.EventHandler(this.wizardControl1_Finished);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.cmbATMs);
            this.wizardPage1.Controls.Add(this.groupBox1);
            this.wizardPage1.Controls.Add(this.butFindATM);
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.Size = new System.Drawing.Size(692, 256);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Seleccionar ATM a configurar";
            this.wizardPage1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            // 
            // cmbATMs
            // 
            this.cmbATMs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbATMs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbATMs.FormattingEnabled = true;
            this.cmbATMs.Location = new System.Drawing.Point(207, 17);
            this.cmbATMs.Name = "cmbATMs";
            this.cmbATMs.Size = new System.Drawing.Size(290, 23);
            this.cmbATMs.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cmbTypeDispenser);
            this.groupBox1.Controls.Add(this.cbxPorts);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dgvCassette);
            this.groupBox1.Controls.Add(this.lblNCassettes);
            this.groupBox1.Controls.Add(this.lblIP);
            this.groupBox1.Controls.Add(this.lblIdOffice);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblNameATM);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(27, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(633, 188);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(35, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 15);
            this.label11.TabIndex = 15;
            this.label11.Text = "Dispensador:";
            // 
            // cmbTypeDispenser
            // 
            this.cmbTypeDispenser.BackColor = System.Drawing.Color.White;
            this.cmbTypeDispenser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeDispenser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTypeDispenser.FormattingEnabled = true;
            this.cmbTypeDispenser.Items.AddRange(new object[] {
            "NMD50",
            "NMD100"});
            this.cmbTypeDispenser.Location = new System.Drawing.Point(124, 154);
            this.cmbTypeDispenser.Name = "cmbTypeDispenser";
            this.cmbTypeDispenser.Size = new System.Drawing.Size(192, 23);
            this.cmbTypeDispenser.TabIndex = 14;
            // 
            // cbxPorts
            // 
            this.cbxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPorts.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxPorts.FormattingEnabled = true;
            this.cbxPorts.Location = new System.Drawing.Point(443, 154);
            this.cbxPorts.Name = "cbxPorts";
            this.cbxPorts.Size = new System.Drawing.Size(162, 23);
            this.cbxPorts.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(361, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Puerto:";
            // 
            // dgvCassette
            // 
            this.dgvCassette.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCassette.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sequence,
            this.Id,
            this.CurrencyCourt,
            this.MinQuantity,
            this.Status,
            this.TotalQuantity});
            this.dgvCassette.Location = new System.Drawing.Point(364, 22);
            this.dgvCassette.Name = "dgvCassette";
            this.dgvCassette.Size = new System.Drawing.Size(241, 126);
            this.dgvCassette.TabIndex = 11;
            // 
            // Sequence
            // 
            this.Sequence.DataPropertyName = "Sequence";
            this.Sequence.HeaderText = "Secuencia";
            this.Sequence.Name = "Sequence";
            this.Sequence.ReadOnly = true;
            this.Sequence.Width = 60;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Nro";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // CurrencyCourt
            // 
            this.CurrencyCourt.DataPropertyName = "CurrencyCourt";
            this.CurrencyCourt.HeaderText = "Corte";
            this.CurrencyCourt.Name = "CurrencyCourt";
            this.CurrencyCourt.ReadOnly = true;
            this.CurrencyCourt.Width = 60;
            // 
            // MinQuantity
            // 
            this.MinQuantity.DataPropertyName = "MinQuantity";
            this.MinQuantity.HeaderText = "Mínimo";
            this.MinQuantity.Name = "MinQuantity";
            this.MinQuantity.ReadOnly = true;
            this.MinQuantity.Width = 60;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            // 
            // TotalQuantity
            // 
            this.TotalQuantity.DataPropertyName = "TotalQuantity";
            this.TotalQuantity.HeaderText = "TotalQuantity";
            this.TotalQuantity.Name = "TotalQuantity";
            this.TotalQuantity.ReadOnly = true;
            this.TotalQuantity.Visible = false;
            // 
            // lblNCassettes
            // 
            this.lblNCassettes.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNCassettes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNCassettes.Location = new System.Drawing.Point(124, 131);
            this.lblNCassettes.Name = "lblNCassettes";
            this.lblNCassettes.Size = new System.Drawing.Size(192, 15);
            this.lblNCassettes.TabIndex = 9;
            // 
            // lblIP
            // 
            this.lblIP.BackColor = System.Drawing.Color.Gainsboro;
            this.lblIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIP.Location = new System.Drawing.Point(124, 105);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(192, 15);
            this.lblIP.TabIndex = 8;
            // 
            // lblIdOffice
            // 
            this.lblIdOffice.BackColor = System.Drawing.Color.Gainsboro;
            this.lblIdOffice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdOffice.Location = new System.Drawing.Point(124, 79);
            this.lblIdOffice.Name = "lblIdOffice";
            this.lblIdOffice.Size = new System.Drawing.Size(192, 15);
            this.lblIdOffice.TabIndex = 7;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Gainsboro;
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDescription.Location = new System.Drawing.Point(124, 52);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(192, 15);
            this.lblDescription.TabIndex = 6;
            // 
            // lblNameATM
            // 
            this.lblNameATM.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNameATM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNameATM.Location = new System.Drawing.Point(124, 25);
            this.lblNameATM.Name = "lblNameATM";
            this.lblNameATM.Size = new System.Drawing.Size(192, 15);
            this.lblNameATM.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Nombre ATM:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Nro. bandejas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "IP ATM:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "ID Oficina:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Código ATM:";
            // 
            // butFindATM
            // 
            this.butFindATM.Location = new System.Drawing.Point(515, 21);
            this.butFindATM.Name = "butFindATM";
            this.butFindATM.Size = new System.Drawing.Size(145, 23);
            this.butFindATM.TabIndex = 1;
            this.butFindATM.Text = "Buscar";
            this.butFindATM.UseVisualStyleBackColor = true;
            this.butFindATM.Click += new System.EventHandler(this.butFindATM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione  ATM a configurar:";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.groupBox4);
            this.wizardPage2.Controls.Add(this.cmbPuertoReceptor);
            this.wizardPage2.Controls.Add(this.label14);
            this.wizardPage2.Controls.Add(this.lblTipoReceptor);
            this.wizardPage2.Controls.Add(this.label12);
            this.wizardPage2.Controls.Add(this.cmbCardReader);
            this.wizardPage2.Controls.Add(this.groupBox3);
            this.wizardPage2.Controls.Add(this.groupBox2);
            this.wizardPage2.Controls.Add(this.label10);
            this.wizardPage2.Controls.Add(this.label9);
            this.wizardPage2.Controls.Add(this.label8);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.Size = new System.Drawing.Size(692, 256);
            this.wizardPage2.TabIndex = 1;
            this.wizardPage2.Text = "Configurar lector de tarjeta y huella";
            this.wizardPage2.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage2_Commit);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbDesReceptor);
            this.groupBox4.Controls.Add(this.rbActReceptor);
            this.groupBox4.Location = new System.Drawing.Point(468, 141);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(171, 87);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            // 
            // rbDesReceptor
            // 
            this.rbDesReceptor.AutoSize = true;
            this.rbDesReceptor.Checked = true;
            this.rbDesReceptor.Location = new System.Drawing.Point(29, 51);
            this.rbDesReceptor.Name = "rbDesReceptor";
            this.rbDesReceptor.Size = new System.Drawing.Size(79, 19);
            this.rbDesReceptor.TabIndex = 1;
            this.rbDesReceptor.TabStop = true;
            this.rbDesReceptor.Text = "Desactivar";
            this.rbDesReceptor.UseVisualStyleBackColor = true;
            // 
            // rbActReceptor
            // 
            this.rbActReceptor.AutoSize = true;
            this.rbActReceptor.Location = new System.Drawing.Point(29, 26);
            this.rbActReceptor.Name = "rbActReceptor";
            this.rbActReceptor.Size = new System.Drawing.Size(62, 19);
            this.rbActReceptor.TabIndex = 0;
            this.rbActReceptor.Text = "Activar";
            this.rbActReceptor.UseVisualStyleBackColor = true;
            // 
            // cmbPuertoReceptor
            // 
            this.cmbPuertoReceptor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPuertoReceptor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbPuertoReceptor.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.cmbPuertoReceptor.FormattingEnabled = true;
            this.cmbPuertoReceptor.Location = new System.Drawing.Point(526, 103);
            this.cmbPuertoReceptor.Name = "cmbPuertoReceptor";
            this.cmbPuertoReceptor.Size = new System.Drawing.Size(113, 23);
            this.cmbPuertoReceptor.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(465, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 15);
            this.label14.TabIndex = 14;
            this.label14.Text = "Puerto:";
            // 
            // lblTipoReceptor
            // 
            this.lblTipoReceptor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTipoReceptor.Location = new System.Drawing.Point(468, 70);
            this.lblTipoReceptor.Name = "lblTipoReceptor";
            this.lblTipoReceptor.Size = new System.Drawing.Size(171, 21);
            this.lblTipoReceptor.TabIndex = 6;
            this.lblTipoReceptor.Text = "MEI";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(465, 43);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 15);
            this.label12.TabIndex = 5;
            this.label12.Text = "Tipo de receptor:";
            // 
            // cmbCardReader
            // 
            this.cmbCardReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardReader.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCardReader.FormattingEnabled = true;
            this.cmbCardReader.Items.AddRange(new object[] {
            "GEMPLUS",
            "CREATOR"});
            this.cmbCardReader.Location = new System.Drawing.Point(15, 61);
            this.cmbCardReader.Name = "cmbCardReader";
            this.cmbCardReader.Size = new System.Drawing.Size(194, 23);
            this.cmbCardReader.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbDesFinger);
            this.groupBox3.Controls.Add(this.rbActFinger);
            this.groupBox3.Location = new System.Drawing.Point(241, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(171, 115);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // rbDesFinger
            // 
            this.rbDesFinger.AutoSize = true;
            this.rbDesFinger.Location = new System.Drawing.Point(29, 60);
            this.rbDesFinger.Name = "rbDesFinger";
            this.rbDesFinger.Size = new System.Drawing.Size(79, 19);
            this.rbDesFinger.TabIndex = 1;
            this.rbDesFinger.Text = "Desactivar";
            this.rbDesFinger.UseVisualStyleBackColor = true;
            // 
            // rbActFinger
            // 
            this.rbActFinger.AutoSize = true;
            this.rbActFinger.Checked = true;
            this.rbActFinger.Location = new System.Drawing.Point(29, 26);
            this.rbActFinger.Name = "rbActFinger";
            this.rbActFinger.Size = new System.Drawing.Size(62, 19);
            this.rbActFinger.TabIndex = 0;
            this.rbActFinger.TabStop = true;
            this.rbActFinger.Text = "Activar";
            this.rbActFinger.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.obDescCardReader);
            this.groupBox2.Controls.Add(this.rbActCardReader);
            this.groupBox2.Location = new System.Drawing.Point(15, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(194, 115);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // obDescCardReader
            // 
            this.obDescCardReader.AutoSize = true;
            this.obDescCardReader.Location = new System.Drawing.Point(29, 60);
            this.obDescCardReader.Name = "obDescCardReader";
            this.obDescCardReader.Size = new System.Drawing.Size(79, 19);
            this.obDescCardReader.TabIndex = 1;
            this.obDescCardReader.Text = "Desactivar";
            this.obDescCardReader.UseVisualStyleBackColor = true;
            // 
            // rbActCardReader
            // 
            this.rbActCardReader.AutoSize = true;
            this.rbActCardReader.Checked = true;
            this.rbActCardReader.Location = new System.Drawing.Point(29, 26);
            this.rbActCardReader.Name = "rbActCardReader";
            this.rbActCardReader.Size = new System.Drawing.Size(62, 19);
            this.rbActCardReader.TabIndex = 0;
            this.rbActCardReader.TabStop = true;
            this.rbActCardReader.Text = "Activar";
            this.rbActCardReader.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(241, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(171, 21);
            this.label10.TabIndex = 3;
            this.label10.Text = "DIGITAL PERSONA";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(238, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Tipo de lector de huella:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "Tipo de lector de tarjeta:";
            // 
            // frmConfigWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 410);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmConfigWizard";
            this.Text = "frmConfigWizard";
            this.Load += new System.EventHandler(this.frmConfigWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCassette)).EndInit();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
        private System.Windows.Forms.Button butFindATM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblNCassettes;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblIdOffice;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblNameATM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvCassette;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrencyCourt;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalQuantity;
        private System.Windows.Forms.ComboBox cbxPorts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbDesFinger;
        private System.Windows.Forms.RadioButton rbActFinger;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton obDescCardReader;
        private System.Windows.Forms.RadioButton rbActCardReader;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbTypeDispenser;
        private System.Windows.Forms.ComboBox cmbCardReader;
        private System.Windows.Forms.ComboBox cmbATMs;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbDesReceptor;
        private System.Windows.Forms.RadioButton rbActReceptor;
        private System.Windows.Forms.ComboBox cmbPuertoReceptor;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTipoReceptor;
        private System.Windows.Forms.Label label12;
    }
}