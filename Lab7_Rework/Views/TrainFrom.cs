using System.ComponentModel;
using Lab7_Rework.Controller;
using Lab7_Rework.Model;
using Lab7_Rework.Views;

namespace Lab7_Rework
{
    /// <summary>
    /// Класс-форма для работы с репозиторием поездов
    /// </summary>
    public partial class TrainFrom : Form, IView
    {
        private readonly TrainController _controller;
        private readonly TrainModel _model;
        private readonly List<Train> _trains = [];

        /// <summary>
        /// Инициализирует форму
        /// </summary>
        public TrainFrom()
        {
            InitializeComponent();

            _model = TrainModel.Instance;

            foreach (var name in Train.s_TrainTypesNames.Values)
                TrainTypeInput.Items.Add(name);
            if (TrainTypeInput.Items.Count > 0)
                TrainTypeInput.SelectedIndex = 0;

            _model.TrainAdded += OnTrainAdded;
            _model.TrainRemoved += OnTrainRemoved;
            _model.TrainModified += OnTrainModified;

            _controller = TrainController.Instance;

            foreach (var train in _controller.GetAll())
                _trains.Add(train);

            RefreshGrid();
        }

        /// <summary>
        /// Вспомогательный метод для обновления таблицы поездов
        /// </summary>
        private void RefreshGrid() => TrainTable.DataSource = new BindingList<Train>(_trains);

        /// <summary>
        /// Обрабатывает событие добавления поезда в репозиторий
        /// </summary>
        /// <param name="train">Добавленный поезд</param>
        public void OnTrainAdded(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id > train.Id)
                {
                    _trains.Insert(i, train);
                    RefreshGrid();
                    return;
                }
            }
            _trains.Add(train);
            RefreshGrid();
        }

        /// <summary>
        /// Обрабатывает событие удаления поезда из репозитория
        /// </summary>
        /// <param name="train">Удалённый поезд</param>
        public void OnTrainRemoved(Train train)
        {
            _trains.Remove(train);
            RefreshGrid();
        }

        /// <summary>
        /// Обрабатывает событие изменения поезда из репозитория
        /// </summary>
        /// <param name="train">Изменённый поезд</param>
        public void OnTrainModified(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id == train.Id)
                {
                    _trains[i] = train;
                    RefreshGrid();
                    return;
                }
            }
        }

        /// <summary>
        /// Отправляет запрос на добваление поезда в репозиторий
        /// </summary>
        /// <param name="_number">Номер поезда</param>
        /// <param name="_departure">Пункт назначения</param>
        /// <param name="_time">Время отправления</param>
        /// <param name="_type">Тип поезда</param>
        /// <param name="_seatsTotal">Количество мест</param>
        /// <param name="_seatsAvailable">Количество свободных мест</param>
        public void AddTrain(string _number, string _departure, string _time,
                             string _type, string _seatsTotal, string _seatsAvailable)
        {
            Time time;
            try
            {
                time = Time.FromString(_time);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Ошибка ввода");
                return;
            }

            Train.TrainType? type = Train.GetTrainTypeEnum(_type);
            if (type == null)
            {
                MessageBox.Show("Введённого типа поезда не существует.", "Ошибка ввода");
                return;
            }

            if (!int.TryParse(_seatsTotal, out int seatsTotal))
            {
                MessageBox.Show("Введённое общее количество мест не является числом", "Ошибка ввода");
                return;
            }
            if (!int.TryParse(_seatsAvailable, out int seatsAvailable))
            {
                MessageBox.Show("Введённое количество доступных мест не является числом", "Ошибка ввода");
                return;
            }

            try
            {
                _controller.Add(_number, _departure, time, type.Value, seatsTotal, seatsAvailable);
                ShowMessage("Поезд добавлен.");
                ClearForm();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Ошибка ввода");
            }
        }

        /// <summary>
        /// Отправляет запрос на удаление поезда из репозитория
        /// </summary>
        /// <param name="_id">Идентификатор поезда</param>
        public void RemoveTrain(string _id)
        {
            if (int.TryParse(_id, out int id))
                _controller.Delete(id);
        }

        /// <summary>
        /// Отправляет запрос на изменение поезда репозитория
        /// </summary>
        /// <param name="_id">Идентификатор поезда</param>
        /// <param name="_number">Номер поезда</param>
        /// <param name="_departure">Пункт назначения</param>
        /// <param name="_time">Время отправления</param>
        /// <param name="_type">Тип поезда</param>
        /// <param name="_seatsTotal">Количество мест</param>
        /// <param name="_seatsAvailable">Количество свободных мест</param>
        public void ModifyTrain(string _id, string _number, string _departure, string _time,
                                string _type, string _seatsTotal, string _seatsAvailable)
        {
            if (!int.TryParse(_id, out int id))
            {
                MessageBox.Show("Некорректный идентификатор поезда.", "Ошибка");
                return;
            }

            Time time;
            try
            {
                time = Time.FromString(_time);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Ошибка ввода");
                return;
            }

            Train.TrainType? type = Train.GetTrainTypeEnum(_type);
            if (type == null)
            {
                MessageBox.Show("Тип поезда не найден.", "Ошибка ввода");
                return;
            }

            if (!int.TryParse(_seatsTotal, out int seatsTotal) || !int.TryParse(_seatsAvailable, out int seatsAvailable))
            {
                MessageBox.Show("Количество мест должно быть числом.", "Ошибка ввода");
                return;
            }

            try
            {
                Train modified = new Train(id, _number, _departure, time, type.Value, seatsTotal, seatsAvailable);
                _model.ModifyTrain(modified);
                ShowMessage($"Поезд #{id} изменён.");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Ошибка ввода");
            }
        }

        /// <summary>
        /// Отправляет запрос на поиск поездов, соответствующих запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Список поездов</returns>
        public IEnumerable<Train> SearchTrain(string query) => _controller.Search(query);

        /// <summary>
        /// Отображает сообщение на форме
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void ShowMessage(string message)
        {
            if (message.StartsWith("Ошибка"))
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                StatusLabel.Text = message;
        }

        /// <summary>
        /// Очищает поля ввода на форме
        /// </summary>
        public void ClearForm()
        {
            TrainNumberInput.Clear();
            TrainDestinationInput.Clear();
            TrainDepartureInput.Clear();
            TrainSeatsInput.Value = TrainSeatsInput.Minimum;
            TrainFreeSeatsInput.Value = TrainFreeSeatsInput.Minimum;
            SearchQueryInput.Clear();
        }

        /// <summary>
        /// Выводит результаты поиска поездов по запросу
        /// </summary>
        /// <param name="trains">Список поездов</param>
        public void ShowSearchResults(IEnumerable<Train> trains)
        {
            TrainTable.DataSource = new BindingList<Train>([.. trains]);
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTrainButton_Click(object sender, EventArgs e)
        {
            AddTrain(
                TrainNumberInput.Text,
                TrainDestinationInput.Text,
                TrainDepartureInput.Text,
                TrainTypeInput.SelectedItem?.ToString() ?? string.Empty,
                TrainSeatsInput.Value.ToString(),
                TrainFreeSeatsInput.Value.ToString()
            );
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTrainButton_Click(object sender, EventArgs e)
        {
            if (TrainTable.CurrentRow?.DataBoundItem is Train selected)
                RemoveTrain(selected.Id.ToString());
            else
                ShowMessage("Ошибка: выберите поезд для удаления.");
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTrainButton_Click(object sender, EventArgs e)
        {
            var results = SearchTrain(SearchQueryInput.Text);
            ShowSearchResults(results);
            ShowMessage($"Найдено: {results.Count()} поездов.");
        }

        /// <summary>
        /// Обрабатывает нажатие на кнопку изменить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyTrainButton_Click(object sender, EventArgs e)
        {
            if (TrainTable.CurrentRow?.DataBoundItem is not Train selected)
            {
                ShowMessage("Ошибка: выберите поезд для изменения.");
                return;
            }

            ModifyTrain(
                selected.Id.ToString(),
                TrainNumberInput.Text,
                TrainDestinationInput.Text,
                TrainDepartureInput.Text,
                TrainTypeInput.SelectedItem?.ToString() ?? string.Empty,
                TrainSeatsInput.Value.ToString(),
                TrainFreeSeatsInput.Value.ToString()
            );
        }

        /// <summary>
        /// Обрабатывает изменение выделения в таблице поездов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrainTable_SelectionChanged(object sender, EventArgs e)
        {
            if (TrainTable.CurrentRow?.DataBoundItem is not Train selected)
                return;

            TrainNumberInput.Text = selected.Number;
            TrainDestinationInput.Text = selected.Destination;
            TrainDepartureInput.Text = selected.Departure.ToString();
            TrainTypeInput.SelectedItem = Train.GetTrainTypeName(selected.Type);
            TrainSeatsInput.Value = selected.TotalSeats;
            TrainFreeSeatsInput.Value = selected.SeatsAvailable;
        }
    }
}