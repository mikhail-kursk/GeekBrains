using System;

namespace ThirdHomeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, выберите номер тестового задания 1-4");
            Console.WriteLine("Задание 1 - вывод элементов двумерного массива по диагонали");
            Console.WriteLine("Задание 2 - телефонный справочник");
            Console.WriteLine("Задание 3 - инверсия введенной строки");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Выбрано задание 1 - вывод элементов двумерного массива по диагонали");
                        Console.WriteLine("Задайте размерность массива в одном измерении");

                        int DimensionSize = 0;

                        try
                        {
                            DimensionSize = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Возникла ошибка при конвертации данных");
                            Console.WriteLine(e);
                        }

                        int[,] array = new int[DimensionSize, DimensionSize];
                        var rand = new Random();

                        string result = "";

                        Console.WriteLine("Сгенерированный массив:" + "\n");

                        for (int i = 0; i < DimensionSize; i++)
                        {
                            for (int j = 0; j < DimensionSize; j++)
                            {
                                array[i, j] = rand.Next(101);
                                result += array[i, j] + " ";
                            }
                            Console.WriteLine(result);
                            result = "";
                        }

                        Console.WriteLine("\n" + "Значения по диагонали" + "\n");

                        for (int i = 0; i < DimensionSize; i++)
                        {
                            Console.WriteLine(array[i, i]);
                        }

                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Выбрано задание 2 - телефонный справочник" + "\n");
                        Console.WriteLine("Задайте количество записей в справочние");

                        int DimensionSize = 0;

                        try
                        {
                            DimensionSize = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Возникла ошибка при конвертации данных");
                            Console.WriteLine(e);
                        }

                        string[,] array = new string[DimensionSize, 2];

                        Console.WriteLine("Заполните справочник" + "\n");

                        for (int i = 0; i < DimensionSize; i++)
                        {
                            Console.WriteLine("Введите Имя пользователя №" + (i + 1));
                            array[i, 0] = Console.ReadLine();
                            Console.WriteLine("Введите его телефон или e-mail");
                            array[i, 1] = Console.ReadLine();
                        }

                        Console.WriteLine("Справочник заполнен, выводим на экран" + "\n");

                        for (int i = 0; i < DimensionSize; i++)
                        {
                            Console.WriteLine(array[i, 0] + "     " + array[i, 1]);
                        }

                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Выбрано задание 3 - инверсия введенной строки" + "\n");

                        Console.WriteLine("Введите любую строку");

                        string str = Console.ReadLine();
                        string invStr = "";

                        for (int i = 1; i <= str.Length; i++)
                        {
                            invStr += str[str.Length - i];
                        }

                        Console.WriteLine(invStr);

                        break;
                    }
            }
        }
    }
}
