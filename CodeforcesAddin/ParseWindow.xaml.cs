using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EnvDTE;
using Path = System.IO.Path;

namespace CodeforcesAddin
{
    public partial class ParseWindow    
    {
        public static void Show(Project project)
        {
            var window = new ParseWindow(project);
            window.ShowDialog();
        }

        public ParseWindow(Project project)
        {
            _project = project;
            InitializeComponent();

            Loaded += delegate { LoadContests(); };

            combo.ItemsSource = _contests;
            cmdImport.Click += delegate { DoImport(); };
        }

        private void DoImport()
        {
            var item = combo.SelectedItem as ContestItem;
            if (item == null)
            {
                MessageBox.Show("Select contest");
                return;
            }
            using (var web = new WebClient())
            {
                const string mask = @"<div class=""ttypography"">(?<problem>.*?)<script";
                const string maskInput = @"<div class=""input"">.*?<pre>(.*?)</pre>";
                var html = (web.DownloadString(new Uri(string.Format("http://codeforces.ru/contest/{0}/problems", item.Id))));
                html = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html));
                var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    var task = "Task" + (char)('A' + i);
                    var problem = match.Groups["problem"].Value;
                    var projectPath = Path.GetDirectoryName(_project.FullName);
                    var path = Path.Combine(projectPath, task, "Problem.txt");
                    var dir = Path.Combine(projectPath, task);
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    dir = Path.Combine(projectPath, task, "Tests");
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    dir = Path.Combine(projectPath, task, "Results");
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                    File.WriteAllText(path, problem);
                    var testItem = _project.ProjectItems.AddFromFile(path);


                    var test1Path = Path.Combine(projectPath, task, "Tests", "test" + i + ".txt");
                    MessageBox.Show(testItem.Name);

                    //_project.ProjectItems.Item(task + "/Problem.txt");
                }
            }
        }

        private readonly ObservableCollection<ContestItem> _contests = new ObservableCollection<ContestItem>();
        private Project _project;

        private void LoadContests()
        {
            var list = new List<ContestItem>();
            using (var web = new WebClient())
            {
                const string mask = @"<tr\s*?data-contestId=""(?<id>\d+)""\s*?>\s*?<td>\s*?(?<name>[^<]+?)<br/>";
                for (var i = 1; i <= 5; i++)
                {
                    var html = web.DownloadString(new Uri(string.Format("http://codeforces.ru/contests/page/{0}", i))).Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
                    var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                    foreach (Match match in matches)
                    {
                        var id = int.Parse(match.Groups["id"].Value);
                        var name = HttpUtility.HtmlDecode(match.Groups["name"].Value.Trim());
                        var item = list.FirstOrDefault(c => c.Id == id);
                        if (item != null) continue;
                        list.Add(new ContestItem{Id = id, Name = name});
                    }
                }
            }
            list = list.OrderByDescending(c => c.Id).ToList();

            _contests.Clear();
            foreach (var item in list) _contests.Add(item);

            combo.SelectedItem = _contests.FirstOrDefault();
        }
    }

    public class ContestItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("@[{0}] {1}", Id, Name);
        }
    }
}
