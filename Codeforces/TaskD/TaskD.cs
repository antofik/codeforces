using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskD
    {
        private void Solve()
        {
            int n = int.Parse(Console.ReadLine()!);
            string s = Console.ReadLine();
            (int plus, int minus) = Count(s);

            int count = 1 << n;

            for(int x=minus+1; x <= count - plus; ++x)
            {
                Console.Write(x);
                Console.Write(' ');
            }
        }

        (int,int) Count(string s)
        {
            int plus = 0;
            int minus = 0;

            var pluses = new int[s.Length + 1];
            var minuses = new int[s.Length + 1];
            int ones = 0;
            int zeroes = 0;

            for (int i=s.Length-1;i>=0 ;--i)
            {
                if (s[i] == '1')
                {
                    ones++;
                } 
                else
                {
                    zeroes++;
                }
                pluses[i] = zeroes > 0 ? 1 << (zeroes-1) : 0;
                minuses[i] = ones > 0 ? 1 << (ones-1) : 0;
            }

            for(int i=0;i<s.Length;++i)
            {
                if (s[i] == '0')
                {
                    plus += pluses[i];
                }
                else
                {
                    minus += minuses[i];
                }
            }

            return (plus, minus);
        }

        public static void Main()
        {
            var task = new TaskD();
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
