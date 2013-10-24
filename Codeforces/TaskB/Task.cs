using System;
/*Library*/

namespace Codeforces.TaskB
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
            long n, m;
            Input.Next(out n);
            var a = new Point[n+1];
            for (var i = 0; i < n; i++) Input.Next(out a[i].X, out a[i].Y);
            a[n] = a[0];

            Input.Next(out m);
            var B = new Point[m];
            for (var i = 0; i < m; i++) Input.Next(out B[i].X, out B[i].Y);

            Point v1, v2;
            foreach (var b in B)
            {
                for (var i = 0; i < n; i++)
                {
                    v1.X = a[i + 1].X - a[i].X;
                    v1.Y = a[i + 1].Y - a[i].Y;
                    v2.X = b.X - a[i].X;
                    v2.Y = b.Y - a[i].Y;
                    var r = v1.X*v2.Y - v1.Y*v2.X;
                    if (r < 0) continue;
                    Console.WriteLine("NO");
                    return;
                }
            }
            Console.WriteLine("YES");
        }

        private struct Point
        {
            public long X;
            public long Y;
        }
    }

    
}
