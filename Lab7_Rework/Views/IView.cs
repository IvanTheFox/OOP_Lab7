using Lab7_Rework.Model;

namespace Lab7_Rework.Views
{
    internal interface IView
    {
        void AddTrain(Train train);
        void RemoveTrain(int id);
        void ModifyTrain(Train train);

        void OnTrainAdded(Train train);
        void OnTrainRemoved(int id);
        void OnTrainModified(Train train);
    }
}
