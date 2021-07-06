using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;

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
            Console.WriteLine("Задание 4 - ToDo list");

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
                        Console.WriteLine("Выбрано задание 5 - ToDo list" + "\n");

                        Console.WriteLine("Выберите путь к папке с ToDo листом" + "\n");
                        string path = Console.ReadLine();

                        ToDoList toDoList = new ToDoList("");

                        bool isDirectoryExist = Directory.Exists(path);
                        if (!isDirectoryExist)
                        {
                            Console.WriteLine("Выбранный путь не существует");
                            break;
                        }

                        Console.WriteLine("Переходим в каталог " + path + "\n");

                        var toDoLists = Directory.GetFiles(path, "*_tsk.json");
                        int toDoListNameInt = -1;
                        string toDoListNameString = null;

                        string toDoListName;
                        if (toDoLists.Length > 0)
                        {
                            Console.WriteLine("Существуюшие списки задач:");
                            foreach (var taskList in Directory.GetFiles(path, "*_tsk.json"))
                            {
                                Console.WriteLine(taskList);
                            }

                            Console.WriteLine("\n" + "Для открытия существующего списка задач введите его имя или номер по порядку");
                            Console.WriteLine("Для создания нового оставьте строку пустой");

                            toDoListName = Console.ReadLine();

                            if (!String.IsNullOrEmpty(toDoListName))
                            {
                                try
                                {
                                    toDoListNameInt = Convert.ToInt32(toDoListName);
                                }
                                catch { }

                                toDoListNameString = path + "\\" + toDoListName;

                                if (toDoListNameInt != -1)
                                {
                                    if (0 < toDoListNameInt && toDoListNameInt <= toDoLists.Length)
                                    {
                                        Console.WriteLine("Найден по номеру");
                                        string json = File.ReadAllText(toDoLists[toDoListNameInt - 1]);
                                        toDoList = JsonSerializer.Deserialize<ToDoList>(json);
                                    }
                                }
                                else
                                {
                                    foreach (var toDoListNameInToDoList in toDoLists)
                                    {
                                        if (toDoListNameInToDoList == toDoListNameString)
                                        {
                                            Console.WriteLine("Найден по имени");
                                            string json = File.ReadAllText(toDoListNameInToDoList);

                                            toDoList = JsonSerializer.Deserialize<ToDoList>(json);


                                        }
                                    }

                                }

                                if (String.IsNullOrEmpty(toDoList._name))
                                {
                                    Console.WriteLine("Выбранный список задач не найден");
                                    return;
                                }


                            }

                        }
                        else
                        {
                            Console.WriteLine("Не найдено ни одного списка задач");
                        }


                        if (String.IsNullOrEmpty(toDoList._name))
                        {
                            Console.WriteLine("Для создания нового списка задач введите его имя, для выхода оставьте строку пустой" + "\n");

                            toDoListName = Console.ReadLine();

                            if (String.IsNullOrEmpty(toDoListName))
                            {
                                Console.WriteLine("Невозможно создать список задач без имени");
                                return;
                            }

                            toDoList = new ToDoList(toDoListName);
                            toDoList.Tasks = new List<ToDo>();
                            toDoListNameString = path + "\\" + toDoListName;
                        }

                        var exit = true;

                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Открыт список задач " + toDoList._name);

                            Console.WriteLine("Выберите операцию над списком задач");

                            Console.WriteLine("1 - изменить имя списка задач");

                            Console.WriteLine("2 - вывести все задачи");
                            Console.WriteLine("3 - вывести все не выполненные задачи");
                            Console.WriteLine("4 - вывести все выполненные задачи");

                            Console.WriteLine("5 - создать новую задачу");
                            Console.WriteLine("6 - изменить описание задачи");
                            Console.WriteLine("7 - отметить задачу как выполненную");
                            Console.WriteLine("8 - отметить задачу как не выполненную");

                            Console.WriteLine("9 - выход" + "\n");


                            int listOptionChoice = 0;

                            try
                            {
                                listOptionChoice = Convert.ToInt32(Console.ReadLine());
                            }
                            catch
                            {
                                Console.WriteLine("Неверный ввод - введите цифру от 1 до 9");
                                continue;
                            }

                            int taskNumber = 0;

                            Console.WriteLine();

                            switch (listOptionChoice)
                            {
                                case 1:
                                    Console.WriteLine("Введите новое название списка задач");
                                    toDoList.ChangeToDoListName(Console.ReadLine());
                                    break;

                                case 2:
                                    toDoList.GetTaskList();
                                    break;

                                case 3:
                                    toDoList.GetActiveTaskList();
                                    break;

                                case 4:
                                    toDoList.GetDoneTaskList();
                                    break;

                                case 5:
                                    Console.WriteLine("Введите название задачи");
                                    toDoList.NewTask(Console.ReadLine());
                                    break;

                                case 6:
                                    Console.WriteLine("Выберите номер задачи для изменения" + "\n");

                                    toDoList.GetTaskList();

                                    try
                                    {
                                        taskNumber = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Необходимо ввести номер");
                                        Console.WriteLine("Возврат в меню");
                                    }

                                    toDoList.ChangeTaskTitle(taskNumber);
                                    break;

                                case 7:
                                    Console.WriteLine("Выберите номер задачи для отметки как сделанная" + "\n");

                                    toDoList.GetTaskList();

                                    try
                                    {
                                        taskNumber = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Необходимо ввести номер");
                                        Console.WriteLine("Возврат в меню");
                                    }

                                    toDoList.MarkTaskAsDone(taskNumber);
                                    break;

                                case 8:
                                    Console.WriteLine("Выберите номер задачи для отметки как не сделанная" + "\n");

                                    toDoList.GetTaskList();

                                    try
                                    {
                                        taskNumber = Convert.ToInt32(Console.ReadLine());
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Необходимо ввести номер");
                                        Console.WriteLine("Возврат в меню");
                                    }

                                    toDoList.MarkTaskAsNotDone(taskNumber);
                                    break;

                                case 9:
                                    exit = false;


                                    string temp = JsonSerializer.Serialize(toDoList);

                                    File.WriteAllText(toDoListNameString + "_tsk.json", JsonSerializer.Serialize(toDoList));
                                    break;
                            }

                            Console.WriteLine("\n" + "Для продолжения нажмите любую кнопку");
                            Console.ReadLine();


                        } while (exit);

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

