using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string username = UsernameTextBox.Text.ToString();
            string password = PasswordTextBox.Text.ToString();
            bool isLoginSucceed = DAODLL.User.Instance.Dangnhap(username, password);
            if (isLoginSucceed)
            {
              LoginSucceed?.Invoke(this, null);
              this.Close();
            }
            else
            {
                MessageBox.Show("Bạn vui lòng kiểm tra lại tên đăng nhập hoặc mật khẩu rồi thử lại.", "Đăng Nhập Thất Bại", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LogInButton_Click(this, null);
        }
    }
}
