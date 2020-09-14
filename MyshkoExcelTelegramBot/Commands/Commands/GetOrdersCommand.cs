using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyshkoExcelTelegramBot.Commands.Commands
{
    public class GetOrdersCommand : Command
    {
        public override string[] Names { get; set; } = new string[] { "getorders"};

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
                    if (cells[i, 18] != null && cells[i, 18].Value != null && cells[i, 18].Value.ToString() == message.From.Id.ToString())
                    {
                        var status = cells[i, 4].Value.ToString();
                        if (status.Contains("выгружен"))
                            continue;
                        #region data
                        var dataZagruz = "Информации нет";
                        var napravlenie = "Информации нет";
                        var auto = "Информации нет";
                        var docksofperevoz = "Информации нет";
                        var link = "Информации нет";

                        if (cells[i, 2].Value == null || cells[i, 2] == null)
                            dataZagruz = "Информации нет.";
                        else dataZagruz = cells[i, 2].Text;

                        if (cells[i, 3].Value == null || cells[i, 3] == null)
                            napravlenie = "Информации нет.";
                        else napravlenie = cells[i, 3].Value.ToString();

                        if (cells[i, 6].Value == null || cells[i, 6] == null)
                            auto = "Информации нет.";
                        else auto = cells[i, 6].Value.ToString();

                        if (cells[i, 11].Value == null || cells[i, 11] == null)
                            docksofperevoz = "Информации нет.";
                        else docksofperevoz = cells[i, 11].Value.ToString();

                        if (cells[i, 17].Value == null || cells[i, 17] == null)
                            link = "Информации нет.";
                        else link = cells[i, 17].Value.ToString();
                        #endregion

                        await client.SendTextMessageAsync
                        (
                            message.Chat.Id,
                            $"{cells[1, 2].Value.ToString()}: {dataZagruz}\n{cells[1, 3].Value.ToString()}: {napravlenie}\n{cells[1, 6].Value.ToString()}: {auto}\n{cells[1, 11].Value.ToString()}: {docksofperevoz}",
                            replyToMessageId: message.MessageId
                        );

                        finded = true;
                    }
                }
                if(finded == false)
                {
                    await client.SendTextMessageAsync(message.Chat.Id, $"Мы не нашли ваши заказы.", replyToMessageId: message.MessageId);
                }
                //Save your file
                excelPackage.Save();
            }
        }
    }
}
