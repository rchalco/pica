using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeReceptor.Service;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Timers;

namespace Runtime.Receptor.Test
{
    [TestClass]
    public class ReceptorServiceTest
    {
        Timer timer = new Timer();
        int counter = 0;
        ReceptorService receptorService = new ReceptorService();

        [TestMethod]
        public void ActivarReceptorTest()
        {

            //var response = receptorService.InitReceptor("COM4");
            //response = receptorService.ActivarReceptor();
            //timer.Interval = 1000;
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                counter++;
                receptorService.GetBills().ListEntities?.ForEach(entity =>
                {
                    Console.WriteLine(entity.Index + "-" + entity.Detalle);
                });
                if (counter > 40)
                {
                    //timer.Stop();
                    //receptorService.DesactivarReceptor();
                    //receptorService.ActivarReceptor();
                    //counter = 0;
                    //timer.Start();
                }
            }
            catch (Exception error)
            {

                Console.WriteLine("Error:" + error.Message);
            }
            
        }


    }
}
