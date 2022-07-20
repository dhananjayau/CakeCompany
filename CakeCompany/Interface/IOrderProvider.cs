using CakeCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeCompany.Service.Interface
{
    public interface IOrderProvider
    {
        public Order[] GetLatestOrders();
    }
}
