using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FifthHomeWork
{

    public class ToDoList
    {

        public string Name;
        List<ToDo> Tasks = new List<ToDo> ();

        public ToDoList(string name)
        {
            if (!String.IsNullOrEmpty(name))
                Name = name;
            else
            {
                Console.WriteLine("Неверное имя файла");
            }
        }


        public bool ChangeToDoListName (string newName)
        {
            if (String.IsNullOrEmpty(newName) || !Directory.Exists(newName))
            {
                Console.WriteLine("Неверный путь");
                return false;
            }

            Name = newName;
            return true;
        }

        public (string, bool) GetTaskList ()
        {
            if (!String.IsNullOrEmpty(Name))
                if(Tasks.Count > 0)
                    foreach (ToDo task in Tasks)
                    {
                        Console.WriteLine(task.GetTaskNameAndStatus());
                        return task.GetTaskNameAndStatus();
                    }

            return ( "" , false );

        }
        public string GetActiveTaskList ()
        {
            string result = "";

            if (Tasks.Count > 0)
                foreach (ToDo task in Tasks)
                {
                    var tempTask = task.GetTaskNameAndStatus();
                    if (!tempTask.Item2)
                        result += tempTask.Item1 + "     " + tempTask.Item2;
                }

            return result;
        }
        public string GetDoneTaskList ()
        {
            string result = "";

            if (Tasks.Count > 0)
                foreach (ToDo task in Tasks)
                {
                    var tempTask = task.GetTaskNameAndStatus();
                    if (tempTask.Item2)
                        result += tempTask.Item1 + "     " + tempTask.Item2;
                }

            return result;
        }
        public bool NewTask(string name)
        {
            if (String.IsNullOrEmpty(name))
                return false;

            ToDo newTask = new ToDo(name);
            Tasks.Add(newTask);

            return true;
        }

        public bool ChangeTaskTitle(int number, string newTaskName)
        {
            bool result = Tasks[number].ChangeTitle(newTaskName);
            if (!result)
            {
                Console.WriteLine("Не удалось переименовать задачу");
                return false;
            }

            return true;
        }

        public bool MarkTaskAsDone(int number)
        {
            bool result = Tasks[number].MarkTaskAsDone();
            if (!result)
            {
                Console.WriteLine("Не удалось изменить статус задачи");
                return false;
            }

            return true;
        }



    }

    public class ToDo
    {
        private string Title;
        private bool IsDone = false;

        public ToDo(string title)
        {
            Title = title;
        }

        public bool ChangeTitle (string newTitle)
        {
            if (!String.IsNullOrEmpty(newTitle))
            {
                Title = newTitle;
                return true;
            }

            return false;
        }

        public (string, bool) GetTaskNameAndStatus()
        {
            return (Title, IsDone);
        }

        public string GetTaskNameAndStatusString()
        {
            return Title + "     " + IsDone;
        }

        public string GetTaskName()
        {
            return Title;
        }

        public bool MarkTaskAsDone()
        {
            IsDone = true;
            return true;
        }

        public bool MarkTaskAsNotDone()
        {
            IsDone = false;
            return true;
        }
    }
}
