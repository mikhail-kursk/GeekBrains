using System;

namespace ForthHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, выберите номер тестового задания 1-4");
            Console.WriteLine("Задание 1 - объединение ФИО");
            Console.WriteLine("Задание 2 - сумма чисел");
            Console.WriteLine("Задание 3 - определение времени года");
            Console.WriteLine("Задание 4 - определение максимального числа Фиббоначи");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Выбрано задание 1 - объединение ФИО" + "\n");

                        Console.WriteLine("Введите количество пользователей для обработки");

                        int number = 0;

                        try
                        {
                            number = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Возникла ошибка при конвертации данных");
                            Console.WriteLine(e);
                            break;
                        }

                        string[] persons = new string[number];

                        for (int i = 0; i < number; i++)
                        {
                            Console.WriteLine("\n" + "Введите имя пользователя №" + (i + 1));
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Введите фамилию пользователя №" + (i + 1));
                            string lastName = Console.ReadLine();
                            Console.WriteLine("Введите отчество пользователя №" + (i + 1));
                            string patronymic = Console.ReadLine();

                            persons[i] = Persons.GetFullName(firstName, lastName, patronymic);
                        }


                        Console.WriteLine("\n" + "Все данные введены, выводим на экран" + "\n");
                        for (int i = 0; i < number; i++)
                        {
                            Console.WriteLine(persons[i]);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Выбрано задание 2 - сумма чисел" + "\n");

                        Console.WriteLine("Введите строку с числами разделенными пробелом");
                        string inputString = Console.ReadLine();

                        if (inputString == null || inputString == "")
                        {
                            Console.WriteLine("Некорректный ввод");
                            break;
                        }

                        string currentValue = "";
                        double sum = 0;
                        bool isCorrect = false;

                        for (int i = 0; i < inputString.Length; i++)
                        {
                            if (inputString[i].Equals(' '))
                            {
                                if (currentValue != "")
                                {
                                    (sum, isCorrect) = Calc.Sum(sum, ref currentValue);
                                    if (!isCorrect) break;
                                }
                            }
                            else
                            {
                                currentValue += inputString[i];
                            }    
                        }

                        // Выход из метода, если в прошлом сложении произошла ошибка конвертауии данных
                        if (!isCorrect) break;

                        // Добавляем последнее число к сумме, если в конце строки не пробел
                        (sum, isCorrect) = Calc.Sum(sum, ref currentValue);
                        
                        if (!isCorrect) break;

                        Console.WriteLine("Сумма равна = " + sum);

                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Выбрано задание 3 - определение времени года" + "\n");

                        Console.WriteLine("Введите номер месяца от 1 до 12");

                        int month = 0;
                        string season = "";

                        try
                        {
                            month = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка при конвертации данных");
                            Console.WriteLine(e);
                            break;
                        }

                        if (0 < month && month < 13)
                            season = Seasons.season(month);
                        else
                        {
                            Console.WriteLine("Неверный номер, номер должен быть от 1 до 12");
                            break;
                        }

                        season = Seasons.season_Rus(season);

                        if (season != "Не корректное значение")
                            Console.WriteLine("Сейчас " + season);

                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Выбрано задание 4 - определение максимального числа Фиббоначи" + "\n");

                        Console.WriteLine("Введите целевое целочисленное значение");

                        int targetValue = 0;
                        
                        try
                        {
                            targetValue = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Ошибка при конвертации данных");
                            Console.WriteLine(e);
                            break;
                        }

                        if (targetValue >= 0)
                            Console.WriteLine(Calc.Fibbonaci(1, 0, targetValue));

                        break;
                    }
            }
        }
    }
}
