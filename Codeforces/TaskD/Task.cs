using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        private void Solve()
        {
            string text;
            long k;
            Input.Next(out text);
            Input.Next(out k);
            long n = text.Length;

            var corrections = new long[n, n + 1];

            for (var i = 0; i < n; i++)
                for (var j = i + 1; j < n; j++)
                    for (var t = 0; t < (j - i + 1) / 2; t++)
                        corrections[i, j - i + 1] += text[i + t] == text[j - t] ? 0 : 1;

            var dp = new long[n + 1, k + 1];
            for (var i = 0; i <= n; i++)
            {
                for (var j = 0; j <= k; j++)
                    dp[i, j] = long.MaxValue / 2;
                dp[i, 0] = 0;
                dp[i, 1] = corrections[0, i];
            }
            for (var i = 0; i <= k; i++) dp[0, i] = 0;

            for (var count = 1; count < k; count++)
            {
                for (var l = 0; l <= n; l++)
                {
                    for (var len = 1; l + len <= n; len++)
                    {
                        dp[l + len, count + 1] = Math.Min(dp[l + len, count + 1], dp[l, count] + corrections[l, len]);
                    }
                }
            }

            var min = long.MaxValue;
            var minIndex = -1;
            for (var i = 1; i <= k; i++)
                if (dp[n, i] < min)
                {
                    min = dp[n, i];
                    minIndex = i;
                }
            Console.WriteLine(min);

            var prev = n;
            var answers = new List<string>();
            for (var j = 1; j < minIndex; j++)
            {
                var m = 100000L;
                for (var i = prev - 1; i > 0; i--)
                {
                    if (dp[i, minIndex - j] + corrections[i, prev - i] > min) continue;
                    m = i;
                }
                answers.Add(text.Substring((int)m, (int)(prev - m)));
                prev = m;
                min = dp[m, minIndex - j];
            }
            answers.Add(text.Substring(0, (int)(prev)));

            answers.Reverse();
            for (var i = 0; i < answers.Count; i++)
                answers[i] = answers[i].Substring(0, (answers[i].Length + 1)/2) + new string(answers[i].Substring(0, (answers[i].Length)/2).Reverse().ToArray());
            
            Console.WriteLine(string.Join("+", answers));
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
            bool ok = Next();
            a = ok ? long.Parse(_line) : 0;
            return ok;
        }

        public static bool Next(out long a, out long b)
        {
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            bool ok = Next();
            if (ok)
            {
                long[] array = _line.Split(' ').Select(long.Parse).ToArray();
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
            if (string.IsNullOrEmpty(_line)) return new List<long>();
            return _line.Split(' ').Select(long.Parse).ToList();
        }

        public static bool Next(out string value)
        {
            value = string.Empty;
            if (!Next()) return false;
            value = _line;
            return true;
        }
    }

    #endregion
}