using System;
using System.IO;

namespace FifthHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, выберите номер тестового задания 1-4");
            Console.WriteLine("Задание 1 - ввод с клавиатуры с сохранением в файл");
            Console.WriteLine("Задание 2 - дописать текущее время в файл");
            Console.WriteLine("Задание 3 - ввод с клавиатуры и сохранение в бинарный файл");
            Console.WriteLine("Задание 4 - дерево каталогов с сохранением в файл");
            Console.WriteLine("Задание 5 - дерево каталогов с сохранением в файл - рекурсия");
            Console.WriteLine("Задание 6 - ToDo list");

            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Выбрано задание 1 - ввод с клавиатуры с сохранением в файл" + "\n");

                        Console.WriteLine("Введите данные для сохранения в файл text.txt");

                        string str1 = Console.ReadLine();
                        string filename = "text.txt";

                        File.WriteAllText(filename, str1);

                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Выбрано задание 2 - дописать текущее время в файл startup.txt" + "\n");

                        string filename = "startup.txt";
                        File.AppendAllText(filename, "\n" + Convert.ToString(DateTime.Now));

                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Выбрано задание 3 - ввод с клавиатуры и сохранение в бинарный файл" + "\n");

                        Console.WriteLine("Введите число от 0 до 255 для сохранения в файл binary.bin");

                        string str1 = Console.ReadLine();
                        byte[] byteArray = new byte[1] { 0 };

                        try
                        {
                            byteArray[0] = Convert.ToByte(str1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Произошла ошибка при конвертации данных");
                            Console.WriteLine(e);
                            break;
                        }

                        string filename = "binary.bin";

                        File.WriteAllBytes(filename, byteArray);

                        break;
                    }

                case "4":
                    {
                        Console.WriteLine("Выбрано задание 4 - дерево каталогов с сохранением в файл" + "\n");

                        Console.WriteLine("Выберите путь");

                        string str1 = Console.ReadLine();

                        bool isExist = Directory.Exists(str1);
                        
                        if (!isExist)
                        {
                            Console.WriteLine("Выбранный путь не существует");
                            break;
                        }    




                        break;
                    }

                case "6":
                    {
                        Console.WriteLine("Выбрано задание 5 - ToDo list" + "\n");

                        Console.WriteLine("Выберите путь к папке с ToDo листом" + "\n");
                        string path = Console.ReadLine();

                        bool isExist = Directory.Exists(path);
                        if (!isExist)
                        {
                            Console.WriteLine("Выбранный путь не существует");
                            break;
                        }

                        Console.WriteLine("Переходим в каталог" + path + "\n");

                        if (!(Directory.GetFiles(path, "*.tsk").Length > 1))
                        {
                            Console.WriteLine("Не найдено ни одного списка задач");
                            Console.WriteLine("Для создания нового списка задач введите его имя, для выхода оставьте строку пустой");
                            var createChoice = Console.ReadLine();
                            
                            if (String.IsNullOrEmpty(createChoice)) {return;}

                            createChoice += ".tsk";
                            File.Create(path + "\\" + createChoice);

                        }
                        else
                        {
                            Console.WriteLine("Существуюшие списки задач:");
                            foreach (var taskList in Directory.GetFiles(path, "*.tsk"))
                            {
                                Console.WriteLine(taskList);
                            }


                        }



                        do
                        {
                            bool exit = false;
                                                        
                            


                            string name = Console.ReadLine();

                            ToDoList list1 = new ToDoList(path + name);

                            list1.NewTask("Задача 1");

                            Console.WriteLine(list1.GetActiveTaskList());
                            list1.ChangeTaskTitle(0, "Задача переименованна");
                            Console.WriteLine(list1.GetActiveTaskList());
                            list1.MarkTaskAsDone(0);

                            Console.WriteLine("Список активных задач");
                            Console.WriteLine(list1.GetActiveTaskList());

                            Console.WriteLine("Список выполненных задач");
                            Console.WriteLine(list1.GetDoneTaskList());


                        while (exit)

                        break;
                    }

                default:
                    {
                        Console.WriteLine("Неверный ввод - введите число от 1 до 6");
                        break;
                    }
            }
        }
    }
}
