﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

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

        private readonly string[] _testItems = {None, "1", "2", "3", "4", "5"};
        private string _currentTestItem = None;

        private readonly string[] _taskItems = {None, "A", "B", "C", "D", "E"};
        private string _currentTaskItem = None;
        private DTE _dte;

        #region Package Members

        protected override void Initialize()
        {
            base.Initialize();

            var service = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (service == null) return;

            var buttonCommit = new CommandID(GuidList.Default, PkgCmdIDList.ButtonCommit);
            _commitCommand = new OleMenuCommand(OnCommit, buttonCommit);
            service.AddCommand(_commitCommand);

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
        private const string None = "none";

        private void OnWindowActivated(Window gotfocus, Window lostfocus)
        {
            _isCorrectSolution = _dte.Solution.FullName.ToLower().Contains("codeforce");
            if (_isCorrectSolution)
            {
                try
                {
                    _currentProject = _dte.ActiveDocument.ProjectItem.ContainingProject;
                }
                catch (Exception)
                {
                    _currentProject = null;
                }
            }

            _isActive = _currentProject != null && _isCorrectSolution;
            _combo1Command.Enabled = _combo2Command.Enabled = _isActive;
            _combo1Command.Visible = _combo2Command.Visible = _isActive;

            if (_isActive) ReadCurrentValues();
        }

        private void OnCommit(object sender, EventArgs e)
        {
            SubmitWindow.Show();
        }

        private void ReadCurrentValues()
        {
            var project = _currentProject;
            if (project == null || project.ConfigurationManager == null) return;
            var config = project.ConfigurationManager.ActiveConfiguration;
            if (config == null) return;
            var constants = ((string)config.Properties.Item("DefineConstants").Value).Split(';').ToList();

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
            var constants = ((string)config.Properties.Item("DefineConstants").Value).Split(';').Where(c=>!_testNames.Contains(c) && !_taskNames.Contains(c)).ToList();
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