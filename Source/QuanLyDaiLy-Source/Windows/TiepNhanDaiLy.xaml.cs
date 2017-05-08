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

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TiepNhanDaiLy.xaml
    /// </summary>
    public partial class TiepNhanDaiLy : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;

        public TiepNhanDaiLy()
        {
            InitializeComponent();
            Loaded += TiepNhanDaiLy_Loaded;
            

            //Field Check EventHandlers
            NameInputTextBox.LostFocus += NameInput_FieldCheck;
            IDInputTextBox.LostFocus += IDInputTextBox_FieldCheck;
            TypeInputComboBox.LostFocus += TypeInputComboBox_FieldCheck;
            PhoneNumberInputTextBox.LostFocus += PhoneNumberInputTextBox_FieldCheck;
            AddressNumberInputTextBox.LostFocus += AddressNumberInputTextBox_FieldCheck;
            StreetInputTextBox.LostFocus += StreetInputTextBox_FieldCheck;
            DistrictInputTextBox.LostFocus += DistrictInputTextBox_FieldCheck;
            AcceptanceDateDatePicker.LostFocus += AcceptanceDateDatePicker_FieldCheck;


        }

        private void TiepNhanDaiLy_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Tiếp Nhận Đại lý";
            pageLoaded?.Invoke(this, e);
        }

        private void AcceptanceDateDatePicker_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (AcceptanceDateDatePicker.SelectedDate == null)
            {
                AcceptanceDateStatus.Visibility = Visibility.Visible;
            }
            else
            {
                AcceptanceDateStatus.Visibility = Visibility.Hidden;
            }
        }

        private void DistrictInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (DistrictInputTextBox.SelectedIndex == -1)
            {
                DistrictStatus.Visibility = Visibility.Visible;
            }
            else
            {
                DistrictStatus.Visibility = Visibility.Hidden;
            }
        }

        private void StreetInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (StreetInputTextBox.Text == "")
            {
                StreetStatus.Visibility = Visibility.Visible;
            }
            else
            {
                StreetStatus.Visibility = Visibility.Hidden;

            }
        }

        private void AddressNumberInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (AddressNumberInputTextBox.Text == "")
            {
                AddressNumberStatus.Visibility = Visibility.Visible;
            }
            else
            {
                AddressNumberStatus.Visibility = Visibility.Hidden;
            }
        }

        private void PhoneNumberInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (PhoneNumberInputTextBox.Text == "")
            {
                PhoneNumberStatus.Visibility = Visibility.Visible;
            }
            else
            {
                PhoneNumberStatus.Visibility = Visibility.Hidden;
            }
        }

        private void NameInput_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (NameInputTextBox.Text == "")
            {
                NameInputStatus.Visibility = Visibility.Visible;
                NameInputStatusToolTipText.Text += "\nKhông được bỏ trống.";
            }
            else
            {
                NameInputStatus.Visibility = Visibility.Hidden;
            }
        }

       
        private void IDInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (IDInputTextBox.Text == "")
            {
                IDStatus.Visibility = Visibility.Visible;
            }
            else
            {
                IDStatus.Visibility = Visibility.Hidden;
            }
        }
        private void TypeInputComboBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (TypeInputComboBox.SelectedIndex == -1)
            {
                TypeStatus.Visibility = Visibility.Visible;
                TypeStatusToolTipText.Text += "\nChọn một trong các loại đại lý thuộc danh sách.";
            }
            else
            {
                TypeStatus.Visibility = Visibility.Hidden;
            }
        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/MainContent.xaml", UriKind.Relative));
            }
        }


    }
}
