using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace laba16
{
    class HeavyMath
    {
        public static void MatrixMultiply()
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

            Console.WriteLine("End.");
        }

        public static int Multi(int x, int y)
        {
            int result;
            result = x * y;
            return result;
        }

        public static int Divide(int x, int y)
        {
            int result;
            result = x / y;
            return result;
        }

        public static int Minus(int x, int y)
        {
            int result;
            result = x - y;
            return result;
        }

        public static void Output(int output)
        {
            Console.WriteLine($"Результат: {output}");
        }

        public static void ReallyBigArray(int multi)
        {
            int size = 1000000 * multi;
            int[] array = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = rnd.Next(10, 100);
            }
        }

        public static void Goods()
        {
            Task p1 = new Task(producer);
            Task p2 = new Task(producer);
            Task p3 = new Task(producer);
            Task p4 = new Task(producer);
            Task p5 = new Task(producer);

            Task c1 = new Task(consumer);
            Task c2 = new Task(consumer);
            Task c3 = new Task(consumer);
            Task c5 = new Task(consumer);
            Task c6 = new Task(consumer);
            Task c7 = new Task(consumer);
            Task c8 = new Task(consumer);
            Task c9 = new Task(consumer);
            Task c10 = new Task(consumer);

            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();
            p5.Start();
            c1.Start();
            c2.Start();
            c3.Start();
            c5.Start();
            c6.Start();
            c7.Start();
            c8.Start();
            c9.Start();
            c10.Start();
        }
        static int mod = 1;
        static BlockingCollection<int> bc = new BlockingCollection<int>(5);
        static void producer()
        {
            mod++;
            for (int i = (mod - 1); i < mod; i++)
            {
                bc.Add(i);
                Thread.Sleep(500);
                Console.WriteLine("added: " + i);
                foreach (var j in bc)
                {
                    Console.WriteLine("amount of products: " + j);
                    Thread.Sleep(500);
                }
                Thread.Sleep(500);
            }
        }
        static void consumer()
        {
            mod++;
            int i;
            while (!bc.IsCompleted)
            {
                Thread.Sleep(500);
                if (bc.TryTake(out i))
                {
                    Console.WriteLine("deleted: " + i);
                    Thread.Sleep(500);
                }
                else
                {
                    if (i == 0)
                    {
                        Console.WriteLine("empty");
                    }
                }
                Thread.Sleep(500);
            }
        }
        public static async void BigArrayAsync()
        {
            await Task.Run(() => ReallyBigArray(100));                // выполняется асинхронно
            Console.WriteLine("Конец метода BigArrayAsync");
        }
    }
}
