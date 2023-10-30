using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceMonitoreo.Contract;
using ServiceMonitoreo.Service;
using System;

namespace Monitoreo.Test
{
    [TestClass]
    public class MonitoreoTest
    {

        [TestMethod]
        public void GetStateFingerPrintTest()
        {
            HandlerMonitoreo handlerMonitoreo = new HandlerMonitoreo();
            var resulPublishers = handlerMonitoreo.GetStateFingerPrint("192.168.234.211");
        }

        [TestMethod]
        public void GetStateCardReaderTest()
        {
            HandlerMonitoreo handlerMonitoreo = new HandlerMonitoreo();
            var resulPublishers = handlerMonitoreo.GetStateCardReader("192.168.234.211", "1");
        }

        [TestMethod]
        public void ReadCardReaderTest()
        {
            HandlerMonitoreo handlerMonitoreo = new HandlerMonitoreo();
            var resulPublishers = handlerMonitoreo.ReadCard("192.168.234.211");
        }

        [TestMethod]
        public void RegisterBinnacleTest()
        {
            HandlerMonitoreo handlerMonitoreo = new HandlerMonitoreo();
            RequestRegisterBinnacle requestRegisterBinnacle = new RequestRegisterBinnacle
            {
                Device = "Device 2",
                Evento = "Evento 2",
                IdTransacction = null,
                IP = "192.168.234.211",
                Operation = "Operacion 2",
                Trace = "Trace 2"
            };
            var resulPublishers = handlerMonitoreo.RegisterBinnacle(requestRegisterBinnacle);
        }
    }
}
