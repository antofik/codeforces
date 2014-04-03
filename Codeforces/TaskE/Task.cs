using System;
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
            return (b/a)%(a + 1)%2 == 0;
            var c = b/a - 1;
            if (a%2 == 1) return c%2 == 1;
            return c%(a+1)==1;
        }

        void Solve()
        {
            long t;
            Input.Next(out t);

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
                Console.WriteLine(Check(a,b) ? "First" : "Second");
            }
        }
    }
}
