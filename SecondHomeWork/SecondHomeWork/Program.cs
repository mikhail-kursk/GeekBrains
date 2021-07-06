using System;

namespace SecondHomeWork
{
    partial class Program
    {

        public static float MidTemperature ()
        {
            float min;
            float max;

            try
            {
                Console.WriteLine("Введите минимальную температуру за сутки.");
                min = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Введите максимальную температуруза сутки.");
                max = Convert.ToSingle(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка при конвертации температуры.");
                Console.WriteLine(e);
                return Single.NaN;
            }

            return (min + max) / 2;
        }

        public static string SelectMonth()
        {
            Console.WriteLine("Введите порядковый номер месяца с 1 по 12");

            int month;

            try
            {
                month = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка при конвертации номера месяца.");
                Console.WriteLine(e);
                return "";
            }

            if (month < 1 || month > 12)
            {
                Console.WriteLine("Неверный ввод, номер месяца должен быть в диапазоне с 1 по 12");
                return "";
            }

            Months choicedMonth = (Months)month;
            return choicedMonth.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, выберите какое тестовое задание требуется запустить:" + "\n");
            Console.WriteLine("Определение средней температуры - номер 1");
            Console.WriteLine("Определение месяца по порядковому номеру - номер 2");
            Console.WriteLine("Определение четности числа - номер 3");
            Console.WriteLine("Формирование чека - номер 4");
            Console.WriteLine("Определение месяца и температуры - номер 5");
            Console.WriteLine("Формирование графики работы офисов - номер 6" + "\n");
            Console.WriteLine("Введите номер - ");

            string choice;
            (choice) = Console.ReadLine();

            Console.WriteLine("");

            switch (choice)
            {
                case "1":
                    {
                        Console.WriteLine("Выбрано определение средней температуры." + "\n");

                        float mid = MidTemperature();
                        if (!Single.IsNaN(mid))
                            Console.WriteLine("Средняя температура за сутки равна = " + mid);

                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Выбрано определение месяца по порядковому номеру." + "\n");

                        string month = SelectMonth();
                        if (month != "")
                            Console.WriteLine("Выбран " + month);

                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Выбрано определение четности числа." + "\n");

                        Console.WriteLine("Введите любое целое число");
                        int number;

                        try
                        {
                            number = Convert.ToInt32(Console.ReadLine());
                        }

                        catch (Exception e)
                        {
                            Console.WriteLine("Произошла ошибка при конвертации числа.");
                            Console.WriteLine(e);
                            break;
                        }

                        if (Math.Abs(number) % 2 == 1)
                        {
                            Console.WriteLine("Введено нечетное число.");
                        }
                        else
                        {
                            Console.WriteLine("Введено четное число.");
                        }
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Выбрано формирование чека." + "\n");

                        int numberOfPosition;

                        try
                        {
                            Console.WriteLine("Введите количество позиций в чеке");
                            numberOfPosition = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Произошла ошибка с конвертацией количества позиций в чеке");
                            Console.WriteLine(e);
                            break;
                        }

                        string[] product = new string[numberOfPosition];
                        int[] priceOfProduct = new int[numberOfPosition];
                        int[] numberOfProduct = new int[numberOfPosition];

                        for (int i = 0; i < numberOfPosition; i++)
                        {
                            Console.WriteLine("Введите название товара");
                            product[i] = Console.ReadLine();

                            try
                            {
                                Console.WriteLine("Введите стоимость за единицу товара");
                                priceOfProduct[i] = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Произошла ошибка при конвертации стоимости товара");
                                Console.WriteLine(e);
                                break;
                            }

                            try
                            {
                                Console.WriteLine("Введите количество единиц товара");
                                numberOfProduct[i] = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Произошла ошибка при конвертации количества товара");
                                Console.WriteLine(e);
                                break;
                            }
                        }


                        // Определение форматирования
                        int maxNameOfProduct = "Наименование".Length;
                        int minSeparator = 10;

                        for (int i = 0; i < numberOfPosition; i++)
                        {
                            if (maxNameOfProduct < product[i].Length)
                                maxNameOfProduct = product[i].Length;
                        }

                        string formatTitleLine = new string(' ', maxNameOfProduct + minSeparator - "Наименование".Length);


                        // Вывод чека на экран
                        if (numberOfPosition > 0)
                            Console.WriteLine("Наименование" + formatTitleLine + "Цена");

                        for (int i = 0; i < numberOfPosition; i++)
                        {
                            string currentSeparator = new string(' ', maxNameOfProduct - product[i].Length + minSeparator);
                            Console.WriteLine(product[i] + currentSeparator + (priceOfProduct[i] * numberOfProduct[i]));
                        }

                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Выбрано определение месяца и температуры.");
                        float mid = MidTemperature();
                        string month = SelectMonth();

                        if (!Single.IsNaN(mid) && month != "")
                        {
                            Console.WriteLine("Средняя температура за сутки равна = " + mid);
                            Console.WriteLine("Выбран " + month);

                            if ((month == "январь" || month == "февраль" || month == "декабрь") && mid > 0)
                                Console.WriteLine("Дождливая зима");
                        }
                        
                        break;
                    }
                case "6":
                    {
                        Console.WriteLine("Выбрано формирование графики работы офисов." + "\n");

                        Console.WriteLine("Для демонстрации будут использоваться графики работы двух офисов");
                        Console.WriteLine("Офис 1 работает во вторник и пятницу");
                        Console.WriteLine("Офис 2 работает с понедельника по субботу, кроме вторника" + "\n");

                        int firstOffice  = 0b_0100100;
                        int secondOffice = 0b_1011110;

                        int anyOffice   = firstOffice | secondOffice;
                        int bothOffices = firstOffice & secondOffice;

                        //Любой из офисов работает
                        bool monday     = (anyOffice & 0b_1000000) > 0;
                        bool tuesday    = (anyOffice & 0b_0100000) > 0;
                        bool wednesday  = (anyOffice & 0b_0010000) > 0;
                        bool thursday   = (anyOffice & 0b_0001000) > 0;
                        bool friday     = (anyOffice & 0b_0000100) > 0;
                        bool saturday   = (anyOffice & 0b_0000010) > 0;
                        bool sunday     = (anyOffice & 0b_0000001) > 0;

                        if (monday)
                            Console.WriteLine("В понедельник работает как минимум один офис");
                        if (tuesday)
                            Console.WriteLine("Во вторник работает как минимум один офис");
                        if (wednesday)
                            Console.WriteLine("В среду работает как минимум один офис");
                        if (thursday)
                            Console.WriteLine("В четверг работает как минимум один офис");
                        if (friday)
                            Console.WriteLine("В пятницу работает как минимум один офис");
                        if (saturday)
                            Console.WriteLine("В субботу работает как минимум один офис");
                        if (sunday)
                            Console.WriteLine("В воскресенье работает как минимум один офис" + "\n");

                        //Оба офиса работают
                        monday      = (bothOffices & 0b_1000000) > 0;
                        tuesday     = (bothOffices & 0b_0100000) > 0;
                        wednesday   = (bothOffices & 0b_0010000) > 0;
                        thursday    = (bothOffices & 0b_0001000) > 0;
                        friday      = (bothOffices & 0b_0000100) > 0;
                        saturday    = (bothOffices & 0b_0000010) > 0;
                        sunday      = (bothOffices & 0b_0000001) > 0;

                        if (monday)
                            Console.WriteLine("В понедельник работают оба офиса");
                        if (tuesday)
                            Console.WriteLine("Во вторник работают оба офиса");
                        if (wednesday)
                            Console.WriteLine("В среду работают оба офиса");
                        if (thursday)
                            Console.WriteLine("В четверг работают оба офиса");
                        if (friday)
                            Console.WriteLine("В пятницу работают оба офиса");
                        if (saturday)
                            Console.WriteLine("В субботу работают оба офисас");
                        if (sunday)
                            Console.WriteLine("В воскресенье работают оба офиса");

                        break;
                    }
                default:
                    Console.WriteLine("Выбран неверный номер.");
                    break;
            }
        }
    }
}
