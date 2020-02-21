using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IUserBasketRepository
    {
        void AddRange(List<UserBasketItem> items);
        void Delete(Func<UserBasketItem, bool> predicate);
        List<UserBasketItem> GetItems(Func<UserBasketItem, bool> predicate);
    }
}
