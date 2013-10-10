using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CodeforcesAddin
{
    public partial class LoginWindow
    {
        public static void Show(Action callback)
        {
            var window = new LoginWindow();
            window.Closed += delegate
            {
                callback();
            };
            window.ShowDialog();
        }

        public LoginWindow()
        {
            InitializeComponent();

            Loaded += delegate
            {
                txtLogin.Focus();
            };

            KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    IsEnabled = false;
                    Dispatcher.BeginInvoke((Action)Login, DispatcherPriority.Normal);
                }
            };

            cmdLogin.Click += delegate
            {
                IsEnabled = false;
                Dispatcher.BeginInvoke((Action)Login, DispatcherPriority.Normal);
            };
        }

        private void Login()
        {
            Codeforces.Instance.SetAuthenticationData(txtLogin.Text, txtPassword.Password);
            var ok = Codeforces.Instance.Login();
            IsEnabled = true;
            if (ok)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа");
            }
        }
    }
}
