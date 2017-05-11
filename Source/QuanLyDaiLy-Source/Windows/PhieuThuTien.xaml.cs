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
using QuanLyDaiLy_Source;
using DAODLL;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using System.Collections.ObjectModel;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for PhieuThuTien.xaml
    /// </summary>
    public partial class PhieuThuTien : Page
    {
        private static PhieuThuTienManager phieuThuTienManager = new PhieuThuTienManager();
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        public PhieuThuTien()
        {
            InitializeComponent();
            Loaded += PhieuThuTien_Loaded;



            DistrictComboBox.LostFocus += DistrictComboBox_FieldCheck;
            AgencyComboBox.LostFocus += AgencyComboBox_FieldCheck;
            MoneyTextBox.LostFocus += MoneyTextBox_FieldCheck;
            DateDatePicker.LostFocus += DateDatePicker_FieldCheck;
        }

        private void DateDatePicker_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (DateDatePicker.SelectedDate == null)
            {
                DateStatus.Visibility = Visibility.Visible;
            }
            else
            {
                DateStatus.Visibility = Visibility.Hidden;
            }
        }

        private void MoneyTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (MoneyTextBox.Text == "")
            {
                MoneyStatus.Visibility = Visibility.Visible;
            }
            else
            {
                MoneyStatus.Visibility = Visibility.Hidden;
            }
        }

        private void AgencyComboBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (AgencyComboBox.SelectedIndex == -1)
            {
                AgencyStatus.Visibility = Visibility.Visible;
            }
            else
            {
                AgencyStatus.Visibility = Visibility.Hidden;
            }
        }

        private void DistrictComboBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (DistrictComboBox.SelectedIndex == -1)
            {
                DistrictStatus.Visibility = Visibility.Visible;
            }
            else
            {
                DistrictStatus.Visibility = Visibility.Hidden;
            }
        }

        private void PhieuThuTien_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Phiếu Thu Tiền";
            pageLoaded?.Invoke(this, e);
        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            PHIEUTHUTIEN obj = new PHIEUTHUTIEN();
            GetAllInput(obj);
            if (IsInputFieldsFilled()) phieuThuTienManager.Insert(obj);

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            PHIEUTHUTIEN obj = new PHIEUTHUTIEN();
            GetAllInput(obj);
            if (IsInputFieldsFilled()) phieuThuTienManager.Insert(obj);
            MessageBox.Show("Đã thêm thành công.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information,
              MessageBoxResult.OK);
        }

        private bool IsInputFieldsFilled()
        {
            if (AgencyComboBox.SelectedItem == null) return false;

            if (MoneyTextBox.Text == "") return false;
            if (!Utilities.IsDigitsOnly(MoneyTextBox.Text.ToString())) return false;

            if (DateDatePicker.SelectedDate == null) return false;

            return true;
        }

        private void GetAllInput(PHIEUTHUTIEN phieu)
        {
            phieu.DAILY = (DAILY)AgencyComboBox.SelectedItem;
            phieu.SOTIEN = decimal.Parse(MoneyTextBox.Text.ToString());
            phieu.NGAYTHUTIEN = DateDatePicker.SelectedDate;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question,
               MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<QUAN> danhSachQuan = DAOView.Instance.GetAllQuan();
            DistrictComboBox.ItemsSource = danhSachQuan;

            ObservableCollection<DAILY> danhSachDaiLy = DAOView.Instance.GetAllDaiLy();
            AgencyComboBox.ItemsSource = danhSachDaiLy;


        }

        private void AgencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DAILY daiLy = (DAILY)AgencyComboBox.SelectedItem;
            PhoneNumberTextBox.Text = daiLy.DIENTHOAI;
            AddressTextBox.Text = daiLy.DIACHI;
        }
    }
}
