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
        public TiepNhanDaiLy()
        {
            InitializeComponent();

            NameInputTextBox.LostFocus += NameInput_FieldCheck;
            IDInputTextBox.LostFocus += IDInputTextBox_FieldCheck;
            TypeInputComboBox.LostFocus += TypeInputComboBox_FieldCheck;
            PhoneNumberInputTextBox.LostFocus += PhoneNumberInputTextBox_FieldCheck;
            AddressNumberInputTextBox.LostFocus += AddressNumberInputTextBox_FieldCheck;
            StreetInputTextBox.LostFocus += StreetInputTextBox_FieldCheck;
            DistrictInputTextBox.LostFocus += DistrictInputTextBox_FieldCheck;
            AcceptanceDateDatePicker.LostFocus += AcceptanceDateDatePicker_FieldCheck;

        }

        private void AcceptanceDateDatePicker_FieldCheck(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DistrictInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StreetInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void AddressNumberInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void PhoneNumberInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NameInput_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (NameInputTextBox.Text == "")
            {
                NameInputStatus.Visibility = Visibility.Visible;
                NameInputStatus.Text = "Trường bắt buộc phải nhập!";
            }
        }

       
        private void IDInputTextBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (IDInputTextBox.Text == "")
            {
                IDStatus.Visibility = Visibility.Visible;
                IDStatus.Text = "Trường bắt buộc phải nhập!";
            }
        }
        private void TypeInputComboBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (TypeInputComboBox.SelectedIndex == -1)
            {
                TypeStatus.Visibility = Visibility.Visible;
                TypeStatus.Text = "Lựa chọn Loại đại lý.";
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
