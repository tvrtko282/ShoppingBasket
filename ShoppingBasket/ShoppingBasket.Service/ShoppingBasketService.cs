using ShoppingBasket.Service.Interfaces;
using ShoppingBasket.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Service
{
    public class ShoppingBasketService
    {
        private readonly IShoppingBasketRepository repository;

        public ShoppingBasketService(IShoppingBasketRepository repository)
        {
            this.repository = repository;
        }

        public void Add(List<AddItemModel> items)
        {
            repository.Add(items);
        }

        public int GetTotalCount()
        {
            return repository.GetTotalCount();
        }
    }
}
