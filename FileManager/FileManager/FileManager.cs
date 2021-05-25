using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{

    class FileManager
    {

        private string[] _currentFolderObjects;

        public FileManager()
        {

        }

        public void Process()
        {
            var exit = false;

            Display.RefreshAll();

            do
            {
                string inputCommand = Console.ReadLine();
                if (String.IsNullOrEmpty(inputCommand))
                    continue;

                string[] command = inputCommand.Split(' ');

                switch (command[0].ToLower())
                {
                    case "cd":
                    case "ls":

                        if (command.Length > 1)
                        {
                            if (Directory.Exists(command[1]))
                            {
                                Display._path = command[1];
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

                        }
                        else
                            Console.WriteLine("Пустой путь к удаляемому объекту");

                        break;

                    case "exit":
                    case "close":
                        exit = true;
                        break;

                    default: continue;


                }

                Display.RefreshAll();

            } while (!exit);


        }
    }
}
