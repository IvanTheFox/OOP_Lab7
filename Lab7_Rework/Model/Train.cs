namespace Lab7_Rework.Model
{
    public class Train
    {
        public const string CHARS = "ABCDEFGHIJKLMNOPQRSTVWXYZ";
        public static readonly List<string> s_CityBank =
        [
            "Москва",
            "Санкт-Питербург",
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

        /// <summary>Возвращает название типа поезда</summary>
        public static string GetTrainTypeName(TrainType value) => s_TrainTypesNames[value];

        /// <summary>Возвращает тип поезда по его названию</summary>
        public static TrainType? GetTrainTypeEnum(string name)
        {
            foreach (var trainType in s_TrainTypesNames)
                if (trainType.Value == name)
                    return trainType.Key;
            return null;
        }

        public int Id { get; set; }

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

        public Time Departure { get; set; }
        public TrainType Type { get; set; }

        public int Seats
        {
            get;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество мест не может быть отрицательным");
                field = value;
            }
        }

        public int FreeSeats
        {
            get;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество свободных мест не может быть отрицательным");
                if (value > Seats)
                    throw new ArgumentException("Количество свободных мест не может превышать количество мест");
                field = value;
            }
        }

        public Train(int id, string number, string destination, Time departure, TrainType type, int seats, int freeSeats)
        {
            Id = id;
            Number = number;
            Destination = destination;
            Departure = departure;
            Type = type;
            Seats = seats;
            FreeSeats = freeSeats;
        }

        public Train(Train otherTrain)
        {
            Id = otherTrain.Id;
            Number = otherTrain.Number;
            Destination = otherTrain.Destination;
            Departure = new(otherTrain.Departure.Hour, otherTrain.Departure.Minute);
            Type = otherTrain.Type;
            Seats = otherTrain.Seats;
            FreeSeats = otherTrain.FreeSeats;
        }

        /// <summary>Создаёт поезд со случайными данными</summary>
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
    }
}