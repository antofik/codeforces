using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskB
    {
        private void Solve(int test)
        {
            Input.Next(out int n, out int k);
            int[] A = Input.ArrayInt();
            var count = new Dictionary<int, int>();
            for(int i=1;i<=n;++i)
            {
                count[A[i]] = count.TryGetValue(A[i], out var z) ? z + 1 : 1;
            }
            var pairs = count.OrderBy(c => c.Key).ToList();

            var sum = new Dictionary<int, int>();
            int prev = 0;
            sum[prev] = 0;
            foreach (var pair in pairs)
            {
                prev += pair.Value;
                sum[pair.Key] = prev;
            }

            prev = 0;
            int ans = 0;
            foreach (var pair in pairs)
            {
                int curr = pair.Key;
                int left = sum[prev];
                int right = n - sum[curr];
                if (Math.Abs(left - right - pair.Value) <= k)
                    ans += curr - prev - 1;
                if (Math.Abs(Math.Max(left,right) - Math.Min(left,right)) <= k + pair.Value)
                    ans++;
                prev = curr;
            }

            Output.Write(ans);
        }

        private void Solve()
        {
            int T = Input.Int();
            for (int t = 1; t <= T; ++t)
            {
                Solve(t);
            }
        }

        public static void Main()
        {
            var task = new TaskB();
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
