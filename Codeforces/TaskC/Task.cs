using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int n;
            Input.Next(out n);
            var X = Input.ArrayInt().ToArray();

            bool[] pr;
            var N = 10000000;
            Primes.SieveOfEratosthenes(N, out pr);

            var counts = new int[n];
            foreach (var x in X)
            {
                if (!pr[x]) //is prime
                {
                    
                }
            }


            int m;
            Input.Next(out m);
            while (m-->0)
            {
                int l, r;
                Input.Next(out l, out r);
                var result = 0L;
                Console.Write(result);
            }

            Console.Write(0);
        }

        public static void Main()
        {
            var task = new Task();
            #if DEBUG
            task.Solve();
            #else
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
            }
            #endif
        }
    }
}
