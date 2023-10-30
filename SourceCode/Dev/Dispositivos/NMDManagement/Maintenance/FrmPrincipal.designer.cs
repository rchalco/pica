namespace NMDManagement
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MenuVertical = new System.Windows.Forms.Panel();
            this.tvwMenu = new System.Windows.Forms.TreeView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblcorreo = new System.Windows.Forms.Label();
            this.lblCargp = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblusuario = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.iconminimizar = new System.Windows.Forms.PictureBox();
            this.iconrestaurar = new System.Windows.Forms.PictureBox();
            this.iconmaximizar = new System.Windows.Forms.PictureBox();
            this.iconcerrar = new System.Windows.Forms.PictureBox();
            this.btnMenu = new System.Windows.Forms.PictureBox();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.pnldBandeja = new System.Windows.Forms.Panel();
            this.btnCerrarIdBandejas = new System.Windows.Forms.Button();
            this.dbgBandejas = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdBandeja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlIDNMD = new System.Windows.Forms.Panel();
            this.btnCerrarIDNMD = new System.Windows.Forms.Button();
            this.btnGrabarIDNMD = new System.Windows.Forms.Button();
            this.txtNumSerial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlEstadoTable = new System.Windows.Forms.Panel();
            this.btnCerrarTablaEstado = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnBorrarTabla = new System.Windows.Forms.Button();
            this.tdgEstadoTabla = new System.Windows.Forms.DataGridView();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.MenuVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconminimizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconrestaurar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconmaximizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            this.pnlPrincipal.SuspendLayout();
            this.pnldBandeja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgBandejas)).BeginInit();
            this.pnlIDNMD.SuspendLayout();
            this.pnlEstadoTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdgEstadoTabla)).BeginInit();
            this.SuspendLayout();
            // 
            // MenuVertical
            // 
            this.MenuVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.MenuVertical.Controls.Add(this.tvwMenu);
            this.MenuVertical.Controls.Add(this.pictureBox1);
            this.MenuVertical.Controls.Add(this.lblcorreo);
            this.MenuVertical.Controls.Add(this.lblCargp);
            this.MenuVertical.Controls.Add(this.pictureBox2);
            this.MenuVertical.Controls.Add(this.lblusuario);
            this.MenuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.MenuVertical.Location = new System.Drawing.Point(0, 0);
            this.MenuVertical.Name = "MenuVertical";
            this.MenuVertical.Size = new System.Drawing.Size(211, 615);
            this.MenuVertical.TabIndex = 0;
            // 
            // tvwMenu
            // 
            this.tvwMenu.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvwMenu.Location = new System.Drawing.Point(3, 116);
            this.tvwMenu.Name = "tvwMenu";
            this.tvwMenu.Size = new System.Drawing.Size(202, 428);
            this.tvwMenu.TabIndex = 26;
            this.tvwMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMenu_AfterSelect);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(207, 87);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // lblcorreo
            // 
            this.lblcorreo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblcorreo.AutoSize = true;
            this.lblcorreo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcorreo.ForeColor = System.Drawing.Color.White;
            this.lblcorreo.Location = new System.Drawing.Point(61, 596);
            this.lblcorreo.Name = "lblcorreo";
            this.lblcorreo.Size = new System.Drawing.Size(32, 16);
            this.lblcorreo.TabIndex = 17;
            this.lblcorreo.Text = "V 2.0";
            // 
            // lblCargp
            // 
            this.lblCargp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCargp.AutoSize = true;
            this.lblCargp.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargp.ForeColor = System.Drawing.Color.White;
            this.lblCargp.Location = new System.Drawing.Point(61, 578);
            this.lblCargp.Name = "lblCargp";
            this.lblCargp.Size = new System.Drawing.Size(46, 16);
            this.lblCargp.TabIndex = 16;
            this.lblCargp.Text = "Usuario";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1, 561);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(54, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // lblusuario
            // 
            this.lblusuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblusuario.AutoSize = true;
            this.lblusuario.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.Location = new System.Drawing.Point(61, 561);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(41, 16);
            this.lblusuario.TabIndex = 14;
            this.lblusuario.Text = "Cargo";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.panel3.Location = new System.Drawing.Point(774, 416);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 10);
            this.panel3.TabIndex = 19;
            this.panel3.Visible = false;
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BarraTitulo.Controls.Add(this.iconminimizar);
            this.BarraTitulo.Controls.Add(this.iconrestaurar);
            this.BarraTitulo.Controls.Add(this.iconmaximizar);
            this.BarraTitulo.Controls.Add(this.iconcerrar);
            this.BarraTitulo.Controls.Add(this.btnMenu);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(211, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(792, 45);
            this.BarraTitulo.TabIndex = 1;
            this.BarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BarraTitulo_MouseDown);
            // 
            // iconminimizar
            // 
            this.iconminimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconminimizar.Image = ((System.Drawing.Image)(resources.GetObject("iconminimizar.Image")));
            this.iconminimizar.Location = new System.Drawing.Point(711, 5);
            this.iconminimizar.Name = "iconminimizar";
            this.iconminimizar.Size = new System.Drawing.Size(18, 18);
            this.iconminimizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconminimizar.TabIndex = 4;
            this.iconminimizar.TabStop = false;
            this.iconminimizar.Click += new System.EventHandler(this.iconminimizar_Click);
            // 
            // iconrestaurar
            // 
            this.iconrestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconrestaurar.Image = ((System.Drawing.Image)(resources.GetObject("iconrestaurar.Image")));
            this.iconrestaurar.Location = new System.Drawing.Point(737, 5);
            this.iconrestaurar.Name = "iconrestaurar";
            this.iconrestaurar.Size = new System.Drawing.Size(18, 18);
            this.iconrestaurar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconrestaurar.TabIndex = 3;
            this.iconrestaurar.TabStop = false;
            this.iconrestaurar.Visible = false;
            this.iconrestaurar.Click += new System.EventHandler(this.iconrestaurar_Click);
            // 
            // iconmaximizar
            // 
            this.iconmaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconmaximizar.Image = ((System.Drawing.Image)(resources.GetObject("iconmaximizar.Image")));
            this.iconmaximizar.Location = new System.Drawing.Point(737, 5);
            this.iconmaximizar.Name = "iconmaximizar";
            this.iconmaximizar.Size = new System.Drawing.Size(18, 18);
            this.iconmaximizar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconmaximizar.TabIndex = 2;
            this.iconmaximizar.TabStop = false;
            this.iconmaximizar.Click += new System.EventHandler(this.iconmaximizar_Click);
            // 
            // iconcerrar
            // 
            this.iconcerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconcerrar.Image = ((System.Drawing.Image)(resources.GetObject("iconcerrar.Image")));
            this.iconcerrar.Location = new System.Drawing.Point(763, 5);
            this.iconcerrar.Name = "iconcerrar";
            this.iconcerrar.Size = new System.Drawing.Size(18, 18);
            this.iconcerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconcerrar.TabIndex = 1;
            this.iconcerrar.TabStop = false;
            this.iconcerrar.Click += new System.EventHandler(this.iconcerrar_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.Location = new System.Drawing.Point(8, 6);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(35, 35);
            this.btnMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnMenu.TabIndex = 0;
            this.btnMenu.TabStop = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlPrincipal.Controls.Add(this.pnldBandeja);
            this.pnlPrincipal.Controls.Add(this.panel3);
            this.pnlPrincipal.Controls.Add(this.pnlIDNMD);
            this.pnlPrincipal.Controls.Add(this.pnlEstadoTable);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(1003, 615);
            this.pnlPrincipal.TabIndex = 2;
            // 
            // pnldBandeja
            // 
            this.pnldBandeja.Controls.Add(this.btnCerrarIdBandejas);
            this.pnldBandeja.Controls.Add(this.dbgBandejas);
            this.pnldBandeja.Location = new System.Drawing.Point(217, 61);
            this.pnldBandeja.Name = "pnldBandeja";
            this.pnldBandeja.Size = new System.Drawing.Size(565, 254);
            this.pnldBandeja.TabIndex = 1;
            this.pnldBandeja.Visible = false;
            // 
            // btnCerrarIdBandejas
            // 
            this.btnCerrarIdBandejas.Location = new System.Drawing.Point(191, 214);
            this.btnCerrarIdBandejas.Name = "btnCerrarIdBandejas";
            this.btnCerrarIdBandejas.Size = new System.Drawing.Size(166, 25);
            this.btnCerrarIdBandejas.TabIndex = 2;
            this.btnCerrarIdBandejas.Text = "Cerrar";
            this.btnCerrarIdBandejas.UseVisualStyleBackColor = true;
            this.btnCerrarIdBandejas.Click += new System.EventHandler(this.btnCerrarIdBandejas_Click);
            // 
            // dbgBandejas
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dbgBandejas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dbgBandejas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgBandejas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.IdBandeja,
            this.Estado,
            this.DesEstado});
            this.dbgBandejas.Location = new System.Drawing.Point(13, 19);
            this.dbgBandejas.Name = "dbgBandejas";
            this.dbgBandejas.Size = new System.Drawing.Size(533, 178);
            this.dbgBandejas.TabIndex = 1;
            // 
            // Numero
            // 
            this.Numero.DataPropertyName = "numero";
            this.Numero.HeaderText = "Número";
            this.Numero.Name = "Numero";
            this.Numero.Width = 80;
            // 
            // IdBandeja
            // 
            this.IdBandeja.DataPropertyName = "IdBandeja";
            this.IdBandeja.HeaderText = "IdBandeja";
            this.IdBandeja.Name = "IdBandeja";
            this.IdBandeja.Width = 80;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Width = 80;
            // 
            // DesEstado
            // 
            this.DesEstado.DataPropertyName = "DesEstado";
            this.DesEstado.HeaderText = "Descripción";
            this.DesEstado.Name = "DesEstado";
            this.DesEstado.Width = 250;
            // 
            // pnlIDNMD
            // 
            this.pnlIDNMD.Controls.Add(this.btnCerrarIDNMD);
            this.pnlIDNMD.Controls.Add(this.btnGrabarIDNMD);
            this.pnlIDNMD.Controls.Add(this.txtNumSerial);
            this.pnlIDNMD.Controls.Add(this.label1);
            this.pnlIDNMD.Location = new System.Drawing.Point(219, 51);
            this.pnlIDNMD.Name = "pnlIDNMD";
            this.pnlIDNMD.Size = new System.Drawing.Size(189, 135);
            this.pnlIDNMD.TabIndex = 3;
            this.pnlIDNMD.Visible = false;
            this.pnlIDNMD.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlIDNMD_Paint);
            // 
            // btnCerrarIDNMD
            // 
            this.btnCerrarIDNMD.Location = new System.Drawing.Point(16, 101);
            this.btnCerrarIDNMD.Name = "btnCerrarIDNMD";
            this.btnCerrarIDNMD.Size = new System.Drawing.Size(163, 24);
            this.btnCerrarIDNMD.TabIndex = 3;
            this.btnCerrarIDNMD.Text = "Cerrar";
            this.btnCerrarIDNMD.UseVisualStyleBackColor = true;
            this.btnCerrarIDNMD.Click += new System.EventHandler(this.btnCerrarIDNMD_Click);
            // 
            // btnGrabarIDNMD
            // 
            this.btnGrabarIDNMD.Location = new System.Drawing.Point(16, 65);
            this.btnGrabarIDNMD.Name = "btnGrabarIDNMD";
            this.btnGrabarIDNMD.Size = new System.Drawing.Size(164, 27);
            this.btnGrabarIDNMD.TabIndex = 2;
            this.btnGrabarIDNMD.Text = "Cambiar";
            this.btnGrabarIDNMD.UseVisualStyleBackColor = true;
            this.btnGrabarIDNMD.Click += new System.EventHandler(this.btnGrabarIDNMD_Click);
            // 
            // txtNumSerial
            // 
            this.txtNumSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumSerial.Location = new System.Drawing.Point(16, 33);
            this.txtNumSerial.MaxLength = 14;
            this.txtNumSerial.Name = "txtNumSerial";
            this.txtNumSerial.Size = new System.Drawing.Size(164, 26);
            this.txtNumSerial.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Serial Number:";
            // 
            // pnlEstadoTable
            // 
            this.pnlEstadoTable.Controls.Add(this.btnCerrarTablaEstado);
            this.pnlEstadoTable.Controls.Add(this.btnRefrescar);
            this.pnlEstadoTable.Controls.Add(this.btnBorrarTabla);
            this.pnlEstadoTable.Controls.Add(this.tdgEstadoTabla);
            this.pnlEstadoTable.Location = new System.Drawing.Point(235, 47);
            this.pnlEstadoTable.Name = "pnlEstadoTable";
            this.pnlEstadoTable.Size = new System.Drawing.Size(530, 537);
            this.pnlEstadoTable.TabIndex = 2;
            this.pnlEstadoTable.Visible = false;
            // 
            // btnCerrarTablaEstado
            // 
            this.btnCerrarTablaEstado.Location = new System.Drawing.Point(390, 504);
            this.btnCerrarTablaEstado.Name = "btnCerrarTablaEstado";
            this.btnCerrarTablaEstado.Size = new System.Drawing.Size(119, 26);
            this.btnCerrarTablaEstado.TabIndex = 3;
            this.btnCerrarTablaEstado.Text = "Cerrar";
            this.btnCerrarTablaEstado.UseVisualStyleBackColor = true;
            this.btnCerrarTablaEstado.Click += new System.EventHandler(this.btnCerrarTablaEstado_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Location = new System.Drawing.Point(201, 504);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(133, 27);
            this.btnRefrescar.TabIndex = 2;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = true;
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // btnBorrarTabla
            // 
            this.btnBorrarTabla.Location = new System.Drawing.Point(38, 504);
            this.btnBorrarTabla.Name = "btnBorrarTabla";
            this.btnBorrarTabla.Size = new System.Drawing.Size(136, 27);
            this.btnBorrarTabla.TabIndex = 1;
            this.btnBorrarTabla.Text = "Borrar Tabla";
            this.btnBorrarTabla.UseVisualStyleBackColor = true;
            this.btnBorrarTabla.Click += new System.EventHandler(this.btnBorrarTabla_Click);
            // 
            // tdgEstadoTabla
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tdgEstadoTabla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tdgEstadoTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tdgEstadoTabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cantidad,
            this.status,
            this.statusDescription});
            this.tdgEstadoTabla.Location = new System.Drawing.Point(21, 15);
            this.tdgEstadoTabla.Name = "tdgEstadoTabla";
            this.tdgEstadoTabla.Size = new System.Drawing.Size(495, 471);
            this.tdgEstadoTabla.TabIndex = 0;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "quantity";
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 60;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Estado";
            this.status.Name = "status";
            this.status.Width = 60;
            // 
            // statusDescription
            // 
            this.statusDescription.DataPropertyName = "statusDescription";
            this.statusDescription.HeaderText = "Descripción";
            this.statusDescription.Name = "statusDescription";
            this.statusDescription.Width = 250;
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(0, 264);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(208, 40);
            this.button4.TabIndex = 4;
            this.button4.Text = "Bandejas";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 615);
            this.Controls.Add(this.BarraTitulo);
            this.Controls.Add(this.MenuVertical);
            this.Controls.Add(this.pnlPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MenuVertical.ResumeLayout(false);
            this.MenuVertical.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconminimizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconrestaurar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconmaximizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconcerrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            this.pnlPrincipal.ResumeLayout(false);
            this.pnldBandeja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbgBandejas)).EndInit();
            this.pnlIDNMD.ResumeLayout(false);
            this.pnlIDNMD.PerformLayout();
            this.pnlEstadoTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tdgEstadoTabla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MenuVertical;
        private System.Windows.Forms.Panel BarraTitulo;
        private System.Windows.Forms.PictureBox btnMenu;
        private System.Windows.Forms.Panel pnlPrincipal;
        private System.Windows.Forms.PictureBox iconminimizar;
        private System.Windows.Forms.PictureBox iconrestaurar;
        private System.Windows.Forms.PictureBox iconmaximizar;
        private System.Windows.Forms.PictureBox iconcerrar;
        private System.Windows.Forms.Label lblcorreo;
        private System.Windows.Forms.Label lblCargp;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnldBandeja;
        private System.Windows.Forms.DataGridView dbgBandejas;
        private System.Windows.Forms.Button btnCerrarIdBandejas;
        private System.Windows.Forms.Panel pnlEstadoTable;
        private System.Windows.Forms.Button btnCerrarTablaEstado;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnBorrarTabla;
        private System.Windows.Forms.DataGridView tdgEstadoTabla;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDescription;
        private System.Windows.Forms.Panel pnlIDNMD;
        private System.Windows.Forms.Button btnCerrarIDNMD;
        private System.Windows.Forms.Button btnGrabarIDNMD;
        private System.Windows.Forms.TextBox txtNumSerial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdBandeja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesEstado;
        private System.Windows.Forms.TreeView tvwMenu;
    }
}

