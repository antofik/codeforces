using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private readonly long MOD = 1000_000_007;

        private bool Solve(int test)
        {
            int n = Input.Int();
            int[] A = Input.ArrayInt();
            var counts = new Dictionary<int, int>();
            for(int i=1;i<=n;++i)
            {
                counts[A[i]] = counts.TryGetValue(A[i], out var c) ? c + 1 : 1;
            }
            var days = counts.OrderBy(c => c.Key).Select(c => new { Day = c.Key, Count = c.Value }).ToList();

            int prev = -1;
            int bits = 2;
            foreach(var day in days)
            {
                if (day.Day - prev > 1) bits = 2;
                if (day.Count >= 4) return true;
                if (bits == 2)
                {
                    if (day.Count >= 2)
                    {
                        bits = 1;
                    } 
                    else
                    {
                        bits = 2;
                    }
                } 
                else if (bits == 1)
                {
                    if (day.Count >= 2)
                    {
                        return true;
                    }
                    else if (day.Count >= 1)
                    {
                        bits = 1;
                    }
                    else
                    {
                        bits = 2;
                    }
                }
                prev = day.Day;
            }

            return false;
        }

        private void Solve()
        {
            int T = Input.Int();
            for (int t = 1; t <= T; ++t)
            {
                Output.Write(Solve(t));
            }
        }

        public static void Main()
        {
            var task = new TaskC();
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
