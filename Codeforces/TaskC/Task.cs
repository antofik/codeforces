using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            int t = int.Parse(Console.ReadLine());
            while (t-- > 0)
            {
                int n = int.Parse(Console.ReadLine());
                int[] a = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                int[] b = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

                int[] bi = new int[n+1];
                for (int i = 0; i < n; i++)
                {
                    bi[b[i]] = i;
                }

                var mid = n / 2; // n=5, mid=2

                var ans = new List<Pair>();

                var bad = false;
                int sameIndex = -1;
                for (int i = 0; i < n; i++)
                {
                    if (a[i] == b[i])
                    {
                        if (sameIndex != -1 || n % 2 == 0)
                        {
                            bad = true;
                            break;
                        }
                        sameIndex = i;
                    }
                }

                if (bad)
                {
                    Console.WriteLine(-1);
                    continue;
                }

                if (sameIndex != -1 && sameIndex != mid)
                {
                    swap(a, sameIndex, mid);
                    swap(b, sameIndex, mid); /// i => b[i]   mid => b[mid]  => bi[b[i]]=i    bi[b[mid]]=mid
                    swap(bi, b[sameIndex], b[mid]);
                    ans.Add(new Pair { j = sameIndex + 1, k = mid + 1 });
                }

                for (int i = 0; i < n; i++)
                {
                    if (b[i] != a[bi[a[i]]])
                    {
                        bad = true;
                        break;
                    }
                }
                if (bad)
                {
                    Console.WriteLine(-1);
                    continue;
                }

                var ok = true;
                for (int i = 0; i < n / 2; i++)
                {
                    var k = n - 1 - i;
                    if (b[k] == a[i]) continue;
                    var j = bi[a[i]];
                    if (j < i)
                    {
                        ok = false;
                        break;
                    }
                    if (j == i)
                    {
                        if (n % 2 == 0)
                        {
                            ok = false;
                            break;
                        }
                        if (a[mid] == b[mid])
                        {
                            ok = false;
                            break;
                        }

                        swap(a, i, mid);
                        swap(b, i, mid);
                        swap(bi, b[i], b[mid]);

                        ans.Add(new Pair { k = i + 1, j = mid + 1 });
                        i--;
                    }
                    else
                    { 
                        swap(a, j, k);
                        swap(b, j, k);
                        swap(bi, b[j], b[k]);

                        ans.Add(new Pair { k = k + 1, j = j + 1 });
                    }
                }

                if (n%2==1)
                {
                    if (a[mid] != b[mid])
                    {
                        ok = false;
                    }
                }

                if (ok)
                {
                    Console.WriteLine(ans.Count);
                    foreach(var p in ans)
                    {
                        Console.WriteLine(p.k + " " + p.j);
                    }
                } 
                else
                {
                    Console.WriteLine(-1);
                }
            }
        }

        private void swap(int[] a, int i, int j)
        {
            var temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }


        public class Pair
        {
            public int k { get; set; }
            public int j { get; set; }
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
