using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyshkoExcelTelegramBot.Commands;
using MyshkoExcelTelegramBot.Commands.Commands;
using Serilog;
using Serilog.Sinks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MyshkoExcelTelegramBot
{
    public class Program
    {
        private static TelegramBotClient client;
        private static List<Command> commands;
        public static void Main(string[] args)
        {
            client = new TelegramBotClient(BotSettings.Token) { Timeout = TimeSpan.FromSeconds(5) };

            commands = new List<Command>(); //список комманд
            commands.Add(new GetOrdersCommand());
            commands.Add(new GetUnloadOrdersCommand());
            commands.Add(new GetMyTelegramIdCommand());
            commands.Add(new DocumentsCommand());

            client.OnMessage += OnMessageReceived;
            client.StartReceiving();
            CreateHostBuilder(args).Build().Run();
        }

        private static void OnMessageReceived(object sender, MessageEventArgs e)
        {
            if(e.Message.Text != null)
            {
                var message = e.Message;
                foreach(var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(message, client);
                    }
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
