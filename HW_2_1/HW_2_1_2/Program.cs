using System;

namespace HW_2_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Проверяем рассчет с рекурсией");

            Test (10, FibbonaciRecursion(1, 1, 3, 10), 55);
            Test(5, 7, 5);
            Test(20, 6722, 6765);
            Test(100, FibbonaciRecursion(1, 1, 3, 100), 354224848179262000000.0);
            Test(40, FibbonaciRecursion(1, 1, 3, 40), 102334155);

            Console.WriteLine("\nПроверяем рассчет с циклом");

            Test(10, 50, 55);
            Test(5, FibbonaciCycle(1, 1, 3, 5), 5);
            Test(20, FibbonaciCycle(1, 1, 3, 20), 6765);
            Test(100, 354224848179272000000.0, 354224848179262000000.0);
            Test(40, FibbonaciCycle(1, 1, 3, 40), 102334155);
        }

        static void Test(int number, double value1, double value2)
        {
            if (value1 == value2)
                Console.WriteLine($" {number} число Фиббоначи равно {value1} - верно");
            else
                Console.WriteLine($" {number} число Фиббоначи равно {value1} - не верно, оно равно {value2}");
        }

        static double FibbonaciRecursion(double current, double prev, int number, int targetNumber)
        {
            double newValue = current + prev;

            if (number == targetNumber)
                return newValue;

            return newValue = FibbonaciRecursion(newValue, current, number + 1, targetNumber);
        }

        static double FibbonaciCycle(double current, double prev, int number, int targetNumber)
        {
            double result = 0;

            for (int i = number; i <= targetNumber; i++)
            {
                result = current + prev;
                
                prev = current;
                current = result;
            }

            return result;
        }
    }
}
