using NUnit.Framework;
using InventoryModel = Bakery.Inventory.DomainApi.Model.Inventory;

namespace Bakery.Inventory.DomainApi.UnitTest.Model
{
    public class InventoryTest
    {
        private readonly InventoryModel _inventory;
        private const int ProductId = 1;
        private const int Quantity = 150;
        private const string User = "Test User";

        public InventoryTest()
        {
            _inventory = new InventoryModel();
        }

        [Test]
        public void TestSetAndGetProductId()
        {
            _inventory.ProductId = ProductId;
            Assert.AreEqual(ProductId, _inventory.ProductId);
        }

        [Test]
        public void TestSetAndGetQuantityId()
        {
            _inventory.Quantity = Quantity;
            Assert.AreEqual(Quantity, _inventory.Quantity);
        }

        [Test]
        public void TestSetAndGetUser()
        {
            _inventory.User = User;
            Assert.AreEqual(User, _inventory.User);
        }
    }
}
