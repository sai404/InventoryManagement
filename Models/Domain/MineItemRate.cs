namespace Backend.API.Models.Domain
{
    public class MineItemRate
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item{ get; set; }
        public int PricePerUnit { get; set; }
        public Guid MineDetailsId { get; set; }
    }
}
