using Autofac.Extras.Moq;
using CakeCompany.Models;
using CakeCompany.Models.Transport;
using CakeCompany.Service;
using CakeCompany.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.UnitTest.Service
{
    [TestClass()]
    public class OrderServiceTest
    {
        [TestMethod()]
        public void OrderService_Process()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var orders = new Order[]
                {
                    new("CakeBox", DateTime.Now.AddHours(2), 1, Cake.RedVelvet, 120.25)
                };

                var paymentIn =  new PaymentIn
                {
                    HasCreditLimit = true,
                    IsSuccessful = true
                };

                var product = new Product
                {
                    Cake = Cake.RedVelvet,
                    Id = new Guid(),
                    Quantity = 120.25
                };
                var products = new List<Product>() { product };
                var requestMock = mock.Mock<ICakeProvider>();
                requestMock.Setup(x => x.Check(orders[0])).Returns(DateTime.Now.Add(TimeSpan.FromMinutes(60)));

                var paymentMock = mock.Mock<IPaymentProvider>();
                paymentMock.Setup(x => x.Process(orders[0])).Returns(paymentIn);

                var bakeMock = requestMock.As<ICakeProvider>();
                bakeMock.Setup(x => x.Bake(orders[0])).Returns(product);

                var function = mock.Create<OrderService>();

                var result = function.process(orders);
                Assert.IsNotNull(result);
                Assert.AreEqual(result[0], products[0]);
            }
            
        }


        [TestMethod()]
        public void OrderService_GetStreamServiceVan()
        {
            using (var mock = AutoMock.GetLoose())
            {

                var mockData = "Van";
                var function = mock.Create<OrderService>();

                var paymentMock = mock.Mock<IServiceProvider>();
                paymentMock.Setup(x => x.GetService(typeof(Van))).Returns(new Van());

                var result = function.GetStreamService(mockData);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.GetType(), typeof(Van));
            }
        }

        [TestMethod()]
        public void OrderService_GetStreamServiceShip()
        {
            using (var mock = AutoMock.GetLoose())
            {

                var mockData = "Ship";
                var function = mock.Create<OrderService>();

                var paymentMock = mock.Mock<IServiceProvider>();
                paymentMock.Setup(x => x.GetService(typeof(Ship))).Returns(new Ship());

                var result = function.GetStreamService(mockData);
                Assert.IsNotNull(result);
                Assert.AreEqual(result.GetType(), typeof(Ship));
            }
        }

        [TestMethod()]
        public void OrderService_GetStreamServiceVanShipNullReference()
        {
            using (var mock = AutoMock.GetLoose())
            {

                var mockData = "Ship";
                var function = mock.Create<OrderService>();
                var result = function.GetStreamService(mockData);

                Assert.IsNull(result);
                Assert.AreEqual(result, null);
            }
        }

    }
}
