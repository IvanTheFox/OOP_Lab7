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

        public TrainConsole()
        {
            AllocConsole();
            _model = TrainModel.Instance;
            _controller = TrainController.Instance;

            _model.TrainAdded += OnTrainAdded;
            _model.TrainRemoved += OnTrainRemoved;
            _model.TrainModified += OnTrainModified;
        }

        public void OnTrainAdded(Train train) =>
            WriteLine($"[+] Добавлен поезд #{train.Id}: {train.Number} ->{train.Destination}");

        public void OnTrainRemoved(Train train) =>
            WriteLine($"[-] Удалён поезд #{train.Id}: {train.Number} ->{train.Destination}");

        public void OnTrainModified(Train train) =>
            WriteLine($"[~] Изменён поезд #{train.Id}: {train.Number} ->{train.Destination}");

       
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

            _controller.Add(number, departure, parsedTime, parsedType.Value, seats, freeSeats);
        }

        public void RemoveTrain(string id)
        {
            if (!int.TryParse(id, out int parsedId))
                throw new InvalidOperationException($"RemoveTrain получил некорректный id: '{id}'");
            _controller.Delete(parsedId);
        }

        public void ModifyTrain(string id, string number, string departure, string time,
                                string type, string seatsTotal, string seatsAvailable)
        {
            if (!int.TryParse(id, out int parsedId))
            { WriteLine("Ошибка: некорректный ID."); return; }

            Time parsedTime;
            try { parsedTime = Time.FromString(time); }
            catch (ArgumentException e) { WriteLine($"Ошибка: {e.Message}"); return; }

            Train.TrainType? parsedType = Train.GetTrainTypeEnum(type);
            if (parsedType == null) { WriteLine("Ошибка: неизвестный тип поезда."); return; }

            if (!int.TryParse(seatsTotal, out int seats) ||
                !int.TryParse(seatsAvailable, out int freeSeats))
            { WriteLine("Ошибка: количество мест должно быть числом."); return; }

            Train modified = new Train(parsedId, number, departure, parsedTime,
                                       parsedType.Value, seats, freeSeats);
            _model.ModifyTrain(modified);
        }

        public IEnumerable<Train> SearchTrain(string query) => _controller.Search(query);

        public void ShowMessage(string message) => WriteLine(message);

        public void ClearForm() { }

        public void ShowSearchResults(IEnumerable<Train> trains)
        {
            var list = trains.ToList();
            if (list.Count == 0) { WriteLine("  (нет записей)"); return; }

            WriteLine(string.Format("\n  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}",
                "ID", "Номер", "Назначение", "Время", "Тип", "Всего", "Своб."));
            WriteLine(new string('-', 75));
            foreach (var t in list)
                WriteLine(string.Format("  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}",
                    t.Id, t.Number, t.Destination, t.Departure,
                    Train.GetTrainTypeName(t.Type), t.Seats, t.FreeSeats));
            WriteLine("");
        }

        public void Run()
        {
            if (_isRunning) return;
            _isRunning = true;

            WriteLine("Консольное приложение \"MVC - Вокзал\"");
            PrintHelp();

            foreach (var train in _controller.GetAll());

            while (true)
            {
                Console.Write(_selectedTrain != null
                    ? $"[#{_selectedTrain.Id} {_selectedTrain.Number}]> "
                    : "> ");

                string input = ReadLine().Trim();
                if (string.IsNullOrEmpty(input)) continue;

                // Разбиение на команду и аргументы
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
                        ShowSearchResults(_model.GetAll());
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

        private static string ReadLine() => Console.ReadLine() ?? "";
        private static void WriteLine(string line) => Console.WriteLine(line);

        private static void PrintHelp()
        {
            WriteLine("\nКоманды:");
            WriteLine("  list                                                                — список поездов");
            WriteLine("  search [запрос]                                                     — поиск");
            WriteLine("  add <номер> <назначение> <время> <тип> <всего мест> <мест свободно> - добавить поезд");
            WriteLine("    Пример: add 100А Москва 18:00 Экспресс 100 90");
            WriteLine("  select <id>                                                         — выбрать поезд");
            WriteLine("  edit <номер> <назначение> <время> <тип> <всего> <своб.>             - изменить выбранный поезд");
            WriteLine("  delete                                                              — удалить выбранный поезд");
            WriteLine("  deselect                                                            — снять выбор");
            WriteLine("  help                                                                — список команд");
            WriteLine("  exit                                                                — выход\n");
        }

        private void HandleSelect(string[] parts)
        {
            if (parts.Length < 2 || !int.TryParse(parts[1], out int id))
            {
                WriteLine("Использование: select <id>");
                return;
            }
            try
            {
                _selectedTrain = _model.GetById(id);
                WriteLine($"Выбран: #{_selectedTrain.Id} {_selectedTrain.Number} -> " +
                          $"{_selectedTrain.Destination} | {_selectedTrain.Departure} | " +
                          $"{Train.GetTrainTypeName(_selectedTrain.Type)} | " +
                          $"мест: {_selectedTrain.Seats}, своб.: {_selectedTrain.FreeSeats}");
                WriteLine("Теперь доступны команды: edit, delete");
            }
            catch
            {
                WriteLine($"Поезд с ID {id} не найден.");
            }
        }

        private void HandleDelete(string[] parts)
        {
            if (_selectedTrain == null)
            {
                WriteLine("Сначала выберите поезд командой select <id>, " +
                          "или укажите id: delete <id>.");
                return;
            }
            RemoveTrain(_selectedTrain.Id.ToString());
            _selectedTrain = null;
        }

        private void HandleAdd(string[] parts)
        {
            if (parts.Length < 7)
            {
                WriteLine("Использование: add <номер> <назначение> <время> <тип> <всего> <своб.>");
                WriteLine("  Пример: add 100А Москва 18:00 Экспресс 100 90");
                return;
            }

            string number = parts[1];
            string destination = parts[2];
            string time = parts[3];
            string type = parts[4];
            string seatsTotal = parts[5];
            string seatsAvail = parts[6];

            AddTrain(number, destination, time, type, seatsTotal, seatsAvail);
        }

        private void HandleEdit(string[] parts)
        {
            if (_selectedTrain == null)
            {
                WriteLine("Сначала выберите поезд командой select <id>.");
                return;
            }

            // Если аргументов меньше чем нужно
            if (parts.Length < 7)
            {
                WriteLine($"Текущие данные: {_selectedTrain.Number} | " +
                          $"{_selectedTrain.Destination} | {_selectedTrain.Departure} | " +
                          $"{Train.GetTrainTypeName(_selectedTrain.Type)} | " +
                          $"{_selectedTrain.Seats} | {_selectedTrain.FreeSeats}");
                WriteLine("Использование: edit <номер> <назначение> <время> <тип> <всего> <своб.>");
                return;
            }

            string number = parts[1];
            string destination = parts[2];
            string time = parts[3];
            string type = parts[4];
            string seatsTotal = parts[5];
            string seatsAvail = parts[6];

            ModifyTrain(_selectedTrain.Id.ToString(),
                        number, destination, time, type, seatsTotal, seatsAvail);
        }
    }
}