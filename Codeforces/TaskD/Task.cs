using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);
            var debug = n == 40000 && m == 100000;
            var timer = new Stopwatch();
            timer.Start();
            Action<string> log = message =>
            {
                if (!debug) return;
                Console.WriteLine("{0}ms {1}", timer.ElapsedMilliseconds, message);
            };

            log("Started");

            var X = new int[m];
            var Y = new int[m];
            var linesSet = new Dictionary<int, List<int>>();
            var lines = new List<int>();

            for (var i = 0; i < m; i++)
            {
                Input.Next(out X[i], out Y[i]);
                if (!linesSet.ContainsKey(Y[i]))
                {
                    linesSet[Y[i]] = new List<int>();
                    lines.Add(Y[i]);
                }
                linesSet[Y[i]].Add(X[i]);
            }
            log("Read input");
            lines.Sort();
            log("Sorted lines");
            foreach(var key in linesSet.Keys)
            {
                linesSet[key].Sort();
            }
            log("Sorted cells");

            var prevLine = 0;
            var min = 1;
            var ranges = new List<Tuple<int, int>>{new Tuple<int, int>(1, 1)};

            log("Lines count = " + lines.Count);

            for (var i = 0; i < lines.Count; i++)
            {
                if (i == lines.Count*1/10) log("Progress 10%");
                else if (i == lines.Count * 2 / 10) log("Progress 20%");
                else if (i == lines.Count * 3 / 10) log("Progress 30%");
                else if (i == lines.Count * 4 / 10) log("Progress 40%");
                else if (i == lines.Count * 5 / 10) log("Progress 50%");
                else if (i == lines.Count * 6 / 10) log("Progress 60%");
                else if (i == lines.Count * 7 / 10) log("Progress 70%");
                else if (i == lines.Count * 8 / 10) log("Progress 80%");
                else if (i == lines.Count * 9 / 10) log("Progress 90%");

                var line = lines[i];
                if (line > prevLine + 1)
                    ranges = new List<Tuple<int, int>>(new List<Tuple<int, int>>{new Tuple<int, int>(min, n)});
                prevLine = line;

                var cells = linesSet[line];

                var newRanges = new List<Tuple<int, int>>();
                var rangeI = 0;
                var range = ranges[rangeI];

                var prevCell = 0;
                for (var j = 0; j < cells.Count; j++)
                {
                    var cell = cells[j];
                    if (cell <= range.Item1 || cell - prevCell == 1)
                    {
                        prevCell = cell;
                        continue;
                    }

                    var left = Math.Max(prevCell + 1, range.Item1);
                    prevCell = cell;

                    if (left <= range.Item2 && left < cell)
                        newRanges.Add(new Tuple<int, int>(left, cell - 1));

                    while (cell >= range.Item2 && rangeI < ranges.Count-1) range = ranges[++rangeI];

                    if (rangeI == ranges.Count) break;
                }
                if (prevCell < n)
                    for(;rangeI<ranges.Count;rangeI++)
                    {
                        if (ranges[rangeI].Item2 > prevCell)
                        {
                            var left = Math.Max(prevCell + 1, ranges[rangeI].Item1);
                            newRanges.Add(new Tuple<int, int>(left, n));                        
                        }
                    }
                ranges = newRanges;

                if (ranges.Count == 0)  break;

                min = ranges[0].Item1;
            }

            var ok = ranges.Count > 0 && (ranges.Last().Item2 == n || !lines.Contains(n));
            Console.Write(ok ? 2 * n - 2 : -1);
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
