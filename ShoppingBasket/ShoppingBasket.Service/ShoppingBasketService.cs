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

        public AddItemsReturnModel Add(List<AddItemModel> items, int userId)
        {
            var itemsIds = items.Select(p => p.ItemId);
            var basketItems = basketItemRepository.Get(b => itemsIds.Contains(b.Id));

            if (basketItems.Count != items.Count)
            {
                // replace with custom exception
                throw new Exception("Unexpected items in the shopping basket!");
            }

            var userBasketItems = AddItems(items, basketItems, userId);

            var totalPrice = GetTotalPrice(userBasketItems);

            var discountPrice = GetDiscountPrice(userBasketItems);

            return new AddItemsReturnModel
            {
                TotalPrice = totalPrice,
                TotalPriceWithDiscount = discountPrice
            };
        }

        private List<BasketItemReturnModel> AddItems(List<AddItemModel> items, List<BasketItem> basketItems, int userId)
        {
            var userItems = new List<UserBasketItem>();

            foreach (var item in items)
            {
                for (var i = 0; i < item.Quantity; i++)
                {
                    userItems.Add(new UserBasketItem()
                    {
                        ItemId = item.ItemId,
                        UserId = userId
                    });
                }
            }

            userBasketRepository.AddRange(userItems);

            // should be returned by the repo?
            var fullItem = userItems.Join(basketItems, b => b.ItemId, i => i.Id, (b, i) => new BasketItemReturnModel
            {
                Id = b.ItemId,
                Price = i.Price
            }).ToList();

            return fullItem;
        }

        private double GetTotalPrice(List<BasketItemReturnModel> basketItems) => basketItems.Sum(p => p.Price);

        private double GetDiscountPrice(List<BasketItemReturnModel> basketItems)
        {
            return 0;
        }

        private List<UserBasketItem> GetUserItems(int userId)
        {
            return userBasketRepository.GetItems(i => i.UserId == userId);
        }
    }
}
