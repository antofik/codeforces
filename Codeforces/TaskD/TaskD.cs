using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskD
    {
        private void Solve(int test)
        {
            int n = Input.Int();
            long[] A = Input.ArrayLong();
            long max = A.Max() + 1;

            var ids = new Dictionary<long, List<int>>();
            for (int i = 1; i < A.Length; ++i)
            {
                if (A[i] == -1)
                {
                    A[i] = max;
                }

                if (!ids.TryGetValue(A[i], out var l)) {
                    ids[A[i]] = l = new();
                }
                l.Add(i);
            }

            int[] p = new int[n + 1];
            int index = n;
            for (int i = 1; i <= max; i += 2)
            {
                var list = ids[i];
                for (int j = 0; j < list.Count / 2; j++)
                {
                    p[list[j]] = index - j * 2 - 1;
                }
                for (int j = 0; j < (list.Count + 1) / 2; j++)
                {
                    p[list[list.Count - 1 - j]] = index - j * 2;
                }
                index -= list.Count;
            }

            index = 1;
            for (int i = 2; i <= max; i += 2)
            {
                var list = ids[i];
                for (int j = 0; j < list.Count / 2; j++)
                {
                    p[list[j]] = index + j * 2 + 1;
                }
                for (int j = 0; j < (list.Count + 1) / 2; j++)
                {
                    p[list[list.Count - 1 - j]] = index + j * 2;
                }
                index += list.Count;
            }

            Output.Write(p);
        }

        public class Node
        {
            public int Count { get; set; }
            public long Value { get; set; }
            public Node? Prev { get; set; }
            public Node? Next { get; set; }
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
