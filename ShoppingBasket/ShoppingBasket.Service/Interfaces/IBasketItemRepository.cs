﻿using ShoppingBasket.Service.Models;
using System;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IBasketItemRepository
    {
        bool Exists(Func<BasketItem, bool> predicate);
    }
}
