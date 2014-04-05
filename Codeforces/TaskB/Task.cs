using System;
using System.Collections.Generic;
using System.Text;

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
            var list = new List<string>();
            string s;
            while(Input.Next(out s)) list.Add(s);
            var result = new List<string>();
            var line = new StringBuilder();
            var count = 0;
            foreach (var i in list)
            {
                if (!i.TrimStart().StartsWith("#"))
                {
                    line.Append(i.Replace(" ", ""));
                    count++;
                }
                else 
                {
                    if (count > 0) result.Add(line.ToString());
                    line = new StringBuilder();
                    count = 0;
                    result.Add(i);
                }
            }
            if (count > 0) result.Add(line.ToString());
            Console.WriteLine(string.Join(Environment.NewLine, result));
        }
    }
}
