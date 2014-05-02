using System;
using System.Globalization;
using System.Linq;
using System.Text;

/*Library*/

namespace Codeforces.TaskE
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
            int n, m;
            Input.Next(out n, out m);
            var a = Input.ArrayInt().ToArray();
            var result = 0L;
            const int bits_count = 17;
            for (var b = 0; b < bits_count; b++)
                for (var i = 0; i < n; i++)
                    if ((a[i] & (1 << b)) != 0)
                    {
                        int c;
                        for (c = i + 1; c < n && (a[c] & (1 << b)) != 0; c++) {}
                        result += (1L*(c-i)*(c - i + 1) >> 1) << b;
                        i = c;
                    }

            var output = new StringBuilder();
            for (var t = 0; t < m; t++)
            {
                int p, v;
                Input.Next(out p, out v);
                p--;

                for (var b = 0; b < bits_count; b++)
                    if (((a[p] & 1 << b) ^ (v & 1 << b)) != 0)
                    {
                        int l, r, i;
                        for (i = p + 1; i < n && ((a[i] & 1 << b) != 0); i++) {}
                        r = i - p;
                        for (i = p - 1; i >= 0 && ((a[i] & 1 << b) != 0); i--) {}
                        l = p - i;

                        result += ((v&1<<b)==0 ? -1 : 1) * (1L*l*r)<<b;
                    }
                a[p] = v;
                output.AppendLine(result.ToString(CultureInfo.InvariantCulture));
            }
            Console.WriteLine(output);
        }
    }
}
