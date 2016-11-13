using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Asynchronous version");
            DisplayPrimeCountsAsyncSimple();

            WriteLine("Asynchronous version 2");
            DisplayPrimeCountsAsync();

            WriteLine("Synchronous version");
            DisplayPrimeCounts();
        }

        static int GetPrimesCount(int start, int count)
        {
            return
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0));
        }

        static void DisplayPrimeCounts()
        {
            for (int i = 0; i < 10; i++)
                WriteLine(GetPrimesCount(i * 1000000 + 2, 1000000) +
                    " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            WriteLine("Done!");
        }

        static async void DisplayPrimeCountsAsync()
        {
            for (int i = 0; i < 10; i++)
                WriteLine(await GetPrimesCountAsync(i * 1000000 + 2, 1000000) +
                    " primes between " + (i * 1000000) + " and " + ((i + 1) * 1000000 - 1));
            WriteLine("Done!");
        }

        static Task<int> GetPrimesCountAsync(int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }

        static async void DisplayPrimeCountsAsyncSimple()
        {
            int result = await GetPrimesCountAsync(2, 1000000);
            WriteLine(result);
        }
    }
}
