using System;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IBasketItemRepository
    {
        bool Exists(Func<IBasketItem, bool> predicate);
    }
}
