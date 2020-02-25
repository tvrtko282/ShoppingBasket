using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Service.Models
{
    public class DiscountItem : BasketItem
    {
        public double DiscountPrice { get; set; }
        public bool DiscountApplied { get; set; }

        public DiscountItem(BasketItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Price = item.Price;
            DiscountPrice = item.Price;
            DiscountApplied = false;
        }

        public void ApplyDiscount(List<DiscountItem> basketItems, Discount discount)
        {
            if (AppliesForDiscount(basketItems, discount))
            {
                DiscountPrice = Price - (Price * discount.DiscountAmount);
                DiscountApplied = true;
            }
        }

        private bool AppliesForDiscount(List<DiscountItem> basketItems, Discount discount)
        {
            var discountItems = basketItems.Where(b => b.Id == discount.DiscountItemId && b.DiscountApplied).ToList();
            var numberOfDiscounts = basketItems.Where(b => b.Id == discount.FirstItemId).Count() / discount.FirstItemQuantity;
            var itemsEligibleForDiscountExist = discountItems.Count < numberOfDiscounts;

            return itemsEligibleForDiscountExist && Id == discount.DiscountItemId;
        }
    }
}
