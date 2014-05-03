using System;
using System.Collections.Generic;

/*Library*/

namespace Codeforces.TaskC
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
                Console.WriteLine(ex.StackTrace);
            }
        }

        private string Mul(char ch, long count)
        {
            if (count == 0) return string.Empty;
            if (count == 1) return ch.ToString();
            var a = new char[count];
            for (var i = 0; i < count; i++)
                a[i] = ch;
            return string.Join("", a);
        }

        void Solve()
        {
            int a, b;
            Input.Next(out a, out b);

            if (a == 0 || b <= 1)
            {
                Console.WriteLine(1L * a * a - 1L * b * b);
                Console.WriteLine(Mul('o', a) + Mul('x', b));
                return;
            }

            long k = 0;
            
            // k = [a-1 + Math.Sqrt((a-1)*(a-1) - 4*(b-a))] / 2
            //var estimate = (a - 1 + (long) Math.Sqrt((a - 1)*(a - 1) - 4*(b - a)))/2;
            //k = estimate;
            var result = Calc(a, b, k);

            //for (var i = Math.Max(estimate - 10, 1); i <= Math.Max(a, estimate + 10); i++)
            for (var i = 0; i <= Math.Min(a-1, b - 2); i++)
            {
                var value = Calc(a, b, i);
                if (value > result)
                {
                    result = value;
                    k = i;
                }
            }

            Console.WriteLine(result);

            var count = k + 1L;
            var x = b/(count + 1);
            var y = x + 1;
            var count_of_y = b - x * (count + 1L);
            var count_of_x = k + 2L - count_of_y;

            var strX = Mul('x', x);
            var strY = Mul('x', y);

            var s = count_of_x-- > 0 ? strX : strY;
            s += Mul('o', a - k);
            s += count_of_x-- > 0 ? strX : strY;

            for (var i = 0; i < k; i++)
            {
                s += 'o';
                s += count_of_x-- > 0 ? strX : strY;
            }

            Console.WriteLine(s);
        }

        private long Calc(long a, long b, long k)
        {
            //-x (a-k) -x 1 -x 1 -x 1 -y 1 -y 1 -y .... -y 1 -y
            // x = y + 1

            var result = 1L*(a - k) * (a - k) + k;
            var count = k + 1L;
            var x = b/(count + 1L);
            var y = x + 1L;
            var count_of_y = b - x*(count + 1L);
            var count_of_x = k + 2L - count_of_y;
            result -= count_of_x*x*x;
            result -= count_of_y*y*y;
            return result;
        }
    }
}
