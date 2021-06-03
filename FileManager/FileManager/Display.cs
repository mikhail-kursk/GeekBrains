using System;
using System.Text;

namespace FileManager
{
    class Display
    {

        public Display()
        {
        }

        public static void RefreshAll()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();

            FileSystem.ReCollectDirectory();
            DisplayForms.Display();

            if (FileManager._activeWindow == 0)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("> ");
            }
            else
                Console.ForegroundColor = ConsoleColor.Black;

                Console.Write(FileManager._currentCommand);

        }
    }
}
