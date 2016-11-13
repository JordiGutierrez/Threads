using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(() => Console.WriteLine("Hello from the thread pool"));

            Task task = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Foo");
                });
            Console.WriteLine(task.Status);
            Console.WriteLine(task.IsCompleted);
            task.Wait();

            Task<int> task2 = Task.Run(() => { Console.WriteLine("Foo2"); return 3; });
            int result = task2.Result;
            Console.WriteLine(result);

            Task<int> primeNumberTask = Task.Run(() =>
                Enumerable.Range(2, 3000000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            
            Console.WriteLine("Task running...");
            var awaiter = primeNumberTask.GetAwaiter();
            awaiter.OnCompleted(() => Console.WriteLine("The answer is " + awaiter.GetResult()));

            Task task3 = Task.Run(() => { throw null; });
            try
            {
                task3.Wait();
            }
            catch(AggregateException aex)
            {
                if (aex.InnerException is NullReferenceException)
                    Console.WriteLine("Null!");
                else
                    throw;
            }

            primeNumberTask.Wait();
        }
    }
}
