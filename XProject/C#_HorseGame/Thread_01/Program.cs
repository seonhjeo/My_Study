using System;
using System.Threading;

namespace ThreadHorse
{
    class MyTask
    {
        // C# static : https://funfunhanblog.tistory.com/51
        public static void Increase()
        {
            for (int i = 1; i < 11; i++)
            {
                Console.WriteLine($"Number increase : {i}");
                Thread.Sleep(100);
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Threading Start...");

            Thread t1 = new Thread(new ThreadStart(MyTask.Increase));

            t1.Start();

            t1.Join();

            Console.WriteLine("Threading Finished...");
        }
    }
}
