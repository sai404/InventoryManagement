using Backend.API.Data;
using Backend.API.Models.Domain;
using Backend.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Repository.Implementation
{
    public class ItemRepository : IItemsRepository
    {
        private readonly ApplicationDBContext dbContext;
        public ItemRepository(ApplicationDBContext dBContext)
        {
            this.dbContext = dBContext;            
        }

        public async Task<Item> Post(Item item)
        {
            await dbContext.Item.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }
        public async Task<List<Item>> Get()
        {
            return await dbContext.Item.ToListAsync();
        }
    }
}
