using System;
using System.Collections.Generic;

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
            var have = new Dictionary<char, int>();
            var need = new Dictionary<char, int>();

            string line;
            Input.Next(out line);
            foreach (var ch in line)
            {
                if (!have.ContainsKey(ch)) have[ch] = 0;
                have[ch]++;
            }
            Input.Next(out line);
            foreach (var ch in line)
            {
                if (!need.ContainsKey(ch)) need[ch] = 0;
                need[ch]++;
            }
            var result = 0;
            foreach (var ch in need.Keys)
            {
                if (!have.ContainsKey(ch))
                {
                    result = -1;
                    break;
                }
                result += Math.Min(have[ch], need[ch]);
            }
            Console.WriteLine(result);
        }
    }
}
