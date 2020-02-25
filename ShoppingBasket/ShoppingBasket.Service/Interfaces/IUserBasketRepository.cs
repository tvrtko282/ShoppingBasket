using System;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IUserBasketRepository
    {
        IEnumerable<IUserBasketItem> AddRange(List<IUserBasketItem> predicate);
        void Delete(IUserBasketItem userBasketItem);
        IUserBasketItem GetItem(Func<IUserBasketItem, bool> predicate);
        IEnumerable<IBasketItem> Get(Func<IUserBasketItem, bool> predicate);
    }
}
