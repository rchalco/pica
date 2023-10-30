using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using OrchestratorDevice.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorDevice.Util
{
    public class PrinterUtil
    {
        private static List<string> allowPrints = new List<string>();
        public static void PrintVoucher(string voucher, string tittle)
        {
            string fileNameVoucher = "c:\\tmp\\" + Foundation.Stone.CrossCuting.Util.CustomGuid.GetGuid() + ".pdf";
            if (!Directory.Exists("c:\\tmp\\"))
            {
                Directory.CreateDirectory("c:\\tmp\\");
            }


            PdfWriter writer = new PdfWriter(fileNameVoucher);
            PdfDocument pdf = new PdfDocument(writer);
            PageSize pageSize = pdf.GetDefaultPageSize();
            pageSize.SetWidth(200f);
            Document document = new Document(pdf, pageSize);
            document.SetBottomMargin(0f);
            document.SetTopMargin(0.2f);
            document.SetLeftMargin(2f);

            ///TODO titulo

            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create(@"C:\HLA\LogBancoPRODEMv1.png"))
              .SetHeight(50)
              .SetWidth(120)
              .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);
            Paragraph header = new Paragraph(tittle)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(10);
            document.Add(header);


            var linesVoucher = voucher.Replace("&", "").Split('|').ToList();
            linesVoucher.ForEach(yy =>
            {
                Text contentText = null;
                if (yy.ToLower().StartsWith("cuenta"))
                {
                    //string cuentaOfuscada = "Cuenta: " + yy.Split(' ')[1].Substring(0, 3) + "XXXXXXXXX" + yy.Split(' ')[1].Substring(11, 4);
                    string cuentaOfuscada =  yy.Substring(0,yy.IndexOf(":") + 5) + "XXXXXXXX" + yy.Substring(yy.IndexOf(":") + 11);
                    contentText = new Text(cuentaOfuscada);
                }
                else
                {
                    contentText = new Text(yy);
                }

                contentText.SetFontSize(7);
                Paragraph paragraphContent = new Paragraph(contentText);
                paragraphContent.SetMultipliedLeading(1f);
                document.Add(paragraphContent);
            });

            Paragraph SpaceFooter = new Paragraph("")
                  .SetTextAlignment(TextAlignment.CENTER)
                  .SetFontSize(10);
            document.Add(SpaceFooter);

            Paragraph paragraphFooter = new Paragraph("GRACIAS POR USAR NUESTRO CAJERO...")
                  .SetTextAlignment(TextAlignment.CENTER)
                  .SetFontSize(10);
            document.Add(paragraphFooter);
            document.Close();
            SendToPrinter(fileNameVoucher);
        }

        public static void SendToPrinter(string fileName)
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(fileName);
            doc.Print();
            doc.Close();
        }
        private  PrinterStatus GetDescriptionPrinterStatus(PrintQueue pq)
        {

            PrinterStatus printerStatus = new PrinterStatus();
            printerStatus.DescStatus = "Correcto";
            printerStatus.IdStatus = 1;
            printerStatus.PrinterName = "Vacia";
            if (pq == null)
            {
                printerStatus.DescStatus = "Null";
                printerStatus.IdStatus = 3;
                return printerStatus;
            }
            if (!pq.Name.ToUpper().Contains( "W80"))
            { 
                    if (pq.QueueStatus == PrintQueueStatus.None)
                    {
                        printerStatus.DescStatus = "Impresora determinada desconectada";
                        printerStatus.IdStatus = 3;
                        return printerStatus;
                    }
            }
            printerStatus.DescStatus = "";
            printerStatus.PrinterName = pq.Name ;

            if ((pq.QueueStatus & PrintQueueStatus.PaperProblem) == PrintQueueStatus.PaperProblem)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Tiene un problema de papel. ";
                printerStatus.IdStatus = 1;
            }
            if ((pq.QueueStatus & PrintQueueStatus.NoToner) == PrintQueueStatus.NoToner)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "se quedó sin tóner. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.DoorOpen) == PrintQueueStatus.DoorOpen)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Tiene una puerta abierta. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.Error) == PrintQueueStatus.Error)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Está en un estado de error. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.NotAvailable) == PrintQueueStatus.NotAvailable)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "No está disponible. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.Offline) == PrintQueueStatus.Offline)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Fuera de linea. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutOfMemory) == PrintQueueStatus.OutOfMemory)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Está fuera de la memoria. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperOut) == PrintQueueStatus.PaperOut)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Se ha quedado sin papel. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.OutputBinFull) == PrintQueueStatus.OutputBinFull)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Tiene una bandeja de salida llena. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.PaperJam) == PrintQueueStatus.PaperJam)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Tiene un atasco de papel. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.Paused) == PrintQueueStatus.Paused)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Está en pausa. ";
                printerStatus.IdStatus = 3;
            }
            if ((pq.QueueStatus & PrintQueueStatus.TonerLow) == PrintQueueStatus.TonerLow)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Tiene poco tóner o papel. ";
                printerStatus.IdStatus = 1;
            }
            if ((pq.QueueStatus & PrintQueueStatus.UserIntervention) == PrintQueueStatus.UserIntervention)
            {
                printerStatus.DescStatus = printerStatus.DescStatus + "Necesita la intervención del usuario. ";
                printerStatus.IdStatus = 3;
            }
            return printerStatus;
        }

        public PrinterStatus GetPrinterStatus()
        {
            //LocalPrintServer ps = new LocalPrintServer();

            //// Get the default print queue
            //PrintQueue printQueue = ps.DefaultPrintQueue;

            //return  GetDescriptionPrinterStatus (printQueue);
            return GetDescLocalPrinterStatus ();
        }

        private string DesStatus(int pStatus)
        {
            string status = string.Empty;
            switch (pStatus)
            {
                case 0:
                case 64:
                case 131072:
                    status = "Online";
                    break;
                case 1:
                    status = "Paused";
                    break;
                case 4194432:
                    status = "Lid Open";
                    break;
                case 16:
                case 144:
                    status = "Out of Paper";
                    break;
                case 4194448:
                    status = "Out of Paper && Lid Open";
                    break;
                case 1024:
                    status = "Printing";
                    break;
                case 32768:
                    status = "Initializing";
                    break;
                case 160:
                    status = "Manual Feed in Progress";
                    break;
                case 1026:
                    status = "Document failed to print";
                    break;
                case 1027:
                    status = "Printing paused";
                    break;
                case 4096:
                case 128:
                    status = "Offline";
                    break;

                default:
                    status = "unknown state";
                    break;
            }
            return status;
        }
        private PrinterStatus GetDescLocalPrinterStatus()
        {
            PrinterStatus printerStatus = new PrinterStatus();
            printerStatus.DescStatus = "Correcto";
            printerStatus.IdStatus = 1;
            printerStatus.PrinterName = "Vacia";

            ConnectionOptions oConn = new ConnectionOptions();
            System.Management.ManagementScope oMs = new System.Management.ManagementScope(@"\root\cimv2", oConn);
            System.Management.ObjectQuery oQuery = new System.Management.ObjectQuery("select * from Win32_Printer where Default =1");//
            ManagementObjectSearcher oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            ManagementObjectCollection oReturnCollection = oSearcher.Get();

            foreach (ManagementObject oReturn in oSearcher.Get())
            {
                int status = Convert.ToInt32(oReturn["PrinterState"]);
                printerStatus.PrinterName  = oReturn["Name"].ToString();

                if (status == 0)
                {
                    printerStatus.DescStatus = "Correcto";
                    printerStatus.IdStatus = 1;
                }
                else if (status == 64 | status == 131072)
                {
                    printerStatus.DescStatus = "Poco papel";
                    printerStatus.IdStatus = 1;
                }
                else
                {
                    printerStatus.DescStatus = DesStatus(status );
                    printerStatus.IdStatus = 3;
                }                
            }
            return printerStatus;
        }
    }

    public class PrinterStatus
    {
        public string DescStatus { set; get; }
        public int IdStatus { set; get; }
        public string PrinterName { set; get; }
    }
}
