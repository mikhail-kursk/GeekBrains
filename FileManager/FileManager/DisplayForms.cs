using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class DisplayForms
    {
        public static int _page = 1;  // Сделать свойством которое нельзя выставить меньше чем 10
        public static int linePerPage = 20;
        private static ConsoleColor defaultColor;

        public DisplayForms()
        {

        }

        public static void DisplayFileManager()
        {
            
            string displayedPath;

            if (FileSystem._path.Length > 60)
                displayedPath = FileSystem._path.Substring(0, 60);
            else
                displayedPath = FileSystem._path;

            if (Worker._activeWindow == 1)
                defaultColor = ConsoleColor.Gray;
            else
                defaultColor = ConsoleColor.DarkGray;

            Console.ForegroundColor = defaultColor;

            // Header
            Console.WriteLine("╔" + new string('═', Console.WindowWidth - 2) + "╗");
            Console.WriteLine("║" + " Path = " + displayedPath + new string(' ', Console.WindowWidth - FileSystem._path.Length - 31) + DateTime.Now + "  ║");
            Console.WriteLine("╠" + new string('═', Console.WindowWidth - 30) + "╦" + new string('═', 27) + "╣");
            Console.WriteLine("║" + " Folder content" + new string(' ', Console.WindowWidth - 45) + "║" + " Additional information    " + "║");
            Console.WriteLine("║" + new string(' ', Console.WindowWidth - 30) + "║" + new string(' ', 27) + "║");

            // Content
            Console.Write("║ "); PrintFolderContentWithOffset(1); Console.WriteLine("║" + new string(' ', 1) + "Attributes:" + new string(' ', 15) + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(2); Console.WriteLine("║" + new string(' ', 1) + GetAttrWithOffset("atr") + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(3); Console.WriteLine("║" + new string(' ', 1) + "Extension:" + new string(' ', 16) + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(4); Console.WriteLine("║" + new string(' ', 1) + GetAttrWithOffset("ext") + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(5); Console.WriteLine("║" + new string(' ', 1) + "Created:" + new string(' ', 18) + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(6); Console.WriteLine("║" + new string(' ', 1) + GetAttrWithOffset("cre") + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(7); Console.WriteLine("║" + new string(' ', 1) + "LastModified:" + new string(' ', 13) + "║");
            Console.Write("║ "); PrintFolderContentWithOffset(8); Console.WriteLine("║" + new string(' ', 1) + GetAttrWithOffset("lmd") + "║");

            // Content - optional
            for (int i = 9; i <= linePerPage; i++)
            {
                Console.Write("║ "); PrintFolderContentWithOffset(i); Console.WriteLine("║" + new string(' ', 27) + "║");
            }

            Console.WriteLine("╚" + new string('═', ((Console.WindowWidth - 30) / 2) - 2 ) + " Page " + _page + " " + new string('═', Console.WindowWidth - 81) + "╩" + new string('═', 27) + "╝");
            Console.WriteLine();

        }

        public static void PrintFolderContentWithOffset(int line)
        {
            var tempResult = "";
            var isActive = false;

            if (line + ((_page - 1) * linePerPage) <= FileSystem.dirContent.Count)
                tempResult = FileSystem.dirContent[line + ((_page - 1) * linePerPage) - 1];

            if (tempResult == FileSystem._currentFileOrDirectory)
                isActive = true;

            if (!String.IsNullOrEmpty(tempResult))
            {
                tempResult = tempResult.Remove(0, FileSystem._path.Length);
                tempResult = tempResult.TrimStart('\\');
            }

            if (tempResult.Length < Console.WindowWidth - 32)
                tempResult += new string(' ', Console.WindowWidth - 32 - tempResult.Length);

            if (tempResult.Length > Console.WindowWidth - 32)
                tempResult = tempResult.Substring(0, Console.WindowWidth - 32);

            if (isActive)
            {
                if (Worker._activeWindow == 1)
                    Console.ForegroundColor = ConsoleColor.Blue;
                else
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(">");
                Console.ForegroundColor = defaultColor;

            }
            else
            {
                Console.ForegroundColor = defaultColor;
                Console.Write(" ");
            }

            if ((line + ((_page - 1) * linePerPage) <= FileSystem.lastDirectoriesNumber))
                if (Worker._activeWindow == 1)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write(tempResult);

            Console.ForegroundColor = defaultColor;

        }

        public static string GetAttrWithOffset(string attributeName)
        {
            var temp = "";

            if (String.IsNullOrEmpty(FileSystem._currentFileOrDirectory))
                return new string(' ', 26);

            switch (attributeName)
            {
                case "atr":
                    temp = FileSystem._fileAttributes.Attributes.ToString();
                    break;
                case "ext":
                    temp = FileSystem._fileAttributes.Extension.ToString();
                    break;
                case "cre":
                    temp = FileSystem._fileAttributes.CreationTime.ToString();
                    break;
                case "lmd":
                    temp = FileSystem._fileAttributes.LastWriteTime.ToString();
                    break;
            }

            if (temp.Length < 26)
                temp += new string(' ', 26 - temp.Length);

            if (temp.Length > 26)
                temp = temp.Substring(0, 26);

            return temp;
        }
    }
}
