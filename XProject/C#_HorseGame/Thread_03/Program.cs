using System;
using System.Threading;

static class Constants
{
    public const int LOOP_TIME = 1000;
}

namespace ThreadHorse
{
    class MyTask
    {
        private int loopTime = Constants.LOOP_TIME;

        private int number;
        private readonly object thisLock;
        
        public MyTask()
        {
            thisLock = new object();
            number = 0;
        }

        public void Increase(object? n)
        {
            int num;
            if (n == null)
                num = 0;
            else
                num = (int)n;

            while (loopTime-- > 0)
            {
                lock (thisLock)
                {
                    number++;
                    Console.WriteLine($"{num}번 쓰레드 : {number}");
                }
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            MyTask task = new MyTask();

            Console.WriteLine("Threading Start...");

            Thread t1 = new Thread(new ParameterizedThreadStart(task.Increase));
            Thread t2 = new Thread(new ParameterizedThreadStart(task.Increase));
            Thread t3 = new Thread(new ParameterizedThreadStart(task.Increase));

            t1.Start(1);
            t2.Start(2);
            t3.Start(3);

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Threading Finished...");
        }
    }
}