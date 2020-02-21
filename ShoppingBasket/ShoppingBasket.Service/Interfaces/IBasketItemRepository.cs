using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IBasketItemRepository
    {
        List<BasketItem> Get(Func<BasketItem, bool> predicate);
    }
}
