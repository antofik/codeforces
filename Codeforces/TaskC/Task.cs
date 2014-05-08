using System;
using System.Linq;

namespace Codeforces.TaskC
{
    public class Task
    {
        void Solve()
        {
            var line = Console.ReadLine().ToArray();

            var current = 1;
            var result = 1;
            while (current < line.Length && line[current] == '0') current++;
            var lastBadIndex = 1;
            while (current < line.Length)
            {
                var length = 1;
                while (current + length < line.Length && line[current + length] == '0') length++;
                if (!IsBiggerOrEqual(line, 0, current, current, length))
                {
                    lastBadIndex = result + 1;
                }
                current += length;
                result++;
            }

            Console.WriteLine(result - lastBadIndex + 1);
        }

        private bool IsBiggerOrEqual(char[] line, int x1, int length1, int x2, int length2)
        {
            if (length1 > length2) return true;
            if (length1 < length2) return false;
            for(var i=0;i<length1;i++)
                if (line[x1 + i] < line[x2 + i])
                    return false;
            return true;
        }

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
