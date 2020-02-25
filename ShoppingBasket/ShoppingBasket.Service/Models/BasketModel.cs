using System.Collections.Generic;

namespace ShoppingBasket.Service.Models
{
    public class BasketModel
    {
        public List<DiscountItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
    }
}
