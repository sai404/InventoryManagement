namespace Backend.API.Models.Domain
{
    public class MineDetails
    {
        public Guid? Id { get; set; }
        public required string mineName { get; set; }
        public string? ContactName{ get; set; }
        public long ContactNumber { get; set; }
        public List<MineItemRate>? MineItems { get; set; }
    }
}
