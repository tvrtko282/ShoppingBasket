using Moq;
using NUnit.Framework;
using ShoppingBasket.Service;
using ShoppingBasket.Service.Interfaces;
using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class AddItemsTests
    {
        [Test]
        public void AddItems_TotalPrice()
        {
            var userId = 1;

            var userBasketRepo = MockUserBasketRepo(userId);

            var basketItemRepo = MockBasketRepo();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object);

            var items = new List<AddItemModel>
            {
                new AddItemModel(1, 2)
            };

            var data = service.Add(items, 1);

            Assert.IsTrue(data.TotalPrice == 1.6);
        }

        private static Mock<IUserBasketRepository> MockUserBasketRepo(int userId)
        {
            var userBasketRepo = new Mock<IUserBasketRepository>();
            userBasketRepo.Setup(x => x.GetItems(It.IsAny<Func<UserBasketItem, bool>>())).Returns(() =>
            {
                return new List<UserBasketItem>
                {
                    new UserBasketItem
                    {
                        ItemId = 1,
                        UserId = userId,
                    },
                    new UserBasketItem
                    {
                        ItemId = 1,
                        UserId = userId,
                    }
                };
            });
            return userBasketRepo;
        }

        private static Mock<IBasketItemRepository> MockBasketRepo()
        {
            var basketItemRepo = new Mock<IBasketItemRepository>();
            basketItemRepo.Setup(x => x.Get(It.IsAny<Func<BasketItem, bool>>())).Returns(() =>
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

            return basketItemRepo;
        }
    }
}
