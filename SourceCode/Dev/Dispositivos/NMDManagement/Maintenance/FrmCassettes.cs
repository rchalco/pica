using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DebugLoger;
using RegisterLogger;
using System.Configuration;
using System.IO;

namespace NMDManagement
{
    public partial class FrmCassettes : Form
    {
        List<DebugLoger.LogDTO> logDTOs = new List<LogDTO>();
        int LvIntBandeja = 0; // Numero de bandeja para WD

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void BarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM globalConfigATM;


        public FrmCassettes()
        {
            InitializeComponent();
            //Dispenser.InitDispenser();
            string pathConfigATM = ConfigurationManager.AppSettings["pathConfigATM"];
            globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<Interop.Main.Cross.Domain.Orchestrator.GlobalConfigATM>(File.ReadAllText(pathConfigATM));
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCassettes));
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rdbNC4 = new System.Windows.Forms.RadioButton();
            this.rdbNC3 = new System.Windows.Forms.RadioButton();
            this.rdbNC2 = new System.Windows.Forms.RadioButton();
            this.rdbNC1 = new System.Windows.Forms.RadioButton();
            this.rdbRV = new System.Windows.Forms.RadioButton();
            this.btnOpenCassette = new System.Windows.Forms.Button();
            this.btnCloseCassette = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSizeH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSizeW = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSetSize = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtGion = new System.Windows.Forms.TextBox();
            this.cmdMonth = new System.Windows.Forms.ComboBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.btnSetDenomination = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetCassette = new System.Windows.Forms.Button();
            this.txtIDCassette = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BarraTitulo = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.panelContenedor.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.BarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(66)))), ((int)(((byte)(82)))));
            this.panelContenedor.Controls.Add(this.panel4);
            this.panelContenedor.Controls.Add(this.btnOpenCassette);
            this.panelContenedor.Controls.Add(this.btnCloseCassette);
            this.panelContenedor.Controls.Add(this.panel3);
            this.panelContenedor.Controls.Add(this.panel2);
            this.panelContenedor.Controls.Add(this.panel1);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(0, 38);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(556, 253);
            this.panelContenedor.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rdbNC4);
            this.panel4.Controls.Add(this.rdbNC3);
            this.panel4.Controls.Add(this.rdbNC2);
            this.panel4.Controls.Add(this.rdbNC1);
            this.panel4.Controls.Add(this.rdbRV);
            this.panel4.Location = new System.Drawing.Point(15, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(527, 46);
            this.panel4.TabIndex = 10;
            // 
            // rdbNC4
            // 
            this.rdbNC4.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbNC4.Location = new System.Drawing.Point(434, 3);
            this.rdbNC4.Name = "rdbNC4";
            this.rdbNC4.Size = new System.Drawing.Size(90, 23);
            this.rdbNC4.TabIndex = 4;
            this.rdbNC4.TabStop = true;
            this.rdbNC4.Text = "NC 4";
            this.rdbNC4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbNC4.UseVisualStyleBackColor = true;
            this.rdbNC4.CheckedChanged += new System.EventHandler(this.rdbNC4_CheckedChanged);
            // 
            // rdbNC3
            // 
            this.rdbNC3.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbNC3.Location = new System.Drawing.Point(329, 3);
            this.rdbNC3.Name = "rdbNC3";
            this.rdbNC3.Size = new System.Drawing.Size(90, 23);
            this.rdbNC3.TabIndex = 3;
            this.rdbNC3.TabStop = true;
            this.rdbNC3.Text = "NC 3";
            this.rdbNC3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbNC3.UseVisualStyleBackColor = true;
            this.rdbNC3.CheckedChanged += new System.EventHandler(this.rdbNC3_CheckedChanged);
            // 
            // rdbNC2
            // 
            this.rdbNC2.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbNC2.Location = new System.Drawing.Point(226, 3);
            this.rdbNC2.Name = "rdbNC2";
            this.rdbNC2.Size = new System.Drawing.Size(90, 23);
            this.rdbNC2.TabIndex = 2;
            this.rdbNC2.TabStop = true;
            this.rdbNC2.Text = "NC 2";
            this.rdbNC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbNC2.UseVisualStyleBackColor = true;
            this.rdbNC2.CheckedChanged += new System.EventHandler(this.rdbNC2_CheckedChanged);
            // 
            // rdbNC1
            // 
            this.rdbNC1.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbNC1.Location = new System.Drawing.Point(117, 3);
            this.rdbNC1.Name = "rdbNC1";
            this.rdbNC1.Size = new System.Drawing.Size(90, 23);
            this.rdbNC1.TabIndex = 1;
            this.rdbNC1.TabStop = true;
            this.rdbNC1.Text = "NC 1";
            this.rdbNC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbNC1.UseVisualStyleBackColor = true;
            this.rdbNC1.CheckedChanged += new System.EventHandler(this.rdbNC1_CheckedChanged);
            // 
            // rdbRV
            // 
            this.rdbRV.Appearance = System.Windows.Forms.Appearance.Button;
            this.rdbRV.Location = new System.Drawing.Point(13, 3);
            this.rdbRV.Name = "rdbRV";
            this.rdbRV.Size = new System.Drawing.Size(90, 23);
            this.rdbRV.TabIndex = 0;
            this.rdbRV.TabStop = true;
            this.rdbRV.Text = "RV";
            this.rdbRV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbRV.UseVisualStyleBackColor = true;
            this.rdbRV.CheckedChanged += new System.EventHandler(this.rdbRV_CheckedChanged);
            // 
            // btnOpenCassette
            // 
            this.btnOpenCassette.Location = new System.Drawing.Point(165, 205);
            this.btnOpenCassette.Name = "btnOpenCassette";
            this.btnOpenCassette.Size = new System.Drawing.Size(138, 25);
            this.btnOpenCassette.TabIndex = 9;
            this.btnOpenCassette.Text = "Open Cassette";
            this.btnOpenCassette.UseVisualStyleBackColor = true;
            this.btnOpenCassette.Click += new System.EventHandler(this.btnOpenCassette_Click);
            // 
            // btnCloseCassette
            // 
            this.btnCloseCassette.Location = new System.Drawing.Point(21, 205);
            this.btnCloseCassette.Name = "btnCloseCassette";
            this.btnCloseCassette.Size = new System.Drawing.Size(138, 25);
            this.btnCloseCassette.TabIndex = 8;
            this.btnCloseCassette.Text = "Close Cassette";
            this.btnCloseCassette.UseVisualStyleBackColor = true;
            this.btnCloseCassette.Click += new System.EventHandler(this.btnCloseCassette_Click);
            // 
            // panel3
            // 
            this.panel3.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.txtSizeH);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.txtSizeW);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.btnSetSize);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(344, 66);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 124);
            this.panel3.TabIndex = 7;
            // 
            // txtSizeH
            // 
            this.txtSizeH.Location = new System.Drawing.Point(111, 63);
            this.txtSizeH.Name = "txtSizeH";
            this.txtSizeH.Size = new System.Drawing.Size(39, 20);
            this.txtSizeH.TabIndex = 7;
            this.txtSizeH.Text = "140";
            this.txtSizeH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(91, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "H:";
            // 
            // txtSizeW
            // 
            this.txtSizeW.Location = new System.Drawing.Point(39, 25);
            this.txtSizeW.Name = "txtSizeW";
            this.txtSizeW.Size = new System.Drawing.Size(39, 20);
            this.txtSizeW.TabIndex = 5;
            this.txtSizeW.Text = "140";
            this.txtSizeW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(19, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "W:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(20, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnSetSize
            // 
            this.btnSetSize.Location = new System.Drawing.Point(5, 89);
            this.btnSetSize.Name = "btnSetSize";
            this.btnSetSize.Size = new System.Drawing.Size(168, 28);
            this.btnSetSize.TabIndex = 2;
            this.btnSetSize.Text = "Set Note Size";
            this.btnSetSize.UseVisualStyleBackColor = true;
            this.btnSetSize.Click += new System.EventHandler(this.btnSetSize_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(67, -2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Note Size";
            // 
            // panel2
            // 
            this.panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtGion);
            this.panel2.Controls.Add(this.cmdMonth);
            this.panel2.Controls.Add(this.cmbCountry);
            this.panel2.Controls.Add(this.btnSetDenomination);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(156, 66);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 124);
            this.panel2.TabIndex = 6;
            // 
            // txtGion
            // 
            this.txtGion.Location = new System.Drawing.Point(154, 24);
            this.txtGion.Name = "txtGion";
            this.txtGion.Size = new System.Drawing.Size(19, 20);
            this.txtGion.TabIndex = 5;
            // 
            // cmdMonth
            // 
            this.cmdMonth.FormattingEnabled = true;
            this.cmdMonth.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "200"});
            this.cmdMonth.Location = new System.Drawing.Point(70, 23);
            this.cmdMonth.Name = "cmdMonth";
            this.cmdMonth.Size = new System.Drawing.Size(78, 21);
            this.cmdMonth.TabIndex = 4;
            // 
            // cmbCountry
            // 
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Items.AddRange(new object[] {
            "BOB",
            "USD"});
            this.cmbCountry.Location = new System.Drawing.Point(5, 23);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(59, 21);
            this.cmbCountry.TabIndex = 3;
            // 
            // btnSetDenomination
            // 
            this.btnSetDenomination.Location = new System.Drawing.Point(5, 89);
            this.btnSetDenomination.Name = "btnSetDenomination";
            this.btnSetDenomination.Size = new System.Drawing.Size(168, 28);
            this.btnSetDenomination.TabIndex = 2;
            this.btnSetDenomination.Text = "Set Denomination";
            this.btnSetDenomination.UseVisualStyleBackColor = true;
            this.btnSetDenomination.Click += new System.EventHandler(this.btnSetDenomination_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Denomination";
            // 
            // panel1
            // 
            this.panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnSetCassette);
            this.panel1.Controls.Add(this.txtIDCassette);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(21, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 124);
            this.panel1.TabIndex = 5;
            // 
            // btnSetCassette
            // 
            this.btnSetCassette.Location = new System.Drawing.Point(5, 89);
            this.btnSetCassette.Name = "btnSetCassette";
            this.btnSetCassette.Size = new System.Drawing.Size(113, 28);
            this.btnSetCassette.TabIndex = 2;
            this.btnSetCassette.Text = "Set Cassette ID";
            this.btnSetCassette.UseVisualStyleBackColor = true;
            this.btnSetCassette.Click += new System.EventHandler(this.btnSetCassette_Click);
            // 
            // txtIDCassette
            // 
            this.txtIDCassette.Location = new System.Drawing.Point(5, 24);
            this.txtIDCassette.Name = "txtIDCassette";
            this.txtIDCassette.Size = new System.Drawing.Size(113, 20);
            this.txtIDCassette.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(36, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cassette ID";
            // 
            // BarraTitulo
            // 
            this.BarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(200)))));
            this.BarraTitulo.Controls.Add(this.btnCerrar);
            this.BarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.BarraTitulo.Name = "BarraTitulo";
            this.BarraTitulo.Size = new System.Drawing.Size(556, 38);
            this.BarraTitulo.TabIndex = 3;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.Location = new System.Drawing.Point(518, 6);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FrmCassettes
            // 
            this.ClientSize = new System.Drawing.Size(556, 291);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.BarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCassettes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelContenedor.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.BarraTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdbRV_CheckedChanged(object sender, EventArgs e)
        {
            LsLeeDatosdeBandeja(0);
        }

        private void LsLeeDatosdeBandeja(int pBandeja)
        {
            ////loggerATM.PsRegisterLogger("LsLeeDatosdeBandeja", "Llamada leer bandea. Bandeja:" + pBandeja.ToString());
            ////var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.ReadCassette  , pBandeja.ToString() + "28");
            ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            ////{
            ////    txtIDCassette.Text = LvVarRespuesta.ResponseOriginal.Substring (2, 8);
            ////}
            ////LvVarRespuesta = ExecutorCommand.ejecutar(Comando.ReadCassette, pBandeja.ToString() + "27");
            ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            ////{
            ////    cmbCountry.Text = LvVarRespuesta.ResponseOriginal.Substring(2, 3);
            ////    cmdMonth.Text = LvVarRespuesta.ResponseOriginal.Substring(6, 1) + new string('0', Convert.ToInt32(LvVarRespuesta.ResponseOriginal.Substring(7, 1)));
            ////    txtGion.Text = LvVarRespuesta.ResponseOriginal.Substring(8, 1);

            ////}
            ////LvVarRespuesta = ExecutorCommand.ejecutar(Comando.ReadCassette, pBandeja.ToString() + "29");
            ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            ////{
            ////    txtSizeW.Text = LvVarRespuesta.ResponseOriginal.Substring(5, 3);
            ////    txtSizeH.Text = LvVarRespuesta.ResponseOriginal.Substring(2, 3);
            ////}
        }

        private void rdbNC1_CheckedChanged(object sender, EventArgs e)
        {
            LvIntBandeja = 1;
            LsLeeDatosdeBandeja(1);

        }

        private void rdbNC2_CheckedChanged(object sender, EventArgs e)
        {
            LvIntBandeja = 2;
            LsLeeDatosdeBandeja(2);
        }

        private void rdbNC3_CheckedChanged(object sender, EventArgs e)
        {


            if (globalConfigATM.configDispenserStatus.NumBandejas >= 3)
            {
                LvIntBandeja = 3;
                LsLeeDatosdeBandeja(3);
            }
            else
            {
                MessageBox.Show("No existe bandeja para obtener datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void rdbNC4_CheckedChanged(object sender, EventArgs e)
        {
            if (globalConfigATM.configDispenserStatus.NumBandejas >= 4)
            {
                LvIntBandeja = 4;
                LsLeeDatosdeBandeja(4);
            }
            else
            {
                MessageBox.Show("No existe bandeja para obtener datos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSetCassette_Click(object sender, EventArgs e)
        {
            if (txtIDCassette.Text.Length != 8)
            {
                MessageBox.Show("Verificar longitud del ID de la bandeja, debe ser de 8 caracteres.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void btnSetDenomination_Click(object sender, EventArgs e)
        {
            try
            {

                ////string param = LvIntBandeja.ToString() + "27/" + cmbCountry.Text.ToString() + "0" + LfStrMontoNMD(Convert.ToInt32(cmdMonth.Text.ToString()));// cmdMonth.Text.ToString().Substring(0, 1) + Convert.ToInt32(cmdMonth.Text.ToString().Substring(1)).ToString();
                //////MessageBox.Show(param);
                ////var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.WriteSize, param);
                ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
                ////{
                ////    MessageBox.Show("Bandeja actualizada...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
                ////else
                ////{
                ////    MessageBox.Show("Error al registrado. Error:" + LvVarRespuesta.Description, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
            }
            catch (Exception)
            {
                //ssageBox.Show(e.Message);
                throw;
            }

        }
        private string LfStrMontoNMD(int pMonto)
        {
            int nro = 0;
            int ceros = 0;
            if (pMonto < 100)
            {
                nro = pMonto / 10;
                ceros = 1;
            }
            else
            {
                nro = pMonto / 100;
                ceros = 2;
            }
            return nro.ToString() + ceros.ToString() + "_";
        }
        private void btnSetSize_Click(object sender, EventArgs e)
        {

            ////string param = LvIntBandeja.ToString() + "29/" + new String('0', 3 - txtSizeH.Text.Length) + txtSizeH.Text + new String('0', 3 - txtSizeW.Text.Length) + txtSizeW.Text;
            ////var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.WriteSize, param);
            ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            ////{
            ////    MessageBox.Show("Bandeja actualizada...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Error al registrado. Error:" + LvVarRespuesta.Description, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
        }

        private void btnCloseCassette_Click(object sender, EventArgs e)
        {
            ////var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.CerrarBandejas);
            ////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            ////{
            ////    MessageBox.Show("Bandejas liberadas...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
            ////else
            ////{
            ////    MessageBox.Show("Error al registrado. Error:" + LvVarRespuesta.Description, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////}
        }

        private void btnOpenCassette_Click(object sender, EventArgs e)
        {
            //////var LvVarRespuesta = ExecutorCommand.ejecutar(Comando.AbrirBandejas );
            //////if (LvVarRespuesta.state == ResponseDispensador.Exito)
            //////{
            //////    MessageBox.Show("Bandejas cerradas...", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////}
            //////else
            //////{
            //////    MessageBox.Show("Error al registrado. Error:" + LvVarRespuesta.Description, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //////}
        }
    }
}
