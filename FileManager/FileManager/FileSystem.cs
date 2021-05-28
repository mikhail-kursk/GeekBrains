using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{

    class FileSystem
    {

        public static string _path = "C://";
        public static List<string> directories = new List<string>();
        public static List<string> files = new List<string>();
        public static FileInfo _fileAttributes;
        public static string _currentFileOrDirectory = null;

        public FileSystem()
        {
        }

        public static void ReCollectDirectory()
        {
            directories.Clear();
            files.Clear();

            foreach (var entity in Directory.GetFileSystemEntries(_path))
            {
                if ((File.GetAttributes(entity) & FileAttributes.Directory) == FileAttributes.Directory)
                    directories.Add(entity);
                else
                    files.Add(entity);
            }

            directories.Sort();
            files.Sort();
        }

        public static void TryToSelectAboveElement()
        {
            bool isFounded = false;

            // Если раньше не был определен выбираем верхний из доступных
            if (String.IsNullOrEmpty(_currentFileOrDirectory))
            {
                if (directories.Count > 0)
                    _currentFileOrDirectory = directories[0];
                else if (files.Count > 0)
                    _currentFileOrDirectory = files[0];
            }

            // поиск в файлах
            else
            {
                for (var i = files.Count; i > 1; i--)
                {
                    var element = files[i - 1];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        _currentFileOrDirectory = files[i - 2];
                        isFounded = true;
                        break;
                    }
                }
            }

            // переход с файлов на директории
            if (files.Count > 0)
                if ((!isFounded) && (files[0].Equals(_currentFileOrDirectory)))
                {
                    if (directories.Count > 0)
                    {
                        _currentFileOrDirectory = directories[directories.Count - 1];
                        isFounded = true;
                    }
                }

            // поиск в директориях
            if (!isFounded)
            {
                for (var i = directories.Count; i > 1; i--)
                {
                    var element = directories[i - 1];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        _currentFileOrDirectory = directories[i - 2];
                        break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(_currentFileOrDirectory))
                _fileAttributes = new FileInfo(_currentFileOrDirectory);
        }

        public static void TryToSelectBelowElement()
        {
            bool isFounded = false;

            // Если раньше не был определен выбираем самый нижний
            if (String.IsNullOrEmpty(_currentFileOrDirectory))
            {
                if (files.Count > 0)
                    _currentFileOrDirectory = files[files.Count - 1];
                else if (directories.Count > 0)
                    _currentFileOrDirectory = directories[0];
            }

            else
            {
                // Ищем в директориях
                for (var i = 0; i < directories.Count - 1; i++)
                {
                    var element = directories[i];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        _currentFileOrDirectory = directories[i + 1];
                        isFounded = true;
                        break;
                    }

                }

                // Переход с директорий в файлы
                if (directories.Count > 0)
                    if ((!isFounded) && (directories[directories.Count - 1].Equals(_currentFileOrDirectory)))
                    {
                        if (files.Count > 0)
                        {
                            _currentFileOrDirectory = files[0];
                            isFounded = true;
                        }
                    }

                // Ищем в файлах
                if (!isFounded)
                {
                    for (var i = 0; i < files.Count - 1; i++)
                    {
                        var element = files[i];

                        if (element.Equals(_currentFileOrDirectory))
                        {
                            _currentFileOrDirectory = files[i + 1];
                            break;
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(_currentFileOrDirectory))
                _fileAttributes = new FileInfo(_currentFileOrDirectory);

        }

        public static void TryToPageUp ()
        {
            if (directories.Count + files.Count > DisplayForms._page * DisplayForms.linePerPage)
            {
                DisplayForms._page++;
                _currentFileOrDirectory = null;
            }
        }

        public static void TryToPageDown()
        {
            if (DisplayForms._page > 1)
            {
                DisplayForms._page--;
                _currentFileOrDirectory = null;
            }
        }

    }


}
