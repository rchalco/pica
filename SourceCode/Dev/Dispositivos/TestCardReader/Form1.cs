using Interop.Main.Cross.Domain.CardReader;
using RuntimeCardReader.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestCardReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel2.Enabled = false;
                CardReaderService cardReaderService = new CardReaderService();
                TypeReaderCard typeReaderCard = rbCreator.Checked ? TypeReaderCard.CREATOR : TypeReaderCard.GENPLUS;
                var resulInit = InitReader(typeReaderCard);

                if (resulInit.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
                {
                    MessageBox.Show($"IMPOSIBLE REALIZAR OPREACION. {resulInit.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    splitContainer1.Panel2.Enabled = true;
                    return;
                }
                int cantidadExito = 0;
                int cantidadFalla = 0;
                lsResul.Items.Add("********INICIANDO**********");
                for (int i = 0; i < numTotal.Value; i++)
                {
                    lsResul.Items.Add($"-----Resul: {(i + 1).ToString()}");
                    var resulRead = cardReaderService.ReadCard();
                    lsResul.Items.Add(":::: READ RESUL -" + resulRead.Message);
                    var resulMotorized = InitReader(typeReaderCard);
                    lsResul.Items.Add(":::: MOTOR RESUL -" + resulMotorized.Message);
                    if (resulRead.State == Foundation.Stone.Application.Wrapper.ResponseType.Success
                        && resulMotorized.State == Foundation.Stone.Application.Wrapper.ResponseType.Success)
                    {
                        cantidadExito++;
                    }
                    else
                    {
                        cantidadFalla++;
                    }
                }
                lsResul.Items.Add("********FIN DE PROCESO**********");
                lsResul.Items.Add("//////////// REPORTE /////////////");
                lsResul.Items.Add($"- Procesos exitosos: {cantidadExito}");
                lsResul.Items.Add($"- Procesos fallidos: {cantidadFalla}");

                splitContainer1.Panel2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"IMPOSIBLE REALIZAR OPREACION. {ex.Message} {ex.StackTrace}", "ERROR FATAL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Foundation.Stone.Application.Wrapper.Response InitReader(TypeReaderCard typeReaderCard)
        {
            CardReaderService cardReaderService = new CardReaderService();
            var resulINi = cardReaderService.InitReader(typeReaderCard);
            System.Threading.Thread.Sleep(2000);
            return resulINi;
        }

        private void lsResul_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder copy_buffer = new System.Text.StringBuilder();
            foreach (object item in lsResul.Items)
                copy_buffer.AppendLine(item.ToString());
            if (copy_buffer.Length > 0)
                Clipboard.SetText(copy_buffer.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TypeReaderCard typeReaderCard = rbCreator.Checked ? TypeReaderCard.CREATOR : TypeReaderCard.GENPLUS;
            var resulInit = InitReader(typeReaderCard);

            if (resulInit.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
            {
                MessageBox.Show($"IMPOSIBLE REALIZAR OPREACION. {resulInit.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                splitContainer1.Panel2.Enabled = true;
            }
            else
            {
                MessageBox.Show($"LECTOR HABILITADO", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CardReaderService cardReaderService = new CardReaderService();
            var resulInit = cardReaderService.EjectCard();

            if (resulInit.State != Foundation.Stone.Application.Wrapper.ResponseType.Success)
            {
                MessageBox.Show($"IMPOSIBLE REALIZAR OPREACION. {resulInit.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
