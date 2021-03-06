﻿using System;
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
using System.Collections.ObjectModel;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using QuanLyDaiLy_Source.Commons;

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

        private Models.BusinessLogic.DaiLyManager daiLyManager = new Models.BusinessLogic.DaiLyManager();

        public TiepNhanDaiLy()
        {
            InitializeComponent();
            Loaded += TiepNhanDaiLy_Loaded;


            // Field Check EventHandlers
            NameInputTextBox.LostFocus += NameInput_FieldCheck;
            //IDInputTextBox.LostFocus += IDInputTextBox_FieldCheck;
            TypeInputComboBox.LostFocus += TypeInputComboBox_FieldCheck;
            PhoneNumberInputTextBox.LostFocus += PhoneNumberInputTextBox_FieldCheck;
            AddressNumberInputTextBox.LostFocus += AddressNumberInputTextBox_FieldCheck;
            StreetInputTextBox.LostFocus += StreetInputTextBox_FieldCheck;
            DistrictInputComboBox.LostFocus += DistrictInputComboBox_FieldCheck;
            AcceptanceDateDatePicker.LostFocus += AcceptanceDateDatePicker_FieldCheck;


        }

        private void TiepNhanDaiLy_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Tiếp Nhận Đại lý";
            pageLoaded?.Invoke(this, e);

            // Get Districts and Agency types from database.
            try
            {
                //ObservableCollection<QUAN> danhSachQuan = ViewManager.Instance.GetAllQuan();
                //ObservableCollection<LOAIDL> danhSachLoaiDL = ViewManager.Instance.GetAllLoaiDL();
                DistrictInputComboBox.Items.Clear();
                TypeInputComboBox.Items.Clear();
                DistrictInputComboBox.ItemsSource = ViewManager.Instance.GetAllQuan();
                TypeInputComboBox.ItemsSource = ViewManager.Instance.GetAllLoaiDL();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void DistrictInputComboBox_FieldCheck(object sender, RoutedEventArgs e)
        {
            if (DistrictInputComboBox.SelectedIndex == -1)
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

        /*
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
        */

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

        /// <summary>
        /// Get all inputs, store into database, and navigate to homepage.
        /// </summary>
        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {

            //var dataAccessObj = DAODLL.DAOTiepNhanDaiLy.Instance;

            DAILY daiLy = new DAILY();

            if (!GetDaiLyInput(daiLy)) return;
            if (daiLyManager.Insert(daiLy))
            {
                MessageBox.Show("Đã thêm thành công.", "Thông Báo",
                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                NavigationService ns = NavigationService.GetNavigationService(this);
                NavigationState state = new NavigationState() { WillNavigatingMethodOfParentsBeSkipped = true };
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative), state);
            }
            else
            {
                MessageBox.Show(Commons.GenericError.MaximumErrorContent, Commons.GenericError.MaximumError,
                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }


        }

        /// <summary>
        /// Get all inputs and store into database. Then clear all input fields.
        /// </summary>
        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            //var dataAccessObj = DAODLL.DAOTiepNhanDaiLy.Instance;

            DAODLL.DAILY daiLy = new DAODLL.DAILY();

            if (!GetDaiLyInput(daiLy)) return;
            if (daiLyManager.Insert(daiLy))
            {
                MessageBox.Show("Đã thêm thành công.", "Thông Báo",
                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
            else
            {
                MessageBox.Show(Commons.GenericError.MaximumErrorContent, Commons.GenericError.MaximumError,
                    MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }


            NameInputTextBox.Text = "";
            //IDInputTextBox.Text = "";
            TypeInputComboBox.SelectedIndex = -1;
            PhoneNumberInputTextBox.Text = "";
            AddressNumberInputTextBox.Text = "";
            StreetInputTextBox.Text = "";
            DistrictInputComboBox.SelectedIndex = -1;
            AcceptanceDateDatePicker.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Handle Exit event. Ask before navigating.
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn quay lại trang Nghiệp Vụ?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                Commons.NavigationState state = new Commons.NavigationState() { WillNavigatingMethodOfParentsBeSkipped = true }; // Custom Exit Button. No need to normally check upon navigating.
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative), state);
            }
        }

        /// <summary>
        /// Get all information from XAML.
        /// </summary>
        /// <param name="daiLy"></param>
        private bool GetDaiLyInput(DAODLL.DAILY daiLy)
        {
            //daiLy.MADL = int.Parse(IDInputTextBox.Text.ToString());
            try
            {
                daiLy.TENDL = NameInputTextBox.Text.ToString();

                if (!Helper.Utilities.IsDigitsOnly(PhoneNumberInputTextBox.Text)) return false;
                daiLy.DIENTHOAI = PhoneNumberInputTextBox.Text.ToString();

                daiLy.DIACHI = AddressNumberInputTextBox.Text.ToString() + ", " + StreetInputTextBox.Text.ToString();
                daiLy.NGAYTIEPNHAN = AcceptanceDateDatePicker.SelectedDate.Value;

                if (DistrictInputComboBox.SelectedValue == null) return false;
                if (TypeInputComboBox.SelectedValue == null) return false;
                daiLy.MAQUAN = (int)DistrictInputComboBox.SelectedValue;
                daiLy.LOAIDL = (int)TypeInputComboBox.SelectedValue;
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi nhập liệu, vui lòng kiểm tra lại các trường nhập liệu.");
            }
            return false;
        }
    }
}
