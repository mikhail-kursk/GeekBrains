using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace FileManager
{

    class FileSystem
    {

        public static string _path;
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

            try
            {
                foreach (var entity in Directory.GetFileSystemEntries(_path))
                {
                    if ((File.GetAttributes(entity) & FileAttributes.Directory) == FileAttributes.Directory)
                        directories.Add(entity);
                    else
                        files.Add(entity);
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
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
            if (String.IsNullOrEmpty(_currentFileOrDirectory) && dirContent.Count > 0)
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
                try
                {
                    _fileAttributes = new FileInfo(_currentFileOrDirectory);
                }
                catch (Exception e)
                {
                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                }
        }

        public static void TryToSelectBelowElement()
        {
            // Если раньше не был определен выбираем самый нижний
            if (String.IsNullOrEmpty(_currentFileOrDirectory) && dirContent.Count > 0)
                if (dirContent.Count > DisplayForms.linePerPage)
                    if (DisplayForms._page - 1 == dirContent.Count / DisplayForms.linePerPage)
                        // Последняя страница
                        _currentFileOrDirectory = dirContent[dirContent.Count - 1];
                    else
                        // не последняя страница, страниц больше 1
                        _currentFileOrDirectory = dirContent[(DisplayForms._page - 1) * DisplayForms.linePerPage + DisplayForms.linePerPage - 1];
                else
                    // Единственная страница
                    _currentFileOrDirectory = dirContent[dirContent.Count - 1];

            else
            {
                for (var i = 0; i < dirContent.Count - 1; i++)
                {
                    var element = dirContent[i];

                    if (element.Equals(_currentFileOrDirectory))
                    {
                        if (i == ((DisplayForms._page * DisplayForms.linePerPage) - 1))
                            TryToPageDown();

                        _currentFileOrDirectory = dirContent[i + 1];
                        break;
                    }

                }
            }

            if (!String.IsNullOrEmpty(_currentFileOrDirectory))
                try
                {
                    _fileAttributes = new FileInfo(_currentFileOrDirectory);
                }
                catch (Exception e)
                {
                    File.AppendAllText(Environment.CurrentDirectory + ".log", e.ToString());
                }

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
            if (dirContent.Count > DisplayForms._page * DisplayForms.linePerPage)
            {
                DisplayForms._page++;
                _currentFileOrDirectory = null;
            }
        }

        public static void OpenFileOrDirectory()
        {
            if (File.Exists(_currentFileOrDirectory))
            {
                try
                {
                    var p = new Process();
                    p.StartInfo = new ProcessStartInfo(_currentFileOrDirectory)
                    {
                        UseShellExecute = true
                    };
                    p.Start();
                }
                catch (Exception e)
                {
                    File.AppendAllText(Environment.CurrentDirectory + "\\errors\\app.log", e.ToString() + "\n\n");
                }
            }

            else if (Directory.Exists(_currentFileOrDirectory))
            {
                _path = _currentFileOrDirectory;
                _currentFileOrDirectory = null;
                DisplayForms._page = 1;
            }


        }

        public static void TryGoUpperDirectory()
        {
            string[] path = FileSystem._path.Split('\\', '/');
            _path = "";

            if (path.Length > 2)
            {

                for (int i = 0; i < path.Length - 2; i++)
                {
                    _path += path[i] + '\\';
                }
                _path += path[path.Length - 2];
            }
            else
                _path = path[0] + '\\';

            _currentFileOrDirectory = null;
            DisplayForms._page = 1;
        }

    }
}
