using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        void Solve()
        {
            var line = Console.ReadLine() ?? "";
            var n = line.Length;
            var left = new List<int>();
            var right = new List<int>();
            var L = line[n - 1] == 'L' ? 'L' : 'R';
            var R = L == 'L' ? 'R' : 'L';

            if (n == 1)
            {
                Console.WriteLine(1);
                return;
            }

            var x = 0;
            var current = line[0];
            var list = current == R ? right : left;
            var direction = current == R ? 1 : -1;

            if (current == R)
                left.Add(0);

            for (var i = 0; i < n; i++)
            {
                var ch = line[i];
                if (ch != current)
                {
                    list.Add(x);
                    current = ch;
                    list = current == R ? right : left;
                    direction = current == R ? 1 : -1;
                }
                x += direction;
            }
            list.Add(x);

            if (right.Count == 0)
            {
                Console.WriteLine(1);
                return;
            }

            var maxLeft = left.Take(left.Count-1).Min();
            var end = left[left.Count - 1];
            if (maxLeft > end)
            {
                Console.WriteLine(1);
                return;
            }

            var maxRight = Math.Max(0, right.Max());
            if (maxRight == 0)
            {
                Console.WriteLine(0);
                return;
            }

            if (!ok(left, right, 0))
            {
                Console.WriteLine(0);
                return;
            }

            var l = 0;
            var r = maxRight - 1;
            while (l < r)
            {
                var m = (l + r + 1)/2;
                if (ok(left, right, m))
                    l = m;
                else
                    r = m - 1;
            }

            if (!ok(left, right, l))
            {
                Console.WriteLine(0);
                return;
            }

            Console.WriteLine(l + 1);
        }

        private bool ok(List<int> left, List<int> right, int stone)
        {
            var maxLeft = Math.Min(0, left[0]);
            var maxRight = 0;
            var delta = 0;
            var end = left[left.Count - 1] - Math.Max(0, right.Max() - stone);
            for (var i = 1; i < left.Count - 1; i++)
            {
                maxRight = Math.Max(maxRight, right[i - 1]);
                if (maxRight > stone + delta)
                    delta += maxRight - stone - delta;
                maxLeft = Math.Min(maxLeft, left[i] - delta);
            }
            return end < maxLeft;
        }
    }
}















