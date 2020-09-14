using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyshkoExcelTelegramBot
{
    public static class BotSettings
    {
        public static string Token { get; private set; } = "1267664213:AAFwlGJ4s57J0eRsmJELQL7JY38IyjC84n4"; //тут нужно вписать токен вашего бота
        public static string Name { get; private set; } = "MyshkoExcelBot"; //имя бота, не особо важно
        public static string TelegramUrl { get; private set; } = "MyshkoExcelBot"; //ссылка на бота, не особо важно
        public static string Excelurl { get; set; } = @"C:\Егор\работа\programming\progr\c#\заказы\MyshkoExcelTelegramBot\database.xlsx"; //тут нужно вписать путь к excel файлу. 
        //ВАЖНО: файл должен находится на том же устройстве, где и бот. Тоесть если бот на сервере, то и файл должен быть на сервере.
    }
}
