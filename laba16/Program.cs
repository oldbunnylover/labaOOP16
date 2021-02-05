using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace laba16
{
    class Program
    {
        static void Main()
        {
            Task task1 = new Task(HeavyMath.MatrixMultiply);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            task1.Start();
            watch.Stop();
            TimeSpan time = watch.Elapsed;
            Console.WriteLine("RunTime " + time);
            Console.WriteLine($"My task: {task1.Id} - {task1.Status}");
            task1.Wait();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            Task task2 = new Task(() =>
            {
                int size = 8000;
                int[,] matrix = new int[size, size];
                Random rnd = new Random();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = rnd.Next(10, 100);
                    }
                }

                int[,] matrix2 = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix2[i, j] = rnd.Next(10, 100);
                    }
                }

                int[,] matrix3 = new int[size, size];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix3[i, j] = matrix[i, j] * matrix[i, j];
                    }
                }

                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                Console.WriteLine("End.");
            });

            if (Console.ReadLine() == "q")
            {
                cancelTokenSource.Cancel();
            }

            
            Task<int> Task1 = new Task<int>(() => HeavyMath.Multi(6, 6));
            Task1.Start();

            Task<int> Task2 = new Task<int>(() => HeavyMath.Divide(6, 3));
            Task2.Start();

            Task<int> Task3 = new Task<int>(() => HeavyMath.Minus(24, 12));
            Task3.Start();

            Task<int> Task4 = new Task<int>(() => Task1.Result + Task2.Result + Task3.Result);
            Task4.Start();

            Console.WriteLine(Task4.Result);

            Task<int> task1_4 = new Task<int>(() => HeavyMath.Minus(24, 12));
            Task tasK2_4 = task1_4.ContinueWith(mul => HeavyMath.Output(mul.Result));
            task1_4.Start();
            tasK2_4.Wait();

            var awaiter = task1_4.GetAwaiter();

            awaiter.OnCompleted(() =>
            {
                Console.WriteLine("Result: " + awaiter.GetResult());
            });


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Parallel.For(0, 5, HeavyMath.ReallyBigArray);
            stopwatch.Stop();
            TimeSpan span = stopwatch.Elapsed;
            Console.WriteLine($"Время: {span}");
            Console.ReadKey();

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch.Start();
            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 3, 5, 8 },
                HeavyMath.ReallyBigArray);
            stopwatch.Stop();
            TimeSpan span2 = stopwatch.Elapsed;
            Console.WriteLine($"Время: {span2}");
            Console.ReadKey();

            Parallel.Invoke(
                () => {
                    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                    Thread.Sleep(3000);
                },
                () => HeavyMath.ReallyBigArray(5));

            Console.ReadKey();
            HeavyMath.BigArrayAsync();
            HeavyMath.Goods();

            Console.ReadKey();
        }
    }
}
