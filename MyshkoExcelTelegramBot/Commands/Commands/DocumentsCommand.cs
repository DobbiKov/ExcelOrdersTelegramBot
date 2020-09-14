using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyshkoExcelTelegramBot.Commands.Commands
{
    public class DocumentsCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "documents" };

        public override async void Execute(Message message, TelegramBotClient client)
        {
            FileInfo fi = new FileInfo(BotSettings.Excelurl);
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                //Get a WorkSheet by index. Note that EPPlus indexes are base 1, not base 0!
                ExcelWorksheet firstWorksheet = excelPackage.Workbook.Worksheets[1];
                var cells = firstWorksheet.Cells;
                var i = 0;
                bool finded = false;
                foreach (var j in cells)
                {
                    i++;
                    if (cells[i, 18] != null && cells[i, 18].Value != null && cells[i, 19].Value != null && cells[i, 19] != null && cells[i, 18].Value.ToString() == message.From.Id.ToString())
                    {
                        await client.SendTextMessageAsync
                        (
                            message.Chat.Id,
                            $"Ссылка на документы: {cells[i, 19].Value.ToString()}",
                            replyToMessageId: message.MessageId
                        );

                        finded = true;
                    }
                }
                if (finded == false)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"Мы не нашли ваши документы.", replyToMessageId: message.MessageId);
                }
                //Save your file
                excelPackage.Save();
            }
        }
    }
}
