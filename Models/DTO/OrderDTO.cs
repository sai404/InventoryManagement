using Backend.API.Models.Domain;

namespace Backend.API.Models.DTO
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Item Item { get; set; }
        public int itemPricePerUnit { get; set; }
        public long Quantity { get; set; }
        public long OrderValue { get; set; }
    }
}
