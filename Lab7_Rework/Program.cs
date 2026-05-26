using Lab7_Rework.Views;
using System.Diagnostics;

namespace Lab7_Rework
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Инициализация Windows Forms
            ApplicationConfiguration.Initialize();

            // Стартовая форма
            StartupForm startupForm = new();
            Application.Run(startupForm);

            // Запуск одного из видов
            if (startupForm.IsConsoleMode())
            {
                TrainConsole console = new();
                console.Run();
            }
            else
                Application.Run(new TrainFrom());
        }
    }
}