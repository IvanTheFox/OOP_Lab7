namespace Lab7_Rework.Model
{
    internal class TrainModel()
    {
        public delegate void ModifiedDataHandler(Train train);
        public event ModifiedDataHandler? TrainAdded;
        public event ModifiedDataHandler? TrainRemoved;
        public event ModifiedDataHandler? TrainModified;

        public static TrainModel Instance
        {
            get
            {
                field ??= new TrainModel(100);
                return field;
            }
            private set;
        }

        private readonly List<Train> _trains = [];

        /// <summary>
        /// Создаёт репозиторий с заданным количеством случайных поездов
        /// </summary>
        /// <param name="count">Количество поездов</param>
        private TrainModel(int count)
            : this()
        {
            for (int id = 0; id < count; id++)
                _trains.Add(Train.RandomTrain(id));
        }

        /// <summary>
        /// Возвращает все поезда репозитория
        /// </summary>
        /// <returns>Список поездов</returns>
        public IEnumerable<Train> GetAll() => _trains.AsReadOnly();

        /// <summary>
        /// Возвращает поезд с заданным идентификатором (если такой существует)
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public Train? GetById(int id) => _trains.FirstOrDefault(t => t.Id == id);
        /// <summary>
        /// Добавляет поезд в репозиторий
        /// </summary>
        /// <param name="train">Добавляемый поезд</param>
        /// <returns>Идентификатор добавленного поезда</returns>
        public int AddTrain(Train train)
        {
            var usedIds = new HashSet<int>(_trains.Select(t => t.Id));
            int newId = 0;
            while (usedIds.Contains(newId))
                newId++;

            train.Id = newId;
            _trains.Add(train);
            TrainAdded?.Invoke(train);

            return train.Id;
        }

        /// <summary>
        /// Удаляет поезд с заданным идентификатором
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Был ли удалён поезд</returns>
        public bool RemoveById(int id)
        {
            foreach (Train train in _trains)
            {
                if (train.Id == id)
                {
                    _trains.Remove(train);
                    TrainRemoved?.Invoke(train);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Изменяет данные о поезде репозитория
        /// </summary>
        /// <param name="train">Изменяемый поезд</param>
        /// <returns>Был ли изменён поезд</returns>
        public bool ModifyTrain(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id == train.Id)
                {
                    _trains[i] = train;
                    TrainModified?.Invoke(train);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Возращает список поездов, соответствующий запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Списко поездов</returns>
        public IEnumerable<Train> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetAll();

            query = query.ToLower();
            return GetAll().Where(t =>
                t.Id.ToString() == query ||
                t.Number.ToLower().Contains(query) ||
                t.Destination.ToLower().Contains(query) ||
                Train.GetTrainTypeName(t.Type).ToLower().Contains(query));
        }
    }
}