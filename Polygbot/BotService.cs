using Polygbot.Handlers;
using Polygbot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Polygbot
{
    public class BotService
    {
        private UserSettings _userSettings;

        private readonly TelegramBotClient _botClient;

        public BotService()
        {
            _botClient = new TelegramBotClient("7561146572:AAF5tBL8fQpMZMy_W-VTpAur25qoe2rh1bQ");
            _userSettings = new UserSettings();
        }

        public async Task StartAsync()
        {
            _botClient.StartReceiving(UpdateHandler, ErrorHandler);
            Console.WriteLine("Polygbot started...");
            Console.ReadLine();
        }

        private Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Error: {exception.Message}");
            return Task.CompletedTask;
        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message != null)
            {
                var message = update.Message;

                if (message.Text.StartsWith("/start"))
                {
                    await CommandHandler.HandlerStartCommand(botClient, message);
                }
                else if (message.Text.StartsWith("/settings"))
                {
                    await CommandHandler.HandlerSettingsCommand(botClient, message);
                }
                else if (message.Text.StartsWith("/ai"))
                {
                    await CommandHandler.HandlerAiCommand(botClient, message);
                }

                Console.WriteLine($"{message.Chat.Username ?? message.Chat.FirstName}({message.Chat.Id}) выбрал команду: \"{message.Text}\"");
            }

            // Обработка callback-данных
            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                var callbackQuery = update.CallbackQuery;
                var message = callbackQuery.Message;

                if (callbackQuery.Data.StartsWith("lang_"))
                {
                    await HandleLanguageSelection(botClient, callbackQuery);
                }
                else if (callbackQuery.Data.StartsWith("time_"))
                {
                    await HandleTimeSelection(botClient, callbackQuery);
                }
                else if (callbackQuery.Data.StartsWith("difficulty_"))
                {
                    await HandleDifficultySelection(botClient, callbackQuery);
                }
                //else if (callbackQuery.Data.StartsWith("settings_back"))
                //{

                //}
                if (callbackQuery.Data == "settings_done")
                {
                    await FinishSettings(botClient, callbackQuery);
                }

                Console.WriteLine($"{message.Chat.Username ?? message.Chat.FirstName}({message.Chat.Id}) выбрал {callbackQuery.Data}");
            }
        }

        /// <summary>
        /// Обработка выбора настроек
        /// </summary>
        private async Task HandleSettingsSelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                $"🔔 В какое время отправлять новые слова?",
                replyMarkup: SettingsHandler.GetTimeKeyboard());
        }

        /// <summary>
        /// Обработка выбора языка
        /// </summary>
        private async Task HandleLanguageSelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                $"🔔 В какое время отправлять новые слова?",
                replyMarkup: SettingsHandler.GetTimeKeyboard());
        }

        /// <summary>
        /// Обработка выбора времени
        /// </summary>
        private async Task HandleTimeSelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                $"🟣 Какая сложность должна быть?",
                replyMarkup: SettingsHandler.GetDifficultyKeyboard());
        }

        /// <summary>
        /// Обработка выбора сложности
        /// </summary>
        private async Task HandleDifficultySelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            callbackQuery.Data = "settings_done";
        }

        /// <summary>
        /// Завершение настроек
        /// </summary>
        private async Task FinishSettings(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            await botClient.AnswerCallbackQuery(callbackQuery.Id, "Настройки завершены!");

            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                "Настройки сохранены. Теперь я буду отправлять вам слова согласно выбранным параметрам!");
        }
    }
}
