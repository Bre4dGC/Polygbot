using Telegram.Bot.Types.ReplyMarkups;

namespace Polygbot.Handlers
{
    public class SettingsHandler
    {
        public static InlineKeyboardMarkup GetLanguageKeyboard()
        {
            return new InlineKeyboardMarkup(
            [   
                [InlineKeyboardButton.WithCallbackData("🎲 Рандом", "lang_random")],
                [InlineKeyboardButton.WithCallbackData("🇬🇧 Английский", "lang_english")],
                [InlineKeyboardButton.WithCallbackData("🇩🇪 Немецкий", "lang_german")],
                [InlineKeyboardButton.WithCallbackData("🇪🇸 Испанский", "lang_spanish")],
                [InlineKeyboardButton.WithCallbackData("🇫🇷 Французкий", "lang_french")],
                [InlineKeyboardButton.WithCallbackData("Назад", "settings_back") ],
            ]);
        }

        public static InlineKeyboardMarkup GetTimeKeyboard()
        {
            return new InlineKeyboardMarkup(
            [
                [InlineKeyboardButton.WithCallbackData("🕗 Утро 08:00", "time_morning")],
                [InlineKeyboardButton.WithCallbackData("🕛 День 12:00", "time_day")],
                [InlineKeyboardButton.WithCallbackData("🕕 Вечер 18:00", "time_evening")],
                [InlineKeyboardButton.WithCallbackData("Ввести вручную", "time_custom")],
                [InlineKeyboardButton.WithCallbackData("Назад", "settings_back") ],
            ]);
        }

        public static InlineKeyboardMarkup GetDifficultyKeyboard()
        {
            return new InlineKeyboardMarkup(
            [
                [InlineKeyboardButton.WithCallbackData("🟢 Легко", "difficulty_easy")],
                [InlineKeyboardButton.WithCallbackData("🟡 Средне", "difficulty_medium")],
                [InlineKeyboardButton.WithCallbackData("🔴 Сложно", "difficulty_hard")],
                [InlineKeyboardButton.WithCallbackData("Назад", "settings_back") ],
            ]);
        }

        public static InlineKeyboardMarkup GetSettingsMenu()
        {
            return new InlineKeyboardMarkup(
            [
                [InlineKeyboardButton.WithCallbackData("👅 Выбрать язык", "settings_language")],
                [InlineKeyboardButton.WithCallbackData("⏰ Установить время", "settings_time")],
                [InlineKeyboardButton.WithCallbackData("🟣 Выбрать сложность", "settings_difficult")],
                [InlineKeyboardButton.WithCallbackData("Назад", "back") ],
            ]);
        }
    }
}
