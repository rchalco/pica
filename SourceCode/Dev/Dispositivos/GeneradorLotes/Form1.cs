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

namespace GeneradorLotes
{
    public partial class Form1 : Form
    {
        List<LoteDistribucion> lotesDistrubucion = new List<LoteDistribucion>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generarArchivoDistribucion();
        }

        public void generarArchivoDistribucion()
        {
            lotesDistrubucion = new List<LoteDistribucion>();
            if (!File.Exists(txtArchivoEmbozo.Text))
            {
                MessageBox.Show($"No se puede encontrar el archivo: {txtArchivoEmbozo.Text}. IMPOSIBLE CONTINUAR!!!!", "ERROR");
                return;
            }
            if (nCBB.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nCBB.Value), codigoSucursal = "203" });//Cochabamba
            }
            if (nBEPN.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nBEPN.Value), codigoSucursal = "208" });//BENI
            }
            if (nPO.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nPO.Value), codigoSucursal = "206" });//POSTOSI
            }
            if (nSC.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nSC.Value), codigoSucursal = "202" });//STA CRUZ
            }
            if (nSU.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nSU.Value), codigoSucursal = "204" });//CHUQUISACA
            }
            if (nTJ.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nTJ.Value), codigoSucursal = "207" });//TARIJA
            }
            if (nLP.Value > 0)
            {
                lotesDistrubucion.Add(new LoteDistribucion { cantidad = Convert.ToInt32(nLP.Value), codigoSucursal = "201" });//LA PAZ
            }

            ///TODO: generacion del archivo            
            List<string> dataEmbbossing = File.ReadLines(txtArchivoEmbozo.Text).ToList();
            List<string> dataDistribution = new List<string>();
            int contLote = 0;
            int contDist = 0;
            int contador = 0;
            long currentTarjeta = 0;

            ///PARAMETROS PARA GENERAR
            int indice = Convert.ToInt32(ultimoIndiceBox.Value);
            long ultimaTarjetaDistribuida = Convert.ToInt64(ultimaTarjetaBox.Value);

            ///TODO: Generamos 
            foreach (var x in dataEmbbossing)
            {
                currentTarjeta = Convert.ToInt64(x.Substring(1, 16));
                if (currentTarjeta <= ultimaTarjetaDistribuida)
                {
                    continue;
                }
                indice++;
                contador++;
                if (contador <= lotesDistrubucion.Sum(yy => yy.cantidad))
                {
                    contDist++;
                    if (contDist > lotesDistrubucion[contLote].cantidad)
                    {
                        contLote++;
                        contDist = 1;
                    }

                    if (!string.IsNullOrEmpty(x))
                    {
                        string partData = "0200|"
                        + lotesDistrubucion[contLote].codigoSucursal.PadLeft(4, '0') + "|"
                        + indice.ToString().PadLeft(9, '0') + "0|"
                        + x.Substring(1, 16) + "|2809";
                        dataDistribution.Add(partData);
                    }
                }
            }
            if (!Directory.Exists("c:\\DistribucionLote"))
            {
                Directory.CreateDirectory("c:\\DistribucionLote");
            }
            string pathNewFile = $"c:\\DistribucionLote\\distribucion{DateTime.Now.ToString("ddMMyyyy")}.data";
            if (File.Exists(pathNewFile))
            {
                File.Delete(pathNewFile);
            }
            File.WriteAllLines(pathNewFile, dataDistribution);
            MessageBox.Show($"Su archivo se genero correctamente, el mismo se encuentra en: {pathNewFile}");
        }
    }
}
