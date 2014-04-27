using System;
using System.Collections.Generic;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private int N;
        private bool[,] a;

        private void dfs(HashSet<int> visited, int v)
        {
            visited.Add(v);
            for (var i = 0; i < N; i++)
                if (a[v, i] && i != v && !visited.Contains(i))
                    dfs(visited, i);
        }

        private void dfs_inv(HashSet<int> visited, int v)
        {
            visited.Add(v);
            for (var i = 0; i < N; i++)
                if (a[i, v] && i != v && !visited.Contains(i))
                    dfs_inv(visited, i);
        }

        private void Solve()
        {
            N = int.Parse(Console.ReadLine());
            a = new bool[N, N];
            for (var i = 0; i < N; i++)
            {
                var args = Console.ReadLine().Split();
                for (var j = 0; j < N; j++)
                    a[i, j] = args[j] != "0";
            }

            var s = 0;
            while (!a[s, s]) s++;
            var set = new HashSet<int>();
            dfs(set, s);
            var ok = set.Count == N;
            if (ok)
            {
                set.Clear();
                dfs_inv(set, s);
                ok = set.Count == N;
            }
            Console.WriteLine(ok ? "YES" : "NO");
        }
    }
}
