using Backend.API.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Repository.Interface
{
    public interface IItemsRepository
    {
        Task<Item> Post(Item item);
        Task<List<Item>> Get();
    }
}
