using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HW_2_4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = 1_000_000;  // Кол-во элементов
            var stringLen = 100;  // Длина строк для генерации

            HashSet<String> hashset = new HashSet<String>();
            String[] stringArray = new String[values];

            for (int i = 0; i < values; i++)
            {
                String randomString = GetRandomString(stringLen);

                hashset.Add(randomString);
                stringArray[i] = randomString;
            }

            String searchString_1 = "0123";
            String searchString_2 = "ABC";
            String searchString_3 = "qwerty";
            String searchString_4 = "12";
            String searchString_5 = "1";

            String[] searchStrings = new String[5] { searchString_1, searchString_2, searchString_3, searchString_4, searchString_5 };

            foreach (String srch in searchStrings)
            {
                Stopwatch timer_1 = new Stopwatch();
                Stopwatch timer_2 = new Stopwatch();

                timer_1.Start();
                hashset.Contains(srch);
                timer_1.Stop();

                timer_2.Start();
                for (int j = 0; j < values; j++)
                {
                    if (stringArray[j].Equals(srch)) break;
                }
                timer_2.Stop();

                Console.WriteLine($"Поиск в hashset = {timer_1.ElapsedMilliseconds}");
                Console.WriteLine($"Поиск в массиве = {timer_2.ElapsedMilliseconds}");
                Console.WriteLine();
            }

        }

        public static String GetRandomString(int length)
        {
            var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

            var chars = new char[length];
            var rd = new Random();

            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new String(chars);
        }
    }
}
