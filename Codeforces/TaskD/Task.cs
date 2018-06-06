using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            Input.Next(out int N);
            var a = Input.ArrayInt().ToArray();
            var max = 10000;
            var dp = new int[N + 2, 2 * max + 2];

            var mod = 1000000007;
            var total = 0;
            var prev = new DefaultDictionary<int, int> { };
            for (var n = 1; n <= N; n++)
            {
                var next = new DefaultDictionary<int, int>();
                var A = a[n - 1];
                foreach (var pair in prev)
                {
                    var value = pair.Key;
                    var count = pair.Value % mod;

                    if (value == A) total = (total + count) % mod;
                    if (value == -A) total = (total + count) % mod;
                    next[value + A] += count;
                    next[value - A] += count;
                }
                next[A]++;
                next[-A]++;

                prev = next;
            }
            Console.WriteLine(total);
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

    public class DefaultDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out var value)) return value;
                return default(TValue);
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
