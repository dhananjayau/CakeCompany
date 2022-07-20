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
    public class TransportProviderTest
    {
        [TestMethod()]
        public void TransportProvider_LessThan1000Quantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var product = new Product
                {
                    Cake = Cake.RedVelvet,
                    Id = new Guid(),
                    Quantity = 120.25
                };
                var products = new List<Product>() { product };

                var function = mock.Create<TransportProvider>();

                var result = function.CheckForAvailability(products);
                Assert.IsNotNull(result);
                Assert.AreEqual(result, "Van");
            }
        }

        [TestMethod()]
        public void TransportProvider_MoreThan1000andlestthan5000Quantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var product = new Product
                {
                    Cake = Cake.RedVelvet,
                    Id = new Guid(),
                    Quantity = 1120.25
                };
                var products = new List<Product>() { product };

                var function = mock.Create<TransportProvider>();

                var result = function.CheckForAvailability(products);
                Assert.IsNotNull(result);
                Assert.AreEqual(result, "Truck");
            }
        }

        [TestMethod()]
        public void TransportProvider_MoreThan5000Quantity()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var product = new Product
                {
                    Cake = Cake.RedVelvet,
                    Id = new Guid(),
                    Quantity = 5120.25
                };
                var products = new List<Product>() { product };

                var function = mock.Create<TransportProvider>();

                var result = function.CheckForAvailability(products);
                Assert.IsNotNull(result);
                Assert.AreEqual(result, "Ship");
            }
        }
    }
}
