namespace ShoppingBasket.Service.Models
{
    public class AddItemModel
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public AddItemModel(int itemId, int quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
