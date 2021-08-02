using DataLayer.Models;
using DataLayer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APIControllerTesting
{
    [TestClass]
    public class ControllerMethodsTests
    {
        [TestMethod]    
        public void CreateAdTest(IAdRepository ar)
        {
            
            Ad TestAd = new Ad();
            TestAd.CategoryId = 1;
            TestAd.AdTypeId = 1;
            TestAd.Cost = 10;

            var resCreate = ar.create(TestAd);
            var resGet = ar.getById(resCreate.Id);
            Assert.AreEqual(resCreate.Id, resGet.Id);
            
        }
    }
}
