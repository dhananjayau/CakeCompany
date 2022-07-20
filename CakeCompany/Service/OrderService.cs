using CakeCompany.Models;
using CakeCompany.Models.Transport;
using CakeCompany.Service.Interface;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Service;

public class OrderService : IOrderService
{
    private readonly ICakeProvider _cakeProvider;
    private readonly IPaymentProvider _paymentProvider;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderService> _logger;
    public OrderService(ICakeProvider cakeProvider, IPaymentProvider paymentProvider, IServiceProvider serviceProvider, ILogger<OrderService> logger)
    {
        _cakeProvider = cakeProvider;
        _paymentProvider = paymentProvider;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public List<Product> process(Order[] orders)
    {
        var cancelledOrders = new List<Order>();
        var products = new List<Product>();

        foreach (var order in orders)
        {
            var estimatedBakeTime = _cakeProvider.Check(order);

            if (estimatedBakeTime > order.EstimatedDeliveryTime)
            {
                cancelledOrders.Add(order);
                continue;
            }

            if (!_paymentProvider.Process(order).IsSuccessful)
            {
                cancelledOrders.Add(order);
                continue;
            }

            var product = _cakeProvider.Bake(order);
            products.Add(product);
        }

        return products;
    }


    public IFactoryDeliver GetStreamService(string transport)
    {
        try
        {
            return transport switch
            {
                "Van" => (IFactoryDeliver)_serviceProvider.GetService(typeof(Van)),
                "Truck" => (IFactoryDeliver)_serviceProvider.GetService(typeof(Truck)),
                "Ship" => (IFactoryDeliver)_serviceProvider.GetService(typeof(Ship)),
                _ => (IFactoryDeliver)_serviceProvider.GetService(typeof(Van)),
            };

        }
        catch (Exception ex)
        {
            string message = $"Issue in getting the correct reference in Factory patter- {0}";
            _logger.LogError(message, ex);
            throw;
        }
    }
}