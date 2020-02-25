using ShoppingBasket.Service.Models;
using System.Collections.Generic;

namespace ShoppingBasket.Service.Interfaces
{
    public interface IDiscountRepository
    {
        List<Discount> Get();
    }
}
