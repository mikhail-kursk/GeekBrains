using System;
using System.Diagnostics;

namespace SixthhomeWork
{
    class Program
    {
        static void Main(string[] args)
        {

            bool exit = true;

            do
            {
                Process[] processList = Process.GetProcesses();

                Console.WriteLine("Запущенные процессы");
                foreach (Process currentProcesses in processList)
                {
                    Console.WriteLine($"{currentProcesses.Id}\t{currentProcesses.ProcessName}");
                }


                Console.WriteLine("\nВведите имя процесса для его завершения или его номер");
                Console.WriteLine("Для выхода введите 0");

                string input = Console.ReadLine();

                if (String.IsNullOrEmpty(input)) continue;

                bool isInt = Int32.TryParse(input, out int processID);

                if (isInt)
                {
                    if (processID == 0)
                        exit = false;
                    else
                        try
                        {
                            Process.GetProcessById(processID).Kill();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Не удалось закрыть процесс");
                            Console.WriteLine(e);
                        }
                }
                else
                {
                    bool isExist = false;
                    bool isSingle = true;

                    foreach (Process currentProcesses in processList)
                    {
                        if (currentProcesses.ProcessName == input)
                            if (isExist == true)
                            {
                                Console.WriteLine("Найдено несколько процессов с таким названием, закройте необходимый процесс по его ID");
                                isSingle = false;
                                break;
                            }
                            else
                            {
                                isExist = true;
                                processID = currentProcesses.Id;
                            }
                    }

                    if (!isExist)
                        Console.WriteLine($"Не найден процесс {input}");

                    if (isExist && isSingle)
                        try
                        {
                            Process.GetProcessById(processID).Kill();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Не удалось закрыть процесс");
                            Console.WriteLine(e);
                        }
                }

                Console.ReadLine();

            } while (exit);
        }
    }
}
