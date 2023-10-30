using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement.Administration
{
    public partial class FrmCheckNMDStatus : Form
    {
        List<ProcessData> processDatas = new List<ProcessData>();

        public FrmCheckNMDStatus()
        {
            InitializeComponent();
            processDatas.Clear();

            processDatas.Add(new ProcessData { Nro = 1, ProccessName = "Archivos de inicio", StatusProccess = "Pendiente"});
            processDatas.Add(new ProcessData { Nro = 2, ProccessName = "Inicialiar dispensador", StatusProccess = "Pendiente" });
            processDatas.Add(new ProcessData { Nro = 3, ProccessName = "Validar Bandejas", StatusProccess = "Pendiente" });
            
            dataGridView1.DataSource= processDatas;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {
            services service = new services();
            foreach(ProcessData processData in processDatas)
            {
                switch (processData.Nro)
                {
                    case 1://verificar archivo de inicio
                        //if(File.Exists(@"C:\HLA\ATM.json"))
                        //{
                        //    processData.StatusProccess = "Procesado";
                        //}
                        //else 
                        //{
                        //    processData.StatusProccess = "Archivo ATM.json no existe";
                        //    return;
                        //}
                        //dataGridView1.Refresh();
                        processData.StatusProccess = "Procesado";
                        break;
                    case 2:
                        ResponseInit response= service.initializa_dispenser();
                        if (response.InicializarResult.State == ResponseType.Success)
                        {
                            processData.StatusProccess = "Procesado";
                        }
                        else
                        {
                            processData.StatusProccess = "Error:" + response.InicializarResult.Message ;
                            return;
                        }
                        dataGridView1.Refresh();
                        break ;
                    case 3:
                        ResponseReadIdCassette responseCassette = service.ReadIdCassette();
                        if (responseCassette.VerificaBanejasResult.State == ResponseType.Success)
                        {
                            processData.StatusProccess = "Procesado";
                        }
                        else
                        {
                            processData.StatusProccess = "Error:" + responseCassette.VerificaBanejasResult.Message;
                            return;
                        }
                        dataGridView1.Refresh();
                        break;

                }
            }
        }

        private void btnInicializa_Click(object sender, EventArgs e)
        {
            services service = new services();
            CardResultIni  resp = service.CardReader_Init(3);
            if(resp.InitReaderResult.State==ResponseType.Success )
            {
                MessageBox.Show("Estado=Correcto\nDescripción=" + resp.InitReaderResult.Message,"Aviso",MessageBoxButtons.OK , MessageBoxIcon.Information );
            }
            else
            {
                MessageBox.Show("Estado="+ resp.InitReaderResult.State.ToString()  +"\nDescripción=" + resp.InitReaderResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            services service = new services();
            CardResultRead  resp = service.CardReader_read();
            if (resp.ReadCardResult.State == ResponseType.Success)
            {
                MessageBox.Show("Estado=Correcto\nDescripción=" + resp.ReadCardResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Estado=" + resp.ReadCardResult.State.ToString() + "\nDescripción=" + resp.ReadCardResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEject_Click(object sender, EventArgs e)
        {
            services service = new services();
            CardResultEject  resp = service.CardReader_eject(); 
            if (resp.EjectCardResult.State == ResponseType.Success)
            {
                MessageBox.Show("Estado=Correcto\nDescripción=" + resp.EjectCardResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Estado=" + resp.EjectCardResult.State.ToString() + "\nDescripción=" + resp.EjectCardResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            services service = new services();
            CardResultReset  resp = service.CardReader_reset();
            if (resp.ResetResult.State == ResponseType.Success)
            {
                MessageBox.Show("Estado=Correcto\nDescripción=" + resp.ResetResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Estado=" + resp.ResetResult.State.ToString() + "\nDescripción=" + resp.ResetResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFinger_Click(object sender, EventArgs e)
        {
            services service = new services();
            DataCaptureFinger  resp = service.Capture_FingerPrintTest(3000);
            if (resp.CaptureFingerResult.State == ResponseType.Success)
            {
                MessageBox.Show("Estado=Correcto\nDescripción=" + resp.CaptureFingerResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Estado=" + resp.CaptureFingerResult.State.ToString() + "\nDescripción=" + resp.CaptureFingerResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    public class ProcessData
    {
        public int Nro { get; set; }    
        public string ProccessName { get; set; }
        public string StatusProccess { get; set; }

    }

}
