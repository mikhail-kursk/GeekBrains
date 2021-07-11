using System;
using System.Diagnostics;
using System.Threading;

namespace HW_2_3
{
    class PointClassFC
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    struct PointStructFS
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    struct PointStructDS
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int number = 100; // Количество проверок
            int multiplier = 1_000_000_000; // Верхняя граница значений для генерации

            PointClassFC[] pointClassFC_1 = new PointClassFC[number];
            PointClassFC[] pointClassFC_2 = new PointClassFC[number];
            
            PointStructFS[] pointStructFS_1 = new PointStructFS[number];
            PointStructFS[] pointStructFS_2 = new PointStructFS[number];
            
            PointStructDS[] pointStructDS_1 = new PointStructDS[number];
            PointStructDS[] pointStructDS_2 = new PointStructDS[number];


            // Генерация данных
            for (int i = 0; i < number; i++)
            {
                // точка 1
                double XD = Generate (multiplier);
                double YD = Generate (multiplier);

                float XF = (float) Generate (multiplier);
                float YF = (float) Generate (multiplier);

                pointClassFC_1[i] = new PointClassFC();

                pointClassFC_1[i].X = XF;
                pointClassFC_1[i].Y = YF;

                pointStructFS_1[i] = new PointStructFS();

                pointStructFS_1[i].X = XF;
                pointStructFS_1[i].Y = YF;

                pointStructDS_1[i] = new PointStructDS();

                pointStructDS_1[i].X = XD;
                pointStructDS_1[i].Y = YD;

                // точка 2
                XD = Generate(multiplier);
                YD = Generate(multiplier);

                XF = (float)Generate(multiplier);
                YF = (float)Generate(multiplier);

                pointClassFC_2[i] = new PointClassFC();

                pointClassFC_2[i].X = XF;
                pointClassFC_2[i].Y = YF;

                pointStructFS_2[i] = new PointStructFS();

                pointStructFS_2[i].X = XF;
                pointStructFS_2[i].Y = YF;

                pointStructDS_2[i] = new PointStructDS();

                pointStructDS_2[i].X = XD;
                pointStructDS_2[i].Y = YD;
            }

            long[] results_1 = new long[100];

            // Для сравнения использую тики, так как миллисекунды не фиксируются у меня на рабочей станции.

            for (int i = 0; i < number; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                PointDistanceFC(pointClassFC_1[i], pointClassFC_2[i]);
                stopWatch.Stop();
                results_1[i] = stopWatch.ElapsedTicks;
            }

            long[] results_2 = new long[100];
            for (int i = 0; i < number; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                PointDistanceFS(pointStructFS_1[i], pointStructFS_2[i]);
                stopWatch.Stop();
                results_2[i] = stopWatch.ElapsedTicks;
            }

            long[] results_3 = new long[100];
            for (int i = 0; i < number; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                PointDistanceDS(pointStructDS_1[i], pointStructDS_2[i]);
                stopWatch.Stop();
                results_3[i] = stopWatch.ElapsedTicks;
            }

            long[] results_4 = new long[100];
            for (int i = 0; i < number; i++)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                PointDistanceShortFS(pointStructFS_1[i], pointStructFS_2[i]);
                stopWatch.Stop();
                results_4[i] = stopWatch.ElapsedTicks;
            }

            Console.WriteLine("Способы рассчета:");
            Console.WriteLine("1 - Обычный метод расчёта дистанции со ссылочным типом (PointClass — координаты типа float).");
            Console.WriteLine("2 - Обычный метод расчёта дистанции со значимым типом (PointStruct — координаты типа float).");
            Console.WriteLine("3 - Обычный метод расчёта дистанции со значимым типом (PointStruct — координаты типа double).");
            Console.WriteLine("4 - Метод расчёта дистанции без квадратного корня со значимым типом (PointStruct — координаты типа float).\n");

            Console.WriteLine("Итерация \t Способ 1 \t Способ 2 \t Способ 3 \t Способ 4");

            for (int i = 0; i < number; i++)
            {
                Console.Write($"{i} \t\t {results_1[i]} \t\t {results_2[i]} \t\t {results_3[i]} \t\t {results_4[i]} \n");
            }

        }

        static public double Generate(int multiplier)
        {
            Random rnd = new Random();
            return rnd.NextDouble() * multiplier;
        }

        public static float PointDistanceFC(PointClassFC pointOne, PointClassFC pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public static float PointDistanceFS(PointStructFS pointOne, PointStructFS pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return MathF.Sqrt((x * x) + (y * y));
        }

        public static double PointDistanceDS(PointStructDS pointOne, PointStructDS pointTwo)
        {
            double x = pointOne.X - pointTwo.X;
            double y = pointOne.Y - pointTwo.Y;
            return Math.Sqrt((x * x) + (y * y));
        }

        public static float PointDistanceShortFS(PointStructFS pointOne, PointStructFS pointTwo)
        {
            float x = pointOne.X - pointTwo.X;
            float y = pointOne.Y - pointTwo.Y;
            return (x * x) + (y * y);
        }

    }
}
