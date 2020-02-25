namespace ShoppingBasket.Service.Interfaces
{
    public interface IUserBasketItem
    {
        int ItemId { get; set; }
        int UserId { get; set; }
    }
}
