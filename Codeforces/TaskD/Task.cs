using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        private int d;
        private Point[] points;

        private double Distance(double l, int i)
        {
            // tg a1 = x/y
            // tg (alpha - a1) = (z-x)/y => z = x + y tg (alpha - a1)
            // tg (a+b) = (tg a + tg b)/(1 - tg a tg b)
            // z = x + y (tg alpha - x/y) / (1 + (x/y) tg alpha) = x + y (y tg alpha - x) / (y + x tg alpha) = 
            // (xy + x2 tg + y2 tg - xy) / (y + x tg) = tg alpha (x2 + y2) / (y + x tg alpha)


            // tg a2 = -x/y
            // tg (alpha + a2) = (z-x)/y

            var p = points[i];
            var x = p.X - l;
            var y = p.Y;
            var tg = p.TgAlpha;

            if (x < 0)
            {
                var maxAlpha = Math.PI/2 - Math.Abs(Math.Atan2(x, y));
                if (p.Alpha >= maxAlpha) return d;
            }

            
            var denom = y + x*tg;
            if (denom == 0) return d;
            var z = l + Math.Abs(tg*(x*x + y*y)/denom);
            return Math.Min(d, z);
        }

        void Solve()
        {
            int n, l, r;
            Input.Next(out n, out l, out r);
            d = r - l;
            
            points = new Point[n];
            for (var i = 0; i < n; i++)
            {
                points[i] = new Point();
                Input.Next(out points[i].X, out points[i].Y, out points[i].Angle);
                points[i].X -= l;
                points[i].Alpha = points[i].Angle * Math.PI / 180;
                points[i].TgAlpha = Math.Tan(points[i].Alpha);
            }

            var dp = new double[1 << n << 1];
            for (var j = 0; j < 1<<n; j++)
                for (var i=0;i<n;i++)
                    if ((j & (1 << i)) == 0)
                        dp[j | (1 << i)] = Math.Max(dp[j | (1 << i)], Distance(dp[j], i));
            Console.Write(dp[(1<<n)-1].ToString("f9", CultureInfo.InvariantCulture));
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

    sealed class Point
    {
        public int X;
        public int Y;
        public int Angle;
        public double Alpha;
        public double TgAlpha;
    }
}
