using NUnit.Framework;
using RuntimeFingerPrint.Services;
using System.Collections.Generic;
using static Interop.Main.Cross.Domain.FingerPrint.FingerContract;

namespace RuntimeFinger.Services.Test
{
    public class FingerPrintServicesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CaptureFingerTest()
        {
            FingerPrintServices fingerPrintServices = new FingerPrintServices();
            var resul = fingerPrintServices.CaptureFinger(10000);
            Assert.Pass();
        }

        [Test]
        public void CaptureFingerForEnrollTest()
        {
            FingerPrintServices fingerPrintServices = new FingerPrintServices();
            var resul = fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.DP, 10000);
            Assert.Pass();
        }

        [Test]
        public void EnrollTestDP()
        {
            FingerPrintServices fingerPrintServices = new FingerPrintServices();
            List<string> list = new List<string>();
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.DP, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.DP, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.DP, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.DP, 10000).Message);
            var resulEnrrollDP = fingerPrintServices.Enroll(TypeEnroll.DP, list);
            Assert.Pass();
        }

        [Test]
        public void EnrollTestISO()
        {
            FingerPrintServices fingerPrintServices = new FingerPrintServices();
            List<string> list = new List<string>();
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.ISO, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.ISO, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.ISO, 10000).Message);
            list.Add(fingerPrintServices.CaptureFingerForEnroll(TypeEnroll.ISO, 10000).Message);
            var resulEnrrollDP = fingerPrintServices.Enroll(TypeEnroll.ISO, list);
            Assert.Pass();
        }
    }
}