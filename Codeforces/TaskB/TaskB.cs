using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class TaskB
    {

        private void Solve()
        {
            int n = Input.Int();

            List<Pair> downs = new();
            List<Pair> ups = new();
            Dictionary<int, int> steadys = new();


            for (int i = 1; i <= n; ++i)
            {
                int[] s = Input.ArrayInt0();
                int count = s[0];
                int min = s[1];
                int max = min;
                bool down = false;
                bool up = false;
                for(int j=2;j<=count;++j)
                {
                    if (s[j] < s[j - 1])
                    {
                        down = true;
                    }
                    else if (s[j] > s[j - 1])
                    {
                        up = true;
                    }
                    min = Math.Min(min, s[j]);
                    max = Math.Max(max, s[j]);
                }
                var p = new Pair { Min = min, Max = max };
                if (up)
                {
                    ups.Add(p);
                }
                else 
                {
                    downs.Add(p);
                }
            }

            long ans = 2L * ups.Count * n - 1L * ups.Count * ups.Count;

            var mins = downs.Select(c=>c.Min).OrderBy(c => c).ToArray();
            var maxs = downs.Select(c=>c.Max).OrderBy(c => c).ToArray();

            foreach(var p in downs)
            {
                int countLarger = downs.Count - FindFirstGreater(maxs, p.Min);
                ans += countLarger;
            }

            Console.WriteLine(ans);
        }

        /// <summary>
        /// Find position of element that is bigger that given
        /// </summary>
        private int FindFirstGreater(int[] list, int v)
        {
            int l = 0;
            int r = list.Length;
            while (l < r)
            {
                int m = l + ((r - l) >> 1);
                if (v < list[m])
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }
            return l;
        }

        /// <summary>
        /// Find position of first element that is bigger or equal that given
        /// </summary>
        private int FindFirstGreaterOrEqual(int[] list, int v)
        {
            int l = 0;
            int r = list.Length;
            while (l < r)
            {
                int m = l + ((r - l) >> 1);
                if (v <= list[m])
                {
                    r = m;
                }
                else
                {
                    l = m + 1;
                }
            }
            return l;
        }


        public class Pair
        {
            public int Min { get; set; }
            public int Max { get; set; }
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
