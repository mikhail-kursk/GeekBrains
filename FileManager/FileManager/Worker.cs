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
        public static List<string> previousCommand = new List<string>();
        public static int numPrevCommand = 0;

        public Worker()
        {
            // Чтение настроек из файла конфигурации
            SettingIO.TryToReadSetting("path", out var path);
            FileSystem._path = "C:\\";
            if (!String.IsNullOrEmpty(path) && (Directory.Exists(path) || File.Exists(path)))
                FileSystem._path = path;

            SettingIO.TryToReadSetting("currentObject", out var currentObject);
            FileSystem._currentFileOrDirectory = null;
            if (!String.IsNullOrEmpty(currentObject) && (File.Exists(currentObject) || Directory.Exists(currentObject)))
            {
                FileSystem._currentFileOrDirectory = currentObject;
                FileSystem._fileAttributes = new FileInfo(FileSystem._currentFileOrDirectory);
            }

            SettingIO.TryToReadSetting("objectPerPage", out var objectPerPage);
            DisplayForms.linePerPage = 8;
            if (!String.IsNullOrEmpty(objectPerPage) && Int32.TryParse(objectPerPage, out int number) && number > 8 && number < 40)
                DisplayForms.linePerPage = number;

            SettingIO.TryToReadSetting("page", out var page);
            DisplayForms._page = 1;
            if (!String.IsNullOrEmpty(page) && Int32.TryParse(page, out int temppage) && temppage > 0)
                DisplayForms._page = temppage;

            // Check or create catalog for errors
            if (!Directory.Exists(Environment.CurrentDirectory + "\\errors"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\errors");
        }

        public void Process()
        {
            var exit = false;

            Display.RefreshDirectories();
            Display.RefreshConsole();

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
                            previousCommand.Add(_currentCommand);
                            numPrevCommand = 0;
                            _currentCommand = "";
                            break;

                        case ConsoleKey.Backspace:
                            if (_currentCommand.Length > 0)
                                _currentCommand = _currentCommand.Substring(0, _currentCommand.Length - 1);
                            break;

                        case ConsoleKey.UpArrow:
                            if (previousCommand.Count >= numPrevCommand + 1)
                            {
                                numPrevCommand++;
                                _currentCommand = previousCommand[previousCommand.Count - numPrevCommand];
                            }
                            break;

                        case ConsoleKey.DownArrow:
                            if (previousCommand.Count > 0)
                                if (numPrevCommand > 1)
                                {
                                    numPrevCommand--;
                                    _currentCommand = previousCommand[previousCommand.Count - numPrevCommand];
                                }
                                else
                                    _currentCommand = previousCommand[previousCommand.Count - 1];
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

                        case ConsoleKey.Enter:
                            FileSystem.OpenFileOrDirectory();
                            break;

                        case ConsoleKey.Backspace:
                            FileSystem.TryGoUpperDirectory();
                            break;

                        default:
                            break;
                    }
                }
            } while (!exit);

            // Save current state
            SettingIO.AddUpdateAppSettings("activeWindow", _activeWindow.ToString());
            SettingIO.AddUpdateAppSettings("path", FileSystem._path);
            SettingIO.AddUpdateAppSettings("currentObject", FileSystem._currentFileOrDirectory);
            SettingIO.AddUpdateAppSettings("objectPerPage", DisplayForms.linePerPage.ToString());
            SettingIO.AddUpdateAppSettings("page", DisplayForms._page.ToString());

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
                            DisplayForms.userFriendlyErrors.Add("Выбранный путь не найден");
                    }
                    else
                        DisplayForms.userFriendlyErrors.Add("Пустой путь к директории");

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
                                try
                                {
                                    File.Delete(tempPath);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                                }
                            }

                            if (Directory.Exists(tempPath))
                            {
                                // рекурсивное удаление папки
                                try
                                {
                                    DeleteDirectoriesRecursion(tempPath);
                                    Directory.Delete(tempPath);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                                }
                            }
                            else
                            {
                                DisplayForms.userFriendlyErrors.Add("Не корректный путь");
                                break;
                            }

                            Display.RefreshDirectories();

                            // #TODO - if current file is deleted select next or previous - FileSystem._currentFileOrDirectory = null;
                        }

                        else
                            DisplayForms.userFriendlyErrors.Add("Пустой путь к удаляемому объекту");
                    }

                    break;

                case "cp":
                    if (command.Length > 0)
                    {
                        if (command.Length > 2)
                        {
                            var tempPath = _currentCommand.Remove(0, 2).TrimStart(' ');

                            // Собираем информацию о пути откуда копируем и куда
                            string SourcePath;
                            string TargetPath;

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
                                DisplayForms.userFriendlyErrors.Add("Неверная команда, вводите команду в формате cp C:\\source.txt D:\\target.txt для файлов. Для копирования директорий вводите без разрешения");
                                DisplayForms.userFriendlyErrors.Add("Если пути содержат пробелы, то указываыйте их в формате cp 'C:\\source.txt' 'D:\\target.txt'");
                                break;
                            }

                            // check source path

                            if (!(Directory.Exists(SourcePath) ^ File.Exists(SourcePath)))
                            {
                                DisplayForms.userFriendlyErrors.Add("Неверный путь к объекту копирования");
                                break;
                            }

                            // Check and prepare target path

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
                                TargetPathDir = path[0] + '\\';


                            if (File.Exists(SourcePath))
                            {
                                // Is file - simple copy
                                if (!(Directory.Exists(TargetPathDir)))
                                    Directory.CreateDirectory(TargetPathDir);
                                try
                                {
                                    File.Copy(SourcePath, TargetPath);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                                }
                            }

                            else if (Directory.Exists(SourcePath))
                            {
                                // Is Directory - copy with recursion
                                DirectoryInfo diSource = new DirectoryInfo(SourcePath);
                                DirectoryInfo diTarget = new DirectoryInfo(TargetPath);
                                try
                                {
                                    CopyAllRecursion(diSource, diTarget);
                                }
                                catch (Exception e)
                                {
                                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                                }
                            }
                            else
                            {
                                DisplayForms.userFriendlyErrors.Add("Не корректный путь");
                                break;
                            }
                            Display.RefreshDirectories();
                        }

                        else
                            DisplayForms.userFriendlyErrors.Add("Неверная команда, вводите команду в формате cp C:\\source.txt D:\\target.txt для файлов. Для копирования директорий вводите без разрешения");
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
