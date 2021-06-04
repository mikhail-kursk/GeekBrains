using System;

namespace FileManager
{
    class Display
    {

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

            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("> ");

            Console.Write(Worker._currentCommand);

        }
    }
}
