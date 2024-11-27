using Polygbot.Handlers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Mistral.Types;

namespace Polygbot
{
    public class BotService
    {
        private readonly TelegramBotClient _botClient;

        public BotService()
        {
            _botClient = new TelegramBotClient("7561146572:AAF5tBL8fQpMZMy_W-VTpAur25qoe2rh1bQ");
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
                    await CommandHandler.HandleStartCommand(botClient, message);
                }
                else if (message.Text.StartsWith("/settings"))
                {
                    await CommandHandler.HandleSettingsCommand(botClient, message);
                }

                Console.WriteLine($"Пользователь {message.Chat.Username ?? message.Chat.FirstName}({message.Chat.Id}) выбрал команду: \"{message.Text}\"");
            }

            // Обработка callback-данных
            if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
                var callbackQuery = update.CallbackQuery;
                var message = callbackQuery.Message;

                if (callbackQuery.Data.StartsWith("language_"))
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
                else if (callbackQuery.Data == "settings_done")
                {
                    await FinishSettings(botClient, callbackQuery);
                }
            }
        }

        /// <summary>
        /// Обработка выбора языка
        /// </summary>
        private async Task HandleLanguageSelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var selectedLanguage = callbackQuery.Data.Substring(5);
            await botClient.AnswerCallbackQuery(callbackQuery.Id, $"Вы выбрали: {selectedLanguage}");

            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                $"🔔 В какое время отправлять новые слова?",
                replyMarkup: SettingsHandler.GetTimeKeyboard());

            Console.WriteLine($"Пользователь выбрал {selectedLanguage} язык");
        }

        /// <summary>
        /// Обработка выбора времени
        /// </summary>
        private async Task HandleTimeSelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var selectedTime = callbackQuery.Data.Substring(5);
            await botClient.AnswerCallbackQuery(callbackQuery.Id, $"Вы выбрали: {selectedTime}");

            await botClient.EditMessageText(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId,
                $"🟣 Какая сложность должна быть?",
                replyMarkup: SettingsHandler.GetDifficultyKeyboard());

            Console.WriteLine($"Пользователь выбрал {selectedTime} время");
        }

        private async Task HandleDifficultySelection(ITelegramBotClient botClient, CallbackQuery callbackQuery)
        {
            var selectedDifficulty = callbackQuery.Data.Substring(5);
            await botClient.AnswerCallbackQuery(callbackQuery.Id, $"Вы выбрали: {selectedDifficulty}");

            Console.WriteLine($"Пользователь выбрал {selectedDifficulty} сложность");
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
