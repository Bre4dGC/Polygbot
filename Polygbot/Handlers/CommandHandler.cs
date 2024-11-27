using Telegram.Bot;
using Telegram.Bot.Types;

namespace Polygbot.Handlers
{
    public class CommandHandler
    {
        public static async Task HandlerStartCommand(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, "👅 Какой язык ты хочешь изучать?", 
                replyMarkup: SettingsHandler.GetLanguageKeyboard());
        }

        public static async Task HandlerSettingsCommand(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, "⚙️ Выберите настройки",
                replyMarkup: SettingsHandler.GetSettingsMenu());
        }

        public static async Task HandlerAiCommand(ITelegramBotClient botClient, Message message)
        {
            var prompt = message.Text.Substring(3);

            if (string.IsNullOrEmpty(prompt)) 
            { 
                await botClient.SendMessage(message.Chat.Id, "Запрос не должен быть пустым"); return;
            }

            await botClient.SendMessage(message.Chat.Id, await AiHandlers.UpdateAiHandler(prompt));
        }
    }
}
