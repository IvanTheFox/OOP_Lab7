using Lab7_Rework.Model;

namespace Lab7_Rework.Views
{
    internal interface IView
    {
        //void AddTrain(Train train);
        //void RemoveTrain(int id);
        //void ModifyTrain(Train train);

        //void OnTrainAdded(Train train);
        //void OnTrainRemoved(int id);
        //void OnTrainModified(Train train);
        /// <summary>Номер поезда, введённый пользователем</summary>
        string InputNumber { get; set; }

        /// <summary>Пункт назначения, введённый пользователем</summary>
        string InputDestination { get; set; }

        /// <summary>Время отправления, введённое пользователем</summary>
        string InputDepartureTime { get; set; }

        /// <summary>Тип поезда, выбранный пользователем</summary>
        Train.TrainType InputTrainType { get; set; }

        /// <summary>Общее количество мест</summary>
        int InputSeatsTotal { get; set; }

        /// <summary>Количество свободных мест</summary>
        int InputSeatsAvailable { get; set; }

        /// <summary>Строка поискового запроса</summary>
        string SearchQuery { get; }

        /// <summary>
        /// Поезд, выбранный в таблице.
        /// Возвращает null, если ничего не выбрано.
        /// </summary>
        Train? SelectedTrain { get; }

        /// <summary>Отображает сообщение пользователю</summary>
        void ShowMessage(string message);

        /// <summary>Очищает все поля ввода формы</summary>
        void ClearForm();

        /// <summary>Отображает результаты поиска</summary>
        void ShowSearchResults(IEnumerable<Train> trains);
    }
}