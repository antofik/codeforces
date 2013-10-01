using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeforcesRound181div2
{
    class Program
    {
        static void Main(string[] args)
        {
#if TASKA
            const char problem = 'A';
#elif TASKB
            const char problem = 'B';
#elif TASKC
            const char problem = 'C';
#elif TASKD
            const char problem = 'D';
#elif TASKE
            const char problem = 'E';
#endif

#if TEST1
            const int test = 1;
#elif TEST2
            const int test = 2;
#elif TEST3
            const int test = 3;
#elif TEST4
            const int test = 4;
#elif TEST5
            const int test = 5;
#else
            const int test = 6;
#endif
            var tournament = MethodBase.GetCurrentMethod().DeclaringType.Namespace ?? "";

            var type = Type.GetType(string.Format("CodeforcesRound181div2.Task{0}.Task", problem));
            if (type == null)
            {
                Console.WriteLine("Task not found: {0}", problem);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("*                                                                             *");
                Console.WriteLine("*                            TOURNAMENT {0}{1}*", tournament, string.Join("", Enumerable.Repeat(" ", 38 - tournament.Length)));
                Console.WriteLine("*                            TASK {0}  TEST {1}                                   *", problem, test);
                Console.WriteLine("*                                                                             *");
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("\n");

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                try
                {
                    Console.SetIn(new StreamReader(string.Format("Task{0}/test{1}.txt", problem, test)));
                    type.InvokeMember("Main", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
                }
                catch (FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Error: test {0} for problem {1} was not found", test, problem);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Error: test directory for problem {0} was not found", problem);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n Error: {0}", ex);
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
