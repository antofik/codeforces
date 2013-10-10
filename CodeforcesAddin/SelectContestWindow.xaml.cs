using System;
using System.Collections.ObjectModel;
using System.Linq;
using MessageBox = System.Windows.MessageBox;

namespace CodeforcesAddin
{
    public partial class SelectContestWindow    
    {
        public static void Show(Action<int> callback)
        {
            var window = new SelectContestWindow();
            window.Closed += delegate
            {
                callback(window.contest);
            };
            window.ShowDialog();
        }

        public SelectContestWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                var list = ParseWindow.LoadContests();
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
            contest = item.Id;
            Close();
        }

        private readonly ObservableCollection<ContestItem> _contests = new ObservableCollection<ContestItem>();
        private int contest;
    }
}
