using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IUserBasketRepository
    {
        List<BasketItem> AddRange(List<UserBasketItem> predicate);
        void Delete(UserBasketItem userBasketItem);
        UserBasketItem GetItem(Func<UserBasketItem, bool> predicate);
    }
}
