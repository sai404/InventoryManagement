namespace Backend.API.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
        public int itemPricePerUnit { get; set; }
        public long Quantity { get; set; }
        public long OrderValue { get; set; }
        public Guid OrderHistoryId { get; set; }
        public OrderHistory OrderHistory { get; set; }
    }
}
