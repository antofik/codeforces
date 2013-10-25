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
            var A = new Point[n];
            for (var i = 0; i < n; i++) Input.Next(out A[i].X, out A[i].Y);

            Input.Next(out m);
            var B = new Point[m];
            for (var i = 0; i < m; i++) Input.Next(out B[i].X, out B[i].Y);

            var C = A[0];

            for (var i = 0; i < n; i++)
            {
                A[i].X -= C.X;
                A[i].Y -= C.Y;
            }
            for (var i = 0; i < m; i++)
            {
                B[i].X -= C.X;
                B[i].Y -= C.Y;
            }

            foreach (var b in B)
            {
                var l = 1L;
                var r = n - 1;

                if (!(A[l] < b) || !(A[r] > b))
                {
                    Console.WriteLine("NO");
                    return;
                }

                while (r > l + 1)
                {
                    var i = (l + r)/2;
                    if (A[i] > b) r = i;
                    else l = i;
                }

                if (A[l + 1] - A[l] < b - A[l]) continue;
                Console.WriteLine("NO");
                return;
            }
            Console.WriteLine("YES");
        }

        private struct Point
        {
            public long X;
            public long Y;

            public static Point operator -(Point p1, Point p2)
            {
                Point p;
                p.X = p1.X - p2.X;
                p.Y = p1.Y - p2.Y;
                return p;
            }

            public static bool operator >(Point p1, Point p2)
            {
                return p1.X*p2.Y - p1.Y*p2.X > 0;
            }

            public static bool operator <(Point p1, Point p2)
            {
                return p1.X*p2.Y - p1.Y*p2.X < 0;
            }

            public override string ToString()
            {
                return string.Format("({0},{1})", X, Y);
            }
        }
    }

    
}
