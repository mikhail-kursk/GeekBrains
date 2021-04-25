using System;

namespace FirstHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Пожалуйста, введите своё имя");
            string Name = Console.ReadLine();

            Console.WriteLine("Привет, " + Name + ", сейчас " + DateTime.Now);

        }
    }
}
