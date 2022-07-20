﻿using CakeCompany.Models;
using CakeCompany.Service.Interface;

namespace CakeCompany.Provider;

public class TransportProvider : ITransportProvider
{
    public string CheckForAvailability(List<Product> products)
    {
        if (products.Sum(p => p.Quantity) < 1000)
        {
            return "Van";
        }

        if (products.Sum(p => p.Quantity) > 1000 && products.Sum(p => p.Quantity) < 5000)
        {
            return "Truck";
        }

        return "Ship";
    }
}
