using Backend.API.Models.Domain;
using Backend.API.Models.DTO;

namespace Backend.API.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<MineDetails> addMineAsync(MineDetails details);
        Task<List<MineDetailsDTO>> listMineAsync();
    }
}
