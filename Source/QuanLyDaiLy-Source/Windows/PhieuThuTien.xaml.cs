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
using QuanLyDaiLy_Source.Helper;
using QuanLyDaiLy_Source.Commons;
using QuanLyDaiLy_Source.Models.BusinessLogic;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for PhieuThuTien.xaml
    /// </summary>
    public partial class PhieuThuTien : Page
    {
        private static PhieuThuTienManager phieuThuTienManager = new PhieuThuTienManager();
        private static DaiLyManager daiLyManager = new DaiLyManager();

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
            //MoneyTextBox.LostFocus += MoneyTextBox_FieldCheck;
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

        /*
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
        */

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
            try
            {
                //DistrictComboBox.Items.Clear();
                DistrictComboBox.ItemsSource = ViewManager.Instance.GetAllQuan();

                //AgencyComboBox.Items.Clear();
                AgencyComboBox.ItemsSource = ViewManager.Instance.GetAllDaiLy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Loading Error");
            }
        }

        /// <summary>
        /// Save and exit to homepage.
        /// </summary>
        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            PHIEUTHUTIEN obj = new PHIEUTHUTIEN();
            GetAllInput(obj);
            if (IsInputFieldsFilled())
                if (phieuThuTienManager.Insert(obj))
                {
                    MessageBox.Show("Đã thêm thành công.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information,
                      MessageBoxResult.OK);
                    NavigationService ns = NavigationService.GetNavigationService(this);
                    ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Không thể thêm Phiếu Thu Tiền.", "Lỗi");
                }
            else
            {
                MessageBox.Show(GenericError.InputErrorContent, GenericError.InputError);
            }

        }

        /// <summary>
        /// Save and clear all the settings.
        /// </summary>
        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            PHIEUTHUTIEN obj = new PHIEUTHUTIEN();
            GetAllInput(obj);
            if (IsInputFieldsFilled())
            {
                if (phieuThuTienManager.Insert(obj))
                {
                    MessageBox.Show("Đã thêm thành công.", "Thông Báo",
                        MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
                else
                {
                    MessageBox.Show("Không thể thêm Phiếu Thu Tiền.", "Lỗi");
                }
            }
            else
            {
                MessageBox.Show(GenericError.InputErrorContent, GenericError.InputError);
            }

        }

        /// <summary>
        /// Double-check all input fields that are required for a PhieuThuTien.
        /// </summary>
        /// <returns>Return true if all fields are in correct formats.</returns>
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

        /// <summary>
        /// After user chose an option. Display its relating information onto the screen.
        /// </summary>
        private void AgencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DAILY daiLy = (DAILY)AgencyComboBox.SelectedItem;
            if (daiLy == null)
            {
                PhoneNumberTextBox.Text = "";
                AddressTextBox.Text = "";
                DebtTextBox.Text = "";
                DebtNotificationTextBlock.Visibility = Visibility.Collapsed;
                return;
            }
            if (daiLy.DIENTHOAI == "" || daiLy.DIENTHOAI == string.Empty) return;
            if (daiLy.DIACHI == "" || daiLy.DIACHI == string.Empty) return;
            PhoneNumberTextBox.Text = daiLy.DIENTHOAI;
            AddressTextBox.Text = daiLy.DIACHI;

            decimal? debt = daiLy.SONO;
            if (debt <= 0 || debt == null) debt = 0;
            DebtTextBox.Text = debt.ToString();

            if (debt <= 0)
            {
                MoneyTextBox.IsEnabled = false;
                DateDatePicker.IsEnabled = false;
                DebtNotificationTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MoneyTextBox.IsEnabled = true;
                DateDatePicker.IsEnabled = true;
                DebtNotificationTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void DistrictComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int maQuan = (int)DistrictComboBox.SelectedValue;
                QUAN selected = ViewManager.Instance.GetQuan(maQuan);
                ObservableCollection<DAILY> listDaiLy = ViewManager.Instance.GetAllDaiLy(selected.MAQUAN);

                AgencyComboBox.ClearValue(ItemsControl.ItemsSourceProperty);
                AgencyComboBox.Items.Clear();
                AgencyComboBox.ItemsSource = listDaiLy;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MoneyTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!FieldChecker.IsTextBoxFilled(MoneyTextBox)) return;
                if (!FieldChecker.IsTextBoxFilled(DebtTextBox)) return;
                if (decimal.Parse(DebtTextBox.Text) < decimal.Parse(MoneyTextBox.Text))
                {
                    MoneyStatus.Visibility = Visibility.Visible;
                }
                else
                {
                    MoneyStatus.Visibility = Visibility.Hidden;
                }
            }
            catch
            {

            }
        }
    }
}
