using System.Text.RegularExpressions;

namespace Lab7_Rework.Model
{
    /// <summary>
    /// Класс, представляющий время
    /// </summary>
    /// <param name="hour">Час</param>
    /// <param name="minute">Минута</param>
    public class Time(int hour, int minute)
    {
        public int Hour
        {
            get;
            set
            {
                if (value < 0 || value > 23)
                    throw new ArgumentException("Некорректное число часов");
                field = value;
            }
        } = hour;
        public int Minute
        {
            get;
            set
            {
                if (value < 0 || value > 59)
                    throw new ArgumentException("Некорректное число минут");
                field = value;
            }
        } = minute;

        /// <summary>
        /// Конвертирует время в строчный формат
        /// </summary>
        /// <returns>Строка</returns>
        public override string ToString() => string.Format("{0:D2}:{1:D2}", Hour, Minute);

        /// <summary>
        /// КОнвертирует строку времени в объект времени
        /// </summary>
        /// <param name="text">Строка</param>
        /// <returns>Время</returns>
        /// <exception cref="ArgumentException">Исключение при некорректном формате времени</exception>
        public static Time FromString(string text)
        {
            text = text.Trim();
            if (!Regex.IsMatch(text, @"^\d{1,2}:\d{2}$"))
                throw new ArgumentException("Некорректный формат времени");

            string[] parts = text.Split(':');
            int hours = int.Parse(parts[0]);
            int minutes = int.Parse(parts[1]);

            if (hours < 0 || hours > 23)
                throw new ArgumentException("Некорректное число часов");
            if (minutes < 0 || minutes > 59)
                throw new ArgumentException("Некорректное число минут");
            return new Time(hours, minutes);
        }
    }
}
