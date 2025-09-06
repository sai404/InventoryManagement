namespace Backend.API.Models.DTO
{
    public class ItemRequestDTO
    {
        public required string Name { get; set; }
        public int PricePerUnit { get; set; }
    }
}
