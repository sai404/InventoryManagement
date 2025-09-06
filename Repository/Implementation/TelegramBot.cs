using Azure;
using Backend.API.Models.Domain;
using Backend.API.Repository.Interface;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;

namespace Backend.API.Repository.Implementation
{
    public class TelegramBot : ITelegramBot
    {
        private readonly TelegramBotClient bot;
        private readonly string token;
        private readonly string clientIds;
        public TelegramBot(IConfiguration config)
        {
            this.token = config["TelegramBot:Token"];
            bot = new TelegramBotClient(token);

            this.clientIds = config["TelegramBot:chatId"];
        }
        public async Task<Message> SendMessage(string message)
        {

            var res=await bot.SendMessage(
                chatId: clientIds,
                text: message
                );
            return res;
        }
    }
}
