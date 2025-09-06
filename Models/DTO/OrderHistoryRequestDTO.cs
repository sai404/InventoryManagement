using Backend.API.Models.Domain;

namespace Backend.API.Models.DTO
{
    public class OrderHistoryRequestDTO
    {
        public Guid? id { get; set; }
        public DateOnly Date { get; set; }
        public MineDetails mineDetails{ get; set; }
        public bool isCompleted { get; set; }
        public List<OrderRequestDTO> Orders { get; set; }
    }
}
