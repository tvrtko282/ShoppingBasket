namespace ShoppingBasket.Service.Models
{
    public class Discount
    {
        public int FirstItemId { get; set; }
        public int FirstItemQuantity { get; set; }
        public int DiscountItemId { get; set; }
        public int DiscountItemQuantity { get; set; }
        public double DiscountAmount { get; set; }

        public Discount(int firstItemId, int firstItemQuantity, int discountItemId, int discountItemQuantity, double discountAmount)
        {
            FirstItemId = firstItemId;
            FirstItemQuantity = firstItemQuantity;
            DiscountItemId = discountItemId;
            DiscountItemQuantity = discountItemQuantity;
            DiscountAmount = discountAmount;
        }
    }
}
