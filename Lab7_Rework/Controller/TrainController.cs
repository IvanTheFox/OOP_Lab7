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

        private TrainController()
        {

        }
    }
}
