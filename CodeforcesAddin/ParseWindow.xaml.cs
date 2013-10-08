using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows;
using EnvDTE;
using Path = System.IO.Path;
using Process = System.Diagnostics.Process;

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
            var process = Process.Start("cmd.exe", "git checkout -b @" + item.Id);
            process.Exited += delegate
            {
                ImportTasks(item);
                Close();
            };
        }

        private void ImportTasks(ContestItem item)
        {
            using (var web = new WebClient())
            {
                const string mask = @"<div class=""ttypography"">(?<problem>.*?)<script";
                const string maskInput = @"<div class=""input"">.*?<pre>(?<data>.*?)</pre>";
                const string maskOutput = @"<div class=""output"">.*?<pre>(?<data>.*?)</pre>";
                var html = (web.DownloadString(new Uri(string.Format("http://codeforces.ru/contest/{0}/problems", item.Id))));
                html = Encoding.UTF8.GetString(Encoding.Default.GetBytes(html));
                var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                for (var i = 0; i < matches.Count; i++)
                {
                    var match = matches[i];
                    var taskLetter = (char) ('A' + i);
                    var task = "Task" + taskLetter;
                    var problem = match.Groups["problem"].Value;
                    var projectPath = Path.GetDirectoryName(_project.FullName) ?? "";
                    var dir = Path.Combine(projectPath, task);
                    if (!Directory.Exists(dir)) //create task structure
                    {
                        Directory.CreateDirectory(dir);
                        try
                        {
                            var taskTemplatePath = _project.ProjectItems.Item("Task.cs").Document.FullName;
                            var taskTemplate = File.ReadAllText(taskTemplatePath);
                            taskTemplate = taskTemplate.Replace("/*#*/", taskLetter.ToString(CultureInfo.InvariantCulture));
                            var taskPath = Path.Combine(dir, "Task.cs");
                            File.WriteAllText(taskPath, taskTemplate);
                            _project.ProjectItems.AddFromFile(taskPath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex);
                        }
                    }
                    dir = Path.Combine(projectPath, task, "Tests");
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    dir = Path.Combine(projectPath, task, "Results");
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                    var path = Path.Combine(projectPath, task, "Problem.html");
                    File.WriteAllText(path, problem);
                    _project.ProjectItems.AddFromFile(path);

                    var inputs =
                        Regex.Matches(problem, maskInput, RegexOptions.Singleline)
                            .Cast<Match>()
                            .Select(c => c.Groups["data"].Value)
                            .ToList();
                    var outputs =
                        Regex.Matches(problem, maskOutput, RegexOptions.Singleline)
                            .Cast<Match>()
                            .Select(c => c.Groups["data"].Value)
                            .ToList();

                    Func<string, string> filter =
                        s => s.Replace("<br/>", "\n").Replace("<br />", "\n").Trim().Trim('\n', '\t', '\r');

                    for (var j = 0; j < inputs.Count + 3; j++) //fetch original tests + 3 empty tests
                    {
                        var testPath = Path.Combine(projectPath, task, "Tests", "test" + (j + 1) + ".txt");
                        var resultPath = Path.Combine(projectPath, task, "Results", "test" + (j + 1) + ".txt");
                        File.WriteAllText(testPath, j < inputs.Count ? filter(inputs[j]) : "");
                        File.WriteAllText(resultPath, j < outputs.Count ? filter(outputs[j]) : "");
                        _project.ProjectItems.AddFromFile(testPath);
                        _project.ProjectItems.AddFromFile(resultPath);
                    }
                }
            }
        }

        private readonly ObservableCollection<ContestItem> _contests = new ObservableCollection<ContestItem>();
        private readonly Project _project;

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
                        var name = HttpUtility.HtmlDecode(match.Groups["name"].Value.Replace("<br/>", " ").Replace("<br />", " ").Trim().Trim('\n', '\t', '\r'));
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
