using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.DomainApi.Port;
using Bakery.Inventory.RestAdapter.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bakery.Inventory.RestAdapter.UnitTest.Controllers
{
    public class DealControllerTest
    {
        private DealController _controller;
        private Mock<IRequestDeal<Bakery.Inventory.DomainApi.Model.Inventory>> _requestDealMock;

        [SetUp]
        public void Setup()
        {
            _requestDealMock = new Mock<IRequestDeal<Bakery.Inventory.DomainApi.Model.Inventory>>();
            _controller = new DealController(_requestDealMock.Object);
        }

        [Test]
        public void GetAllDealTestOkResult()
        {
            _requestDealMock.Setup(mock => mock.GetDeals())
            .Returns(InitInventorieList());

            var response = _controller.Get();
            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var deals = result.Value as List<Bakery.Inventory.DomainApi.Model.Inventory>;
            Assert.IsNotNull(deals);
            Assert.AreEqual(2, deals.Count);
            Assert.AreEqual(1, deals[0].Id);
            Assert.AreEqual(1, deals[0].Quantity);
            Assert.AreEqual("Almacen 1", deals[0].Location);
        }

        [Test]
        public void GetAllDealByIdTestOkResult()
        {
            _requestDealMock.Setup(mock => mock.GetDeal(It.IsAny<int>()))
            .Returns(GetInventory());

            var response = _controller.Get(1);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var deal = result.Value as Bakery.Inventory.DomainApi.Model.Inventory;
            Assert.IsNotNull(deal);
            Assert.AreEqual(1, deal.Id);
            Assert.AreEqual(1, deal.Quantity);
            Assert.AreEqual("Almacen 1", deal.Location);
        }

        [Test]
        public void AddDealTestOkResult()
        {
            var inventory = GetInventory();
            _requestDealMock.Setup(mock => mock.AddValue(It.IsAny<Bakery.Inventory.DomainApi.Model.Inventory>()))
            .Returns(GetInventory());

            var response = _controller.AddInventory(inventory);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var deal = result.Value as Bakery.Inventory.DomainApi.Model.Inventory;
            Assert.IsNotNull(deal);
            Assert.AreEqual(inventory.Id, deal.Id);
            Assert.AreEqual(inventory.ProductId, deal.ProductId);
            Assert.AreEqual(inventory.Location, deal.Location);
        }

        [Test]
        public void DeleteDealTestNoValue()
        {
            var inventory = GetInventory();
            _requestDealMock.Setup(mock => mock.DeleteValue(It.IsAny<Bakery.Inventory.DomainApi.Model.Inventory>()))
            .Returns((Bakery.Inventory.DomainApi.Model.Inventory)null);

            var response = _controller.DeleteInventory(inventory);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void EditDealTestNoValue()
        {
            var inventory = GetInventory();
            _requestDealMock.Setup(mock => mock.EditValue(It.IsAny<Bakery.Inventory.DomainApi.Model.Inventory>()))
            .Returns((Bakery.Inventory.DomainApi.Model.Inventory)null);

            var response = _controller.UpdateInventory(inventory);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        private List<Bakery.Inventory.DomainApi.Model.Inventory> InitInventorieList()
        {
            var inventories = new List<Bakery.Inventory.DomainApi.Model.Inventory>
            {
                new Bakery.Inventory.DomainApi.Model.Inventory
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 1,
                    Invoice = "",
                    CreatedDate = DateTime.Now,
                    Location = "Almacen 1"
                },
                new Bakery.Inventory.DomainApi.Model.Inventory
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 2,
                    Invoice = "",
                    CreatedDate = DateTime.Now,
                    Location = "Almacen 2"
                }
            };

            return inventories;
        }

        private Bakery.Inventory.DomainApi.Model.Inventory GetInventory()
        {
            return
                new Bakery.Inventory.DomainApi.Model.Inventory
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 1,
                    Invoice = "",
                    CreatedDate = DateTime.Now,
                    Location = "Almacen 1"
                };
        }
    }
}
