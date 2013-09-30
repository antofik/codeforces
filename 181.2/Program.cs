using System;
using System.IO;
using System.Numerics;
using System.Reflection;

namespace Codeforces
{
    class Program
    {
        static void Main(string[] args)
        {
            const string tournament = "Round181_2";
            const int test = 3;

#if task1
            const int problem = 1;
#elif task2
            const int problem = 2;
#elif task3
            const int problem = 3;
#elif task4
            const int problem = 4;
#elif task5
            const int problem = 5;
#else
            const int problem = 5;
#endif

            var type = Type.GetType(string.Format("Codeforces.Rounds.{0}.Problem{1}", tournament, problem));
            if (type == null)
            {
                Console.WriteLine("Tournament or problem not found: {0}, {1}", tournament, problem);
            }
            else
            {
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("\t\t Tournament \"{0}\", problem {1}, test {2}", tournament, problem, test);
                Console.WriteLine("*******************************************************************************");

                try
                {
                    Console.SetIn(new StreamReader(string.Format("Rounds/{0}/Tests/test{1}_{2}.txt", tournament, problem, test)));
                    //type.InvokeMember("Main", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
                    Rounds.Round181_2.Problem5.Main();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Error: test {0} for problem {1} was not found", test, problem);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Error: test directory for problem {1} was not found", test, problem);
                }
            }
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
