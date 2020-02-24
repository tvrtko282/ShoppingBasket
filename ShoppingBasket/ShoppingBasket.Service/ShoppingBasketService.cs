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

        public ShoppingBasketService(IUserBasketRepository userBasketRepository, IBasketItemRepository basketItemRepository)
        {
            this.userBasketRepository = userBasketRepository;
            this.basketItemRepository = basketItemRepository;
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

            var userItems = new List<UserBasketItem>();

            for (var i = 0; i < item.Quantity; i++)
            {
                userItems.Add(new UserBasketItem()
                {
                    ItemId = item.ItemId,
                    UserId = userId
                });
            }

            var basketItems = userBasketRepository.AddRange(userItems);

            return basketItems.Count == item.Quantity;
        }

        private double GetTotalPrice(List<BasketItem> basketItems) => basketItems.Sum(p => p.Price);
    }
}
