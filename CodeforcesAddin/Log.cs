using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Log
{
    private static IVsOutputWindowPane pane;
    private static IVsOutputWindow _outputWindow;
    public static void Initialize(IVsOutputWindow outputWindow)
    {
        if (pane != null) return;
        _outputWindow = outputWindow;
        if (outputWindow == null) return;

        Guid customGuid = new Guid("0F44E2D1-F5FA-4d2d-AB3A-12348ECD9789");
        string customTitle = "Codeforces Helper";
        outputWindow.CreatePane(ref customGuid, customTitle, 1, 1);        
        outputWindow.GetPane(ref customGuid, out pane);
        if (pane != null)
        {
            pane.OutputString("Codeforces Helper is initialized\n");
        }
    }

    public static void Error(Exception ex, string message = "")
    {
        Initialize(_outputWindow);
        if (pane == null) return;
        pane.OutputString(string.Format("Error {0}\n{1}\n{2}\n{3}", message, ex, ex.StackTrace, ex.InnerException));
        pane.OutputString(Environment.NewLine);
    }

    public static void Info(string message, params object[] args)
    {
        Initialize(_outputWindow);
        if (pane == null) return;
        pane.OutputString(args == null ? message : string.Format(message, args));
        pane.OutputString(Environment.NewLine);
    }
}
