using System;
using System.IO;
using System.Collections.Generic;
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
            //Display.RefreshDirectories();
            
            Console.WriteLine(DisplayForms.GetForm());

            Console.Write(FileManager._currentCommand);

        }

        private static void RefreshDirectories()
        {

            foreach (var entity in FileSystem.directories)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (FileManager._activeWindow == 1)
                    Console.WriteLine($"\t{entity}");
            }

            Console.ResetColor();

            foreach (var entity in FileSystem.files)
            {
                if (FileManager._activeWindow == 1)
                    Console.WriteLine($"\t{entity}");
            }

        }

    }
}
