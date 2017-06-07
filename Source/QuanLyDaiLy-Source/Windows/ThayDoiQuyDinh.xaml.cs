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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyDaiLy_Source.Commons.Exceptions;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for ThayDoiQuyDinh.xaml
    /// </summary>
    public partial class ThayDoiQuyDinh : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;

        public ThayDoiQuyDinh()
        {
            InitializeComponent();
            Loaded += ThayDoiQuyDinh_Loaded;
        }

        private void ThayDoiQuyDinh_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Thay Đổi Quy Định";
            pageLoaded?.Invoke(this, e);
        }


        private void DonViTinhWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsCurrentUserAdmin())
                    throw new InvalidPrivilegeException();

                TDQD_ThemDonViTinhWindow themDVTWindow = new TDQD_ThemDonViTinhWindow();
                themDVTWindow.Owner = Window.GetWindow(this);
                themDVTWindow.ShowDialog();
            }
            catch (InvalidPrivilegeException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Không thể mở. Cửa sổ chính hiện tại đã đóng.", "Lỗi");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui lòng khởi động lại chương trình.", "Lỗi");
            }
        }

        private void MatHangWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsCurrentUserAdmin())
                    throw new InvalidPrivilegeException();
                TDQD_ThemMatHangWindow matHangWindow = new TDQD_ThemMatHangWindow();
                matHangWindow.Owner = Window.GetWindow(this);
                matHangWindow.ShowDialog();
            }
            catch (InvalidPrivilegeException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Không thể mở. Cửa sổ chính hiện tại đã đóng.", "Lỗi");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui lòng khởi động lại chương trình.", "Lỗi");
            }
        }

        private void ToggleButton_QuyDinhMatHang_Click(object sender, RoutedEventArgs e)
        {
            if (MerchandiseRulesBorder.Visibility == Visibility.Visible)
                MerchandiseRulesBorder.Visibility = Visibility.Collapsed;
            else
                MerchandiseRulesBorder.Visibility = Visibility.Visible;
        }
        
        /// <summary>
        /// Check if the current session's user is Admin or not. 
        /// Only admin can change the settings.
        /// </summary>
        /// <returns>Return true if the current session's user is Admin</returns>
        private bool IsCurrentUserAdmin()
        {
            if (DAODLL.User.Instance.Chucvu != "Admin") return false;
            return true;
        }

        private void SoDLToiDaWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsCurrentUserAdmin())
                    throw new InvalidPrivilegeException();
                TDQD_SoDaiLyToiDaWindow window = new TDQD_SoDaiLyToiDaWindow();
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
            }
            catch (InvalidPrivilegeException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Không thể mở. Cửa sổ chính hiện tại đã đóng.", "Lỗi");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui lòng khởi động lại chương trình.", "Lỗi");
            }
        }

        private void LoaiDaiLyWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsCurrentUserAdmin())
                    throw new InvalidPrivilegeException();
                TDQD_LoaiDaiLyWindow window = new TDQD_LoaiDaiLyWindow();
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
            }
            catch (InvalidPrivilegeException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Không thể mở. Cửa sổ chính hiện tại đã đóng.", "Lỗi");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui lòng khởi động lại chương trình.", "Lỗi");
            }
        }

        private void ToggleButton_AngecyRules_Click(object sender, RoutedEventArgs e)
        {
            if (AgencyRulesBorder.Visibility == Visibility.Visible)
                AgencyRulesBorder.Visibility = Visibility.Collapsed;
            else
                AgencyRulesBorder.Visibility = Visibility.Visible;
        }


        private void ToggleButton_Employee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeBorder.Visibility == Visibility.Visible)
                EmployeeBorder.Visibility = Visibility.Collapsed;
            else
                EmployeeBorder.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Open an Employee Window
        /// </summary>
        private void EmployeeWindowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsCurrentUserAdmin())
                    throw new InvalidPrivilegeException();
                TiepNhanNhanVienWindow window = new TiepNhanNhanVienWindow();
                window.Owner = Window.GetWindow(this);
                window.ShowDialog();
            }
            catch (InvalidPrivilegeException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Không thể thực hiện tác vụ.", "Lỗi");
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Vui lòng khởi động lại chương trình.", "Lỗi");
            }
        }
    }
}
