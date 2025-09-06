using Backend.API.Models.DTO;

namespace Backend.API.Models.Domain
{
    public class Bot
    {
        public Boolean newOrder { get; set; }
        public OrderHistoryRequestDTO order{ get; set; }
    }
}
