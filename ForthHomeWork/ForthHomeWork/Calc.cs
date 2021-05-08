using System;
using System.Collections.Generic;
using System.Text;

namespace ForthHomeWork
{
    class Calc
    {
        public static (double, bool) Sum(double sum, ref string currentValue)
        {
            try
            {
                sum += Convert.ToInt32(currentValue);
                currentValue = "";
            }
            catch (Exception e)
            {
                Console.WriteLine("Возникла ошибка при конвертации данных");
                Console.WriteLine(e);
                return (0.0, false);
            }

            return (sum, true);
        }

        public static int Fibbonaci(int currentValue, int previousValue, int targetValue)
        {
            int temp = 0;
            if (currentValue < targetValue)
            {
                temp = currentValue;
                currentValue += previousValue;
                currentValue = Fibbonaci(currentValue, temp, targetValue);
            }

            if (currentValue > targetValue)
                return previousValue;

            return currentValue;


        }
    }
}
