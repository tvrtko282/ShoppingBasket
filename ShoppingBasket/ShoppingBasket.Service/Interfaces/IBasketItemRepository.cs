using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IBasketItemRepository
    {
        bool Exists(Func<BasketItem, bool> predicate);
    }
}
