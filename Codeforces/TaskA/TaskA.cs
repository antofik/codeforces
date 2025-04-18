using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskA
    {
        private void Solve()
        {
            Input.Next(out long l, out long r);
            r++;
            string sl = l.ToString();
            string sr = r.ToString();
            long ans = 0;
            for(int i=sl.Length;i<=sr.Length-1;++i)
            {
                ans += (long) (i >= 2 ? Math.Pow(10, i-2) : 1) * 9;
            }
            ans -= Count(l);
            ans += Count(r);
            Console.WriteLine(ans);
        }

        private long Count(long v)
        {
            string s = v.ToString();
            var d0 = int.Parse(s.Substring(0, 1));
            var dn = int.Parse(s.Substring(s.Length-1));
            long mid = s.Length > 2 ? long.Parse(s.Substring(1, s.Length - 2)) : 0;
            if (s.Length == 1)
            {
                return d0 - 1;
            }
            if (s.Length == 2)
            {
                return dn > d0 ? d0 : d0-1;
            }
            long ans = 0;
            ans += (long) Math.Pow(10, s.Length - 2) * (d0 - 1);
            if (dn < d0)
            {
                ans += mid;
            }
            else
            {
                ans += mid;
                if (d0 != dn)
                {
                    ans++;
                }
            }
            return ans;
        }


        public static void Main()
        {
            var task = new TaskA();
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
