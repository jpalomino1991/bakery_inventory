using Bakery.Inventory.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Inventory.Persistence.Adapter
{
    public static class PersistenceExtensions
    {
        public static void AddPersistence(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=tcp:bakery0.database.windows.net,1433;Initial Catalog=InventoryDb;Persist Security Info=False;User ID=bakery;Password=dojonet02.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
        }
    }
}
