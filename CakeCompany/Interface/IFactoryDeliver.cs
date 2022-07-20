using CakeCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.Service.Interface
{
    public interface IFactoryDeliver
    {
        public bool Deliver(List<Product> products);
    }
}
