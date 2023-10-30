using Foundation.Stone.Application.Wrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuntimeCardReader.Core.Base;
using RuntimeCardReader.Core.Implement.GenPlus;

namespace RuntimeCardReader.Test
{
    [TestClass]
    public class GemplusTest
    {
      
        [TestMethod]
        public void ReaderCardTest()
        {
            ReaderCard readerCard = new GenPlusReader();
            
            var resulRead = readerCard.ReadCard();
           
            Assert.AreEqual(resulRead.State, ResponseType.Success);
        }
       
    }
}
