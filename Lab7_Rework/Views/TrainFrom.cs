using Lab7_Rework.Controller;
using Lab7_Rework.Model;

namespace Lab7_Rework
{
    public partial class TrainFrom : Form
    {
        private readonly TrainController _controller;
        private readonly TrainModel _model;

        public TrainFrom()
        {
            InitializeComponent();

            _controller = TrainController.Instance;
            _model = TrainModel.Instance;
        }
    }
}
