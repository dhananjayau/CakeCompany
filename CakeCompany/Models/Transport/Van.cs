using CakeCompany.Service.Interface;

namespace CakeCompany.Models.Transport;

public class Van: IFactoryDeliver
{
    public bool Deliver(List<Product> products)
    {
        return true;
    }
}