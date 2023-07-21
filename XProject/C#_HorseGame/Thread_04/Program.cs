using System;
using System.Threading;
using System.Diagnostics;

namespace ThreadHorse
{
    static class Constants
    {
        public const long LOOP_TIME = 4000000000; //10000000000000000;
    }

    class Generals
    {
        public static bool isFinished;
    }

    class MyTask
    {
        private long loopTime;
        private long number;

        public MyTask()
        {
            loopTime = Constants.LOOP_TIME;
            number = 0;
        }

        public void Increase()
        {
            while (loopTime-- > 0)
                number++;
            Console.WriteLine($"최종 증가값 : {number}");
            Generals.isFinished = true;
        }
    }

    class MyTimer
    {
        private int second;
        private long milli;
        
        private Stopwatch t;

        public MyTimer()
        {
            second = 1;
            milli = 0;

            t = new Stopwatch();
        }

        public void TimerStart()
        {
            t.Start();
            Console.WriteLine("타이머 시작");

            while (Generals.isFinished == false)
            {
                milli = t.ElapsedMilliseconds;
                if (milli >= second * 1000)
                    Console.WriteLine($"{second++}초 경과...");
            }

            t.Stop();
            TimeSpan ts = t.Elapsed;
            Console.WriteLine("최종적으로 걸린 시간 : {0:00}:{1:00}:{2:00}.{3}",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Generals.isFinished = false;

            MyTask task = new MyTask();
            MyTimer timer = new MyTimer();

            Console.WriteLine("Threading Start...");

            Thread t1 = new Thread(new ThreadStart(task.Increase));
            Thread t2 = new Thread(new ThreadStart(timer.TimerStart));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("Threading Finished...");
        }
    }
}