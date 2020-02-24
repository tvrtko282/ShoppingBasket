using System;

namespace ShoppingBasket.Service.Models
{
    public class ShoppingBasketException : Exception
    {
        public ShoppingBasketException(string message): base(message)
        {
        }
    }
}
