namespace Backend.API.Models.Domain
{
    public class OrderHistory
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid MineId { get; set; }
        public MineDetails Mine { get; set; }
        public bool IsCompleted { get; set; }
        public List<Order> Orders { get; set; }

    }
}
