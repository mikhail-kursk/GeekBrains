using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{

    class Worker
    {

        public static int _activeWindow = 1; // 0 - console, 1 - Directories 
        public static string _currentCommand = "";

        public Worker()
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
                if (_activeWindow == 1)
                    Display.RefreshDirectories();
                else
                    Display.RefreshConsole();

                var currentChar = Console.ReadKey();

                if (_activeWindow == 0) // Console
                {
                    switch (currentChar.Key)
                    {
                        case ConsoleKey.Tab:
                            _activeWindow = 1;
                            Display.RefreshConsole();
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
                            Display.RefreshDirectories();
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
                        var tempPath = _currentCommand.Remove(0, 2).TrimStart(' ');
                        if (Directory.Exists(tempPath))
                        {
                            if (!(tempPath.Length == 2 && tempPath[0] == '.' && tempPath[1] == '.'))
                            {
                                FileSystem._path = _currentCommand.Remove(0, 2).TrimStart(' ');
                                DisplayForms._page = 1;
                                FileSystem._currentFileOrDirectory = null;
                            }
                            else
                            {
                                //# todo try - catch
                                string[] path = FileSystem._path.Split('\\', '/');
                                FileSystem._path = "";

                                if (path.Length > 2)
                                {

                                    for (int i = 0; i < path.Length - 2; i++)
                                    {
                                        FileSystem._path += path[i] + '\\';
                                    }
                                    FileSystem._path += path[path.Length - 2];
                                }
                                else
                                    FileSystem._path = path[0] + '\\';
                            }
                        }
                        else
                            Console.WriteLine("Выбранный путь не найден");
                    }
                    else
                        Console.WriteLine("Пустой путь к директории");

                    Display.RefreshAll();

                    break;

                case "rm":
                case "del":
                    if (command.Length > 0)
                    {
                        if (command.Length > 1)
                        {
                            var tempPath = _currentCommand.Remove(0, 2).TrimStart(' ');

                            // #Todo try - catch
                            if (File.Exists(tempPath))
                            {
                                // Удаление файла   
                                File.Delete(tempPath);
                            }

                            if (Directory.Exists(tempPath))
                            {
                                // рекурсивное удаление папки
                                DeleteDirectoriesRecursion(tempPath);
                                Directory.Delete(tempPath);
                            }
                            else
                            {
                                Console.WriteLine("Не корректный путь");
                                break;
                            }

                            Display.RefreshDirectories();

                            // if current file is deleted select next or previous - FileSystem._currentFileOrDirectory = null;
                        }

                        else
                            Console.WriteLine("Пустой путь к удаляемому объекту");
                    }

                    break;

                case "cp":
                    if (command.Length > 0)
                    {
                        if (command.Length > 2)
                        {
                            var tempPath = _currentCommand.Remove(0, 2).TrimStart(' ');

                            // Собираем информацию о пути откуда копируем и куда
                            string SourcePath = "";
                            string TargetPath = "";

                            if (tempPath.Split(' ').Length == 2)
                            {
                                SourcePath = tempPath.Split(' ')[0].TrimStart('\'').TrimEnd('\'');
                                TargetPath = tempPath.Split(' ')[1].TrimStart('\'').TrimEnd('\'');
                            }
                            else if (tempPath.Split('\'').Length == 5)
                            {
                                SourcePath = tempPath.Split('\'')[1].TrimStart('\'').TrimEnd('\'');
                                TargetPath = tempPath.Split('\'')[3].TrimStart('\'').TrimEnd('\'');
                            }
                            else
                            {
                                Console.WriteLine("Неверная команда, вводите команду в формате cp C:\\source.txt D:\\target.txt для файлов. Для копирования директорий вводите без разрешения");
                                Console.WriteLine("Если пути содержат пробелы, то указываыйте их в формате cp 'C:\\source.txt' 'D:\\target.txt'");
                                break;
                            }

                            // Проверить путь откуда копируем

                            if (!(Directory.Exists(SourcePath) ^ File.Exists(SourcePath)))
                            {
                                Console.WriteLine("Неверный файл к объекту копирорвания");
                            }

                            // Проверить путь куда копируем - существующая директория или создать

                            string[] path = TargetPath.Split('\\', '/');
                            string TargetPathDir = "";

                            if (path.Length > 2)
                            {

                                for (int i = 0; i < path.Length - 2; i++)
                                {
                                    TargetPathDir += path[i] + '\\';
                                }
                                TargetPathDir += path[path.Length - 2];
                            }
                            else
                                TargetPathDir = path[0] +'\\';

                            // Файл или директория копируется
                            // #Todo try - catch

                            if (File.Exists(SourcePath))
                            {
                                if (!(Directory.Exists(TargetPathDir)))
                                    Directory.CreateDirectory(TargetPathDir);
                                
                                // #Todo try - catch
                                File.Copy(SourcePath, TargetPath);
                            }

                            else if (Directory.Exists(SourcePath))
                            {
                                // Директория - рекурсивное копирование
                                DirectoryInfo diSource = new DirectoryInfo(SourcePath);
                                DirectoryInfo diTarget = new DirectoryInfo(TargetPath);
                                CopyAllRecursion(diSource, diTarget);
                            }
                            else
                            {
                                Console.WriteLine("Не корректный путь");
                                break;
                            }

                            /*



                            */
                            Display.RefreshDirectories();

                            // if current file is deleted select next or previous - FileSystem._currentFileOrDirectory = null;
                        }

                        else
                            Console.WriteLine("Неверная команда, вводите команду в формате cp C:\\source.txt D:\\target.txt для файлов. Для копирования директорий вводите без разрешения");
                    }

                    break;

                case "exit":
                case "close":
                    exit = true;
                    break;

                default: return;


            }
        }

        public void DeleteDirectoriesRecursion(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
                File.Delete(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(path);
            foreach (string subdirectory in subdirectoryEntries)
            {
                DeleteDirectoriesRecursion(subdirectory);
                Directory.Delete(subdirectory);
            }
        }

        public void CopyAllRecursion(DirectoryInfo source, DirectoryInfo target)
        {
            var files = source.GetFiles();
            var directories = source.GetDirectories();

            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in files)
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in directories)
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAllRecursion(diSourceSubDir, nextTargetSubDir);
            }
        }

    }
}
