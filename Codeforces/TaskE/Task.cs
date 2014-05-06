using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private List<int>[] tree;
        private int[] start;
        private int[] end;
        private int[] level;
        private int timer;

        private long[] st1, st2;

        private void dfs(int v, int lvl=1)
        {
            start[v] = ++timer;
            level[v] = lvl;
            foreach (var u in tree[v])
                dfs(u, lvl+1);
            end[v] = timer;
        }

        private const long mod = 1000000007;

        private void update(long v1, long v2, int t, int l, int r, int start, int end)
        {
            if (start<=l && r <= end)
            {
                st1[t] = (st1[t] + v1)%mod;
                st2[t] = (st2[t] + v2)%mod;
            }
            else if (r >= start && l <= end)
            {
                update(v1, v2, 2*t, l, (l + r)/2, start, end);
                update(v1, v2, 2*t + 1, (l + r)/2 + 1, r, start, end);
            }
        }

        private long sum(int v, long[] st)
        {
            var result = 0L;
            while (v > 0)
            {
                result = (result + st[v])%mod;
                v /= 2;
            }
            return result;
        }

        void Solve()
        {
            var n = int.Parse(Console.ReadLine() ?? "");
            tree = new List<int>[n+1];
            start = new int[n+1];
            end = new int[n+1];
            level = new int[n+1];
            for(var i=1;i<=n;i++)
                tree[i] = new List<int>();
            if (n > 1)
            {
                var P = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (var i = 2; i <= n; i++)
                    tree[P[i - 2]].Add(i);
            }
            else Console.ReadLine();

            dfs(1);

            var size = 0;
            for (var z = n; z > 0; z >>= 1) size++;
            size = 1 << (size - 1);
            if (size < n) size <<= 1;

            st1 = new long[size*2];
            st2 = new long[size*2];
            
            var output = new StringBuilder();
            var q = int.Parse(Console.ReadLine() ?? "");
            while (q-- > 0)
            {
                var line = Console.ReadLine() ?? "";
                var args = line.Split().Select(long.Parse).ToArray();
                var v = args[1];
                if (args[0] == 1)
                {
                    var x = args[2];
                    var k = args[3];
                    update((x + 1L*level[v]*k)%mod, k, 1, 1, size, start[v], end[v]);
                }
                else
                {
                    var u = size + start[v] - 1;
                    var result = (sum(u, st1) - level[v] * sum(u, st2))%mod;
                    result = (result + mod)%mod;
                    output.AppendLine(string.Format("{0}", result));
                }
            }
            Console.Write(output);
        }
    }
}
