using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class Display
    {

        // Default path
        public static string _path = "C://";
        public static string _file;

        public Display()
        {
        }

        public static void RefreshAll()
        {
            Console.Clear();
            

            Display.RefreshDirectories();
            Display.RefreshFileInfo();
            Display.RefreshConsole();
        }

        private static void RefreshDirectories()
        {
            //_path
            List<string> directories = new List<string>();
            List<string> files = new List<string>();

            foreach (var entity in Directory.GetFileSystemEntries(_path))
            {
                if ((File.GetAttributes(entity) & FileAttributes.Directory) == FileAttributes.Directory)
                    directories.Add(entity);
                else
                    files.Add(entity);
            }

            directories.Sort();
            files.Sort();

            foreach (var entity in directories)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(entity);
            }

            Console.ResetColor();

            foreach (var entity in files)
            {
                Console.WriteLine(entity);
            }

        }

        private static void RefreshFileInfo()
        {
            //_file
        }

        private static void RefreshConsole()
        { }

    }
}
