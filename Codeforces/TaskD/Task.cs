using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            var vin = new List<int>[n + 1];
            var vout = new List<int>[n + 1];
            for (var i = 1; i <= n; i++)
            {
                vin[i] = new List<int>();
                vout[i] = new List<int>();
            }

            while (m-->0)
            {
                int from, to;
                Input.Next(out from, out to);
                vin[to].Add(from);
                vout[from].Add(to);
            }

            var center = -1;
            var centerCount = 0;
            for (var i = 1; i <= n; i++)
            {
                var count = vin[i].Count + vout[i].Count;
                if (vin[i].Contains(i)) count--;
                if (count > centerCount)
                {
                    center = i;
                    centerCount = count;
                }
            }

            if (center == -1)
            {
                Console.WriteLine("Could not find center");
                center = 1;
            }

            var result = 0;

            for (var i = 1; i <= n; i++)
            {
                if (!vin[center].Contains(i))
                {
                    vin[center].Add(i);
                    vout[i].Add(center);
                    result++;
                }
                if (!vout[center].Contains(i))
                {
                    vout[center].Add(i);
                    vin[i].Add(center);
                    result++;
                }
            }

            for (var i = 1; i <= n; i++)
                if (i != center)
                {
                    while (vin[i].Count > 2)
                    {
                        var k = vin[i].Where(c => c != center).OrderByDescending(c => vout[c].Count).First();
                        vin[i].Remove(k);
                        vout[k].Remove(i);
                        result++;
                    }
                    while (vout[i].Count > 2)
                    {
                        var k = vout[i].Where(c => c != center).OrderByDescending(c => vin[c].Count).First();
                        vout[i].Remove(k);
                        vin[k].Remove(i);
                        result++;
                    }
                    if (vin[i].Count == 1 && vout[i].Count == 1)
                    {
                        vin[i].Add(i);
                        vout[i].Add(i);
                        result++;
                    }
                    if (vin[i].Count == 1)
                        result++;
                }
            Console.WriteLine(result);
        }

        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
