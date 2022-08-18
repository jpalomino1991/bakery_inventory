using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.DomainApi.Port;
using Bakery.Inventory.RestAdapter.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Bakery.Inventory.RestAdapter.UnitTest.Controllers
{
    public class DealControllerTest
    {
        private DealController _controller;
        private Mock<IRequestDeal<Deal>> _requestDealMock;

        [SetUp]
        public void Setup()
        {
            _requestDealMock = new Mock<IRequestDeal<Deal>>();
            _controller = new DealController(_requestDealMock.Object);
        }

        [Test]
        public void GetAllDealTestOkResult()
        {
            var response = _controller.Get();
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void GetAllDealByIdTestOkResult()
        {
            var response = _controller.Get(1);
            Assert.IsInstanceOf<OkObjectResult>(response);
        }
    }
}
