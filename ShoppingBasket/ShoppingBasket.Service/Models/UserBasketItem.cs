using ShoppingBasket.Service.Interfaces;

namespace ShoppingBasket.Service.Models
{
    public class UserBasketItem : IUserBasketItem
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
    }
}
