using System.Collections.Generic;

namespace Bakery.Inventory.DomainApi.Port
{
    public interface IRequestInventory<T>
    {
        T GetValue(int id);
        IEnumerable<T> GetValues();
        T AddValue(T value);
        T DeleteValue(T value);
        T EditValue(T value);
    }
}
