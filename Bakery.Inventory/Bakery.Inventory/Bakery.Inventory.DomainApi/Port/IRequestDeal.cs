using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Inventory.DomainApi.Port
{
    public interface IRequestDeal<T>
    {
        List<T> GetDeals();
        T GetDeal(int id);
        T AddValue(T value);
        T DeleteValue(T value);
        T EditValue(T value);
    }
}
