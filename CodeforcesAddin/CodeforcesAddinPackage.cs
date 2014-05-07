using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Window = EnvDTE.Window;

namespace CodeforcesAddin
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof (MyToolWindow))]
    [ProvideKeyBindingTable(GuidList.guidCodeforcesAddinEditorFactoryString, 102)]
    [Guid(GuidList.guidCodeforcesAddinPkgString)]
    public sealed class CodeforcesAddinPackage : Package
    {
        private const string TestPrefix = "TEST";
        private const string TaskPrefix = "TASK";
        private const string None = "none";
        private const string All = "ALL";

        private string[] _testItems = {None, All};
        private string _currentTestItem = None;

        private string[] _taskItems = {None};
        private string _currentTaskItem = None;
        private DTE _dte;
        private readonly Codeforces _cf = Codeforces.Instance;

        #region Package Members

        protected override void Initialize()
        {
/*
            try
            {
                var contest = 351;
                var problem = 'B';
                var language = 7;
                var code = "print '" + Guid.NewGuid() + "'";

                var cf = Codeforces.Instance;
                cf.Login();
                var submissionId = cf.SubmitProgram(contest, problem, language, code);
                if (submissionId > 0)
                {
                    var status = cf.GetSubmissionStatus(contest, submissionId);
                    
                    MessageBox.Show("Submition: " + status);
                }
                else if (submissionId == -2)
                {
                    MessageBox.Show("Ранее вы уже отсылали абсолютно такой же код");
                }
                else
                {
                    MessageBox.Show("Submission failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            System.Diagnostics.Debugger.Break();
*/
            base.Initialize();

            var service = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (service == null) return;

            var buttonCommit = new CommandID(GuidList.Default, PkgCmdIDList.ButtonCommit);
            _commitCommand = new OleMenuCommand(OnCommit, buttonCommit);
            service.AddCommand(_commitCommand);

            var buttonCopy = new CommandID(GuidList.Default, PkgCmdIDList.ButtonCopy);
            _copyCommand = new OleMenuCommand(OnCopy, buttonCopy);
            service.AddCommand(_copyCommand);

            var buttonParse = new CommandID(GuidList.Default, PkgCmdIDList.ButtonParse);
            _parseCommand = new OleMenuCommand(OnParse, buttonParse);
            service.AddCommand(_parseCommand);

            var combo1 = new CommandID(GuidList.Default, PkgCmdIDList.Combo1);
            _combo1Command = new OleMenuCommand(OnCombo, combo1);
            service.AddCommand(_combo1Command);
            
            var combo1GetItems = new CommandID(GuidList.Default, PkgCmdIDList.Combo1Items);
            service.AddCommand(new OleMenuCommand(OnMenuMyDropDownComboGetList, combo1GetItems));

            var combo2 = new CommandID(GuidList.Default, PkgCmdIDList.Combo2);
            _combo2Command = new OleMenuCommand(OnCombo, combo2);
            service.AddCommand(_combo2Command);

            var combo2GetItems = new CommandID(GuidList.Default, PkgCmdIDList.Combo2Items);
            service.AddCommand(new OleMenuCommand(OnMenuMyDropDownComboGetList, combo2GetItems));

            _dte = (DTE)GetService(typeof (DTE));
            _dte.Events.WindowEvents.WindowActivated += OnWindowActivated;


            /*_cf.OnLogged += () =>
            {
                _commitCommand.ParametersDescription = "test";
                _commitCommand.Enabled = _cf.IsLogged;
            };
            _cf.Login();*/
        }

        private bool _isCorrectSolution;
        private bool _isActive;
        private OleMenuCommand _combo1Command;
        private OleMenuCommand _combo2Command;
        private Project _currentProject;
        private OleMenuCommand _copyCommand;
        private OleMenuCommand _commitCommand;
        private OleMenuCommand _parseCommand;

        private void OnWindowActivated(Window gotfocus, Window lostfocus)
        {
            Check();
        }

        private void Check()
        {
            var projects = _dte.ActiveSolutionProjects as object[];
            _currentProject = projects != null && projects.Length > 0 ? projects[0] as Project : null;

            _isCorrectSolution = _currentProject != null && _currentProject.CodeModel.Language == CodeModelLanguageConstants.vsCMLanguageCSharp;
            ReadAvailableTasks();
            ReadAvailableTests();

            _isActive = _currentProject != null && _isCorrectSolution;
            _commitCommand.Enabled = _copyCommand.Enabled =  _parseCommand.Enabled = _isActive;
            _combo1Command.Enabled = _combo2Command.Enabled = _isActive;
            _combo1Command.Visible = _combo2Command.Visible = _isActive;

            if (_isActive) ReadCurrentValues();
        }

        private void ReadAvailableTasks()
        {
            if (_currentProject == null) return;
            var projectPath = Path.GetDirectoryName(_currentProject.FileName) ?? "";
            var tasks = Directory.EnumerateDirectories(projectPath)
                .Select(Path.GetFileName)
                .Where(c => c.StartsWith("Task") && c.Length==5).Select(c=>c[4].ToString(CultureInfo.InvariantCulture)).OrderBy(c=>c).ToList();
            tasks.Insert(0, None);
            _taskItems = tasks.ToArray();
        }

        private void ReadAvailableTests()
        {
            if (_currentProject == null) return;
            if (_currentTaskItem == None || _currentTaskItem == null) return;
            var projectPath = Path.GetDirectoryName(_currentProject.FileName) ?? "";
            var testsPath = Path.Combine(projectPath, "Task" + _currentTaskItem, "Tests");
            var tests = Directory.EnumerateFiles(testsPath).Select(Path.GetFileName).Where(c => c.StartsWith("test") && c.EndsWith(".txt"))
                .Select(c=>c.Substring(4, c.Length-8)).OrderBy(c=>c).ToList();
            tests.Insert(0, None);
            tests.Add(All);
            _testItems = tests.ToArray();
        }

        private void OnCopy(object sender, EventArgs e)
        {
            var code = PrepareCode();
            Clipboard.SetText(code);
        }

        private string PrepareCode()
        {
            Check();
            if (_currentProject == null) return "";
            if (_currentTaskItem == None) return "";

            var libraryUsings = "";
            var libraryCode = "";
            try
            {
                var item = _currentProject.ProjectItems.Item("Library.cs");
                if (!item.Saved) item.Save();
                var lines = File.ReadAllLines(item.FileNames[0]);
                int i;
                for (i = 0; i < lines.Count(); i++)
                {
                    var line = lines[i];
                    if (!line.StartsWith("using ")) break;
                    libraryUsings += line + Environment.NewLine;
                }
                libraryCode = string.Join(Environment.NewLine, lines.Skip(i));
            }
            catch (Exception)
            {
                MessageBox.Show("Your project doesn't contain Library.cs file. Default library will be used");
            }

            var code = "";
            try
            {
                ProjectItem doc;
                try
                {
                    var folder = _currentProject.ProjectItems.Item("Task" + _currentTaskItem);
                    doc = folder.ProjectItems.Item("Task.cs");
                }
                catch (Exception)
                {
                    doc = _dte.ActiveDocument.ProjectItem;
                }
                if (doc != null)
                {
                    if (!doc.Saved) doc.Save();
                    code = File.ReadAllText(doc.FileNames[0]);
                    code = string.Format("{0}{1}{2}", libraryUsings, code, libraryCode);
                    _dte.StatusBar.Text = "Code successfuly imported.";
                }
                else
                {
                    _dte.StatusBar.Text = "Opened document is not TextDocument";
                }
            }
            catch (Exception ex)
            {
                code = "";
                _dte.StatusBar.Text = "Your project doesn't contain Library.cs file. " + ex.Message;
            }
            return code;
        }

        private void OnCommit(object sender, EventArgs e)
        {
            Check();
            if (_currentProject == null) return;

            if (_currentTaskItem == None)
            {
                MessageBox.Show("Select task to submit");
                return;
            }

            if (!_cf.IsLogged)
            {
                LoginWindow.Show(() =>
                {
                    if (!_cf.IsLogged) return;
                    Commit();
                });
            }
            else
            {
                Commit();
            }
        }

        private int _lastContestId;
        private Project _lastContestProject;

        private void Commit()
        {
            if (_currentTaskItem == None)
            {
                MessageBox.Show("Select task to submit");
                return;
            }

            var code = PrepareCode();
            var problem = _currentTaskItem[0];
            const int language = 29; //MS C#

            var workingPath = Path.GetDirectoryName(_currentProject.FileName);
            string error;
            var branch = ParseWindow.Git("rev-parse --abbrev-ref HEAD", workingPath, out error);
            Action<int> submit = contest =>
            {
                var submissionId = _cf.SubmitProgram(contest, problem, language, code);
                if (!CheckSubmissionCode(submissionId)) return;
                CommitWindow.Show(contest, submissionId);
            };

            if (!branch.StartsWith("@"))
            {
                if (_lastContestId > 0 && _lastContestProject == _currentProject)
                {
                    submit(_lastContestId);
                }
                else
                {
                    var result = MessageBox.Show("Unable to determine current contest id. Whould you like to enter it manually?", "Question", MessageBoxButton.YesNo);
                    if (result != MessageBoxResult.Yes) return;
                    SelectContestWindow.Show(contest =>
                    {
                        _lastContestId = contest;
                        _lastContestProject = _currentProject;
                        submit(contest);
                    });
                }
            }
            else
            {
                var contest = int.Parse(branch.Substring(1));
                submit(contest);
            }
        }

        private bool CheckSubmissionCode(long submissionId)
        {
            if (submissionId > 0) return true;
            var error = "";
            if (submissionId == -1)
            {
                error = "Submission failed";
            }
            else if (submissionId == -2)
            {
                error = "You've sent the same code before";
            }
            MessageBox.Show(error);
            return false;
        }

        private void OnParse(object sender, EventArgs e)
        {
            Check();
            if (_currentProject == null)
            {
                _dte.StatusBar.Text = "Select project first";
                return;
            }
            ParseWindow.Show(_currentProject);
        }

        private void ReadCurrentValues()
        {
            var project = _currentProject;
            if (project == null || project.ConfigurationManager == null) return;
            var config = project.ConfigurationManager.ActiveConfiguration;
            if (config == null) return;
            
            var constants = new List<string>();
            switch (project.CodeModel.Language)
            {
                case CodeModelLanguageConstants.vsCMLanguageCSharp:
                    constants = ((string)config.Properties.Item("DefineConstants").Value).Split(';').ToList();
                    break;
                case CodeModelLanguageConstants.vsCMLanguageVC:
                    constants = new List<string>();
                    break;
            }

            var _testNames = _testItems.Select(c => TestPrefix + c).ToList();
            var _taskNames = _taskItems.Select(c => TaskPrefix + c).ToList();

            var task = constants.FirstOrDefault(_taskNames.Contains);
            _currentTaskItem = task == null ? None : task.Substring(TaskPrefix.Length);

            var test = constants.FirstOrDefault(_testNames.Contains);
            _currentTestItem = test == null ? None : test.Substring(TestPrefix.Length);

        }

        private void WriteValues()
        {
            var project = _currentProject;
            if (project == null || project.ConfigurationManager == null) return;
            var config = project.ConfigurationManager.ActiveConfiguration;
            if (config == null) return;
            var constants = ((string)config.Properties.Item("DefineConstants").Value).Split(';')
                .Where(c => !(c.StartsWith(TaskPrefix) && c.Length == TaskPrefix.Length + 1) && !c.StartsWith(TestPrefix)).ToList();
            if (_currentTaskItem != None) constants.Add(TaskPrefix + _currentTaskItem);
            if (_currentTestItem != None) constants.Add(TestPrefix + _currentTestItem);
            config.Properties.Item("DefineConstants").Value = string.Join(";", constants);
        }

        private void OnCombo(object sender, EventArgs e)
        {
            var item = sender as OleMenuCommand;
            if (item == null) return;
            var args = e as OleMenuCmdEventArgs;
            if (args == null) return;
            var value = args.InValue as string;
            if (value != null)
            {
                switch (item.CommandID.ID)
                {
                    case PkgCmdIDList.Combo1:
                        _currentTaskItem = value;
                        break;
                    case PkgCmdIDList.Combo2:
                        _currentTestItem = value;
                        break;
                }
                WriteValues();
                ReadAvailableTests();
            }
            else
            {
                if (args.OutValue != IntPtr.Zero)
                {
                    string result;
                    switch (item.CommandID.ID)
                    {
                        case PkgCmdIDList.Combo1:
                            result = _currentTaskItem;
                            break;
                        case PkgCmdIDList.Combo2:
                            result = _currentTestItem;
                            break;
                        default:
                            result = "unknown";
                            break;
                    }
                    
                    Marshal.GetNativeVariantForObject(result, args.OutValue);
                }
            }
            item.Visible = _isActive;
            item.Enabled = _isActive;
        }

        private void OnMenuMyDropDownComboGetList(object sender, EventArgs e)
        {
            var item = sender as OleMenuCommand;
            if (item == null) return;
            var args = e as OleMenuCmdEventArgs;
            if (args == null) return;
            string[] choices;
            switch (item.CommandID.ID)
            {
                case PkgCmdIDList.Combo1Items:
                    choices = _taskItems;
                    break;
                case PkgCmdIDList.Combo2Items:
                    choices = _testItems;
                    break;
                default:
                    choices = new string[]{};
                    break;
            }
            Marshal.GetNativeVariantForObject(choices, args.OutValue);
        }

        #endregion
    }
}