using Backend.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<MineDetails> MineDetails { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<MineItemRate> MineItemRate { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
    }
}
