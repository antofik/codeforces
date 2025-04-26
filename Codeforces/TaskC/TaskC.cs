using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskC
    {
        private void Solve(int test)
        {
            Input.Next(out int n, out int k);
            long[] A = Input.ArrayLong();
            long[] A2 = new long[A.Length];
            for(int i=1;i<=n;++i)
            {
                A2[i] = A[n - i + 1];
            }
            var (left, li) = Count(A, n, k);
            var (right, ri) = Count(A2, n, k);
            ri = n - ri + 1;

            var ok = left > 1 || right > 1 || (left>0 && right > 0 && li < ri);

            Output.Write(ok);
        }

        private (int,int) Count(long[] A, int n, int k)
        {
            var sorted = new List<long>();
            int minBound = 0;
            int less = 0;
            int more = 0;
            for(int i=1;i<n-1;++i)
            {
                if (A[i] > k) more++; else less++;
                if (Ok(less, more))
                {
                    minBound = i;
                    while (i < n - 1 && A[i + 1] > k)
                    {
                        i++;
                        if (A[i] > k) more++; else less++;                        
                        if (!Ok(less, more))
                        {
                            i--;
                            break;
                        }
                    }

                    less = 0;
                    more = 0;
                    for (int j = i + 1; j < n; ++j)
                    {
                        if (A[j] > k) more++; else less++;
                        if (Ok(less, more))
                        {
                            return (2, j);
                        }
                    }
                    return (1, minBound);
                }
            }
            return (0, n);
        }

        private bool Ok(int less, int more)
        {
            if ((less + more) % 2 == 0)
            {
                return less >= more;
            }
            return less >= more + 1;
        }

        private long Med(List<long> list)
        {
            var index = (list.Count+1)/2-1; // 1=>0, 2=>0 3=>1 4=>1
            return list[index];
        }

        private void Add(List<long> list, long x)
        {
            var index = list.BinarySearch(x);
            if (index >= 0)
            {
                list.Insert(index, x);
            }
            else
            {
                list.Insert(~index, x);
            }
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
            var task = new TaskC();
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
