using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace CodeforcesAddin
{
    public partial class CommitWindow 
    {
        private readonly DispatcherTimer _timer;
        private readonly int _contest;
        private readonly long _submissionId;

        public static void Show(int contest, long submissionId)
        {
            var window = new CommitWindow(contest, submissionId) {Topmost = true};
            window.Show();
        }

        private static double height = 500;
        private static double width = 600;

        protected override void OnClosing(CancelEventArgs e)
        {
            height = ActualHeight;
            width = ActualWidth;

            base.OnClosing(e);
        }

        private CommitWindow(int contest, long submissionId)
        {
            InitializeComponent();

            //Width = width;
            Height = height;

            _contest = contest;
            _submissionId = submissionId;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += delegate
            {
                _timer.Stop();
                UpdateStatus();
            };
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            var status = Codeforces.Instance.GetSubmissionStatus(_contest, _submissionId);
            if (status == null)
            {
                MessageBox.Show("Error while reading submission status");
                return;
            }

            DataContext = status;
            txtStatus.Visibility = Visibility.Visible;
            if (status.waiting)
            {
                _timer.Start();
            }
            else
            {
                txtVerdict.Foreground = status.verdict == "Полное решение" ? System.Windows.Media.Brushes.Green : System.Windows.Media.Brushes.Red;
                Progress.Visibility = Visibility.Collapsed;
            }
        }

        private void OpenLink(object sender, RoutedEventArgs e)
        {
            var status = (SubmissionStatus) DataContext;
            if (status == null) return;
            System.Diagnostics.Process.Start("http://codeforces.ru" + status.href);
        }
    }
}
