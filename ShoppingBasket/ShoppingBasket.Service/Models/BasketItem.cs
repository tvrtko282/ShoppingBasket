using ShoppingBasket.Service.Interfaces;

namespace ShoppingBasket.Service.Models
{
    public class BasketItem : IBasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
