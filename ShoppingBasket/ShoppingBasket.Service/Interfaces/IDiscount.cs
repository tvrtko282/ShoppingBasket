namespace ShoppingBasket.Service.Interfaces
{
    public interface IDiscount
    {
        int FirstItemId { get; set; }
        int FirstItemQuantity { get; set; }
        int DiscountItemId { get; set; }
        int DiscountItemQuantity { get; set; }
        double DiscountAmount { get; set; }
    }
}
