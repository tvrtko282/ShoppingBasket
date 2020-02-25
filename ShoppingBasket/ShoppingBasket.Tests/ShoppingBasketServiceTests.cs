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
        private readonly Mock<IDiscountRepository> discountRepo = new Mock<IDiscountRepository>();

        private const int USER_ID = 1;

        [Test]
        public void AddItems()
        {
            userBasketRepo.Setup(x => x.AddRange(It.IsAny<List<IUserBasketItem>>()))
                .Returns(() =>
                {
                    return new List<IUserBasketItem>
                    {
                        new UserBasketItem
                        {
                            ItemId = 1,
                            UserId = USER_ID
                        }
                    };
                });

            basketItemRepo.Setup(x => x.Exists(It.IsAny<Func<IBasketItem, bool>>()))
                .Returns(true);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var newItem = new AddItemModel(1, 1);

            var hasAddedItem = service.Add(newItem, USER_ID);

            Assert.IsTrue(hasAddedItem);
        }

        [Test]
        public void AddItemsThrowsArgumentNullException()
        {
            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            Assert.Throws<ArgumentNullException>(delegate { service.Add(null, USER_ID); });
        }

        [Test]
        public void AddItemsThrowsShoppingBasketException()
        {
            basketItemRepo.Setup(b => b.Exists(It.IsAny<Func<IBasketItem, bool>>()))
                .Returns(false);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var newItem = new AddItemModel(1, 1);

            Assert.Throws<ShoppingBasketException>(delegate { service.Add(newItem, USER_ID); });
        }

        [Test]
        public void DeleteThrowsShoppingBasketException()
        {
            userBasketRepo.Setup(b => b.GetItem(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns((UserBasketItem)null);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            Assert.Throws<ShoppingBasketException>(delegate { service.Delete(1, USER_ID); });
        }

        [Test]
        public void GetBasket_1_bread_1_butter_1_milk()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() =>
                {
                    return new List<IBasketItem>
                    {
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 },
                    };
                });

            SetupDiscounts();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.DiscountPrice == 2.95);
            Assert.IsTrue(basket.TotalPrice == 2.95);
        }

        private void SetupDiscounts()
        {
            discountRepo.Setup(b => b.Get()).Returns(() =>
            {
                return new List<IDiscount>
                {
                    new Discount(1, 2, 3, 1, 0.5), // buy 2 butters and get 1 bread at 50% off
                    new Discount(2, 3, 2, 1, 1) // buy 3 milks and get the 4th at 100% off
                };
            });
        }

        [Test]
        public void GetBasket_2_butters_2_breads()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() =>
                {
                    return new List<IBasketItem>
                    {
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 }
                    };
                });

            SetupDiscounts();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.TotalPrice == 3.6);
            Assert.IsTrue(basket.DiscountPrice == 3.1);
        }

        [Test]
        public void GetBasket_2_butters_2_breads_NoDiscounts()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() =>
                {
                    return new List<IBasketItem>
                    {
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 }
                    };
                });

            discountRepo.Setup(b => b.Get()).Returns(() => null);

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.TotalPrice == 3.6);
            Assert.IsTrue(basket.DiscountPrice == 3.6);
        }

        [Test]
        public void GetBasket_4_milks()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() =>
                {
                    return new List<IBasketItem>
                    {
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 }
                    };
                });

            SetupDiscounts();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.TotalPrice == 4.6);
            Assert.IsTrue(basket.DiscountPrice == 3.45);
        }

        [Test]
        public void GetBasket_2_butter_1_bread_8_milk()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() =>
                {
                    return new List<IBasketItem>
                    {
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 1, Name = "Butter", Price = 0.8 },
                        new BasketItem(){ Id = 3, Name = "Bread", Price = 1.0 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 },
                        new BasketItem(){ Id = 2, Name = "Milk", Price = 1.15 }
                    };
                });

            SetupDiscounts();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.TotalPrice == 11.8);
            Assert.IsTrue(basket.DiscountPrice == 9.0);
        }

        [Test]
        public void GetBasket_NoItems()
        {
            userBasketRepo.Setup(b => b.Get(It.IsAny<Func<IUserBasketItem, bool>>()))
                .Returns(() => null);

            SetupDiscounts();

            var service = new ShoppingBasketService(userBasketRepo.Object, basketItemRepo.Object, discountRepo.Object);

            var basket = service.GetUserBasket(USER_ID);

            PrintBasketToConsole(basket);

            Assert.IsTrue(basket.TotalPrice == 0);
            Assert.IsTrue(basket.DiscountPrice == 0);
        }

        private void PrintBasketToConsole(BasketModel basket)
        {
            Console.WriteLine("Id\tName\tPrice\tDiscount Price");
            basket.Items.ForEach(i => Console.WriteLine($"{i.Id}\t{i.Name}\t${i.Price}\t${i.DiscountPrice}"));
            Console.WriteLine($"Total price: ${basket.TotalPrice}");
            Console.WriteLine($"Total discount price: ${basket.DiscountPrice}");
        }
    }
}
