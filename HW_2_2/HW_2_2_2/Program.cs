using System;

namespace HW_2_2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[11] {0, 14, 3, 25, 1, 9, 71, 59, 6, 66, 88};
            Array.Sort(array);

            // 0 14 3 25 1 9 71 59 6 66 88      до сортировки
            // 0 1 3 6 9 14 25 59 66 71 88      после сортировки

            if (BinarySearch(array, 25) == 6)
                Console.WriteLine("Позиция значения 25 в массиве = 6 - верно");
            else
                Console.WriteLine("Позиция значения 25 в массиве = 6 - не верно");

            if (BinarySearch(array, 71) == 9)
                Console.WriteLine("Позиция значения 71 в массиве = 9 - верно");
            else
                Console.WriteLine("Позиция значения 71 в массиве = 9 - не верно");

            if (BinarySearch(array, 33) == 4)
                Console.WriteLine("Позиция значения 33 в массиве = 4 - верно");
            else
                Console.WriteLine("Позиция значения 33 в массиве = 4 - не верно");
        }

        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid])
                {
                    return mid;
                }
                else if (searchValue < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }
    }
}
