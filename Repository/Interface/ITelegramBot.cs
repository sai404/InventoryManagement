using Backend.API.Models.Domain;
using Telegram.Bot.Types;

namespace Backend.API.Repository.Interface
{
    public interface ITelegramBot
    {
        Task<Message> SendMessage(string message);
    }
}
