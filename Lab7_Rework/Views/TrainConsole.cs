using System.Runtime.InteropServices;
using Lab7_Rework.Controller;
using Lab7_Rework.Model;

namespace Lab7_Rework.Views
{
    internal class TrainConsole : IView
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private const string EXIT_COMMAND = "exit";
        private readonly TrainController _controller;
        private readonly TrainModel _model;
        private bool _isRunning = false;
        private Train? _selectedTrain = null;

        /// <summary>
        /// Создаёт консольное приложение
        /// </summary>
        public TrainConsole()
        {
            AllocConsole();
            _model = TrainModel.Instance;
            _controller = TrainController.Instance;

            _model.TrainAdded += OnTrainAdded;
            _model.TrainRemoved += OnTrainRemoved;
            _model.TrainModified += OnTrainModified;
        }

        /// <summary>
        /// Обрабатывает событие добавления поезда в репозиторий
        /// </summary>
        /// <param name="train">Добавленный поезд</param>
        public void OnTrainAdded(Train train) =>
            WriteLine($"[+] Добавлен поезд #{train.Id}: {train.Number} ->{train.Destination}");

        /// <summary>
        /// Обрабатывает событие удаления поезда из репозитория
        /// </summary>
        /// <param name="train">Удалённый поезд</param>
        public void OnTrainRemoved(Train train) =>
            WriteLine($"[-] Удалён поезд #{train.Id}: {train.Number} ->{train.Destination}");

        /// <summary>
        /// Обрабатывает событие изменения поезда из репозитория
        /// </summary>
        /// <param name="train">Изменённый поезд</param>
        public void OnTrainModified(Train train) =>
            WriteLine($"[~] Изменён поезд #{train.Id}: {train.Number} ->{train.Destination}");

        /// <summary>
        /// Отправляет запрос на добваление поезда в репозиторий
        /// </summary>
        /// <param name="number">Номер поезда</param>
        /// <param name="departure">Пункт назначения</param>
        /// <param name="time">Время отправления</param>
        /// <param name="type">Тип поезда</param>
        /// <param name="seatsTotal">Количество мест</param>
        /// <param name="seatsAvailable">Количество свободных мест</param>
        public void AddTrain(string number, string departure, string time,
                             string type, string seatsTotal, string seatsAvailable)
        {
            Time parsedTime;
            try { parsedTime = Time.FromString(time); }
            catch (ArgumentException e) { WriteLine($"Ошибка: {e.Message}"); return; }

            Train.TrainType? parsedType = Train.GetTrainTypeEnum(type);
            if (parsedType == null) { WriteLine("Ошибка: неизвестный тип поезда. Доступные типы поездов: Пассажирский, Скоростной, Экспресс, Грузовой"); return; }

            if (!int.TryParse(seatsTotal, out int seats))
            { WriteLine("Ошибка: количество мест должно быть числом."); return; }

            if (!int.TryParse(seatsAvailable, out int freeSeats))
            { WriteLine("Ошибка: количество свободных мест должно быть числом."); return; }

            try
            {
                _controller.Add(number, departure, parsedTime, parsedType.Value, seats, freeSeats);
            }
            catch (ArgumentException e)
            {
                WriteLine($"Ошибка: {e.Message}");
            }
        }

        /// <summary>
        /// Отправляет запрос на удаление поезда из репозитория
        /// </summary>
        /// <param name="id">Идентификатор поезда</param>
        /// <exception cref="InvalidOperationException">Исключение при некорректном вводе идентфикатора</exception>
        public void RemoveTrain(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                throw new InvalidOperationException($"Введённый некорректный идентификатор '{id}'");
            _controller.Delete(parsedId);
        }

        /// <summary>
        /// Отправляет запрос на изменение поезда репозитория
        /// </summary>
        /// <param name="id">Идентификатор поезда</param>
        /// <param name="number">Номер поезда</param>
        /// <param name="departure">Пункт назначения</param>
        /// <param name="time">Время отправления</param>
        /// <param name="type">Тип поезда</param>
        /// <param name="seatsTotal">Количество мест</param>
        /// <param name="seatsAvailable">Количество свободных мест</param>
        public void ModifyTrain(string id, string number, string departure, string time,
                                string type, string seatsTotal, string seatsAvailable)
        {
            if (!int.TryParse(id, out int parsedId))
            { WriteLine("Ошибка: некорректный ID."); return; }

            Time parsedTime;
            try
            {
                parsedTime = Time.FromString(time);
            }
            catch (ArgumentException e)
            {
                WriteLine($"Ошибка: {e.Message}"); return;
            }

            Train.TrainType? parsedType = Train.GetTrainTypeEnum(type);
            if (parsedType == null)
            {
                WriteLine("Ошибка: неизвестный тип поезда.");
                return;
            }

            if (!int.TryParse(seatsTotal, out int seats) || !int.TryParse(seatsAvailable, out int freeSeats))
            {
                WriteLine("Ошибка: количество мест должно быть числом.");
                return;
            }

            Train modified = new Train(parsedId, number, departure, parsedTime, parsedType.Value, seats, freeSeats);
            _model.ModifyTrain(modified);
        }

        /// <summary>
        /// Отправляет запрос на поиск поездов, соответствующих запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Список поездов</returns>
        public IEnumerable<Train> SearchTrain(string query) => _controller.Search(query);

        /// <summary>
        /// Выводит результаты поиска поездов по запросу
        /// </summary>
        /// <param name="trains">Список поездов</param>
        public static void ShowSearchResults(IEnumerable<Train> trains)
        {
            var list = trains.ToList();
            if (list.Count == 0)
            {
                WriteLine("  (нет записей)");
                return;
            }

            WriteLine(string.Format("\n  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}", "ID", "Номер", "Назначение", "Время", "Тип", "Всего", "Своб."));
            WriteLine(new string('-', 75));
            foreach (var t in list)
                WriteLine(string.Format("  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}", t.Id, t.Number, t.Destination, t.Departure,
                    Train.GetTrainTypeName(t.Type), t.TotalSeats, t.SeatsAvailable));
        }

        /// <summary>
        /// Запускает консольное приложение
        /// </summary>
        public void Run()
        {
            if (_isRunning) return;
            _isRunning = true;

            WriteLine("Консольное приложение \"MVC - Вокзал\"");
            PrintHelp();

            while (true)
            {
                Console.Write(_selectedTrain != null ? $"[#{_selectedTrain.Id} {_selectedTrain.Number}]> " : "> ");

                string input = ReadLine().Trim();
                if (string.IsNullOrEmpty(input))
                    continue;

                string[] parts = input.Split(' ');
                string command = parts[0].ToLower();

                switch (command)
                {
                    case EXIT_COMMAND:
                        return;

                    case "help":
                        PrintHelp();
                        break;

                    case "list":
                        ShowSearchResults(_controller.GetAll());
                        break;

                    case "search":
                        string query = parts.Length > 1 ? string.Join(' ', parts[1..]) : "";
                        ShowSearchResults(SearchTrain(query));
                        break;

                    case "add":
                        HandleAdd(parts);
                        break;

                    case "select":
                        HandleSelect(parts);
                        break;

                    case "edit":
                        HandleEdit(parts);
                        break;

                    case "delete":
                        HandleDelete(parts);
                        break;

                    case "deselect":
                        _selectedTrain = null;
                        WriteLine("Выбор снят.");
                        break;

                    default:
                        WriteLine($"Неизвестная команда: \"{command}\". Введите help.");
                        break;
                }
            }
        }

        /// <summary>
        /// Считывает введённую пользователем в консоль строку
        /// </summary>
        /// <returns>Введённая строка</returns>
        private static string ReadLine() => Console.ReadLine() ?? "";

        /// <summary>
        /// Выводит строку в консоль
        /// </summary>
        /// <param name="line">Выводимая строка</param>
        private static void WriteLine(string line) => Console.WriteLine(line);

        /// <summary>
        /// Выводит список комманд
        /// </summary>
        private static void PrintHelp()
        {
            WriteLine("\nКоманды:");
            WriteLine("  list                                                                 — список поездов");
            WriteLine("  search [запрос]                                                      — поиск");
            WriteLine("  add <номер> <назначение> <время> <тип> <всего мест> <мест свободно>  - добавить поезд");
            WriteLine("    Пример: add 100А Москва 18:00 Экспресс 100 90");
            WriteLine("  select <id>                                                          — выбрать поезд");
            WriteLine("  edit <номер> <назначение> <время> <тип> <всего мест> <мест свободно> - изменить выбранный поезд");
            WriteLine("  delete                                                               — удалить выбранный поезд");
            WriteLine("  deselect                                                             — снять выбор");
            WriteLine("  help                                                                 — список команд");
            WriteLine("  exit                                                                 — выход\n");
        }

        /// <summary>
        /// Обрабаывает комманду выбора поезда
        /// </summary>
        /// <param name="parts">Аргументы</param>
        private void HandleSelect(string[] parts)
        {
            if (parts.Length < 2 || !int.TryParse(parts[1], out int id))
            {
                WriteLine("Использование: select <id>");
                return;
            }

            _selectedTrain = _controller.GetById(id);
            if (_selectedTrain != null)
                WriteLine($"Выбран: {_selectedTrain}\nТеперь доступны команды: edit, delete");
            else
                WriteLine($"Поезд с ID {id} не найден.");
        }

        /// <summary>
        /// Обрабатывает комманду удаления поезда
        /// </summary>
        /// <param name="parts">Аргументы</param>
        private void HandleDelete(string[] _)
        {
            if (_selectedTrain == null)
            {
                WriteLine("Сначала выберите поезд командой select <id>, или укажите id: delete <id>.");
                return;
            }

            RemoveTrain(_selectedTrain.Id.ToString());
            _selectedTrain = null;
        }

        /// <summary>
        /// Обрабатывает комманду добавления поезда
        /// </summary>
        /// <param name="parts">Аргументы</param>
        private void HandleAdd(string[] parts)
        {
            if (parts.Length != 7)
            {
                WriteLine("Использование: add <номер> <назначение> <время> <тип> <всего> <своб.>\n\tПример: add 100А Москва 18:00 Экспресс 100 90");
                return;
            }

            AddTrain(parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
        }

        /// <summary>
        /// Обрабатывает комманду изменения поезда
        /// </summary>
        /// <param name="parts">Аргументы</param>
        private void HandleEdit(string[] parts)
        {
            if (_selectedTrain == null)
            {
                WriteLine("Сначала выберите поезд командой select <id>.");
                return;
            }

            if (parts.Length != 7)
            {
                WriteLine($"Текущие данные: {_selectedTrain}\nИспользование: edit <номер> <назначение> <время> <тип> <всего> <своб.>");
                return;
            }

            ModifyTrain(_selectedTrain.Id.ToString(), parts[1], parts[2], parts[3], parts[4], parts[5], parts[6]);
        }
    }
}