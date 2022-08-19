using Bakery.Inventory.DomainApi.Port;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Inventory.Domain
{
    public static class DomainExtension
    {
        public static void AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IRequestInventory<>), typeof(InventoryDomain<>));
        }
    }
}
