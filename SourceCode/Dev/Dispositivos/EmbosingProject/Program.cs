using EmbosingProject.Properties;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EmbosingProject
{
    internal class Program
    {
        static List<Lote> lotes = new List<Lote>();
        static List<LoteDistribucion> lotesDistrubucion = new List<LoteDistribucion>();
        static void Main(string[] args)
        {
            //generarArchivoDistribucion();
            generarArchivoEmbozo();
        }


        public static void generarArchivoDistribucion()
        {

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS JULIO
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 4000, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1290, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 3000, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1000, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1920, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS AGOSTO
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1300, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 430, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1230, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 800, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS SEPTIEMBRE
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1000, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 430, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1230, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 350, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1250, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS OCTUBRE
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1000, codigoSucursal = "203" });//CBBA
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 0, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 430, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1230, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 400, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1320, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS NOVIEMBRE
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 430, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 390, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 200, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1140, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS DICIEMBRE
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "202" });//STA CRUZ
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 390, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1300, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS ENERO 2023
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "203" });//CBBA
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "202" });//STA CRUZ
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 390, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 100, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1200, codigoSucursal = "201" });//LA PAZ

            /////CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS FEBRERO 2023
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "203" });//CBBA
            ////lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1130, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 350, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1650, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS MARZO 2023 primer archivo solo se hizo 610 pra CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 2000, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 840, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1000, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS MARZO 2023 segundo archivo de embozo
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1390, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 840, codigoSucursal = "208" });//BENI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1000, codigoSucursal = "202" });//STA CRUZ
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "204" });//CHUQUISACA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "207" });//TARIJA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "201" });//LA PAZ

            ///CONFIGURACION DEFINIDA POR EL AREA DE NEGOCIOS MARZO 2023 segundo archivo de embozo
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1500, codigoSucursal = "203" });//CBBA
            //lotesDistrubucion.Add(new LoteDistribucion { cantidad = 840, codigoSucursal = "208" });//BENI
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 450, codigoSucursal = "206" });//POSTOSI
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 500, codigoSucursal = "202" });//STA CRUZ
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 250, codigoSucursal = "204" });//CHUQUISACA
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 200, codigoSucursal = "207" });//TARIJA
            lotesDistrubucion.Add(new LoteDistribucion { cantidad = 1040, codigoSucursal = "201" });//LA PAZ

            ///TODO: generacion del archivo
            //List<string> dataEmbbossing = File.ReadLines(@"D:\nuevas tarjetas xh smart\Primer envio 50000\embossing_data.txt").ToList();
            List<string> dataEmbbossing = File.ReadLines(@"D:\nuevas tarjetas xh smart\Segundo envio 50000\20220825.txt").ToList();
            List<string> dataDistribution = new List<string>();
            int contLote = 0;
            int contDist = 0;
            int contador = 0;
            long currentTarjeta = 0;

            ///PARAMETROS PARA GENERAR
            int indice = 56131;
            long ultimaTarjetaDistribuida = 7265210000056138;

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

            File.WriteAllLines("distribucion.data", dataDistribution);
        }

        public static void generarArchivoEmbozo()
        {
            //lotes.Add(new Lote { nro = 1, pan_inicial = 726521000001000, pan_final = 726521000002000, cantidad = 1000, fecha_vencimiento = "12/26" });
            //lotes.Add(new Lote { nro = 1, pan_inicial = 726521000002001, pan_final = 726521000003000, cantidad = 1000, fecha_vencimiento = "01/27" });
            //lotes.Add(new Lote { nro = 1, pan_inicial = 726521000003001, pan_final = 726521000004000, cantidad = 1000, fecha_vencimiento = "02/27" });
            //lotes.Add(new Lote { nro = 1, pan_inicial = 726521000004001, pan_final = 726521000005000, cantidad = 1000, fecha_vencimiento = "03/27" });

            //lotes.Add(new Lote { nro = 1, pan_inicial = 7265210000000006, pan_final = 7265210000050006, cantidad = 50000, fecha_vencimiento = "09/28" });
            //lotes.Add(new Lote { nro = 2, pan_inicial = 7265210000000003, pan_final = 7265210000000005, cantidad = 3, fecha_vencimiento = "01/27" });
            //lotes.Add(new Lote { nro = 1, pan_inicial = 7265210000050007, pan_final = 7265210000100007, cantidad = 50000, fecha_vencimiento = "01/29" });

            //lotes.Add(new Lote { nro = 1, pan_inicial = 6265210000050007, pan_final = 6265210000050012, cantidad = 5, fecha_vencimiento = "01/29" });//lote para probar

            lotes.Add(new Lote { nro = 1, pan_inicial = 7265210000100007, pan_final = 7265210000150006, cantidad = 50000, fecha_vencimiento = "01/30" });//tercer envio 50000

            StringBuilder stringBuilder = new StringBuilder();
            Random rnd = new Random();
            lotes.ForEach(lote =>
            {
                for (long i = lote.pan_inicial; i <= lote.pan_final; i++)
                {
                    string gestionVenc = lote.fecha_vencimiento.Substring(3, 2);
                    string mesVenc = lote.fecha_vencimiento.Substring(0, 2);
                    string codServicio = Resources.codigoServicio;
                    string issuePreparatoryData = Resources.ISSUER_PREPARATORY_DATA;
                    string CVV1 = rnd.Next(1, 999).ToString().PadLeft(3, '0');
                    string ICVV1 = rnd.Next(1, 999).ToString().PadLeft(3, '0');
                    string rowCard = string.Format(Resources.stringEmbosing.Substring(0, Resources.stringEmbosing.Length - 1), i.ToString(), lote.fecha_vencimiento, gestionVenc, mesVenc, codServicio, issuePreparatoryData, CVV1, ICVV1);
                    stringBuilder.AppendLine(rowCard);
                }
            });
            string aaaa = "1".PadLeft(3, '0');
            File.WriteAllText("data.txt", stringBuilder.ToString());

        }
    }
}
