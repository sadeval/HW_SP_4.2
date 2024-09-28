using System;
using System.Threading;

class Program
{
    private static readonly int printerCount = 3;

    private static Semaphore semaphore = new Semaphore(printerCount, printerCount);

    static void Main()
    {
        for (int i = 1; i <= 10; i++)
        {
            int taskNumber = i;
            Thread thread = new Thread(() => Print(taskNumber));
            thread.Start();
        }
    }

    static void Print(int taskNumber)
    {
        Console.WriteLine($"Документ {taskNumber} ожидает принтер...");

        semaphore.WaitOne();
        
        Random rnd = new Random();
        try
        {
            Console.WriteLine($"Документ {taskNumber} печатается на принтере...");
            Thread.Sleep(rnd.Next(1000, 3000));
            Console.WriteLine($"Печать документа {taskNumber} завершено.");
        }
        finally
        {
            semaphore.Release();
        }
    }
}
