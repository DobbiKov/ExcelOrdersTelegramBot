using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyshkoExcelTelegramBot.Commands.Commands
{
    public class GetMyTelegramIdCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "getmyid", "мой телерам айди" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat.Id, $"Ваш telegram id: {message.From.Id}", replyToMessageId: message.MessageId);
        }
    }
}
