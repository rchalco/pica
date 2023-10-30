namespace NMDManagement
{
    partial class FrmSelfTestData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.NoteQualifier = new System.Windows.Forms.TabPage();
            this.lblCalibracion = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSensor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSelfTestA = new System.Windows.Forms.DataGridView();
            this.Denomination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gainA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thicknessA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offsetB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gainB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thicknessB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Feeders = new System.Windows.Forms.TabPage();
            this.btnNF4 = new System.Windows.Forms.Button();
            this.btnNF3 = new System.Windows.Forms.Button();
            this.btnNF2 = new System.Windows.Forms.Button();
            this.btnNF1 = new System.Windows.Forms.Button();
            this.lblTamaño = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDenominacion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvFeeders = new System.Windows.Forms.DataGridView();
            this.sensor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calibration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rango = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoteTransport = new System.Windows.Forms.TabPage();
            this.dtgNoteTransport = new System.Windows.Forms.DataGridView();
            this.btnCerrarSelf = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSctackPresenter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.NoteQualifier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelfTestA)).BeginInit();
            this.Feeders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeeders)).BeginInit();
            this.NoteTransport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNoteTransport)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSctackPresenter)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.NoteQualifier);
            this.tabControl1.Controls.Add(this.Feeders);
            this.tabControl1.Controls.Add(this.NoteTransport);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(672, 353);
            this.tabControl1.TabIndex = 0;
            // 
            // NoteQualifier
            // 
            this.NoteQualifier.Controls.Add(this.lblCalibracion);
            this.NoteQualifier.Controls.Add(this.label4);
            this.NoteQualifier.Controls.Add(this.lblEstado);
            this.NoteQualifier.Controls.Add(this.label2);
            this.NoteQualifier.Controls.Add(this.lblSensor);
            this.NoteQualifier.Controls.Add(this.label1);
            this.NoteQualifier.Controls.Add(this.dgvSelfTestA);
            this.NoteQualifier.Location = new System.Drawing.Point(4, 22);
            this.NoteQualifier.Name = "NoteQualifier";
            this.NoteQualifier.Padding = new System.Windows.Forms.Padding(3);
            this.NoteQualifier.Size = new System.Drawing.Size(664, 327);
            this.NoteQualifier.TabIndex = 0;
            this.NoteQualifier.Text = "Note Qualifier";
            this.NoteQualifier.UseVisualStyleBackColor = true;
            // 
            // lblCalibracion
            // 
            this.lblCalibracion.AutoSize = true;
            this.lblCalibracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalibracion.Location = new System.Drawing.Point(516, 301);
            this.lblCalibracion.Name = "lblCalibracion";
            this.lblCalibracion.Size = new System.Drawing.Size(41, 13);
            this.lblCalibracion.TabIndex = 6;
            this.lblCalibracion.Text = "label2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(457, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Calibración:";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(297, 301);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(41, 13);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(258, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Estado:";
            // 
            // lblSensor
            // 
            this.lblSensor.AutoSize = true;
            this.lblSensor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensor.Location = new System.Drawing.Point(41, 301);
            this.lblSensor.Name = "lblSensor";
            this.lblSensor.Size = new System.Drawing.Size(41, 13);
            this.lblSensor.TabIndex = 2;
            this.lblSensor.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sensor:";
            // 
            // dgvSelfTestA
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelfTestA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSelfTestA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelfTestA.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Denomination,
            this.offsetA,
            this.gainA,
            this.thicknessA,
            this.offsetB,
            this.gainB,
            this.thicknessB});
            this.dgvSelfTestA.Location = new System.Drawing.Point(6, 31);
            this.dgvSelfTestA.Name = "dgvSelfTestA";
            this.dgvSelfTestA.ReadOnly = true;
            this.dgvSelfTestA.Size = new System.Drawing.Size(641, 165);
            this.dgvSelfTestA.TabIndex = 0;
            // 
            // Denomination
            // 
            this.Denomination.DataPropertyName = "denomination";
            this.Denomination.HeaderText = "Denomination";
            this.Denomination.Name = "Denomination";
            this.Denomination.ReadOnly = true;
            // 
            // offsetA
            // 
            this.offsetA.DataPropertyName = "offsetA";
            this.offsetA.HeaderText = "offset";
            this.offsetA.Name = "offsetA";
            this.offsetA.ReadOnly = true;
            this.offsetA.Width = 50;
            // 
            // gainA
            // 
            this.gainA.DataPropertyName = "gainA";
            this.gainA.HeaderText = "gain";
            this.gainA.Name = "gainA";
            this.gainA.ReadOnly = true;
            this.gainA.Width = 50;
            // 
            // thicknessA
            // 
            this.thicknessA.DataPropertyName = "thicknessA";
            this.thicknessA.HeaderText = "thickness";
            this.thicknessA.Name = "thicknessA";
            this.thicknessA.ReadOnly = true;
            this.thicknessA.Width = 80;
            // 
            // offsetB
            // 
            this.offsetB.DataPropertyName = "offsetB";
            this.offsetB.HeaderText = "offset";
            this.offsetB.Name = "offsetB";
            this.offsetB.ReadOnly = true;
            this.offsetB.Width = 50;
            // 
            // gainB
            // 
            this.gainB.DataPropertyName = "gainB";
            this.gainB.HeaderText = "gain";
            this.gainB.Name = "gainB";
            this.gainB.ReadOnly = true;
            this.gainB.Width = 50;
            // 
            // thicknessB
            // 
            this.thicknessB.DataPropertyName = "thicknessB";
            this.thicknessB.HeaderText = "thickness";
            this.thicknessB.Name = "thicknessB";
            this.thicknessB.ReadOnly = true;
            this.thicknessB.Width = 80;
            // 
            // Feeders
            // 
            this.Feeders.Controls.Add(this.btnNF4);
            this.Feeders.Controls.Add(this.btnNF3);
            this.Feeders.Controls.Add(this.btnNF2);
            this.Feeders.Controls.Add(this.btnNF1);
            this.Feeders.Controls.Add(this.lblTamaño);
            this.Feeders.Controls.Add(this.label7);
            this.Feeders.Controls.Add(this.lblDenominacion);
            this.Feeders.Controls.Add(this.label3);
            this.Feeders.Controls.Add(this.dgvFeeders);
            this.Feeders.Location = new System.Drawing.Point(4, 22);
            this.Feeders.Name = "Feeders";
            this.Feeders.Padding = new System.Windows.Forms.Padding(3);
            this.Feeders.Size = new System.Drawing.Size(664, 327);
            this.Feeders.TabIndex = 1;
            this.Feeders.Text = "Feeders";
            this.Feeders.UseVisualStyleBackColor = true;
            // 
            // btnNF4
            // 
            this.btnNF4.Location = new System.Drawing.Point(433, 6);
            this.btnNF4.Name = "btnNF4";
            this.btnNF4.Size = new System.Drawing.Size(106, 22);
            this.btnNF4.TabIndex = 12;
            this.btnNF4.Text = "NF 4";
            this.btnNF4.UseVisualStyleBackColor = true;
            this.btnNF4.Click += new System.EventHandler(this.btnNF4_Click);
            // 
            // btnNF3
            // 
            this.btnNF3.Location = new System.Drawing.Point(297, 6);
            this.btnNF3.Name = "btnNF3";
            this.btnNF3.Size = new System.Drawing.Size(106, 22);
            this.btnNF3.TabIndex = 11;
            this.btnNF3.Text = "NF 3";
            this.btnNF3.UseVisualStyleBackColor = true;
            this.btnNF3.Click += new System.EventHandler(this.btnNF3_Click);
            // 
            // btnNF2
            // 
            this.btnNF2.Location = new System.Drawing.Point(165, 6);
            this.btnNF2.Name = "btnNF2";
            this.btnNF2.Size = new System.Drawing.Size(106, 22);
            this.btnNF2.TabIndex = 10;
            this.btnNF2.Text = "NF 2";
            this.btnNF2.UseVisualStyleBackColor = true;
            this.btnNF2.Click += new System.EventHandler(this.btnNF2_Click);
            // 
            // btnNF1
            // 
            this.btnNF1.Location = new System.Drawing.Point(33, 6);
            this.btnNF1.Name = "btnNF1";
            this.btnNF1.Size = new System.Drawing.Size(106, 22);
            this.btnNF1.TabIndex = 9;
            this.btnNF1.Text = "NF 1";
            this.btnNF1.UseVisualStyleBackColor = true;
            this.btnNF1.Click += new System.EventHandler(this.btnNF1_Click);
            // 
            // lblTamaño
            // 
            this.lblTamaño.AutoSize = true;
            this.lblTamaño.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTamaño.Location = new System.Drawing.Point(373, 311);
            this.lblTamaño.Name = "lblTamaño";
            this.lblTamaño.Size = new System.Drawing.Size(0, 13);
            this.lblTamaño.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(337, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Size:";
            // 
            // lblDenominacion
            // 
            this.lblDenominacion.AutoSize = true;
            this.lblDenominacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenominacion.Location = new System.Drawing.Point(111, 311);
            this.lblDenominacion.Name = "lblDenominacion";
            this.lblDenominacion.Size = new System.Drawing.Size(0, 13);
            this.lblDenominacion.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Denomination:";
            // 
            // dgvFeeders
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFeeders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvFeeders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFeeders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sensor,
            this.state,
            this.calibration,
            this.rango});
            this.dgvFeeders.Location = new System.Drawing.Point(33, 34);
            this.dgvFeeders.Name = "dgvFeeders";
            this.dgvFeeders.Size = new System.Drawing.Size(580, 274);
            this.dgvFeeders.TabIndex = 4;
            // 
            // sensor
            // 
            this.sensor.DataPropertyName = "sensor";
            this.sensor.HeaderText = "Sensor";
            this.sensor.Name = "sensor";
            this.sensor.Width = 200;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "State";
            this.state.Name = "state";
            this.state.Width = 200;
            // 
            // calibration
            // 
            this.calibration.DataPropertyName = "calibration";
            this.calibration.HeaderText = "Calibration";
            this.calibration.Name = "calibration";
            this.calibration.Width = 80;
            // 
            // rango
            // 
            this.rango.DataPropertyName = "rango";
            this.rango.HeaderText = "Rango";
            this.rango.Name = "rango";
            this.rango.Width = 50;
            // 
            // NoteTransport
            // 
            this.NoteTransport.Controls.Add(this.dtgNoteTransport);
            this.NoteTransport.Location = new System.Drawing.Point(4, 22);
            this.NoteTransport.Name = "NoteTransport";
            this.NoteTransport.Size = new System.Drawing.Size(664, 327);
            this.NoteTransport.TabIndex = 2;
            this.NoteTransport.Text = "Note Transport";
            this.NoteTransport.UseVisualStyleBackColor = true;
            // 
            // dtgNoteTransport
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgNoteTransport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgNoteTransport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgNoteTransport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dtgNoteTransport.Location = new System.Drawing.Point(42, 26);
            this.dtgNoteTransport.Name = "dtgNoteTransport";
            this.dtgNoteTransport.Size = new System.Drawing.Size(580, 274);
            this.dtgNoteTransport.TabIndex = 5;
            // 
            // btnCerrarSelf
            // 
            this.btnCerrarSelf.Location = new System.Drawing.Point(541, 371);
            this.btnCerrarSelf.Name = "btnCerrarSelf";
            this.btnCerrarSelf.Size = new System.Drawing.Size(143, 25);
            this.btnCerrarSelf.TabIndex = 1;
            this.btnCerrarSelf.Text = "Cerrar";
            this.btnCerrarSelf.UseVisualStyleBackColor = true;
            this.btnCerrarSelf.Click += new System.EventHandler(this.btnCerrarSelf_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvSctackPresenter);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(664, 327);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Stack Presenter";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvSctackPresenter
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSctackPresenter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSctackPresenter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSctackPresenter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dgvSctackPresenter.Location = new System.Drawing.Point(42, 26);
            this.dgvSctackPresenter.Name = "dgvSctackPresenter";
            this.dgvSctackPresenter.Size = new System.Drawing.Size(580, 274);
            this.dgvSctackPresenter.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "sensor";
            this.dataGridViewTextBoxColumn1.HeaderText = "Sensor";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 220;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "state";
            this.dataGridViewTextBoxColumn2.HeaderText = "State";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 160;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "calibration";
            this.dataGridViewTextBoxColumn3.HeaderText = "Calibration";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "rango";
            this.dataGridViewTextBoxColumn4.HeaderText = "Rango";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "sensor";
            this.dataGridViewTextBoxColumn5.HeaderText = "Sensor";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 230;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "state";
            this.dataGridViewTextBoxColumn6.HeaderText = "State";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "calibration";
            this.dataGridViewTextBoxColumn7.HeaderText = "Calibration";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "rango";
            this.dataGridViewTextBoxColumn8.HeaderText = "Rango";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 50;
            // 
            // FrmSelfTestData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 402);
            this.Controls.Add(this.btnCerrarSelf);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSelfTestData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Self Test Data";
            this.tabControl1.ResumeLayout(false);
            this.NoteQualifier.ResumeLayout(false);
            this.NoteQualifier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelfTestA)).EndInit();
            this.Feeders.ResumeLayout(false);
            this.Feeders.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeeders)).EndInit();
            this.NoteTransport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgNoteTransport)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSctackPresenter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage NoteQualifier;
        private System.Windows.Forms.TabPage Feeders;
        private System.Windows.Forms.DataGridView dgvSelfTestA;
        private System.Windows.Forms.Label lblCalibracion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSensor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Denomination;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetA;
        private System.Windows.Forms.DataGridViewTextBoxColumn gainA;
        private System.Windows.Forms.DataGridViewTextBoxColumn thicknessA;
        private System.Windows.Forms.DataGridViewTextBoxColumn offsetB;
        private System.Windows.Forms.DataGridViewTextBoxColumn gainB;
        private System.Windows.Forms.DataGridViewTextBoxColumn thicknessB;
        private System.Windows.Forms.Button btnCerrarSelf;
        private System.Windows.Forms.DataGridView dgvFeeders;
        private System.Windows.Forms.Label lblTamaño;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDenominacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnNF4;
        private System.Windows.Forms.Button btnNF3;
        private System.Windows.Forms.Button btnNF2;
        private System.Windows.Forms.Button btnNF1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sensor;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn calibration;
        private System.Windows.Forms.DataGridViewTextBoxColumn rango;
        private System.Windows.Forms.TabPage NoteTransport;
        private System.Windows.Forms.DataGridView dtgNoteTransport;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvSctackPresenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}