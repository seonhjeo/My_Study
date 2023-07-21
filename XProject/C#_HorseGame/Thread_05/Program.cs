using System;
using System.Threading;

namespace ThreadHorse
{
    public static class Constants
    {
        public const Int32 GOAL_NUMBER = 100;
        public const Int32 MIN_SLEEP_TIME = 50;
        public const Int32 MAX_SLEEP_TIME = 100;
    }

    class Generals
    {
        public static List<Int32> finishLineThread = new List<Int32>();
    }

    class MyTask
    {
        public static void Increase(object? n)
        {
            Int32 num;
            Int32 sleepTime;
            Random random = new Random();

            if (n == null)
                num = 0;
            else
                num = (Int32)n;

            for (int i = 1; i < Constants.GOAL_NUMBER + 1; i++)
            {
                Console.WriteLine($"{num}번마 : {i}m");
                if (i == Constants.GOAL_NUMBER)
                {
                    Generals.finishLineThread.Add(num);
                }

                sleepTime = random.Next(Constants.MIN_SLEEP_TIME, Constants.MAX_SLEEP_TIME + 1);
                Thread.Sleep(sleepTime);
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Console.Write("경주마의 수 : ");
            Int32 threadNum = Convert.ToInt32(Console.ReadLine());
            if (threadNum < 4)
            {
                Console.WriteLine("경주마가 부족합니다. 4마리 이상의 말이 필요합니다.");
                return;
            }

            Console.WriteLine("\n경기 준비...\n");
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < threadNum; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(MyTask.Increase));
                threads.Add(t);
            }

            Console.WriteLine("\n경기 시작!\n");
            for (int i = 0; i < threadNum; i++)
            {
                threads[i].Start(i + 1);
            }

            for (int i = 0; i < threadNum; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("\n\n_____결과 발표_____\n");
            for (int i = 0;i < threadNum; i++)
            {
                Console.WriteLine($"{i + 1}등 : {Generals.finishLineThread[i]}번마");
            }
        }
    }
}