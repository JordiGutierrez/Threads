using System;
using System.Threading;

namespace Threads
{
    class Program
    {
        static int common = 0;

        static void Main(string[] args)
        {
            Thread t = new Thread(WriteY);
            t.Start();

            for (int i = 0; i < 100; i++) Console.Write("x");
            Console.WriteLine(++common + " main");

            t = new Thread(Go);
            t.Start();
            t.Join();
            Console.WriteLine("Thread t has ended");
        }

        private static void Go(object obj)
        {
            for (int i = 0; i < 100; i++) Console.Write("g");
            Console.WriteLine(++common + " Go");
        }

        private static void WriteY(object obj)
        {
            for (int i = 0; i < 100; i++) Console.Write("y");
            Console.WriteLine(++common + "WriteY");
        }
    }
}
