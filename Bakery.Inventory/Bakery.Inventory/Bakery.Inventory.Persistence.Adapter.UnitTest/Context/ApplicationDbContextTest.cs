using Bakery.Inventory.Persistence.Adapter.UnitTest.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using InventoryModel = Bakery.Inventory.DomainApi.Model.Inventory;

namespace Bakery.Inventory.Persistence.Adapter.UnitTest.Context
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertInventoryIntoDatabase()
        {
            using var context = ApplicationDbContextFactory.Create();
            var inventory = new InventoryModel();
            context.Inventories.Add(inventory);
            Assert.AreEqual(EntityState.Added, context.Entry(inventory).State);

            var result = context.SaveChangesAsync();
            Assert.AreEqual(1, result.Result);
            Assert.AreEqual(Task.CompletedTask.Status, result.Status);
            Assert.AreEqual(EntityState.Unchanged, context.Entry(inventory).State);

        }

        [Test]
        public void CanDeleteInventoryIntoDatabase()
        {
            using var context = ApplicationDbContextFactory.Create();
            var inventory = new InventoryModel();
            context.Inventories.Remove(inventory);
            Assert.AreEqual(EntityState.Deleted, context.Entry(inventory).State);
        }
    }
}
