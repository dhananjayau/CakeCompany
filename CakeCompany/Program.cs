// See https://aka.ms/new-console-template for more information
using CakeCompany.Models.Transport;
using CakeCompany.Provider;
using CakeCompany.Service;
using CakeCompany.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            services.AddScoped<IShipmentProvider, ShipmentProvider>();
            services.AddScoped<IOrderProvider, OrderProvider>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICakeProvider, CakeProvider>();
            services.AddScoped<IPaymentProvider, PaymentProvider>();
            services.AddScoped<ITransportProvider, TransportProvider>();
            services.AddScoped<Van>().AddScoped<IFactoryDeliver, Van>(s => s.GetService<Van>());
            services.AddScoped<Truck>().AddScoped<IFactoryDeliver, Truck>(s => s.GetService<Truck>());
            services.AddScoped<Ship>().AddScoped<IFactoryDeliver, Ship>(s => s.GetService<Ship>());
            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.ClearProviders();
                loggerBuilder.AddConsole();
            });
        });

    return hostBuilder;
}

var host = CreateHostBuilder(args).Build();
var shipment = host.Services.GetService<IShipmentProvider>().GetShipment();
var logger = host.Services.GetService<ILogger<Program>>();

if (shipment)
{
    Console.WriteLine("Shipment Done...");
    logger.LogInformation("Shipment Done...");
}
else
{
    Console.WriteLine("Shipment have issue!...");
    logger.LogInformation("Shipment have issue!...");
}