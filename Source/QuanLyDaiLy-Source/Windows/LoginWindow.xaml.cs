using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            //LoadingWindowHandler(this, e);
            string username = UsernameTextBox.Text.ToString();
            //for (int i = 1; i <= (20 - UsernameTextBox.Text.Count()); i++)
            //{
            //    username += " ";
            //}
            string password = PasswordTextBox.Text.ToString();
            if (username == "" || password == "" ||
                 username == null || password == null)
            {
                MessageBox.Show("Vui lòng nhập thông tin đăng nhập.", "Đăng Nhập Thất Bại",
                               MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
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
                    MessageBox.Show("Bạn vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu rồi thử lại.", "Đăng Nhập Thất Bại", 
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    //LoggingStatus.Visibility = Visibility.Visible;

                    UsernameTextBox.Text = "";
                    PasswordTextBox.Text = "";
                    UsernameTextBox.Focus();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Bạn vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu rồi thử lại.", "Đăng Nhập Thất Bại",
                //    MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            //finally
            //{
            //    AbortLoadingWindow();
            //}

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
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Hide this window's exit button
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            //StartCloseTimer();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
            //this.Owner.Dispatcher.InvokeShutdown();
            this.Close();
        }
    }
}
