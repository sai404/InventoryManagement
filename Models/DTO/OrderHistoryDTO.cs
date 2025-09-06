using Backend.API.Models.Domain;

namespace Backend.API.Models.DTO
{
    public class OrderHistoryDTO
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public MineDetails MineDetails { get; set; }
        public bool isCompleted { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
