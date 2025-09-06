namespace Backend.API.Models.DTO
{
    public class ItemDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int PricePerUnit { get; set; }
    }
}
