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

        #region Package Members

        protected override void Initialize()
        {
           // System.Diagnostics.Debugger.Break();
            try
            {
                var cf = Codeforces.Instance;
                cf.Login();
                var id = cf.SubmitProgram(351, 'B', 7, "print 'test'" + Guid.NewGuid());
                var html = cf.Post("/data/submitSource", "submissionId=" + id, string.Format("/contest/{0}/my", 351));
                MessageBox.Show("Submition id: " + id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
            System.Diagnostics.Debugger.Break();

            base.Initialize();

            var service = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (service == null) return;

            var buttonCommit = new CommandID(GuidList.Default, PkgCmdIDList.ButtonCommit);
            _commitCommand = new OleMenuCommand(OnCommit, buttonCommit);
            service.AddCommand(_commitCommand);

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
            
            _testNames = _testItems.Select(c => TestPrefix + c).ToList();
            _taskNames = _taskItems.Select(c => TaskPrefix + c).ToList();
        }

        private bool _isCorrectSolution;
        private bool _isActive;
        private OleMenuCommand _combo1Command;
        private OleMenuCommand _combo2Command;
        private Project _currentProject;
        private List<string> _testNames;
        private List<string> _taskNames;
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
            _commitCommand.Enabled = _parseCommand.Enabled = _isActive;
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
            tests.Add(All);
            _testItems = tests.ToArray();
        }

        private void OnCommit(object sender, EventArgs e)
        {
            Check();
            if (_currentProject == null) return;

            var library = "";
            try
            {
                var item = _currentProject.ProjectItems.Item("Library.cs");
                library = File.ReadAllText(item.FileNames[0]);
            }
            catch (Exception)
            {
                MessageBox.Show("Your project doesn't contain Library.cs file. Default library will be used");
            }

            try
            {
                var doc = _dte.ActiveDocument;
                if (doc != null)
                {
                    var code = File.ReadAllText(doc.FullName);
                    code = code.Replace("/*Library*/", library);
                    Clipboard.SetText(code);
                    _dte.StatusBar.Text = "Code successfuly imported.";
                }
                else
                {
                    _dte.StatusBar.Text = "Opened document is not TextDocument";
                }
            }
            catch (Exception ex)
            {
                _dte.StatusBar.Text = "Your project doesn't contain Library.cs file. " + ex.Message;
            }

            //SubmitWindow.Show();
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
            var task = constants.FirstOrDefault(c => _taskNames.Contains(c));
            _currentTaskItem = task == null ? None : task.Substring(TaskPrefix.Length);

            var test = constants.FirstOrDefault(c => _testNames.Contains(c));
            _currentTestItem = test == null ? None : test.Substring(TestPrefix.Length);

        }

        private void WriteValues()
        {
            var project = _currentProject;
            if (project == null || project.ConfigurationManager == null) return;
            var config = project.ConfigurationManager.ActiveConfiguration;
            if (config == null) return;
            var constants = ((string)config.Properties.Item("DefineConstants").Value).Split(';')
                .Where(c => !(c.StartsWith(TaskPrefix) && c.Length == TaskPrefix.Length + 1) && !(c.StartsWith(TestPrefix) && c.Length == TestPrefix.Length + 1)).ToList();
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