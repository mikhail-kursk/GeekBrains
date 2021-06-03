using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{

    class FileSystem
    {

        public static string _path = "D://Работа";
        public static List<string> directories = new List<string>();
        public static List<string> files = new List<string>();
        public static List<string> dirContent = new List<string>();
        public static FileInfo _fileAttributes;
        public static string _currentFileOrDirectory = null;
        public static int lastDirectoriesNumber = 0;

        public FileSystem()
        {
        }

        public static void ReCollectDirectory()
        {
            directories.Clear();
            files.Clear();
            dirContent.Clear();

            foreach (var entity in Directory.GetFileSystemEntries(_path))
            {
                if ((File.GetAttributes(entity) & FileAttributes.Directory) == FileAttributes.Directory)
                    directories.Add(entity);
                else
                    files.Add(entity);
            }

            directories.Sort();
            files.Sort();

            dirContent.AddRange(directories);
            dirContent.AddRange(files);

            lastDirectoriesNumber = directories.Count;
        }

        public static void TryToSelectAboveElement()
        {

            // Если раньше не был определен выбираем верхний из доступных
            if (String.IsNullOrEmpty(_currentFileOrDirectory))
                _currentFileOrDirectory = dirContent[(DisplayForms._page - 1) * DisplayForms.linePerPage];

            else
            {
                for (var i = dirContent.Count; i > 1; i--)
                {
                    var element = dirContent[i - 1];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        if ((i == (((DisplayForms._page - 1) * DisplayForms.linePerPage) + 1)))
                            TryToPageUp();

                        _currentFileOrDirectory = dirContent[i - 2];
                        break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(_currentFileOrDirectory))
                _fileAttributes = new FileInfo(_currentFileOrDirectory);
        }

        public static void TryToSelectBelowElement()
        {
            // Если раньше не был определен выбираем самый нижний
            if (String.IsNullOrEmpty(_currentFileOrDirectory))
                _currentFileOrDirectory = dirContent[(DisplayForms._page - 1) * DisplayForms.linePerPage + DisplayForms.linePerPage - 1];

            else
            {
                for (var i = 0; i < dirContent.Count - 1; i++)
                {
                    var element = dirContent[i];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        if (i == ((DisplayForms._page * DisplayForms.linePerPage) - 1 ))
                            TryToPageDown();

                        _currentFileOrDirectory = dirContent[i + 1];
                        break;
                    }

                }
            }

            if (!String.IsNullOrEmpty(_currentFileOrDirectory))
                _fileAttributes = new FileInfo(_currentFileOrDirectory);

        }

        public static void TryToPageUp()
        {
            if (DisplayForms._page > 1)
            {
                DisplayForms._page--;
                _currentFileOrDirectory = null;
            }
        }

        public static void TryToPageDown()
        {
            if (directories.Count + files.Count > DisplayForms._page * DisplayForms.linePerPage)
            {
                DisplayForms._page++;
                _currentFileOrDirectory = null;
            }
        }

    }


}
