using System;

namespace HW_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 " + isSimple(1));
            Console.WriteLine("10 " + isSimple(10));
            Console.WriteLine("7 " + isSimple(7));
            Console.WriteLine("6 " + isSimple(6));
            Console.WriteLine("144 " + isSimple(144));

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
