using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int n;
            Input.Next(out n);
            var X = Input.ArrayInt().ToArray();

            List<int> pr;
            var N = 20000000;
            Primes.SieveOfEratosthenes(N, out pr);

            var counts = new int[N + 1];
            foreach (var x in X)
                counts[x]++;

            var f = new Dictionary<int, int>();
            foreach (var p in pr)
            {
                var r = 0;
                for (var i = p; i <= N; i += p)
                    r += counts[i];
                f[p] = r;
            }

            var prefix = new long[N + 1];
            var pi = 0;
            for (var i = 2; i <= N; i++)
            {
                prefix[i] = prefix[i - 1];
                if (pi<pr.Count && pr[pi] == i)
                {
                    prefix[i] += f[i];
                    pi++;
                }
            }

            int m;
            Input.Next(out m);
            var output = new StringBuilder();
            while (m-->0)
            {
                int l, r;
                Input.Next(out l, out r);
                l = Math.Min(N, l);
                r = Math.Min(N, r);
                output.Append(prefix[r] - prefix[l-1]);
                output.Append('\n');
            }
            Console.Write(output);
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
