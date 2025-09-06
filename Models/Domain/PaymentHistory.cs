namespace Backend.API.Models.Domain
{
    public class PaymentHistory
    {
        public Guid Id { get; set; }
        public Guid MineId { get; set; }
        public MineDetails Mine { get; set; }
        public DateOnly PaymentDate { get; set; }
        public long Amount { get; set; }
        public string PaymentMode { get; set; }

    }
}
