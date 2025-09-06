using Backend.API.Repository.Implementation;
using Backend.API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBot bot;

        public BotController(ITelegramBot _bot)
        {
            this.bot = _bot;
        }

        [HttpGet]
        public async Task<IActionResult> botController(string message)
        {
            var result = await bot.SendMessage(message);
            return Ok(result);
        }
    }
}
