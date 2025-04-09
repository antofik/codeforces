using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        void Solve()
        {
            int t = int.Parse(Console.ReadLine());
            while (t-- > 0)
            {
                int n = int.Parse(Console.ReadLine()); 
                long[] a = Console.ReadLine().Split(' ').Select(x => long.Parse(x)).ToArray();
                var ok = false;

                int minIndex = -1;
                long min = long.MaxValue;
                for (int i = 0; i < a.Length; i++) {
                    if (a[i] < min)
                    {
                        min = a[i];
                        minIndex = i;
                    }
                }


                List<long> nums = new List<long>();
                for (int i = 0; i < a.Length; i++)
                {
                    if (i == minIndex) continue;
                    if (a[i] % min == 0)
                    {
                        nums.Add(a[i]);
                    }
                }

                if (nums.Count > 0)
                {
                    long gcd = nums[0];
                    foreach (var x in nums)
                    {
                        gcd = Gcd(gcd, x);
                    }

                    ok = gcd == min;
                }

                Console.WriteLine(ok ? "Yes" : "No");
            }
        }

        public static long Gcd(long x, long y)
        {
            while (y != 0)
            {
                var c = y;
                y = x % y;
                x = c;
            }
            return x;
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
