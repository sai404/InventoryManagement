namespace Backend.API.Models.DTO
{
    public class AddMineRequestDTO
    {
        public Guid? Id { get; set; }
        public required string MineName { get; set; }
        public required string ContactName { get; set; }
        public long ContactNumber { get; set; }
    }
}
