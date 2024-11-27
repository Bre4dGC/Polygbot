namespace Polygbot.Models
{
    public class UserSettings
    {
        /// <summary>
        /// Айди пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Количество слов в день
        /// </summary>
        public int WordsPerDay { get; set; }
        /// <summary>
        /// Время оповещения
        /// </summary>
        public TimeSpan NotificationTime { get; set; }
        /// <summary>
        /// Язык для изучения
        /// </summary>
        public Language Language { get; set; }
        /// <summary>
        /// Сложность слов
        /// </summary>
        public Difficult Difficulty { get; set; }
    }

    public enum Language
    {
        Random,
        English,
        German,
        Spanish,
        French
    }

    public enum Difficult
    {
        Easy,
        Medium,
        Hard
    }
}
