using System;

/*Library*/

namespace Codeforces.TaskC
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

        /*abacabadabacaba*/
        
        private long MaxSubstring(long best, long l, long r, long a, long b, int ch)
        {
            if (l > r || a > b) return best;
            if ((best >= b - a + 1) || (best >= r - l + 1)) return best;

            best = Math.Max(best, Math.Min(r, b) - Math.Max(l, a) + 1);
            var divide = 1 << ch;
             bool f1 = false, f2 = false;

            if (r == divide && l == r) return best;
            if (a == divide && a == b) return best;
            
            if (l == divide) l++;
            if (r == divide) r--;
            if (a == divide) a++;
            if (b == divide) b--;

            if (l > divide)
            {
                l -= divide;
                f1 = true;
            }
            if (r > divide)
            {
                r -= divide;
                f1 = !f1;
            }
            if (a > divide)
            {
                a -= divide;
                f2 = true;
            }
            if (b > divide)
            {
                b -= divide;
                f2 = !f2;
            }

            if (--ch >= 0)
            {
                if (f1 && f2) best = Math.Max(Math.Max(best, divide - Math.Max(l, a)), Math.Min(r, b));
                if (f1 && !f2)
                {
                    best = MaxSubstring(best, l, divide - 1, a, b, ch);
                    best = MaxSubstring(best, 1, r, a, b, ch);
                }
                if (!f1 && f2)
                {
                    best = MaxSubstring(best, l, r, a, divide - 1, ch);
                    best = MaxSubstring(best, l, r, 1, b, ch);
                }
                if (!f1 && !f2) best = MaxSubstring(best, l, r, a, b, ch);
                if (f1 && f2)
                {
                    best = MaxSubstring(best, 1, r, a, divide - 1, ch);
                    best = MaxSubstring(best, l, divide - 1, 1, b, ch);
                }
            }

            return best;
        }

        void Solve()
        {
            long l1, r1, l2, r2;
            Input.Next(out l1, out r1, out l2, out r2);
            var n = MaxSubstring(0, l1, r1, l2, r2, 29);
            Console.WriteLine(n);
        }
    }
}
