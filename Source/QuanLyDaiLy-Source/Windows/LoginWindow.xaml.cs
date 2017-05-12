using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static event EventHandler LoginSucceed;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindowHandler(this, e);
            string username = UsernameTextBox.Text.ToString();
            string password = PasswordTextBox.Text.ToString();
            try
            {
                bool isLoginSucceed = DAODLL.User.Instance.Dangnhap(username, password);
                if (isLoginSucceed)
                {
                    LoginSucceed?.Invoke(this, null);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Bạn vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu rồi thử lại.", "Đăng Nhập Thất Bại", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    UsernameTextBox.Text = "";
                    PasswordTextBox.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu rồi thử lại.", "Đăng Nhập Thất Bại",
                    MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            finally
            {
                AbortLoadingWindow();
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogInButton_Click(this, null);
            }
        }
        private static Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
        private void LoadingWindowHandler(object sender, RoutedEventArgs e)
        {
            //Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
           
        }
        private void AbortLoadingWindow()
        {
            newWindowThread.Abort();
        }
        private static void ThreadStartingPoint()
        {
            ProgressBarWindow pbWindow = new ProgressBarWindow();
            pbWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
