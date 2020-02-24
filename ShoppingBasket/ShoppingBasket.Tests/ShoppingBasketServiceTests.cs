using Moq;
using NUnit.Framework;
using ShoppingBasket.Service;
using ShoppingBasket.Service.Interfaces;
using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ShoppingBasketServiceTests
    {
        private readonly Mock<IUserBasketRepository> userBasketRepo = new Mock<IUserBasketRepository>();
        private readonly Mock<IBasketItemRepository> basketItemRepo = new Mock<IBasketItemRepository>();

        private const int USER_ID = 1;

        [Test]
        public void AddItems()
        {
            userBasketRepo.Setup(x => x.AddRange(It.IsAny<List<UserBasketItem>>()))
                .Returns(() =>
                {
                    return new List<BasketItem>
                    {
                        new BasketItem
                        {
                            Id = 1,
                            Name = "Butter",
                            Price = 0.8
                        }
                    };
                });

            basketItemRepo.Setup(x => x.Exists(It.IsAny<Func<BasketItem, bool>>()))
                .Returns(true);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object);

            var newItem = new AddItemModel(1, 1);

            var data = service.Add(newItem, USER_ID);

            Assert.IsTrue(data);
        }

        [Test]
        public void AddItemsThrowsArgumentNullException()
        {
            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object);

            Assert.Throws<ArgumentNullException>(delegate { service.Add(null, USER_ID); });
        }

        [Test]
        public void AddItemsThrowsShoppingBasketException()
        {
            basketItemRepo.Setup(b => b.Exists(It.IsAny<Func<BasketItem, bool>>()))
                .Returns(false);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object);

            var newItem = new AddItemModel(1, 1);

            Assert.Throws<ShoppingBasketException>(delegate { service.Add(newItem, USER_ID); });
        }

        [Test]
        public void DeleteThrowsShoppingBasketException()
        {
            userBasketRepo.Setup(b => b.GetItem(It.IsAny<Func<UserBasketItem, bool>>()))
                .Returns((UserBasketItem)null);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object);

            Assert.Throws<ShoppingBasketException>(delegate { service.Delete(1, USER_ID); });
        }
    }
}
