using Telegram.Bot;
using Telegram.Bot.Types;

namespace Polygbot.Handlers
{
    public class CommandHandler
    {
        public static async Task HandleStartCommand(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, "👅 Какой язык ты хочешь изучать?", 
                replyMarkup: SettingsHandler.GetLanguageKeyboard());
        }

        public static async Task HandleSettingsCommand(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendMessage(message.Chat.Id, "⚙️ Выберите настройки",
                replyMarkup: SettingsHandler.GetSettingsMenu());
        }
    }
}
