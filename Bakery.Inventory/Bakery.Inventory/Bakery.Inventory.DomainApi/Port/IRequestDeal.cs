using System.Collections.Generic;

namespace Bakery.Inventory.DomainApi.Port
{
    public interface IRequestDeal<T>
    {
        List<T> GetDeals();
        T GetDeal(int id);
    }
}
