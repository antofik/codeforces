using System;
using System.Collections.Generic;

/*Library*/

namespace Codeforces.TaskE
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            try
            {
                task.Solve();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                throw;
            }
        }

        bool Check(long b, long a)
        {
            if (a == 0) return false;
            if (!Check(a, b%a)) return true;
            var c = b/a;
            return c%(a+1)%2==0;
        }

        void Solve()
        {
            long t;
            Input.Next(out t);

            var l = new List<string>();
            for (var i = 0; i < t; i++)
            {
                long a, b;
                Input.Next(out a, out b);
                if (a < b)
                {
                    var c = b;
                    b = a;
                    a = c;
                }
                l.Add(Check(a,b) ? "First" : "Second");
            }
            Console.WriteLine(string.Join(Environment.NewLine, l));
        }
    }
}
