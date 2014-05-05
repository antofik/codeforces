using System;
using System.Collections.Generic;

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

        void Solve()
        {
            int t;
            Input.Next(out t);

            List<int> pr;
            Primes.SieveOfEratosthenes(1000000000, out pr);

            while (t-->0)
            {
                int n;
                Input.Next(out n);

                // p ... n ... q

                long p;
                for (p = n; p > 1; p--)
                    if (Primes.IsPrime(p, pr))
                        break;

                long q;
                for (q = n+1;; q++)
                    if (Primes.IsPrime(q, pr))
                        break;

                var up = p*q - 2L*q + 2L*(n - p + 1);
                var down = 2L*p*q;
                var gcd = Primes.Gcd(up, down);
                up /= gcd;
                down /= gcd;
                Console.WriteLine(up + "/" + down);

                //var result = 1/2 - 1/p + (n - p + 1)/(p*q);

                // 1/2 - 1/p    where   p = n + 1
            }
        }
    }
}
