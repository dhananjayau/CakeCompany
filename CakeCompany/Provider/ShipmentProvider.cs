using System.Diagnostics;
using CakeCompany.Models;
using CakeCompany.Models.Transport;
using CakeCompany.Service.Interface;
using Microsoft.Extensions.Logging;

namespace CakeCompany.Provider;

public class ShipmentProvider : IShipmentProvider
{
    private readonly IOrderProvider _orderProvider;
    private readonly IOrderService _orderService;
    private readonly ITransportProvider _transportProvider;
    private readonly ILogger<ShipmentProvider> _logger;
    public ShipmentProvider(IOrderProvider orderProvider, 
                      IOrderService orderService,
                     ITransportProvider transportProvider,
                     ILogger<ShipmentProvider> logger)
    {
        _orderProvider = orderProvider;
        _orderService = orderService;
        _transportProvider = transportProvider;
        _logger = logger;
    }
    public bool GetShipment()
    {
        try
        {
            var orders = _orderProvider.GetLatestOrders();
            if (!orders.Any())
            {
                return false;
            }
            var products = _orderService.process(orders);
            var transport = _transportProvider.CheckForAvailability(products);
            var service = _orderService.GetStreamService(transport);
            return service.Deliver(products);
        }
        catch (Exception ex)
        {
            string message = $"Issue in shipment- {0}";
            _logger.LogError(message, ex);
            throw;
        }
    }
}
