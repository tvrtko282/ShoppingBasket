namespace ShoppingBasket.Service.Interfaces
{
    public interface IBasketItem
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}
