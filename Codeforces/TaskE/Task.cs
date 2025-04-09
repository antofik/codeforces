using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codeforces.TaskE
{
    public class Task
    {
        private const int MOD = 1000_000_007;

        void Solve()
        {
            int T = int.Parse(Console.ReadLine());
            for(int t=1;t<=T;++t)
            {
                int n = int.Parse(Console.ReadLine());
                int[] A = ReadArray();

                var factorials = GetFactorials(n);
                var C = GetCombinations(n);

                int[] Q = GetCountOfMisses(n, A);
                int ZEROES = Q[n];

                int[] Z = GetCountOfMissesByN(n, A);

                int[,] N = new int[n + 1, n + 1];

                int left = n;
                for (int i = 1; i <= n; ++i)
                {
                    int right = n;
                    for (int j = n; j >= i; j--)
                    {
                        var K = Math.Min(j - i, Math.Min(left, right) - 1);
                        var q = Q[j] - Q[i - 1]; // how many -1 in [i,j]
                        var p = ZEROES - q;
                        if (K >= 0)
                        {
                            var misses = Z[K]; // how many -1 in A[0..k]

                            N[0, q]++;
                            N[K+1, q]--;
                        }

                        if (A[j] != -1)
                        {
                            right = Math.Min(right, A[j]);
                        }
                    }

                    if (A[i] != -1)
                    {
                        left = Math.Min(left, A[i]);
                    }
                }
                for (int q = 0; q <= ZEROES; ++q)
                {
                    for (int k=1;k<n;++k)
                    {
                        N[k, q] += N[k - 1, q];
                    }
                }

                int ans = 0;
                for(int k=0;k<n;k++)
                {
                    int misses = Z[k];
                    for(int q=0;q<=ZEROES;++q)
                    {
                        if (N[k,q] != 0)
                        {
                            int multiplier = (int)(C[q, misses] * ((long) factorials[misses] * factorials[ZEROES - misses] % MOD) % MOD);
                            ans = (int)((ans + (long) N[k, q] * multiplier) % MOD);
                        }
                    }
                }

                Console.WriteLine(ans);
            }
        }

        private static string Dump(int[,] array)
        {
            var str = new StringBuilder();
            var X = array.GetLength(0);
            var Y = array.GetLength(1);
            for (int x=0;x<X;++x)
            {
                for(int y=0;y<Y;++y)
                {
                    str.AppendFormat(("" + array[x, y]).PadLeft(4));
                }
                str.Append("\n");
            }
            return str.ToString();
        }

        private static int[] ReadArray()
        {
            var list = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            list.Insert(0, 0);
            return list.ToArray();
        }

        private static int[] GetCountOfMisses(int n, int[] A)
        {
            var Q = new int[n + 2];
            for (int i = 1; i <= n; ++i)
            {
                Q[i] = Q[i - 1];
                if (A[i] == -1)
                {
                    Q[i]++;
                }
            }

            return Q;
        }

        private static int[] GetCountOfMissesByN(int n, int[] A)
        {
            var nums = new int[n + 1];
            for (int i = 1; i <= n; ++i)
            {
                if (A[i] != -1)
                {
                    nums[A[i]] = 1;
                }
            }
            var counts = new int[n + 1];
            counts[0] = 1-nums[0];
            for (int i = 1; i < n; ++i)
            {
                counts[i] = counts[i - 1] + 1 - nums[i];
            }

            return counts;
        }

        private int[] GetFactorials(int n)
        {
            int[] factorials = new int[n + 1];
            factorials[0] = 1;
            long value = 1;
            for (int i = 1; i <= n; ++i)
            {
                value = value * i % MOD;
                factorials[i] = (int) value;
            }
            return factorials;
        }

        private int[,] GetCombinations(int n)
        {
            int[,] C = new int[n + 1, n + 1];
            C[0, 0] = 1;
            for (int i = 1; i <= n; ++i)
            {
                C[i, 0] = 1;
                C[i, i] = 1;
                for (int j=1; j<i; j++)
                {
                    long c = ((long)C[i - 1, j] + C[i - 1, j - 1]) % MOD;
                    C[i, j] = (int)c;
                }
            }
            return C;
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
