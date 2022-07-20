using System;
using System.Collections.Generic;
using CakeCompany.Provider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac.Extras.Moq;
using CakeCompany.Service.Interface;
using CakeCompany.Models;
using CakeCompany.Models.Transport;

namespace CakeCompany.UnitTest.Provider
{
    [TestClass()]
    public class ShipmentProviderTest
    {
        [TestMethod()]
        public void GetShipment_Done()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                };

                var product = new Product
                {
                    Cake = Cake.RedVelvet,
                    Id = new Guid(),
                    Quantity = 120.25
                };
                var products = new List<Product>() { product };
                var requestMock = mock.Mock<IOrderProvider>();
                requestMock.Setup(x => x.GetLatestOrders()).Returns(orders);

                var shipmentMock = mock.Mock<IOrderService>();
                shipmentMock.Setup(x => x.process(orders)).Returns(products);

                var transportMock = mock.Mock<ITransportProvider>();
                transportMock.Setup(x => x.CheckForAvailability(products)).Returns("Van");

                shipmentMock.Setup(x => x.GetStreamService("Van")).Returns(new Van());

                var function = mock.Create<ShipmentProvider>();
                
                var result = function.GetShipment();
                Assert.IsNotNull(result);
                Assert.AreEqual(result, true);
            }

        }

        [TestMethod()]
        public void GetShipment_exception()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                                {
                    new("CakeBox", DateTime.Now, 1, Cake.RedVelvet, 120.25)
                                };

                var requestMock = mock.Mock<IOrderProvider>();
                requestMock.Setup(x => x.GetLatestOrders()).Returns(orders);

                var function = mock.Create<ShipmentProvider>();
                Assert.ThrowsException<NullReferenceException>(() => function.GetShipment());
            }
        }
    }
}
