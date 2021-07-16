using System;

namespace FileManager
{
    class Display
    {

        public static int _consoleLine;
        public static int _consoleLastLine;

        public Display()
        {
        }

        public static void RefreshAll()
        {
            RefreshDirectories();
            RefreshConsole();
        }

        public static void RefreshDirectories()
        {
            Console.Clear();
            FileSystem.ReCollectDirectory();
            DisplayForms.DisplayFileManager();
        }

        public static void RefreshConsole()
        {

            for (int i = _consoleLine; i <= _consoleLastLine; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, _consoleLine);

            foreach (var error in DisplayForms.userFriendlyErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(error);
            }

            DisplayForms.userFriendlyErrors.Clear();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("> ");
            Console.Write(Worker._currentCommand);
            _consoleLastLine = Console.CursorTop;

        }
    }
}
