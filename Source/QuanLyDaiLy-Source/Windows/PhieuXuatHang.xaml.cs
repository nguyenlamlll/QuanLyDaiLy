using DAODLL;
using QuanLyDaiLy_Source.Commons;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using QuanLyDaiLy_Source.Helper;
using QuanLyDaiLy_Source.Models.BusinessLogic;

namespace QuanLyDaiLy_Source.Windows
{

    /// <summary>
    /// Interaction logic for PhieuXuatHang.xaml
    /// </summary>
    public partial class PhieuXuatHang : Page
    {
        MatHangManager matHangManager = new MatHangManager();
        DaiLyManager daiLyManager = new DaiLyManager();
        public ObservableCollection<MATHANG> dummyMatHang { get; set; }

        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        public PhieuXuatHang()
        {
            InitializeComponent();
            Loaded += PhieuXuatHang_Loaded;
            //AgencySelectComboBox.IsEnabled = false;

            //DataContext = new PhieuXuatHang_ViewModel();
            //dummyMatHang = matHangManager.GetMatHang();
            //MerchandiseDataGrid.ItemsSource = dummyMatHang;


        }

        private void PhieuXuatHang_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Phiếu Xuất Hàng";
            pageLoaded?.Invoke(this, e);
            try
            {
                DistrictSelectComboBox.Items.Clear();
                DistrictSelectComboBox.ItemsSource = ViewManager.Instance.GetAllQuan();

                AgencySelectComboBox.Items.Clear();
                AgencySelectComboBox.ItemsSource = ViewManager.Instance.GetAllDaiLy();

                MatHangComboBox.Items.Clear();
                MatHangComboBox.ItemsSource = DAOView.Instance.GetAllMatHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// After user selects what district the agency is in, narrow down the search in Agency List.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DistrictSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int maQuan = (int)DistrictSelectComboBox.SelectedValue;
                QUAN selected = ViewManager.Instance.GetQuan(maQuan);
                ObservableCollection<DAILY> listDaiLy = ViewManager.Instance.GetAllDaiLy(selected.MAQUAN);

                AgencySelectComboBox.ClearValue(ItemsControl.ItemsSourceProperty);
                AgencySelectComboBox.Items.Clear();
                AgencySelectComboBox.ItemsSource = listDaiLy;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            //MerchandiseDataGrid.Items.Add();
        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Save
            double sumMoney = 0;
            foreach (System.Data.DataRowView row in MerchandiseDataGrid.Items)
            {
                sumMoney = (double)row.Row.ItemArray[4];
            }
            // Clear all input fields

            //Exit
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
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

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Save

            // Clear all input fields
            MerchandiseDataGrid.Items.Clear();
            MerchandiseDataGrid.Items.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// After user selects Product. Load all relating information of that product.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MatHangComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int maHang = (int)MatHangComboBox.SelectedValue;
            DonViTinhTextBox.Text = ViewManager.Instance.GetDonViTinh(maHang);
            DonGiaTextBox.Text = ViewManager.Instance.GetDonGia(maHang).ToString();
            SoLuongTextBox.IsEnabled = true;
        }


        /// <summary>
        /// Validate its input and update relating information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoLuongTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string soLuong = SoLuongTextBox.Text;

            if (!FieldChecker.IsTextBoxFilled(SoLuongTextBox))
                SoLuongTextBlock_Status.Visibility = Visibility.Visible;
            else
            {
                if (!Utilities.IsDigitsOnly(soLuong))
                    SoLuongTextBlock_Status.Visibility = Visibility.Visible;
                else
                {
                    SoLuongTextBlock_Status.Visibility = Visibility.Collapsed;
                    try
                    {
                        decimal thanhTien = int.Parse(soLuong) * decimal.Parse(DonGiaTextBox.Text);
                        ThanhTienTextBox.Text = thanhTien.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }


        /// <summary>
        /// Validate its input and update relating information right after user enters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoLuongTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            string soLuong = SoLuongTextBox.Text;

            if (!FieldChecker.IsTextBoxFilled(SoLuongTextBox))
                SoLuongTextBlock_Status.Visibility = Visibility.Visible;
            else
            {
                if (!Utilities.IsDigitsOnly(soLuong))
                    SoLuongTextBlock_Status.Visibility = Visibility.Visible;
                else
                {
                    SoLuongTextBlock_Status.Visibility = Visibility.Collapsed;
                    try
                    {
                        decimal thanhTien = int.Parse(soLuong) * decimal.Parse(DonGiaTextBox.Text);
                        ThanhTienTextBox.Text = thanhTien.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Add a product to the order from details dock panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MATHANG selected = (MATHANG)MatHangComboBox.SelectedItem;
                XuatHangGrid item = new XuatHangGrid
                {
                    MatHang = selected.TENHANG,
                    DonViTinh = ViewManager.Instance.GetDonViTinh(selected.MAHANG),
                    SoLuong = int.Parse(SoLuongTextBox.Text),
                    DonGia = selected.DONGIA.Value,
                    ThanhTien = decimal.Parse(ThanhTienTextBox.Text)
                };
                if (MerchandiseDataGrid.Items.Add(item) == -1) // The item couldn't be added
                {
                    return;
                }
                else // The item is added successfully
                {
                    SoLuongTextBox.Text = string.Empty;
                    ThanhTienTextBox.Text = string.Empty;

                    // Update Sum Money for PhieuXuatHang
                    decimal sum = 0;
                    for (int i = 0; i < MerchandiseDataGrid.Items.Count; i++)
                    {
                        try
                        {
                            //DataGridRow row = (DataGridRow)MerchandiseDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                            //var cell = DataGridHelper.GetCell(MerchandiseDataGrid, row, 4);

                            DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, i, 4);
                            TextBlock tb = cell.Content as TextBlock;
                            decimal money = decimal.Parse(tb.Text.ToString());
                            sum += money;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                    SumTextBox.Text = sum.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void MinimizeDockPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MinimizeDockPanelButton.Content.ToString() == "\uE015") //If the dock panel is up
            {
                MinimizeDockPanelToolTip.Text = "Phóng to";
                MinimizeDockPanelButton.Content = "\uE014";
                MerchandiseStackPanel.Margin = new Thickness(0, 220, 0, 0); // Margin="0,220,0,0"
            }
            else //If the dock panel is hiden
            {
                MinimizeDockPanelToolTip.Text = "Thu nhỏ";
                MinimizeDockPanelButton.Content = "\uE015";
                MerchandiseStackPanel.Margin = new Thickness(0, 0, 0, 0);
            }

        }

        /// <summary>
        /// Update the current editing row to  MerchandiseDataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
        private void MerchandiseDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            foreach (System.Data.DataRowView row in MerchandiseDataGrid.Items)
            {
                //Sum up all "Thanh Tien"
                try
                {
                    SumTextBox.Text += (double)row.Row.ItemArray[4];
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        */


        /*
        private void MerchandiseDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (this.MerchandiseDataGrid.SelectedItem != null)
            {
                (sender as DataGrid).RowEditEnding -= MerchandiseDataGrid_RowEditEnding;
                (sender as DataGrid).CommitEdit();
                (sender as DataGrid).Items.Refresh();
                (sender as DataGrid).RowEditEnding += MerchandiseDataGrid_RowEditEnding;
            }
            else return;
        }
        */


    }

    /// <summary>
    /// A template class for a grid with columns as its property
    /// </summary>
    public class XuatHangGrid
    {
        public string MatHang { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
