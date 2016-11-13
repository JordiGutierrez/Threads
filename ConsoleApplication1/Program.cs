using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static bool _done;
        static readonly object _locker = new object();

        static void Main(string[] args)
        {
            new Thread(Go).Start();
            Go();

            Thread t = new Thread(() => Print("Hello from t!"));
            t.Start();

            for (int i = 0; i < 10; i++)
            {
                int temp = i;
                new Thread(() => Console.Write(temp)).Start();
            }

            Console.WriteLine();

            var signal = new ManualResetEvent(false);

            new Thread(() =>
            {
                Console.WriteLine("Waiting for signal...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("Got signal!");
            }).Start();

            Thread.Sleep(2000);
            signal.Set();
        }

        private static void Print(string p)
        {
            Console.WriteLine(p);
        }

        private static void Go()
        {
            lock (_locker)
            {
                if (!_done) { Console.WriteLine("Done"); _done = true; }
            }
        }
    }
}
