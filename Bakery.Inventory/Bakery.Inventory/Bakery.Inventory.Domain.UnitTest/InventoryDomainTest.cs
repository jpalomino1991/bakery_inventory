using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.Persistence.Adapter.UnitTest.Common;
using NUnit.Framework;
using System.Linq;

namespace Bakery.Inventory.Domain.UnitTest
{
    public class InventoryDomainTest
    {
        private InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory> _dealDomain;

        [Test]
        public void GetDealsTest()
        {
            using var context = ApplicationDbContextFactory.Create();
            _dealDomain = new InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory>(context);
            var deals = _dealDomain.GetValues().ToList();
            Assert.AreEqual(3, deals.Count);
            Assert.AreEqual(1, deals[0].Id);
            Assert.AreEqual(2, deals[0].Quantity);
            Assert.AreEqual("Almacen 1", deals[0].Location);

        }

        [Test]
        public void GetDealByIdTest()
        {
            using var context = ApplicationDbContextFactory.Create();
            _dealDomain = new InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory>(context);
            var deals = _dealDomain.GetValue(1);
            Assert.AreEqual(1, deals.Id);
            Assert.AreEqual(2, deals.Quantity);
            Assert.AreEqual("Almacen 1", deals.Location);

        }

        [Test]
        public void AddDealTest()
        {
            using var context = ApplicationDbContextFactory.Create();
            _dealDomain = new InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory>(context);

            var inventory = ApplicationDbContextFactory.dummyInventory();

            var deal = _dealDomain.AddValue(inventory);
            Assert.AreEqual(4, deal.Id);
            Assert.AreEqual(4, deal.ProductId);
            Assert.AreEqual(1, deal.Quantity);
            Assert.AreEqual("Almacen 1", deal.Location);

        }

        [Test]
        public void EditDealTest()
        {
            using var context = ApplicationDbContextFactory.Create();
            _dealDomain = new InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory>(context);

            var inventory = _dealDomain.GetValue(2);
            inventory.Location = "Almacen 2";
            inventory.Invoice = "Invoice 1";
            inventory.Quantity = 5;

            var deal = _dealDomain.EditValue(inventory);
            Assert.AreEqual(inventory.Id, deal.Id);
            Assert.AreEqual(inventory.Quantity, deal.Quantity);
            Assert.AreEqual(inventory.Invoice, deal.Invoice);
            Assert.AreEqual(inventory.Location, deal.Location);
        }

        [Test]
        public void DeleteDealTest()
        {
            using var context = ApplicationDbContextFactory.Create();
            _dealDomain = new InventoryDomain<Bakery.Inventory.DomainApi.Model.Inventory>(context);

            var inventory = _dealDomain.GetValue(2);
            inventory.Quantity = 2;

            var deal = _dealDomain.DeleteValue(inventory);
            Assert.AreEqual(inventory.Id, deal.Id);
            Assert.AreEqual(0, deal.Quantity);
            Assert.AreEqual(inventory.Invoice, deal.Invoice);
            Assert.AreEqual(inventory.Location, deal.Location);

        }
    }
}