using Moq;
using NUnit.Framework;
using ShoppingBasket.Service;
using ShoppingBasket.Service.Interfaces;
using ShoppingBasket.Service.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ShoppingServiceTest
    {
        [Test]
        public void AddItems()
        {
            var repo = new Mock<IShoppingBasketRepository>();
            repo.Setup(x => x.GetTotalCount()).Returns(1);

            var service = new ShoppingBasketService(repo.Object);

            var items = new List<AddItemModel>
            {
                new AddItemModel(1, 1)
            };

            service.Add(items);

            var total = service.GetTotalCount();

            Assert.IsTrue(total == 1);
        }
    }
}
