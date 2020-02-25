using ShoppingBasket.Service.Interfaces;
using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Service
{
    public class ShoppingBasketService
    {
        private readonly IUserBasketRepository userBasketRepository;
        private readonly IBasketItemRepository basketItemRepository;
        private readonly IDiscountRepository discountRepository;

        public ShoppingBasketService(IUserBasketRepository userBasketRepository, 
            IBasketItemRepository basketItemRepository,
            IDiscountRepository discountRepository)
        {
            this.userBasketRepository = userBasketRepository;
            this.basketItemRepository = basketItemRepository;
            this.discountRepository = discountRepository;
        }

        public bool Add(AddItemModel item, int userId)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var itemExists = basketItemRepository.Exists(b => item.ItemId == b.Id);

            if (!itemExists)
            {
                throw new ShoppingBasketException("Unexpected item in the shopping basket!");
            }

            var userItems = new List<IUserBasketItem>();

            for (var i = 0; i < item.Quantity; i++)
            {
                userItems.Add(new UserBasketItem()
                {
                    ItemId = item.ItemId,
                    UserId = userId
                });
            }

            var basketItems = userBasketRepository.AddRange(userItems);

            return basketItems?.Count() == item.Quantity;
        }

        public void Delete(int userId, int itemId)
        {
            var userBasketItem = userBasketRepository.GetItem(b => b.UserId == userId && b.ItemId == itemId);

            if (userBasketItem == null)
            {
                throw new ShoppingBasketException("Item not found in the basket!");
            }

            userBasketRepository.Delete(userBasketItem);
        }

        public BasketModel GetUserBasket(int userId)
        {
            var items = userBasketRepository.Get(u => u.UserId == userId) ?? new List<IBasketItem>();

            var discounts = discountRepository.Get() ?? new List<IDiscount>();

            var discountItems = items.Select(item => new DiscountItem(item)).ToList();

            foreach (var item in discountItems)
            {
                foreach (var discount in discounts)
                {
                    item.ApplyDiscount(discountItems, discount);
                }
            }

            var totalPrice = GetTotalPrice(discountItems);
            var discountPrice = GetTotalDiscountPrice(discountItems);

            return new BasketModel()
            {
                Items = discountItems,
                TotalPrice = totalPrice,
                DiscountPrice = discountPrice
            };
        }

        private double GetTotalPrice(List<DiscountItem> basketItems) => Math.Round(basketItems.Sum(p => p.Price), 2);

        private double GetTotalDiscountPrice(List<DiscountItem> basketItems) => Math.Round(basketItems.Sum(p => p.DiscountPrice), 2);
    }
}
