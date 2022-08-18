using Bakery.Inventory.DomainApi.Model;
using System.Collections.Generic;

namespace Bakery.Inventory.DomainApi.Port
{
    public interface IObtainDeal<T>
    {
        List<Deal> GetDeals();
        Deal GetDeal(T id);
    }
}
