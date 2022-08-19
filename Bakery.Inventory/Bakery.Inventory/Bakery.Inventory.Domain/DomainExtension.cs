using Bakery.Inventory.DomainApi.Port;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Bakery.Inventory.Domain
{
    public static class DomainExtension
    {
        [ExcludeFromCodeCoverage]
        public static void AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IRequestInventory<>), typeof(InventoryDomain<>));
        }
    }
}
