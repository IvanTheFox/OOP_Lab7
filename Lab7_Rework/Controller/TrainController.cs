using Lab7_Rework.Model;
using Lab7_Rework.Views;

namespace Lab7_Rework.Controller
{
    internal class TrainController
    {
        public static TrainController Instance
        {
            get
            {
                field ??= new TrainController();
                return field;
            }
            private set;
        }

        private readonly TrainModel _model;
        private IView? _view;

        private TrainController()
        {
            _model = TrainModel.Instance;
        }

        /// <summary>
        /// Привязывает представление к контроллеру.
        /// Вызывается из конструктора формы или консоли.
        /// </summary>
        public void AttachView(IView view)
        {
            _view = view;
        }

        /// <summary>Загрузить все поезда — инициирует начальное отображение</summary>
        public void LoadAll()
        {
            EnsureView();
            _view!.ShowSearchResults(_model.GetAll());
            _view.ShowMessage("Список поездов загружен.");
        }

        /// <summary>Добавить новый поезд из данных представления</summary>
        public void Add()
        {
            EnsureView();
            try
            {
                var train = BuildTrainFromView();
                _model.AddTrain(train);
                _view!.ClearForm();
                _view.ShowMessage($"Поезд №{train.Number} успешно добавлен.");
            }
            catch (ArgumentException ex)
            {
                _view!.ShowMessage($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>Удалить выбранный поезд</summary>
        public void Delete()
        {
            EnsureView();
            var selected = _view!.SelectedTrain;
            if (selected == null)
            {
                _view.ShowMessage("Выберите поезд в таблице для удаления.");
                return;
            }

            if (_model.RemoveById(selected.Id))
            {
                _view.ClearForm();
                _view.ShowMessage($"Поезд №{selected.Number} удалён.");
            }
        }

        /// <summary>Поиск по таблице</summary>
        public void Search()
        {
            EnsureView();
            var results = SearchTrains(_view!.SearchQuery);
            _view.ShowSearchResults(results);
            _view.ShowMessage($"Поиск: найдено {results.Count()} записей.");
        }

        // Вспомогательные методы

        private IEnumerable<Train> SearchTrains(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return _model.GetAll();

            query = query.ToLower();
            return _model.GetAll().Where(t =>
                t.Id.ToString() == query ||
                t.Number.ToLower().Contains(query) ||
                t.Destination.ToLower().Contains(query) ||
                Train.GetTrainTypeName(t.Type).ToLower().Contains(query));
        }

        /// <summary>
        /// Собирает поезд из данных представления строго сверху вниз.
        /// Каждый шаг может бросить ArgumentException — она поймается в Add().
        ///
        /// Порядок проверок:
        ///   1. Number       (сеттер Train)
        ///   2. Destination  (сеттер Train)
        ///   3. DepartureTime (Time.FromString)
        ///   4. Seats        (сеттер Train)
        ///   5. FreeSeats    (сеттер Train)
        /// </summary>
        private Train BuildTrainFromView()
        {
            string number = _view!.InputNumber.Trim();
            string destination = _view.InputDestination.Trim();
            Time departure = Time.FromString(_view.InputDepartureTime);
            Train.TrainType type = _view.InputTrainType;
            int seats = _view.InputSeatsTotal;
            int freeSeats = _view.InputSeatsAvailable;

            // Конструктор Train вызывает сеттеры в порядке объявления полей:
            // Number → Destination → Seats → FreeSeats
            return new Train(0, number, destination, departure, type, seats, freeSeats);
        }

        /// <summary>Проверяет, что представление привязано</summary>
        private void EnsureView()
        {
            if (_view == null)
                throw new InvalidOperationException(
                    "Представление не привязано. Вызовите AttachView перед использованием контроллера.");
        }
    }
}