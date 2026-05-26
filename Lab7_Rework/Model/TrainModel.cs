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

        private TrainModel(int count)
            : this()
        {
            for (int id = 0; id < count; id++)
                _trains.Add(Train.RandomTrain(id));
        }

        public IEnumerable<Train> GetAll() => _trains.AsReadOnly();
        public Train GetById(int id) => new(_trains.First(t => t.Id == id));

        public int AddTrain(Train train)
        {
            int i = 0;
            while (i < _trains.Count - 1 && _trains[i].Id == _trains[i + 1].Id - 1)
                i++;
            if (_trains[i].Id == _trains[i + 1].Id - 1)
                i++;
            train.Id = _trains[i].Id + 1;
            _trains.Insert(i, train);
            TrainAdded?.Invoke(new(train));

            return train.Id;
        }
        public bool RemoveById(int id)
        {
            foreach (Train train in _trains)
            {
                if (train.Id == id)
                {
                    _trains.Remove(train);
                    TrainRemoved?.Invoke(new(train));
                    return true;
                }
            }
            return false;
        }
        public bool ModifyTrain(Train train)
        {
            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].Id == train.Id)
                {
                    _trains[i] = train;
                    TrainModified?.Invoke(new(train));
                    return true;
                }
            }
            return false;
        }
    }
}
