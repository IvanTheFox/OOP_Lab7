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

        private string _inputNumber = "";
        private string _inputDestination = "";
        private string _inputDepartureTime = "";
        private Train.TrainType _inputTrainType = Train.TrainType.Passanger;
        private int _inputSeatsTotal = 0;
        private int _inputSeatsAvailable = 0;
        private string _searchQuery = "";
        private Train? _selectedTrain = null;

        // ── IView ────────────────────────────────────────────────────────────

        public string InputNumber
        {
            get => _inputNumber;
            set => _inputNumber = value;
        }

        public string InputDestination
        {
            get => _inputDestination;
            set => _inputDestination = value;
        }

        public string InputDepartureTime
        {
            get => _inputDepartureTime;
            set => _inputDepartureTime = value;
        }

        public Train.TrainType InputTrainType
        {
            get => _inputTrainType;
            set => _inputTrainType = value;
        }

        public int InputSeatsTotal
        {
            get => _inputSeatsTotal;
            set => _inputSeatsTotal = value;
        }

        public int InputSeatsAvailable
        {
            get => _inputSeatsAvailable;
            set => _inputSeatsAvailable = value;
        }

        public string SearchQuery => _searchQuery;

        public Train? SelectedTrain => _selectedTrain;

        public void ShowMessage(string message) => WriteLine(message);

        public void ClearForm()
        {
            _inputNumber = "";
            _inputDestination = "";
            _inputDepartureTime = "";
            _inputTrainType = Train.TrainType.Passanger;
            _inputSeatsTotal = 0;
            _inputSeatsAvailable = 0;
            _searchQuery = "";
            _selectedTrain = null;
        }

        public void ShowSearchResults(IEnumerable<Train> trains)
        {
            var list = trains.ToList();
            if (list.Count == 0)
            {
                WriteLine("  (нет записей)");
                return;
            }

            WriteLine(string.Format("  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}",
                "ID", "Номер", "Назначение", "Время", "Тип", "Всего", "Своб."));
            WriteLine(new string('-', 75));
            foreach (var t in list)
            {
                WriteLine(string.Format("  {0,-5} {1,-8} {2,-20} {3,-6} {4,-16} {5,-6} {6,-6}",
                    t.Id,
                    t.Number,
                    t.Destination,
                    t.Departure.ToString(),
                    Train.GetTrainTypeName(t.Type),
                    t.Seats,
                    t.FreeSeats));
            }
        }

        // Конструктор и запуск

        public TrainConsole()
        {
            AllocConsole();

            _model = TrainModel.Instance;
            _controller = TrainController.Instance;
            _controller.AttachView(this);
        }

        private static string ReadLine() => Console.ReadLine() ?? "";
        private static void WriteLine(string line) => Console.WriteLine(line);

        private static void PrintHelp()
        {
            WriteLine("\nКоманды:");
            WriteLine("  list              — показать все поезда");
            WriteLine("  search            — поиск поездов");
            WriteLine("  add               — добавить поезд");
            WriteLine("  delete <id>       — удалить поезд по ID");
            WriteLine("  select <id>       — выбрать поезд по ID");
            WriteLine("  help              — список команд");
            WriteLine("  exit              — выход\n");
        }

        public void Run()
        {
            if (_isRunning) return;
            _isRunning = true;

            WriteLine("Консольное приложение \"MVC - Вокзал\"");
            PrintHelp();

            _controller.LoadAll();

            while (true)
            {
                Console.Write("> ");
                string input = ReadLine().Trim();
                string[] parts = input.Split(' ', 2);
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
                        Console.Write("Поиск: ");
                        _searchQuery = ReadLine();
                        _controller.Search();
                        break;

                    case "select":
                        HandleSelect(parts);
                        break;

                    case "delete":
                        if (parts.Length > 1) HandleSelect(parts);
                        _controller.Delete();
                        break;

                    case "add":
                        HandleAdd();
                        break;

                    default:
                        WriteLine($"Неизвестная команда: \"{command}\". Введите help.");
                        break;
                }
            }
        }

        // Вспомогательные методы ввода

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
                WriteLine($"Выбран: {_selectedTrain.Number} → {_selectedTrain.Destination}");
            }
            catch
            {
                WriteLine($"Поезд с ID {id} не найден.");
            }
        }

        private void HandleAdd()
        {
            Console.Write("Номер поезда: ");
            _inputNumber = ReadLine();

            Console.Write("Назначение: ");
            _inputDestination = ReadLine();

            Console.Write("Время отправления (чч:мм): ");
            _inputDepartureTime = ReadLine();

            Console.Write("Тип поезда:\n");
            foreach (var kv in Train.s_TrainTypesNames)
                WriteLine($"  {(int)kv.Key} — {kv.Value}");
            Console.Write("Выберите номер: ");
            if (!int.TryParse(ReadLine(), out int typeNum) ||
                !Enum.IsDefined(typeof(Train.TrainType), typeNum))
            {
                WriteLine("Ошибка: некорректный тип поезда.");
                return;
            }
            _inputTrainType = (Train.TrainType)typeNum;

            Console.Write("Мест всего: ");
            if (!int.TryParse(ReadLine(), out _inputSeatsTotal))
            {
                WriteLine("Ошибка: введите целое число.");
                return;
            }

            Console.Write("Мест свободно: ");
            if (!int.TryParse(ReadLine(), out _inputSeatsAvailable))
            {
                WriteLine("Ошибка: введите целое число.");
                return;
            }

            _controller.Add();
        }
    }
}