using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskC
{
    public class Task
    {
        public static void Main()
        {
            try
            {
                var task = new Task();
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.StackTrace);
            }
        }

        void Solve()
        {
            int n;
            Input.Next(out n);
            var A = Input.ArrayInt().ToArray();

            List<int> pr;
            Primes.SieveOfEratosthenes(1000000000, out pr);

            var numbers = new Dictionary<int, int>();
            
            foreach (var a in A)
            {
                foreach (var d in Primes.GetDivisors(a))
                {
                    var i = (int) d;
                    if (!numbers.ContainsKey(i))
                        numbers[i] = 0;
                    numbers[i]++;
                }
            }

            var mod = 1000000007L;

            var CN = 20000;
            var c = new long[CN + 1];
            var prev = new long[CN + 1];
            var current = new long[CN + 1];
            c[0] = c[1] = prev[0] = current[0] = prev[1] = 1;

            for (var i = 2; i <= CN; i++)
            {
                for (var j = 1; j <= i && j <= 505; j++)
                    current[j] = (prev[j] + prev[j - 1])%mod;
                c[i] = current[n - 1];

                var x = prev;
                prev = current;
                current = x;
            }

            var result = 1L;
            foreach(var key in numbers.Keys)
                result = (result * c[n + numbers[key] - 1]) % mod;

            Console.WriteLine(result);
        }
    }
}
