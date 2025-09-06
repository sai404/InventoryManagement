using Backend.API.Models.Domain;
using Backend.API.Repository.Interface;
using Backend.API.Data;
using Microsoft.EntityFrameworkCore;
using Backend.API.Models.DTO;

namespace Backend.API.Repository.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext dbContext;
        public CustomerRepository(ApplicationDBContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<MineDetails> addMineAsync(MineDetails data)
        {
            await dbContext.MineDetails.AddAsync(data);
            await dbContext.SaveChangesAsync();
            return data;
        }
        public async Task<List<MineDetailsDTO>> listMineAsync()
        {
            var data=await dbContext.MineDetails.Select(m => new MineDetailsDTO
            {
                Id = (Guid)m.Id,
                MineName = m.mineName,
                ContactName = m.ContactName,
                ContactNumber = m.ContactNumber,
                MineItems=m.MineItems.Select(mi=>new MineItemRate
                {
                    Id=mi.Id,
                    ItemId=mi.ItemId,
                    Item=mi.Item,
                    PricePerUnit=mi.PricePerUnit
                }).ToList()
            }).ToListAsync();
            return data;
        }
    }
}
