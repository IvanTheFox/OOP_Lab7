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

        private TrainController()
        {
            _model = TrainModel.Instance;
        }


        /// <summary>Загрузить все поезда — инициирует начальное отображение</summary>
        public IEnumerable<Train> GetAll()
        {
            return _model.GetAll();
        }

        /// <summary>Добавить новый поезд из данных представления</summary>
        public void Add(string number, string departure, Time time, Train.TrainType type, int seatsTotal, int seatsAvailable)
        {
            // Проверка на здравый смысл переданных параметров
            _model.AddTrain(train);
        }

        /// <summary>Удалить выбранный поезд</summary>
        public void Delete(int id)
        {
            _model.RemoveById(id);
        }

        /// <summary>Поиск по таблице</summary>
        public IEnumerable<Train> Search(string query)
        {
            return _model.Search(query);
        }

    }
}