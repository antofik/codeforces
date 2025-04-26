using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task
{
    public class TaskB
    {
        private void Solve(int test)
        {
            int n = Input.Int();
            char[] s = Console.ReadLine().ToCharArray();
            if (s.All(ch => ch == '0'))
            {
                Output.Write(s.Length);
            }
            else if (s.All(ch => ch == '1'))
            {
                Output.Write(s.Length + 1);
            } else
            {
                int ones = 0;
                int zeroes = 0;
                bool found = false;
                bool oneFirst = false;
                for (int i=1;i<s.Length;++i)
                {
                    if (s[i] == '1' && s[i-1] == '0')
                    {
                        if (!found)
                        {
                            oneFirst = true;
                            found = true;
                        }
                        ones++;
                    }
                    if (s[i] == '0' && s[i-1] == '1')
                    {
                        if (!found)
                        {
                            oneFirst = false;
                            found = true;
                        }
                        zeroes++;
                    }
                }

                if (ones == 1 && zeroes==0)
                {
                    Output.Write(n + 1);
                }
                else if (zeroes==1 && ones==0)
                {
                    Output.Write(n + 1);
                }
                else // both > 0
                {
                    if (oneFirst && zeroes==1 && ones == 1)
                    {
                        Output.Write(n + 1);
                    }
                    else
                    {
                        Output.Write(n + zeroes + ones - 2 + (s[0]=='1' ? 1 : 0));
                    }
                }
            }
        }

        private void Solve()
        {
            int T = Input.Int();
            for (int t = 1; t <= T; ++t)
            {
                Solve(t);
            }
        }

        public static void Main()
        {
            var task = new TaskB();
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
