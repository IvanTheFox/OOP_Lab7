using System.Runtime.InteropServices;
using Lab7_Rework.Controller;
using Lab7_Rework.Model;

namespace Lab7_Rework.Views
{
    internal class TrainConsole : IView
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private const string EXIT_COMMAND = "exit";

        private readonly TrainController _controller;
        private readonly TrainModel _model;

        private bool _isRunning = false;

        public TrainConsole()
        {
            _controller = TrainController.Instance;
            _model = TrainModel.Instance;

            AllocConsole();
        }

        private static string ReadLine()
        {
            if (Console.ReadLine() is string line && line != null)
                return line;
            else
                return "";
        }

        private static void WriteLine(string line) => Console.WriteLine(line);

        private static void Greet() => WriteLine("Консольное приложение \"MVC - Вокзал\"\n\texit - выход из приложения\n\thelp - список комманд");

        public void Run()
        {
            if (_isRunning)
                return;
            _isRunning = true;

            Greet();
            while (true)
            {
                string command = ReadLine();
                if (command == EXIT_COMMAND)
                    break;
                else
                    WriteLine("Fuck off");
            }
        }
    }
}
