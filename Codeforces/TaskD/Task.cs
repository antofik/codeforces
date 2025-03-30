using System;
using System.Collections.Generic;
using System.Linq;


namespace Codeforces.TaskD
{
    public class Task
    {
        void Solve()
        {
            int t;
            Input.Next(out t);
            while(t-->0)
            {
                int n;
                Input.Next(out n);
                var s = Console.ReadLine().ToCharArray().ToList();
                if (s.Count <= 1)
                {
                    Console.WriteLine(-1);
                    continue;
                }
                if (s.Count == 2 && s[0] == s[1])
                {
                    Console.WriteLine(-1);
                    continue;
                }
                int countL = 0;
                int countI = 0;
                int countT = 0;
                foreach(var ch in s)
                {
                    if (ch == 'L') countL++;
                    if (ch == 'I') countI++;
                    if (ch == 'T') countT++;
                }
                var zeroCount = (countL == 0?1:0) + (countI == 0 ? 1 : 0) + (countT==0?1:0);
                if (zeroCount == 2)
                {
                    Console.WriteLine(-1);
                    continue;
                }

                // AAABBBC   
                // AAAcBBBC
                // 

                var output = new List<int>();
                while (countL != countI || countL != countT || countI != countT)
                {
                    var ok = false;
                    var max = Math.Max(countL, Math.Max(countI, countT));

                    var minLetter = countI <= countL && countI <= countT ? 'I'
                        : countL <= countT && countL <= countI ? 'L' : 'T';
                    var maxLetter = countI >= countL && countI >= countT ? 'I'
                        : countL >= countT && countL >= countI ? 'L' : 'T';
                    var midLetter = (char)('L' + 'I' + 'T' - minLetter - maxLetter);

                    ok = Insert(s, minLetter, ref countL, ref countI, ref countT, output);
                    if (!ok)
                    {
                        ok = Insert(s, midLetter, ref countL, ref countI, ref countT, output);
                        if (!ok)
                        {
                            ok = Insert(s, maxLetter, ref countL, ref countI, ref countT, output);
                        }
                    }

                    if (!ok) break;
                }

                if (countI == countL && countI == countT && output.Count <= 2*n)
                {
                    Console.WriteLine(output.Count);
                    foreach (var x in output)
                    {
                        Console.WriteLine(x);
                    }
                } else
                {
                    Console.WriteLine(-1);
                }
            }
        }

        private bool Insert(List<char> s, char ch, ref int countL, ref int countI, ref int countT, List<int> output)
        {
            if (ch == 'L')
                return Insert(s, ch, 1, ref countL, output);
            if (ch == 'I')
                return Insert(s, ch, 1, ref countI, output);
            if (ch == 'T')
                return Insert(s, ch, 1, ref countT, output);
            return false;
        }

        private bool Insert(List<char> s, char ch, int required, ref int count, List<int> output)
        {
            var ok = false;
            while (required > 0)
            {
                var f = false;
                for (int i = 0; i < s.Count - 1 && required > 0; ++i)
                {
                    if (s[i] != s[i + 1] && s[i] != ch && s[i + 1] != ch)
                    {
                        ok = true;
                        f = true;
                        s.Insert(i+1, ch);
                        output.Add(i+1);
                        required--;
                        count++;
#if DEBUG
             //           Console.WriteLine($"Inserting {ch} at position {i + 1}: {(string.Join("", s))}");
#endif
                    }
                }
                if (!f) break;
            }
            return ok;
        }
        //TILII
        //TITLII
        //TITLTII
        //TLITLTII
        //TLILTLTII

        public static void Main()
        {
            var task = new Task();
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
