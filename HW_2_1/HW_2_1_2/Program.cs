using System;

namespace HW_2_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("10 число фиббоначи равно = " + FibbonaciRecursion(1, 1, 3, 10));
            Console.WriteLine("5 число фиббоначи равно = " + FibbonaciRecursion(1, 1, 3, 5));
            Console.WriteLine("20 число фиббоначи равно = " + FibbonaciRecursion(1, 1, 3, 20));
            Console.WriteLine("100 число фиббоначи равно = " + FibbonaciRecursion(1, 1, 3, 500));


            Console.WriteLine("10 число фиббоначи равно = " + FibbonaciCycle(1, 1, 3, 10));
            Console.WriteLine("5 число фиббоначи равно = " + FibbonaciCycle(1, 1, 3, 5));
            Console.WriteLine("20 число фиббоначи равно = " + FibbonaciCycle(1, 1, 3, 20));
            Console.WriteLine("100 число фиббоначи равно = " + FibbonaciCycle(1, 1, 3, 500));
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
