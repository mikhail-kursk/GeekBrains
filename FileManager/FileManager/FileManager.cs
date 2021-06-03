using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{

    class FileManager
    {

        public static int _activeWindow = 1; // 0 - console, 1 - Directories 
        public static string _currentCommand = "";

        public FileManager()
        {
            // Chech app setting and if activeWindow is defined - read it
            // Chech app setting and if path is defined - read it
            // Chech app setting and if _currentFolderObjects is defined - read it
        }

        public void Process()
        {
            var exit = false;

            do
            {
                Display.RefreshAll();

                var currentChar = Console.ReadKey();

                if (_activeWindow == 0) // Console
                {
                    switch (currentChar.Key)
                    {
                        case ConsoleKey.Tab:
                            _activeWindow = 1;
                            break;

                        case ConsoleKey.Enter:
                            IntepritateUserCommand(ref exit);
                            _currentCommand = "";
                            break;

                        case ConsoleKey.Backspace:
                            if (_currentCommand.Length > 0)
                                _currentCommand = _currentCommand.Substring(0, _currentCommand.Length - 1);
                            break;

                        default:
                            _currentCommand += currentChar.KeyChar;
                            break;
                    }
                }

                else if (_activeWindow == 1) // Directories
                {
                    switch (currentChar.Key)
                    {
                        case ConsoleKey.Tab:
                            _activeWindow = 0;
                            break;

                        case ConsoleKey.UpArrow:
                            FileSystem.TryToSelectAboveElement();
                            break;

                        case ConsoleKey.DownArrow:
                            FileSystem.TryToSelectBelowElement();
                            break;

                        case ConsoleKey.PageUp:
                            FileSystem.TryToPageUp();
                            break;

                        case ConsoleKey.PageDown:
                            FileSystem.TryToPageDown();
                            break;

                        default:
                            break;
                    }
                }
            } while (!exit);
        }

        public void ReSize()
        {

        }

        public void IntepritateUserCommand(ref bool exit)
        {
            if (String.IsNullOrEmpty(_currentCommand))
                return;

            string[] command = _currentCommand.Split(' ');

            switch (command[0].ToLower())
            {
                case "cd":
                case "ls":

                    if (command.Length > 1)
                    {
                        if (Directory.Exists(command[1]))
                        {
                            FileSystem._path = command[1];
                            DisplayForms._page = 1;
                            FileSystem._currentFileOrDirectory = null;
                        }
                        else
                            Console.WriteLine("Выбранный путь не найден");
                    }
                    else
                        Console.WriteLine("Пустой путь к директории");

                    break;

                case "rm":
                case "del":
                    if (command.Length > 0)
                    {

                        // if current file is deleted select next or previous - FileSystem._currentFileOrDirectory = null;
                    }
                    else
                        Console.WriteLine("Пустой путь к удаляемому объекту");

                    break;

                case "exit":
                case "close":
                    exit = true;
                    break;

                default: return;


            }
        }
    }
}
