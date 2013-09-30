using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Rounds.Round181_2
{
    public class Problem5
    {
        public static void Main()
        {
            var instance = new Problem5();
            instance.Solve();
        }
        
        public void Solve()
        {
            long[] lp;
            List<long> pr;

            var k = long.Parse(Console.ReadLine());
            var a = Console.ReadLine().Split(' ').Select(long.Parse)
                //.Take((int)k)
                .ToList();

            var n = Math.Max(a.Max(), (long) Math.Sqrt(a.Sum())) + 1;
            ImprovedSieveOfEratosthenes.Generate(n, out lp, out pr);

            var cnt = new long[n + 1];
            foreach (var ai in a) cnt[ai]++;
            for (var i = n - 1; i >= 2; i--) cnt[i] += cnt[i + 1];
            for (var i = n - 1; i >= 2; i--)
            {
                if (lp[i] != i) cnt[lp[i]] += cnt[i];
                cnt[i/lp[i]] += cnt[i];
            }
            
            var rg = a.Sum();
            var lf = a.Max();
            while (rg != lf)
            {
                var mid = (rg + lf)/2;

                //Console.WriteLine("{0}..{1}..{2}", lf, mid, rg);
                var ok = true;

                foreach (var prime in pr)
                {
                    long s = 0;
                    for (var t = mid / prime; t > 0; t /= prime) s += t;
                    if (s >= cnt[prime]) continue;
                    //Console.WriteLine("Error at {0}: {1} != {2}");
                    ok = false;
                    break;
                }

                if (ok)
                    rg = mid;
                else
                    lf = mid + 1;
            }

            Console.WriteLine(lf);
        }
    }

    public class ImprovedSieveOfEratosthenes
    {
        public static void Generate(long n, out long[] lp, out List<long> pr)
        {
            var a = new List<int>();a.Sort();

            lp = new long[n];
            pr = new List<long>();
            for (var i = 2; i < n; i++)
            {
                if (lp[i] == 0)
                {
                    lp[i] = i;
                    pr.Add(i);
                }
                foreach (var prJ in pr)
                {
                    var prIj = i * prJ;
                    if (prJ <= lp[i] && prIj <= n - 1)
                    {
                        lp[prIj] = prJ;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
