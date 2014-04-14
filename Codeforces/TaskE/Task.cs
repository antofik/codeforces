using System;
using System.Collections.Generic;
using System.Linq;
/*Library*/
using System.Text;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            long t;
            Input.Next(out t);
            var P = new long[t];
            for (var i = 0; i < t; i++)
                Input.Next(out P[i]);
            var builder = new StringBuilder();

            var max = 100000;
            int[] lp;
            List<int> pr;
            Primes.ImprovedSieveOfEratosthenes(max, out lp, out pr);
            var prs2 = pr.Where(c => c < 100).Select(c => c.ToString("D2")).Select(c => new { ch = c[0], value = c }).GroupBy(c => c.ch).ToDictionary(c => c.Key, y=>y.Select(c=>c.value).ToList());
            var prs3 = pr.Where(c => c < 1000).Select(c => c.ToString("D3")).Select(c => new { ch = c[0], value = c }).GroupBy(c => c.ch).ToDictionary(c => c.Key, y => y.Select(c => c.value).ToList());
            var prs4 = pr.Where(c => c < 1000).Select(c => c.ToString("D4")).Select(c => new { ch = c[0], value = c }).GroupBy(c => c.ch).ToDictionary(c => c.Key, y => y.Select(c => c.value).ToList());
            var prs5 = pr.Select(c => c.ToString("D5")).Select(c => new { ch = c[0], value = c }).GroupBy(c => c.ch).ToDictionary(c => c.Key, y => y.Select(c => c.value).ToList());

            foreach (var p in P)
            {
                var ps = p.ToString("D");
                Dictionary<char, List<string>> prs = new Dictionary<char, List<string>>();
                switch (ps.Length)
                {
                    case 2: prs = prs2; break;
                    case 3: prs = prs3; break;
                    case 4: prs = prs4; break;
                    case 5: prs = prs5; break;
                    default: Environment.Exit(-1);
                }

                var result = 0;
                foreach (var n2 in prs[ps[1]])
                    foreach(var n3 in prs[ps[2]])
                        if (n2[2] == n3[1])


                builder.Append(p);
                builder.Append(Environment.NewLine);
            }


            Console.WriteLine(builder);
        }
    }
}
