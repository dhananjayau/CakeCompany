using CakeCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.Service.Interface
{
    public interface IOrderService
    {
        public List<Product> process(Order[] orders);
        public IFactoryDeliver GetStreamService(string transport);
    }
}
