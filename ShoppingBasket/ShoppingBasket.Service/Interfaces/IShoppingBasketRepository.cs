using ShoppingBasket.Service.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IShoppingBasketRepository
    {
        void Add(List<AddItemModel> items);
        void Remove(int itemId);
        int GetTotalCount();
    }
}
