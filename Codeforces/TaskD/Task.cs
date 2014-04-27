using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private HashSet<int> bad = new HashSet<int>();
        private readonly Dictionary<long, int> cache = new Dictionary<long, int>();
        private List<int> pr;
        private int[] lp;

        int Get(long value)
        {
            var result = 0;
            if (value == 1) return result;
            if (!cache.TryGetValue(value, out result))
            {
                var ok = false;
                for(var i=0;pr[i]*pr[i] <= value;i++)
                {
                    if (value%pr[i] == 0)
                    {
                        ok = true;
                        result = Get(value/pr[i]) + (bad.Contains(pr[i]) ? -1 : 1);
                        break;
                    }
                }
                if (!ok) result = bad.Contains((int)value) ? -1 : 1;
            }
            cache[value] = result;
            return result;
        }

        void Solve()
        {
            int N, M;
            Input.Next(out N, out M);
            var A = Input.ArrayLong().ToArray();
            bad = new HashSet<int>(Input.ArrayInt());

            Primes.SieveOfEratosthenes(1000000000, out pr);

            var gcd = new long[N];
            gcd[0] = A[0];
            for (var i = 1; i < N; i++)
                gcd[i] = Primes.Gcd(A[i], gcd[i - 1]);

            var divisor = 1L;
            for (var i = N - 1; i >= 0; i--)
            {
                var g = gcd[i]/divisor;
                if (Get(g) < 0)
                    divisor *= g;
                A[i] /= divisor;
            }
            
            var result = A.Sum(x => Get(x));
            Console.WriteLine(result);
        }
    }
}
