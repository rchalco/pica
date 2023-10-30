using Foundation.Stone.Application.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeCardReader.Core.Base;
using RuntimeCardReader.Core.Implement.Creator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeCardReader.Test
{
    [TestClass]
    public class ReadCardrTest
    {
        [TestMethod]
        public void InitializeCardReader()
        {
            ReaderCard readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            Assert.AreEqual(resulInit.State, ResponseType.Success);
        }

        [TestMethod]
        public void EjectCardReader()
        {
            ReaderCard readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            var resulEject = readerCard.EjectCard();
            Assert.AreEqual(resulEject.State, ResponseType.Success);
        }

        [TestMethod]
        public void ReaderCardTest()
        {
            ReaderCard readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            var resulRead = readerCard.ReadCard();
            if (resulRead.State != ResponseType.Success)
            {
                var resulReaet = readerCard.Reset();
            }
            resulRead = readerCard.ReadCard();
            var resulEject = readerCard.EjectCard();
            Assert.AreEqual(resulRead.State, ResponseType.Success);
        }

        [TestMethod]
        public void LedPowerTest()
        {
            CreatorReader readerCard = new CreatorReader();

            var resulLed = readerCard.LedOn(CreatorReader.ledColor.ledoff);

            resulLed = readerCard.LedOn(CreatorReader.ledColor.ledgreen);
            resulLed = readerCard.LedOn(CreatorReader.ledColor.ledred);
            resulLed = readerCard.LedOn(CreatorReader.ledColor.ledorange);

            Assert.AreEqual(resulLed.State, ResponseType.Success);
        }

        [TestMethod]
        public void ReaderCardBatchTest()
        {
            ReaderCard readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            Response resulRead = new Response();
            List<string> list = new List<string>();

            for (int i = 0; i < 20; i++)
            {
                DateTime initTime = DateTime.Now;
                int timseSecond = 0;

                resulRead = readerCard.ReadCard();
                if (resulRead.State != ResponseType.Success)
                {
                    throw new Exception(resulRead.Message);
                }

                timseSecond = DateTime.Now.Subtract(initTime).Seconds;
                resulRead.CodeBase = timseSecond.ToString();
                list.Add(i + " " + resulRead.State + " " + resulRead.Message + " " + resulRead.CodeBase);
                Task.WaitAll(Task.Delay(2000).ContinueWith((task) => { }));
                if (resulRead.State != ResponseType.Success)
                {
                    throw new Exception(resulRead.Message);
                }
            }

            var resulEject = readerCard.EjectCard();
            Assert.AreEqual(resulRead.State, ResponseType.Success);
        }


        [TestMethod]
        public void Latch()
        {
            CreatorReader readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            var resulRead = readerCard.Latch();
            Assert.AreEqual(resulRead.State, ResponseType.Success);
        }

        [TestMethod]
        public void ResetTest()
        {
            ReaderCard readerCard = new CreatorReader();
            var resulInit = readerCard.InitReader();
            var resulReset = readerCard.Reset();    
            Assert.AreEqual(resulReset.State, ResponseType.Success);
        }

        [TestMethod]
        public void VerErrorest()
        {
            byte[] a = new byte[] { (byte)'4', (byte)'3' };
            string hex = ByteArrayToHexString(a);
        }

        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }
    }
}
