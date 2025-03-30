using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskA
{
    public class Task
    {
        void Solve()
        {
            int t;
            Input.Next(out t);
            while(t-->0)
            {
                int n;
                Input.Next(out n);
                var a = Input.ArrayInt().ToList();

                var max = 0;
                for(int i=0;i<n;i++)
                {
                    for(int j=i+1;j<n;j++)
                    {
                        var g = FindMax(a[i], a[j]);
                        if (g > max) max = g;
                    }
                }
                Console.WriteLine(max);
            }
        }

        int FindMax(int aa, int bb)
        {
            if (true) return Math.Abs(aa - bb);

            var a = Math.Min(aa, bb);
            var b = Math.Max(aa, bb);

            if (b >= 2*a)
            {
                return b - a;
            }

            var max = 0;
            var d = Math.Max(a,b);
            for(int i=0;i<d;++i)
            {                
                var gcd = Gcd(a+i, b+i);
                if (gcd > max) max = gcd;

                if (Math.Min(a + i, b + i) * 2 > Math.Max(a + i, b + i)) break;
            }
            return max;
        }

        public static int Gcd(int x, int y)
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
