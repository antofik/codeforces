using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeforcesAddin
{
    public partial class SubmitWindow
    {
        public static void Show(Action callback = null)
        {
            var window = new SubmitWindow();
            window.Closed += delegate { if (callback != null) callback(); };
            ((Window) window).Show();
        }

        private const string _enter = "http://codeforces.ru/enter";
        private const string _submit = "http://codeforces.ru/contest/302/submit";

        private SubmitWindow()
        {
            InitializeComponent();

            web.Navigated += Navigated;
            web.Source = new Uri(_enter);
            web.LoadCompleted += delegate
                {
                    if (web.Source.AbsoluteUri == _enter)
                    {
                        try
                        {
                            web.InvokeScript("eval", @"$('input[name=handle]').val('antofik');");
                            web.InvokeScript("eval", @"$('input[name=password]').val('....password....');");
                            web.InvokeScript("eval", @"$('input[type=submit]').click();");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else if (web.Source.AbsoluteUri == _submit)
                    {
                        try
                        {
                            web.InvokeScript("eval", @"$('select[name=submittedProblemIndex]').val('D');");
                            web.InvokeScript("eval", @"$('select[name=programTypeId]').val('7');");
                            web.InvokeScript("eval", @"$('textarea[name=source]').val('print ""Hello, world""');");
                         //   web.InvokeScript("eval", @"$('input[type=submit]').click();");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    else
                    {
                        web.Source = new Uri(_submit);
                    }
                };
        }

        private void Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}
