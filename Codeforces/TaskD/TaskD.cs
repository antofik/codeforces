using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskD
    {
        private void Solve()
        {
            Input.Next(out int n, out int m);

            var debug = n == 31111 && m == 100000;

            var timer = new Stopwatch();
            timer.Start();
            Action<string> log = message =>
            {
                if (!debug) return;
                Console.WriteLine("{0}ms {1}", timer.ElapsedMilliseconds, message);
            };

            log("Started");

            List<Point> points = new List<Point>(m);
            for (int i = 0; i < m; i++)
            {
                Input.Next(out int x, out int y);
                points.Add(new Point(x, y));
            }

            log("Points read");

            Compress(points, m, n, out int maxX, out bool hasFreeSpaceInTheEnd);

            log("Compressed");

            var ok = Solve(n, m, points, maxX, hasFreeSpaceInTheEnd, log);

            Console.WriteLine(ok ? 2 * n - 2 : -1);
        }

        private static bool Solve(int n, int m, List<Point> points, int maxX, bool hasFreeSpaceInTheEnd, Action<string> log)
        {
            int count0 = 0;
            int[] l0 = new int[m*2 + 10];
            int[] r0 = new int[m * 2 + 10];

            int count = 0;
            int[] l = new int[m*2 + 10];
            int[] r = new int[m*2 + 10];

            count0 = 1;
            l0[0] = 1;
            r0[0] = points[0].Y==1 ? points[0].X-1 : maxX;

            log("Arrays created");


            int i = 0;
            int lineY = 0;
            int minX = 1;
            while (i < points.Count) // go over the points line by line
            {

                var p = points[i];
                if (p.Y > lineY + 1)
                {
                    // we have empty line
                    count0 = 1;
                    l0[0] = minX;
                    r0[0] = maxX;
                }

                lineY = p.Y;


                {
                    if (lineY == maxX * 1 / 10) log("Progress 10%");
                    else if (lineY == maxX * 2 / 10) log("Progress 20%");
                    else if (lineY == maxX * 3 / 10) log("Progress 30%");
                    else if (lineY == maxX * 4 / 10) log("Progress 40%");
                    else if (lineY == maxX * 5 / 10) log("Progress 50%");
                    else if (lineY == maxX * 6 / 10) log("Progress 60%");
                    else if (lineY == maxX * 7 / 10) log("Progress 70%");
                    else if (lineY == maxX * 8 / 10) log("Progress 80%");
                    else if (lineY == maxX * 9 / 10) log("Progress 90%");
                }


                count = 0; // reset target array
                for (int j0 = 0; j0 < count0; ++j0) // go over all the spans
                {
                    int x0 = l0[j0];
                    while (i < points.Count && points[i].Y == lineY && points[i].X <= r0[j0])
                    {
                        p = points[i];
                        if (p.X >= l0[j0])
                        {

                            // split span into two
                            if (x0 < p.X)
                            {
                                l[count] = x0;
                                r[count] = p.X - 1;
                                count++;
                            }

                            x0 = p.X + 1;
                        }
                        i++;
                    }

                    if (x0 <= r0[j0]) // copy reminder of the span
                    {
                        int finish = r0[j0];

                        if (i < points.Count && points[i].Y == lineY) // limit finish by next point
                        {
                            finish = Math.Max(finish, points[i].X - 1);
                        } 
                        else
                        {
                            finish = maxX;
                        }

                        if (j0 + 1 < count0) // ensure we have not overlapped with next span
                        {
                            finish = Math.Min(finish, l0[j0 + 1]);
                        }

                        l[count] = x0;
                        r[count] = finish;
                        count++;
                    }
                }
                if (count == 0)
                {
                    // no more free spans
                    return false;
                }

                while (i < points.Count && points[i].Y == lineY)
                {                    
                    i++;
                }

                minX = l[0];

                Helper.Swap(ref l0, ref l);
                Helper.Swap(ref r0, ref r);
                Helper.Swap(ref count0, ref count);

#if DEBUG
                for(int t=0;t<count0;t++)
                {
                    Console.Write($"{lineY}: {l0[t]}..{r0[t]} ");
                }
                Console.WriteLine();
#endif
            }
            if (hasFreeSpaceInTheEnd)
            {
                return true;
            }
                        
            return r0[count0 - 1] == maxX;
        }

        private static void Compress(List<Point> points, int m, int n, out int maxX, out bool hasFreeSpaceInTheEnd)
        {
            points.Sort((a, b) => a.X - b.X);
            int delta = 0;
            int prev = 0;
            foreach (var p in points)
            {
                p.X -= delta;
                if (p.X > prev + 1)
                {
                    int extra = p.X - prev - 2;
                    p.X -= extra;
                    delta += extra;
                }
                prev = p.X;
            }
            maxX = Math.Min(n - delta, prev+1);

            points.Sort((a, b) => a.Y == b.Y ? a.X - b.X : a.Y - b.Y);
            delta = 0;
            prev = 0;
            foreach (var p in points)
            {
                p.Y -= delta;
                if (p.Y > prev + 1)
                {
                    int extra = p.Y - prev - 2;
                    p.Y -= extra;
                    delta += extra;
                }
                prev = p.Y;
            }
            hasFreeSpaceInTheEnd = n - delta > prev;
        }

        public class Point
        {
            public Point() { }
            public Point(int x, int y) { X = x; Y = y; }

            public int X { get; set; }
            public int Y { get; set; }

            public override string? ToString()
            {
                return X + "," + Y;
            }
        }

        public static void Main()
        {
            var task = new TaskD();
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
