namespace Backend.API.Models.DTO
{
    public class PaymentHistoryRequestDTO
    {
        public Guid MineId { get; set; }
        public DateOnly PaymentDate { get; set; }
        public long Amount { get; set; }
        public string PaymentMode { get; set; }
    }
}
