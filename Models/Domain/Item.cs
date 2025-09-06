namespace Backend.API.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int PricePerUnit { get; set; }
    }
}
