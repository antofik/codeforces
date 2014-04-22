using System;
using System.Collections.Generic;
using System.Linq;

/*Library*/

namespace Codeforces.TaskB
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

        void Solve()
        {
            int n;
            Input.Next(out n);
            string D;
            Input.Next(out D);
            var before = 0;
            var middle = 0;
            var found = false;
            var result = 0;
            foreach (var d in D.ToCharArray())
            {
                switch (d)
                {
                    case '.':
                        if (found) middle++;
                        else before++;
                        break;
                    case 'L':
                        if (found)
                        {
                            if (middle%2 == 1) result++;
                        }
                        found = false;
                        before = 0;
                        break;
                    case 'R':
                        result += before;
                        before = 0;
                        middle = 0;
                        found = true;
                        break;
                }
            }
            result += before;
            Console.WriteLine(result);
        }
    }
}
