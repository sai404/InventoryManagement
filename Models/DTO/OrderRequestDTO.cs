using Backend.API.Models.Domain;

namespace Backend.API.Models.DTO
{
    public class OrderRequestDTO
    {
        public Guid Id { get; set; }
        public Item item { get; set; }
        public int itemPricePerUnit { get; set; }
        public long Quantity { get; set; }
    }
}
