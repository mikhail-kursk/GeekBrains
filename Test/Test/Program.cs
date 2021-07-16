using System;

class Test
{
    public static void Main()
    {
        // Step 1: Get the current window dimensions.
        //
        while (true)
        {
            Console.Clear();
            Console.WriteLine("0123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789");
            Console.WriteLine("Ширина = {0}", Console.WindowWidth);
            Console.WriteLine("высота = {0}", Console.WindowHeight);
            Console.ReadLine();
        }
    }
}