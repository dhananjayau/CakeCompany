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
    public class PaymentProviderTest
    {
        [TestMethod()]
        public void PaymentProvider_ProcessNonImportantClient()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                };
                var function = mock.Create<PaymentProvider>();

                var result = function.Process(orders[0]);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.HasCreditLimit, true);
                Assert.AreEqual(result.IsSuccessful, true);
            }
        }

        [TestMethod()]
        public void PaymentProvider_ProcessImportantClient()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("ImportantCakeShop", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                };
                var function = mock.Create<PaymentProvider>();

                var result = function.Process(orders[0]);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.HasCreditLimit, false);
                Assert.AreEqual(result.IsSuccessful, true);
            }
        }
    }
}
