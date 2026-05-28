using Lab7_Rework.Model;

namespace Lab7_Rework.Views
{
    internal interface IView
    {
        void AddTrain(string number, string departure, string time, string type, string seatsTotal, string seatsAvailable);
        void RemoveTrain(string id);
        void ModifyTrain(string id, string number, string departure, string time, string type, string seatsTotal, string seatsAvailable);
        IEnumerable<Train> SearchTrain(string query);

        void OnTrainAdded(Train train);
        void OnTrainRemoved(Train train);
        void OnTrainModified(Train train);
    }
}