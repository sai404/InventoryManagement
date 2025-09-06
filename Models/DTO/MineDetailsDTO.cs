using Backend.API.Models.Domain;

namespace Backend.API.Models.DTO
{
    public class MineDetailsDTO
    {
        public Guid Id { get; set; }
        public required string MineName { get; set; }
        public required string ContactName { get; set; }
        public List<MineItemRate>? MineItems { get; set; }
        public long ContactNumber { get; set; }
    }
}
