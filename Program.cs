
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using V_secretary_bot.Models;

namespace V_secretary_bot
{

    class Program
    {
        public static string nasaResult = "";
        static ITelegramBotClient bot = new TelegramBotClient(AppSettings.Key);
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            Console.WriteLine("---------------");
            Console.WriteLine(update.Type.CompareTo(Telegram.Bot.Types.Enums.UpdateType.CallbackQuery));

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text != null)
                {
                    var responceToChat = ResponceToChat(message.Text);
                    await botClient.SendTextMessageAsync(message.Chat, responceToChat);
                }
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var callback = update.CallbackQuery;
                Console.WriteLine("*---------------*");
                Console.WriteLine(callback.Data);
                if (callback != null)
                {
                    Console.WriteLine("*---------------*");
                    Console.WriteLine(callback.Message.Text);

                    var responceToChat = ResponceToChat(callback.Data);

                    await botClient.SendTextMessageAsync(191247026, responceToChat);

                }
            }
            
        }




        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }



        private static readonly HttpClient client = new HttpClient();

        

        static async Task Main(string[] args)
        {

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
            var NasaGeter = new NasaGeter();
            var nasas = await NasaGeter.ProcessNasa();
            nasaResult = nasas.Title.ToString() + " " + nasas.url.ToString();
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
            );
            //bot.SendTextMessageAsync(191247026, "jhdfjhdjhdjhdjhdjh");
            var text = "Доступные комманды";

            var ikm = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("start", "/start"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Nasa", "/nasa"),
                },
                
            });
            //bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
            //{
            //    var message = ev.CallbackQuery.Message;
            //    if (ev.CallbackQuery.Data == "callback1")
            //    {
            //        // сюда то что тебе нужно сделать при нажатии на первую кнопку 
            //    }
            //    else
            //    if (ev.CallbackQuery.Data == "callback2")
            //    {
            //        // сюда то что нужно сделать при нажатии на вторую кнопку
            //    }
            //};
            
            await bot.SendTextMessageAsync(191247026, text, replyMarkup: ikm);


            Console.ReadLine();
        }

        private static ValueTask Bot_OnMakingApiRequest(ITelegramBotClient botClient, Telegram.Bot.Args.ApiRequestEventArgs args, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        static string ResponceToChat(string message)
        {
            string responceToChat = "";
            if (message.ToLower() == "/start")
            {
                responceToChat = "Добро пожаловать на борт, добрый путник!";

            }
            else if
            (message.ToLower().StartsWith("/nasa"))
            {

                responceToChat = nasaResult;
            }
            else if (message.ToLower().StartsWith("/zadacha"))
            {
                responceToChat = message;
            }
            else
            {
                responceToChat = "Привет-привет!!";
            }

            return responceToChat;

        }
    }
}