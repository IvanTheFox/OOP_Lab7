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

        public TrainFrom()
        {
            InitializeComponent();

            _model = TrainModel.Instance;

            foreach (var name in Train.s_TrainTypesNames.Values)
                cmbTrainType.Items.Add(name);
            if (cmbTrainType.Items.Count > 0)
                cmbTrainType.SelectedIndex = 0;

            _model.TrainAdded += _ => RefreshGrid();
            _model.TrainRemoved += _ => RefreshGrid();
            _model.TrainModified += _ => RefreshGrid();

            _controller = TrainController.Instance;
            _controller.AttachView(this);
            _controller.LoadAll();
        }

        private void RefreshGrid()
        {
            var list = new BindingList<Train>(new List<Train>(_model.GetAll()));
            dataGridView1.DataSource = list;
        }

        // ── IView ────────────────────────────────────────────────────────────

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InputNumber
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InputDestination
        {
            get => textBox2.Text;
            set => textBox2.Text = value;
        }

        // Форма отдаёт сырую строку — парсинг времени делает контроллер
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string InputDepartureTime
        {
            get => textBox3.Text;
            set => textBox3.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Train.TrainType InputTrainType
        {
            get => Train.GetTrainTypeEnum(cmbTrainType.Text) ?? Train.TrainType.Passanger;
            set => cmbTrainType.Text = Train.GetTrainTypeName(value);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InputSeatsTotal
        {
            get => (int)nudSeatsTotal.Value;
            set => nudSeatsTotal.Value = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int InputSeatsAvailable
        {
            get => (int)nudSeatsAvailable.Value;
            set => nudSeatsAvailable.Value = value;
        }

        public string SearchQuery => txtSearch.Text;

        public Train? SelectedTrain
        {
            get
            {
                if (dataGridView1.SelectedRows.Count == 0) return null;
                return dataGridView1.SelectedRows[0].DataBoundItem as Train;
            }
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