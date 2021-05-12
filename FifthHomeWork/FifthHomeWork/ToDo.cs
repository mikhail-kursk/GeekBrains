using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FifthHomeWork
{

    public class ToDoList
    {

        public string _name;
        List<ToDo> Tasks = new List<ToDo> ();

        public ToDoList(string name)
        {
            _name = name;
        }

        public void ChangeToDoListName (string newName)
        {
            if (String.IsNullOrEmpty(newName))
            {
                Console.WriteLine("Нельзя ввести пустое имя");
                return;
            }

            _name = newName;
            return;
        }

        public void GetTaskList ()
        {
            if (!String.IsNullOrEmpty(_name))
                if (Tasks.Count > 0)
                {
                    int i = 1;
                    foreach (ToDo task in Tasks)
                    {
                        var result = task.GetTaskNameAndStatus();
                        Console.WriteLine($"{i}\t{result.Item1}\t{result.Item2}");
                        i++;
                    } 
                }
            return;
        }

        public void GetActiveTaskList ()
        {
            if (Tasks.Count > 0)
            {
                int i = 1;
                foreach (ToDo task in Tasks)
                {
                    var tempTask = task.GetTaskNameAndStatus();
                    if (!tempTask.Item2)
                        Console.WriteLine($"{i}\t{tempTask.Item1}\t{tempTask.Item2}");
                    i++;
                }
            }
            return;
        }

        public void GetDoneTaskList ()
        {
            if (Tasks.Count > 0)
            {
                int i = 1;
                foreach (ToDo task in Tasks)
                {
                    var tempTask = task.GetTaskNameAndStatus();
                    if (tempTask.Item2)
                        Console.WriteLine($"{i}\t{tempTask.Item1}\t{tempTask.Item2}");
                    i++;
                }
            }
            return ;
        }

        public void NewTask(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                Console.WriteLine("Имя задачи не может быть пустым");
                return;
            }

            ToDo newTask = new ToDo(name);
            Tasks.Add(newTask);

            return;
        }

        public void ChangeTaskTitle(int number)
        {
            if (number < 1 ||number > Tasks.Count)
            {
                Console.WriteLine("Нет задачи с таким номером");
                return;
            }

            Console.WriteLine("Введите новое имя задачи");
            var newTaskName = Console.ReadLine();

            if (String.IsNullOrEmpty(newTaskName))
            {
                Console.WriteLine("Имя не может быть пустое");
                return;
            }

            bool result = Tasks[number-1].ChangeTitle(newTaskName);
            if (!result)
            {
                Console.WriteLine("Не удалось переименовать задачу");
                return;
            }

            return;
        }

        public void MarkTaskAsDone(int number)
        {
            if (number < 1 || number > Tasks.Count)
            {
                Console.WriteLine("Нет задачи с таким номером");
                return;
            }

            bool result = Tasks[number-1].MarkTaskAsDone();
            if (!result)
            {
                Console.WriteLine("Не удалось изменить статус задачи");
                return;
            }

            return;
        }

        public void MarkTaskAsNotDone(int number)
        {
            if (number < 1 || number > Tasks.Count)
            {
                Console.WriteLine("Нет задачи с таким номером");
                return;
            }

            bool result = Tasks[number-1].MarkTaskAsNotDone();
            if (!result)
            {
                Console.WriteLine("Не удалось изменить статус задачи");
                return;
            }

            return;
        }



    }

    public class ToDo
    {
        private string _title;
        private bool _isDone = false;

        public ToDo(string title)
        {
            _title = title;
        }

        public bool ChangeTitle (string newTitle)
        {
            if (!String.IsNullOrEmpty(newTitle))
            {
                _title = newTitle;
                return true;
            }

            return false;
        }

        public (string, bool) GetTaskNameAndStatus()
        {
            return (_title, _isDone);
        }

        public string GetTaskNameAndStatusString()
        {
            return $"{_title}\t{_isDone}";
        }

        public string GetTaskName()
        {
            return _title;
        }

        public bool MarkTaskAsDone()
        {
            _isDone = true;
            return true;
        }

        public bool MarkTaskAsNotDone()
        {
            _isDone = false;
            return true;
        }
    }
}
