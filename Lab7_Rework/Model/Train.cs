using System.ComponentModel;

namespace Lab7_Rework.Model
{
    /// <summary>
    /// Класс, представляющий поезд
    /// </summary>
    public class Train
    {
        public const string CHARS = "ABCDEFGHIJKLMNOPQRSTVWXYZ";
        public static readonly List<string> s_CityBank =
        [
            "Москва",
            "Санкт-Петербург",
            "Новосибирск",
            "Екатеринбург",
            "Казань",
            "Нижний новгород",
            "Челябинск",
            "Красноярск",
            "Самара",
            "Уфа",
            "Ростов-на-Дону",
            "Краснодар",
            "Омск",
            "Воронеж",
            "Пермь"
        ];

        private static readonly Random _random = new();

        /// <summary>
        /// Типы поездов
        /// </summary>
        public enum TrainType
        {
            Passanger = 1,
            HighSpeed = 2,
            Express = 3,
            Freight = 4
        }

        public static readonly Dictionary<TrainType, string> s_TrainTypesNames = new()
        {
            { TrainType.Passanger, "Пассажирский" },
            { TrainType.HighSpeed, "Скоростной" },
            { TrainType.Express, "Экспресс" },
            { TrainType.Freight, "Грузовой" },
        };

        /// <summary>
        /// Возвращает название типа поезда
        /// </summary>
        /// <param name="value">Тип поезда</param>
        /// <returns>Название</returns>
        public static string GetTrainTypeName(TrainType value) => s_TrainTypesNames[value];

        /// <summary>
        /// Возвращает тип поезда по его названию
        /// </summary>
        /// <param name="name">Название типа поезда</param>
        /// <returns>Тип поезда</returns>
        public static TrainType? GetTrainTypeEnum(string name)
        {
            foreach (var trainType in s_TrainTypesNames)
                if (trainType.Value == name)
                    return trainType.Key;
            return null;
        }

        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Номер")]
        public string Number
        {
            get;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номер поезда не может быть пустым");
                field = value;
            }
        }

        [DisplayName("Назначение")]
        public string Destination
        {
            get;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назначение не может быть пустым");
                field = value;
            }
        }

        [DisplayName("Отправление")]
        public Time Departure { get; set; }

        [Browsable(false)]
        public TrainType Type { get; set; }

        [DisplayName("Тип")]
        public string TypeName => GetTrainTypeName(Type);

        [DisplayName("Мест всего")]
        public int TotalSeats
        {
            get;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество мест не может быть отрицательным");
                field = value;
            }
        }

        [DisplayName("Мест свободно")]
        public int SeatsAvailable
        {
            get;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество свободных мест не может быть отрицательным");
                if (value > TotalSeats)
                    throw new ArgumentException("Количество свободных мест не может превышать количество мест");
                field = value;
            }
        }

        public Train(int id, string number, string destination, Time departure, TrainType type, int totalSeats, int seatsAvailable)
        {
            Id = id;
            Number = number;
            Destination = destination;
            Departure = departure;
            Type = type;
            this.TotalSeats = totalSeats;
            SeatsAvailable = seatsAvailable;
        }

        /// <summary>
        /// Создаёт случайный поезд
        /// </summary>
        /// <param name="id">Идентификатор случайного поезда</param>
        /// <returns>Случайный поезд</returns>
        public static Train RandomTrain(int id)
        {
            string number = string.Format("{0:D3}", _random.Next(0, 1000).ToString()) + CHARS[_random.Next(0, CHARS.Length)];
            string destination = s_CityBank[_random.Next(0, s_CityBank.Count)];
            Time departure = new(_random.Next(0, 24), _random.Next(0, 60));
            TrainType type = (TrainType)_random.Next(1, s_TrainTypesNames.Count + 1);
            int seats = _random.Next(500, 1500);
            int freeSeats = _random.Next(0, seats + 1);

            return new Train(id, number, destination, departure, type, seats, freeSeats);
        }

        /// <summary>
        /// Конвертирует поезд в строковый формат
        /// </summary>
        /// <returns>Строковый формат поезда</returns>
        public override string ToString() => $"#{Id} {Number} -> {Destination} | {Departure} | {GetTrainTypeName(Type)} | мест: {TotalSeats}, своб.: {SeatsAvailable}";
    }
}