using System.Threading.Tasks;

namespace Bakery.Inventory.DomainApi.Port
{
    public interface IProcessQueue
    {
        Task InitializeAsync();
    }
}
