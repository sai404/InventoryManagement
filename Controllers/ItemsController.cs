using Backend.API.Data;
using Backend.API.Models.Domain;
using Backend.API.Models.DTO;
using Backend.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository operations;
        public ItemsController(IItemsRepository operations)
        {
            this.operations = operations;
        }
        [HttpPost]
        public async Task<IActionResult> post(ItemRequestDTO request)
        {
            var item = new Item
            {
                Name = request.Name,
                PricePerUnit = request.PricePerUnit
            };
            await operations.Post(item);
            var response = new ItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                PricePerUnit = item.PricePerUnit
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await operations.Get());
        }
    }
}
