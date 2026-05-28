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
        private readonly List<Train> _trains;

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
            _controller.GetAll();
        }

        public void OnTrainAdded(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id > train.Id)
                {
                    _trains.Insert(i,train);
                    //dataGridView1.DataSource = _trains;
                    return;
                }
            }
            _trains.Add(train);
            //dataGridView1.DataSource = _trains;
        }
        public void OnTrainRemoved(Train train)
        {
            _trains.Remove(train);
            //dataGridView1.DataSource = _trains;
        }
        public void OnTrainModified(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id == train.Id)
                {
                    _trains[i] = train;
                    //dataGridView1.DataSource = _trains;
                    return;
                }
            }
        }

        public void AddTrain(string _number, string _departure, string _time, string _type, string _seatsTotal, string _seatsAvailable) 
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
                MessageBox.Show("Введенное общее количество мест не является числом");
            }
            if (!int.TryParse(_seatsAvailable, out int seatsAvailable))
            {
                MessageBox.Show("Введенное количество доступных мест не является числом");
            }
            _controller.Add(_number, _departure, time, type.Value, seatsTotal, seatsAvailable);
        }
        public void RemoveTrain(string id)
        {

        }
        public void ModifyTrain(string _id, string _number, string _departure, string _time, string _type, string _seatsTotal, string _seatsAvailable)
        {

        }
        public IEnumerable<Train> SearchTrain(string query)
        {

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
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            nudSeatsTotal.Value = nudSeatsTotal.Minimum;
            nudSeatsAvailable.Value = nudSeatsAvailable.Minimum;
            txtSearch.Clear();
        }

        public void ShowSearchResults(IEnumerable<Train> trains)
        {
            var list = new BindingList<Train>(new List<Train>(trains));
            dataGridView1.DataSource = list;
        }

        // ── Обработчики кнопок ───────────────────────────────────────────────

        private void btnAdd_Click(object sender, EventArgs e) => _controller.Add();
        private void btnDelete_Click(object sender, EventArgs e) => _controller.Delete();
        private void btnSearch_Click(object sender, EventArgs e) => _controller.Search();

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            ShowMessage("Форма очищена.");
        }
    }
}