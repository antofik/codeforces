using System;
using System.Text;
/*Library*/

namespace Codeforces.TaskA
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
            long n, m, x, y;
            Input.Next(out n, out m, out x, out y);
            var A = Input.Numbers();
            var B = Input.Numbers();

            var builder = new StringBuilder(Environment.NewLine);
            var count = 0;
            var j = 0;
            for(var i=0;i<B.Count;i++)
            {
                var b = B[i];
                while(j<A.Count)
                {
                    var a = A[j];
                    if (b >= a - x && b <= a + y)
                    {
                        count++;
                        builder.Append(++j);
                        builder.Append(' ');
                        builder.Append(i + 1);
                        builder.Append(Environment.NewLine);
                        break;
                    }
                    if (b < a - x) break;
                    j++;
                }
            }

            builder.Insert(0, count);
            Console.WriteLine(builder.ToString());
        }
    }
}
