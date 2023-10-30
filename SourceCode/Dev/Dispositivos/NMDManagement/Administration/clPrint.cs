using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace NMDManagement.Administration
{
    public  class clPrint
    {
        List<BalanceData> balanceDatas ;
        long IdTransac = 0;
        string title = "";
        string operation = "";
        string numTicket="";
        decimal Total = 0;
        string User = "";
        public void PrintCalibrateDispenser(List<BalanceData> pBalanceDatas, long pIdTransac, string pNumTicket, decimal pAmountTicket, string pUser)
        {
            try
            {
                title = "          Calibrado de dispensador";
                operation = "";
                balanceDatas = pBalanceDatas;
                IdTransac = pIdTransac;
                if (pNumTicket == "")
                {
                    numTicket = "sin retoma";
                }
                else
                {
                    numTicket = pNumTicket;
                }
                Total = pAmountTicket;
                User = pUser;
                PrinterSettings printerSettings = new PrinterSettings();
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings = printerSettings;
                printDocument.PrintPage += new PrintPageEventHandler(PrintingLoadDispenser);
                printDocument.Print();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void PrintLoadDispenser( List<BalanceData> pBalanceDatas , long pIdTransac, string pNumTicket, decimal pAmountTicket, string pUser )
        {
            try
            {
                title = "          Carga ATM";
                operation = "Nro. Carga:   ";
                balanceDatas = pBalanceDatas;
                IdTransac = pIdTransac;
                if (pNumTicket == "")
                {
                    numTicket = "sin retoma";
                }
                else
                {
                    numTicket = pNumTicket;
                }
                Total = pAmountTicket;
                User = pUser;
                PrinterSettings printerSettings = new PrinterSettings();
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings = printerSettings;
                printDocument.PrintPage += new PrintPageEventHandler(PrintingLoadDispenser);
                printDocument.Print();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void PrintBalance(List<BalanceData> pBalanceDatas, long pIdTransac, string pUser)
        {
            try
            {
                title = "          Arqueo ATM";
                operation = "Nro. Arqueo:  ";
                balanceDatas = pBalanceDatas;
                IdTransac = pIdTransac;
                User = pUser;
                PrinterSettings printerSettings = new PrinterSettings();
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings = printerSettings;
                printDocument.PrintPage += new PrintPageEventHandler(PrintingLoadDispenser);
                printDocument.Print();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void PrintingLoadDispenser(object sender, PrintPageEventArgs  e)
        {
            services servicios = new services();
            int PosX = 5;
            int PosY = 10;
            string imageRuth = @"C:\HLA\LogBancoPRODEMv1.png";
            Image imageButh = Image.FromFile(imageRuth);

            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            ResultConfgATM globalConfigATM = servicios.GetHotState();
            //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);

            Font fontPrint = new Font("Areal", 12);

            e.Graphics.DrawImage(imageButh ,new Rectangle(10, PosY, 200,100)); 
            PosY += 50;

            e.Graphics.DrawString(title , fontPrint, Brushes.Black , PosX, PosY+=50, new StringFormat());

            fontPrint = new Font("Areal", 10);
            e.Graphics.DrawString("Nro. ATM     :" + globalConfigATM.GetHotStateResult.Object.Id.ToString()  , fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
            e.Graphics.DrawString("Nombre       :" + globalConfigATM.GetHotStateResult.Object.Name , fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
            e.Graphics.DrawString( operation  +  IdTransac.ToString () , fontPrint, Brushes.Black, PosX, PosY += 30, new StringFormat());
            e.Graphics.DrawString("Fecha y hora :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
            e.Graphics.DrawString("Usuario          :" + User , fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
           
            fontPrint = new Font("Areal", 8);
            e.Graphics.DrawString("Bandeja   Corte          Cantidad        Monto", fontPrint, Brushes.Black, PosX, PosY+=30, new StringFormat());
            e.Graphics.DrawString("------------------------------------------------------", fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());

            foreach (BalanceData balanceData in balanceDatas)
            {  
                string printString = balanceData.NroCassette.ToString() + "                " + balanceData.Denomination.ToString() +"            " + balanceData.RegisterBalance.ToString() + "                 " + (balanceData.Denomination * Convert.ToInt32( balanceData.RegisterBalance)).ToString() ;
                e.Graphics.DrawString(printString, fontPrint, Brushes.Black , PosX, PosY +=20, new StringFormat());
            }
            if(numTicket != "")
            {
                e.Graphics.DrawString("------------------------------------------------------", fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
                e.Graphics.DrawString("Número Retoma:" + numTicket.ToString() , fontPrint, Brushes.Black, PosX, PosY += 20, new StringFormat());
                e.Graphics.DrawString("Monto                 :" + Total.ToString(), fontPrint, Brushes.Black, PosX, PosY += 30, new StringFormat());
                
            }
            e.Graphics.DrawString("", fontPrint, Brushes.Black, PosX, PosY += 30, new StringFormat());
            e.HasMorePages = false;
        }
    }
}
