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
using MessageBox = System.Windows.MessageBox;
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

            Loaded += delegate
            {
                var list = LoadContests();
                _contests.Clear();
                foreach (var item in list) _contests.Add(item);
                combo.SelectedItem = _contests.FirstOrDefault();
            };

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
            var result = MessageBox.Show("Create Git branch?\nAll your files will be overwritten otherwise.", "Git", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                var workingPath = Path.GetDirectoryName(_project.FileName);
                string error;
                Git("checkout master", workingPath, out error);
                var output = Git("rev-parse --abbrev-ref HEAD", workingPath, out error);
                if (output != "master")
                {
                    MessageBox.Show("Could not checkout master. Output: " + output + " " + error + "\nCheck if you have git installed and project is under git control");
                    return;
                }

                Git("branch @" + item.Id, workingPath, out error);
                Git("checkout @" + item.Id, workingPath, out error);

                output = Git("rev-parse --abbrev-ref HEAD", workingPath, out error);
                if (output != "@" + item.Id)
                {
                    MessageBox.Show("Could not checkout to branch: " + output + " " + error);
                    return;
                }
                ImportTasks(item);
                Close();
            }
            else if (result == MessageBoxResult.No)
            {
                ImportTasks(item);
                Close();
            }
        }

        internal static string Git(string command, string workingDirectory, out string error)
        {
            using (var process = new Process())
            {
                process.StartInfo = new ProcessStartInfo("cmd.exe", "/C git " + command)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                };
                process.Start();
                process.WaitForExit();
                error = process.StandardError.ReadToEnd().Trim(' ', '\n', '\r', '\t');
                return process.StandardOutput.ReadToEnd().Trim(' ', '\n', '\r', '\t');
            }
        }

        private void ImportTasks(ContestItem item)
        {
            string taskTemplate = @"#define Library
using System;
using System.Collections.Generic;
using System.Linq;

namespace Codeforces.Task/*#*/
{
    public class Task
    {
        public static void Main()
        {
            var task = new Task();
            task.Solve();
        }

        void Solve()
        {

        }
    }
}";
            try
            {
                var projectItem = _project.ProjectItems.Item("Task.cs");
                var taskTemplatePath = projectItem.FileNames[0];
                taskTemplate = File.ReadAllText(taskTemplatePath);
            }
            catch (Exception)
            {
                MessageBox.Show("File Task.cs not found in your project. Default template will be used.");
            }
            
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
                            var taskPath = Path.Combine(dir, "Task.cs");
                            File.WriteAllText(taskPath, taskTemplate.Replace("/*#*/", taskLetter.ToString(CultureInfo.InvariantCulture)));
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

        internal static List<ContestItem> LoadContests()
        {
            var list = new List<ContestItem>();
            using (var web = new WebClient())
            {
                const string mask = @"<tr\s*?data-contestId=""(?<id>\d+)""\s*?>\s*?<td>\s*?(?<name>[^<]+?)<br";
                for (var i = 1; i <= 10; i++)
                {
                    var html = web.DownloadString(new Uri(string.Format("http://codeforces.ru/contests/page/{0}", i))).Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
                    var matches = Regex.Matches(html, mask, RegexOptions.Singleline);
                    var added = false;
                    foreach (Match match in matches)
                    {
                        var id = int.Parse(match.Groups["id"].Value);
                        var name = HttpUtility.HtmlDecode(match.Groups["name"].Value.Replace("<br/>", " ").Replace("<br />", " ").Trim().Trim('\n', '\t', '\r'));
                        var item = list.FirstOrDefault(c => c.Id == id);
                        if (item != null) continue;
                        added = true;
                        list.Add(new ContestItem{Id = id, Name = name});
                    }
                    if (!added) break;
                }
            }
            return list.OrderByDescending(c => c.Id).ToList();
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
