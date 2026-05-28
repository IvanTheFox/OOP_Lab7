using System.ComponentModel;
using Lab7_Rework.Controller;
using Lab7_Rework.Model;
using Lab7_Rework.Views;

namespace Lab7_Rework
{
    public partial class TrainFrom : Form, IView
    {
        private readonly TrainController _controller;
        private readonly TrainModel _model;
        private readonly List<Train> _trains = new();

        public TrainFrom()
        {
            InitializeComponent();

            _model = TrainModel.Instance;

            foreach (var name in Train.s_TrainTypesNames.Values)
                cmbTrainType.Items.Add(name);
            if (cmbTrainType.Items.Count > 0)
                cmbTrainType.SelectedIndex = 0;

            _model.TrainAdded += OnTrainAdded;
            _model.TrainRemoved += OnTrainRemoved;
            _model.TrainModified += OnTrainModified;

            _controller = TrainController.Instance;

            // Загрузка всех поездов в локальный список при старте
            foreach (var train in _controller.GetAll())
                _trains.Add(train);

            RefreshGrid();
        }

        /// <summary>
        /// Вспомогательный метод для перепривязки DataGridView к _trains
        /// </summary>
        private void RefreshGrid()
        {
            dataGridView1.DataSource = new BindingList<Train>(_trains);
        }

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

        public void OnTrainRemoved(Train train)
        {
            _trains.Remove(train);
            RefreshGrid();
        }

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

        public void RemoveTrain(string _id)
        {
            if (int.TryParse(_id, out int id))
                _controller.Delete(id);
        }

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

            if (!int.TryParse(_seatsTotal, out int seatsTotal) ||
                !int.TryParse(_seatsAvailable, out int seatsAvailable))
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

        public IEnumerable<Train> SearchTrain(string query)
        {
            return _controller.Search(query);
        }

        public void ShowMessage(string message)
        {
            if (message.StartsWith("Ошибка"))
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                lblStatus.Text = message;
        }

        public void ClearForm()
        {
            tbTrainNumber.Clear();
            tbDestination.Clear();
            tbTime.Clear();
            nudSeatsTotal.Value = nudSeatsTotal.Minimum;
            nudSeatsAvailable.Value = nudSeatsAvailable.Minimum;
            tbSearch.Clear();
        }

        public void ShowSearchResults(IEnumerable<Train> trains)
        {
            dataGridView1.DataSource = new BindingList<Train>(new List<Train>(trains));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTrain(
                tbTrainNumber.Text,
                tbDestination.Text,
                tbTime.Text,
                cmbTrainType.SelectedItem?.ToString() ?? string.Empty,
                nudSeatsTotal.Value.ToString(),
                nudSeatsAvailable.Value.ToString()
            );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Выбранная строка из DataGridView
            if (dataGridView1.CurrentRow?.DataBoundItem is Train selected)
                RemoveTrain(selected.Id.ToString());
            else
                ShowMessage("Ошибка: выберите поезд для удаления.");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var results = SearchTrain(tbSearch.Text);
            ShowSearchResults(results);
            ShowMessage($"Найдено: {results.Count()} поездов.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is not Train selected)
            {
                ShowMessage("Ошибка: выберите поезд для изменения.");
                return;
            }

            ModifyTrain(
                selected.Id.ToString(),
                tbTrainNumber.Text,
                tbDestination.Text,
                tbTime.Text,
                cmbTrainType.SelectedItem?.ToString() ?? string.Empty,
                nudSeatsTotal.Value.ToString(),
                nudSeatsAvailable.Value.ToString()
            );
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is not Train selected)
                return;

            tbTrainNumber.Text = selected.Number;
            tbDestination.Text = selected.Destination;
            tbTime.Text = selected.Departure.ToString();
            cmbTrainType.SelectedItem = Train.GetTrainTypeName(selected.Type);
            nudSeatsTotal.Value = selected.Seats;
            nudSeatsAvailable.Value = selected.FreeSeats;
        }
    }
}