using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskB
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {
            string input;
            Input.Next(out input);
            var n = input.Length;
            const int count = 100;

            if (n > ('z' - 'a' + 1)*(count - 1))
            {
                var d = new Dictionary<char, int>();
                for (var ch = 'a'; ch <= 'z'; ch++) d[ch] = 0;
                foreach (var ch in input) d[ch]++;
                var s = d.First(c => c.Value >= count).Key;
                Console.WriteLine(string.Join("", Enumerable.Repeat(s, count)));
            }
            else
            {
                var dp0 = new string[n];
                var dp1 = new string[n];
                var dp2 = new string[n];
                for (var i = 0; i < n; i++)
                {
                    dp0[i] = string.Empty;
                    dp1[i] = input[i] + "";
                }

                for (var len = 2; len <= n; len++)
                {
                    for (var l = 0; l + len - 1 < n; l++)
                    {
                        if (input[l] == input[l + len - 1])
                        {
                            dp2[l] = input[l] + dp0[l + 1] + input[l];
                        }
                        else
                        {
                            var s1 = dp1[l + 1];
                            var s2 = dp1[l];
                            dp2[l] = s1.Length > s2.Length ? s1 : s2;
                        }
                    }
                    foreach (var s in dp2)
                    {
                        if (s != null && s.Length == count)
                        {
                            Console.WriteLine(s);
                            Environment.Exit(0);
                        }
                        else if (s != null && s.Length == count + 1)
                        {
                            Console.WriteLine(s.Substring(0, count/2) + s.Substring(count/2 + 1));
                            Environment.Exit(0);
                        }
                    }

                    dp0 = dp1;
                    dp1 = dp2;
                    dp2 = new string[n - len + 1];
                }
                Console.WriteLine(dp1[0]);
            }
        }
    }

    #region Input

    public class Input
    {
        private static string _line;

        public static bool Next()
        {
            _line = Console.ReadLine();
            return !string.IsNullOrEmpty(_line);
        }

        public static bool Next(out long a)
        {
            var ok = Next();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out long a, out long b)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
            }
            else
            {
                a = b = 0;
            }

            return ok;
        }

        public static bool Next(out long a, out long b, out long c)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
            }
            else
            {
                a = b = c = 0;
            }
            return ok;
        }

        public static bool Next(out long a, out long b, out long c, out long d)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
            }
            else
            {
                a = b = c = d = 0;
            }
            return ok;
        }

        public static bool Next(out long a, out long b, out long c, out long d, out long e)
        {
            var ok = Next();
            if (ok)
            {
                var array = _line.Split(' ').Select(long.Parse).ToArray();
                a = array[0];
                b = array[1];
                c = array[2];
                d = array[3];
                e = array[4];
            }
            else
            {
                a = b = c = d = e = 0;
            }
            return ok;
        }

        public static List<long> Numbers()
        {
            Next();
            return _line.Split(' ').Select(long.Parse).ToList();
        }

        public static bool Next(out string value)
        {
            value = "";
            if (!Next()) return false;
            value = _line;
            return true;
        }
    }

    #endregion

}
