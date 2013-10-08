using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Snippets
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("This is a snippet program");

            Parse();
        }

        private static void Parse()
        {
            using (var web = new WebClient())
            {
                const string mask = @"<div class=""ttypography"">(?<problem>.*?)<script";
                var html = (web.DownloadString(new Uri(string.Format("http://codeforces.ru/contest/{0}/problems", 100))));
                html = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html));
                var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    var task = "Task" + ('A' + i);
                    var problem = match.Groups["problem"].Value;
                    File.WriteAllText("d:/test.html", problem);
                   // _project.ProjectItems.Item(task + "/Problem.txt");
                }
            }
        }

        private static void LoadContests()
        {
            var str = "Codeforces Beta Round #25 (&#1044;&#1080;&#1074;. 2)  ";
            var s = HttpUtility.HtmlDecode(str);

            using (var web = new WebClient())
            {
                const string mask = @"<tr\s*?data-contestId=""(?<id>\d+)""\s*?>\s*?<td>\s*?(?<name>[^<]+?)<br/>";
                var previous = "";
                for (var i = 0; i < 100; i++)
                {
                    var html = web.DownloadString(new Uri("http://codeforces.ru/contests/page/{0}")).Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
                    if (html == previous) break;
                    var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                    foreach (Match match in matches)
                    {
                        var g = int.Parse(match.Groups["id"].Value);
                        var name = match.Groups["name"].Value.Trim();
                    }
                    previous = html;
                }
            }
        }
    }
}
