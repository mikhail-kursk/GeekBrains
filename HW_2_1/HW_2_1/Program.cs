using System;

namespace HW_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(1, "Простое");
            Test(10, "Не простое");
            Test(7, "Простое");
            Test(6, "Простое");
            Test(144, "Простое");
        }

        static void Test(int number, string value)
        {
            if (СheckEqual(isSimple(number), value))
                Console.WriteLine($"Утверждение, что число {number} {value} - верно");
            else
                Console.WriteLine($"Утверждение, что число { number} { value} - не верно");
        }
        static bool СheckEqual(string arg1, string arg2)
        {
            if (String.Compare(arg1, arg2) >= 0)
                return true;
            else
                return false;
        }

        static string isSimple(int n)
        {
            int d = 0;
            int i = 2;


            while (i < n)
            {
                if ((n % i) == 0)
                    d++;
                i++;
            }

            if (d == 0)
                return "Простое";
            else
                return "Не простое";
        }
    }
}
