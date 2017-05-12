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
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using EmployeeManagerQuanLyDaiLy_Source.Models.BusinessLogic;
using System.Windows.Navigation;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TiepNhanNhanVienWindow.xaml
    /// </summary>
    public partial class TiepNhanNhanVienWindow : Window
    {
        public TiepNhanNhanVienWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Check if all input fields are filled properly
        /// </summary>
        /// <returns></returns>
        private bool isInputFieldsFilled()
        {
            if (EmployeeNameTextBox.Text == "") return false;
            if (UsernameTextBox.Text == "") return false;
            if (PasswordTextBox.Password == "") return false;
            if (AddressTextBox.Text == "") return false;
            if (EmployeeDatePicker.SelectedDate == null) return false;
            if (PositionComboBox.SelectedIndex == -1) return false;
            return true;
        }

        /// <summary>
        /// Reset all input fields
        /// </summary>
        private void ResetInputFields()
        {
            EmployeeNameTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordTextBox.Password = "";
            AddressTextBox.Text = "";
            EmployeeDatePicker.SelectedDate = DateTime.Now;
            PositionComboBox.SelectedIndex = -1;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManager.Instance.Insert(EmployeeNameTextBox.Text.ToString(), EmployeeDatePicker.SelectedDate.Value,
                AddressTextBox.Text.ToString(), (int)PositionComboBox.SelectedValue, UsernameTextBox.Text.ToString(),
                PasswordTextBox.Password.ToString());

            MessageBox.Show("Đã thêm thành công.", "Thành công");
            ResetInputFields();
        }



        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PositionComboBox.ItemsSource = ViewManager.Instance.GetAllCV();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordShowTextBox.Text = PasswordTextBox.Password;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            PasswordShowTextBox.Visibility = Visibility.Visible;
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            PasswordTextBox.Visibility = Visibility.Visible;
            PasswordShowTextBox.Visibility = Visibility.Collapsed;
        }
    }
}
