using System;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        private static string token { get; } = BotToken.token;
        private static TelegramBotClient client;
        private static ExchangeRates ex;
        private static Activity activity;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving();
            client.OnMessage += OnMessageHandler;

            hideWindow();
            //Console.ReadLine();            
            
            client.StopReceiving();
        }

        private static void hideWindow()
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            Console.Read();
        }
        private static async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var msg = e.Message;
                ex = new ExchangeRates();
                string pattern = @"^[0-9 ]+$";
                Regex rg = new Regex(pattern);
                Console.WriteLine($"Сообщение из бота: {msg.Text}");

                //todo прописать все исключения на картинки, видео, аудио и тд
                if (msg.Text != null && rg.IsMatch(msg.Text))
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        Convert.ToString(ex.PrintSend(msg.Text)),
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Photo",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Sticker)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Sticker",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Voice)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Voice",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Video)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Video",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.VideoNote)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "VideoNote",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Location)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Location",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Contact)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Contact",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Poll)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Poll",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.MessagePinned)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "MessagePinned",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Location)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Location",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.ChatMemberLeft)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Нас покинули",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Venue)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Venue",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Document)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Document",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Dice)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Dice",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.Unknown)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Unknown",
                        replyToMessageId: msg.MessageId);
                }
                else if (msg.Type == Telegram.Bot.Types.Enums.MessageType.ChatMembersAdded)
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Привет");
                    await client.SendAudioAsync(msg.Chat.Id,
                        "https://github.com/Chudovad/TelegramBot/raw/master/TelegramBot/files/audio_hello.mp3");
                    // работает https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3
                }
                else if (msg.Text[0] == '/')
                {
                    Command(msg.Text, msg.Chat.Id);
                }
                else
                {
                    await client.SendTextMessageAsync(msg.Chat.Id,
                        "Введи целое число",
                        replyToMessageId: msg.MessageId);
                }
            }
            catch 
            {
                Console.WriteLine("Ошибка");
            }
            
        }
        private static async void Command(String msg, long id)
        {
            string messege = "";
            if (msg == "/start")
            {
                messege = "Здаров! Введи БО целым числом и я тебе посчитаю в рубли.";
                await client.SendTextMessageAsync(id, messege);
            } 
            else if (msg == "/help" || msg == "/help@B0ringBot")
            {
                messege = "Введи БО целым числом и я тебе посчитаю сколько ты заработал, сколько налогов платить";
                await client.SendTextMessageAsync(id, messege);
            }
            else if (msg == "/random" || msg == "/random@B0ringBot")
            {
                activity = new Activity();
                List<string> listActivity = activity.PrintActivity();
                messege = "Занятие: " + listActivity[0] + "\r\nТип: "
                    + listActivity[1] + "\r\nКоличество людей: " + listActivity[2];
                await client.SendTextMessageAsync(id, messege);
            }
            else if (msg == "/1000minus7" || msg == "/1000minus7@B0ringBot")
            {
                int result = 1000;
                for (int i = 0; result > 7; i++)
                {
                    result = result - 7;
                    messege = result + 7 + " - 7 = " + result;
                    await client.SendTextMessageAsync(id, messege);
                }
            }
            else if (msg == "/1000minus7onemsg" || msg == "/1000minus7onemsg@B0ringBot")
            {
                int result = 1000;
                for (int i = 0; result > 7; i++)
                {
                    result = result - 7;
                    messege += result + 7 + " - 7 = " + result + "\r\n";
                }
                await client.SendTextMessageAsync(id, messege);
            }
            else
            {
                messege = "Команда не найдена";
                await client.SendTextMessageAsync(id, messege);
            }
        }

    }
}
