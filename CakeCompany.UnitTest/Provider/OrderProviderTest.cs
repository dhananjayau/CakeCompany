using Autofac.Extras.Moq;
using CakeCompany.Models;
using CakeCompany.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.UnitTest.Provider
{
    [TestClass()]
    public class OrderProviderTest
    {
        [TestMethod()]
        public void OrderProvider_GetLatestOrders()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                };

                var function = mock.Create<OrderProvider>();

                var result = function.GetLatestOrders();
                Assert.IsNotNull(result);
                Assert.AreEqual(result[0].Name, orders[0].Name);
                Assert.AreEqual(result[0].ClientName, orders[0].ClientName);
                Assert.AreEqual(result[0].Quantity, orders[0].Quantity);
                Assert.AreEqual(result[0].Id, orders[0].Id);
            }

        }
    }
}
