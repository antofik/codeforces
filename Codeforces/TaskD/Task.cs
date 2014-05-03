using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

public class Input
{
    private static string _line;

    public static bool Next()
    {
        _line = Console.ReadLine();
        return _line != null;
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

    public static bool Next(out int a)
    {
        var ok = Next();
        a = ok ? int.Parse(_line) : 0;
        return ok;
    }

    public static bool Next(out int a, out int b)
    {
        var ok = Next();
        if (ok)
        {
            var array = _line.Split(' ').Select(int.Parse).ToArray();
            a = array[0];
            b = array[1];
        }
        else
        {
            a = b = 0;
        }

        return ok;
    }

    public static bool Next(out int a, out int b, out int c)
    {
        var ok = Next();
        if (ok)
        {
            var array = _line.Split(' ').Select(int.Parse).ToArray();
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

    public static bool Next(out int a, out int b, out int c, out int d)
    {
        var ok = Next();
        if (ok)
        {
            var array = _line.Split(' ').Select(int.Parse).ToArray();
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

    public static bool Next(out int a, out int b, out int c, out int d, out int e)
    {
        var ok = Next();
        if (ok)
        {
            var array = _line.Split(' ').Select(int.Parse).ToArray();
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

    public static bool Next(out int a, out int b, out int c, out int d, out int e, out int f)
    {
        var ok = Next();
        if (ok)
        {
            var array = _line.Split(' ').Select(int.Parse).ToArray();
            a = array[0];
            b = array[1];
            c = array[2];
            d = array[3];
            e = array[4];
            f = array[5];
        }
        else
        {
            a = b = c = d = e = f = 0;
        }
        return ok;
    }

    public static IEnumerable<long> ArrayLong()
    {
        return !Next() ? new List<long>() : _line.Split().Select(long.Parse);
    }

    public static IEnumerable<int> ArrayInt()
    {
        return !Next() ? new List<int>() : _line.Split().Select(int.Parse);
    }

    public static bool Next(out string value)
    {
        value = string.Empty;
        if (!Next()) return false;
        value = _line;
        return true;
    }
}

namespace Codeforces.TaskD
{
    public class Task
    {
        public static void Main()
        {
            try
            {
                var task = new Task();
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.InnerException);
            }
        }

        void Solve()
        {
            int n, m;
            Input.Next(out n, out m);

            var rows = new HashSet<int>();
            var columns = new HashSet<int>();

            var t = m;
            while (t-->0)
            {
                int r, c;
                Input.Next(out r, out c);
                rows.Add(r);
                columns.Add(c);
            }

            var dp = new double[n+2, n+2];
            
            for(var i=n;i>=0;i--)
                for(var j=n;j>=0;j--)
                    if (i != n || j != n)
                        dp[i, j] = ((n - i)*j*dp[i + 1, j] + i*(n - j)*dp[i, j + 1] + (n - i)*(n - j)*dp[i + 1, j + 1] + n*n)/(1d*n*n - i*j);

            Console.WriteLine(dp[rows.Count, columns.Count].ToString("F10"));
        }
    }
}
