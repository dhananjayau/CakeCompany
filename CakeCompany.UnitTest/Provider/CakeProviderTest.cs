using Autofac.Extras.Moq;
using CakeCompany.Models;
using CakeCompany.Provider;
using CakeCompany.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.UnitTest.Provider
{
    [TestClass()]
    public class CakeProviderTest
    {
        [TestMethod()]
        public void CakeProvider_CheckRedVelvet()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("ImportantCakeShop", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                };

                var mockData = DateTime.Now.Add(TimeSpan.FromMinutes(60)).Minute;
                var function = mock.Create<CakeProvider>();

                var result = function.Check(orders[0]);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Minute, mockData);
            }
        }
        [TestMethod()]
        public void CakeProvider_CheckChocolate()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.Chocolate, 120.25)
                };

                var mockData = DateTime.Now.Add(TimeSpan.FromMinutes(30)).Minute;
                var function = mock.Create<CakeProvider>();

                var result = function.Check(orders[0]);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Minute, mockData);
            }
        }

        [TestMethod()]
        public void CakeProvider_Checkdefaulttime()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.Vanilla, 120.25)
                };

                var mockData = DateTime.Now.Add(TimeSpan.FromHours(15)).Hour;
                var function = mock.Create<CakeProvider>();

                var result = function.Check(orders[0]);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Hour, mockData);
            }
        }
    }
}
