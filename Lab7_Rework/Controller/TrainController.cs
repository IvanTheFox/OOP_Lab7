using Lab7_Rework.Model;

namespace Lab7_Rework.Controller
{
    internal class TrainController()
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

        private readonly TrainModel _model = TrainModel.Instance;


        /// <summary>
        /// Возвращает список всех поездов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Train> GetAll() => _model.GetAll();

        /// <summary>
        /// Возвращает поезд с заданным идентификатором (если такой существует)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Поезд</returns>
        public Train? GetById(int id) => _model.GetById(id);

        /// <summary>
        /// Добавляет новый поезд в репозиторий
        /// </summary>
        /// <param name="number">Номер поезда</param>
        /// <param name="departure">Пункт назначения</param>
        /// <param name="time">Время отправления</param>
        /// <param name="type">Тип поезда</param>
        /// <param name="seatsTotal">Количество мест</param>
        /// <param name="seatsAvailable">Количество свободных мест</param>
        /// <exception cref="ArgumentException">Исключение при некорректных данных</exception>
        public void Add(string number, string departure, Time time, Train.TrainType type, int seatsTotal, int seatsAvailable)
        {
            if (seatsAvailable > seatsTotal)
                throw new ArgumentException("Свободных мест не может быть больше общего количества мест");

            _model.AddTrain(new Train(0, number, departure, time, type, seatsTotal, seatsAvailable));
        }

        /// <summary>
        /// Удаляет поезд с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id) => _model.RemoveById(id);

        /// <summary>
        /// Возвращает поезда, соответствующие заданному запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Список поездов</returns>
        public IEnumerable<Train> Search(string query) => _model.Search(query);
    }
}