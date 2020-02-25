namespace ShoppingBasket.Service.Models
{
    public class AddItemModel
    {
        public int ItemId { get; set; }
        public uint Quantity { get; set; }

        public AddItemModel(int itemId, uint quantity)
        {
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
