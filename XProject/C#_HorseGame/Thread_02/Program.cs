using System;
using System.Threading;

namespace ThreadHorse
{
    class MyTask
    {
        // https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/builtin-types/nullable-value-types
        // https://stackoverflow.com/questions/71950764/cs8622-nullability-of-reference-types-in-type-of-parameter-sender
        public static void Increase(object? n)
        {
            int num;
            if (n == null)
                num = 0;
            else
                num = (int)n;

            for (int i = 1; i < 101; i++)
            {
                Console.WriteLine($"{num}번 쓰레드 : {i}");
            }
        }
    }

    class MainApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Threading Start...");

            // https://docs.microsoft.com/ko-kr/dotnet/api/system.threading.parameterizedthreadstart?view=net-6.0
            Thread t1 = new Thread(new ParameterizedThreadStart(MyTask.Increase));
            Thread t2 = new Thread(new ParameterizedThreadStart(MyTask.Increase));
            Thread t3 = new Thread(new ParameterizedThreadStart(MyTask.Increase));

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
