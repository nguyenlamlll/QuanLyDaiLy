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
using System.ComponentModel;

namespace QuanLyDaiLy_Source.Windows
{

    /// <summary>
    /// Interaction logic for PhieuXuatHang.xaml
    /// </summary>
    public partial class PhieuXuatHang : Page
    {
        MatHangManager matHangManager = new MatHangManager();
        DaiLyManager daiLyManager = new DaiLyManager();
        XuatHangManager xuatHangManager = new XuatHangManager();

        private ObservableCollection<XuatHangGrid> myDataItems = new ObservableCollection<XuatHangGrid>();

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
            MerchandiseDataGrid.ItemsSource = myDataItems;


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

        private System.Collections.ArrayList GetAllMaHang()
        {
            System.Collections.ArrayList listMaHang = new System.Collections.ArrayList();
            for (int i = 0; i < MerchandiseDataGrid.Items.Count; i++)
            {
                try
                {
                    // Get Mã Hàng from Tên Hàng
                    DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, i, 0);
                    TextBlock tb = cell.Content as TextBlock;
                    int maHang = ViewManager.Instance.GetMaHang(tb.Text);
                    listMaHang.Add(maHang);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return listMaHang;
        }
        private System.Collections.ArrayList GetAllSoLuongHang()
        {
            System.Collections.ArrayList listSoLuong = new System.Collections.ArrayList();
            for (int i = 0; i < MerchandiseDataGrid.Items.Count; i++)
            {
                try
                {
                    // Get Số Lượng
                    DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, i, 2);
                    TextBlock tb = cell.Content as TextBlock;
                    int soLuong = int.Parse(tb.Text);
                    listSoLuong.Add(soLuong);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return listSoLuong;
        }
        private PHIEUXUATHANG GetCurrentPhieuXuatHang()
        {
            PHIEUXUATHANG phieu = new PHIEUXUATHANG();
            try
            {
                phieu.MADL = (int)AgencySelectComboBox.SelectedValue;

                phieu.SOTIENTRA = decimal.Parse(PaidTextBox.Text);
                phieu.TONGTIEN = decimal.Parse(SumTextBox.Text);
                phieu.CONLAI = decimal.Parse(RemainderTextBox.Text);

                phieu.NGAYLAP = DateTime.Now;
            }
            catch
            {

            }
            return phieu;
        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Save
            try
            {
                if (xuatHangManager.Insert(GetCurrentPhieuXuatHang(), GetAllMaHang(), GetAllSoLuongHang()))
                    MessageBox.Show("Thêm Thành Công", "Thành Công");
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

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
                NavigationState state = new NavigationState() { WillNavigatingMethodOfParentsBeSkipped = true }; // Custom Exit Button. No need to normally check upon navigating.
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative), state);
            }
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Save
            try
            {
                if (xuatHangManager.Insert(GetCurrentPhieuXuatHang(), GetAllMaHang(), GetAllSoLuongHang()))
                    MessageBox.Show("Thêm Thành Công", "Thành Công");
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            // Clear all input fields
            //MerchandiseDataGrid.Items.Clear();
            //MerchandiseDataGrid.Items.Refresh();
            myDataItems.Clear();

            SumTextBox.Text = string.Empty;
            PaidTextBox.Text = string.Empty;
            RemainderTextBox.Text = string.Empty;

            MatHangComboBox.SelectedIndex = -1;
            DonViTinhTextBox.Text = string.Empty;
            DonGiaTextBox.Text = string.Empty;
            SoLuongTextBox.Text = string.Empty;
            ThanhTienTextBox.Text = string.Empty;
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
            try
            {
                int maHang = (int)MatHangComboBox.SelectedValue;
                DonViTinhTextBox.Text = ViewManager.Instance.GetDonViTinh(maHang);
                DonGiaTextBox.Text = ViewManager.Instance.GetDonGia(maHang).ToString();
                ThanhTienTextBox.Text = string.Empty;
                SoLuongTextBox.IsEnabled = true;
            }
            catch
            {

            }
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
        /// Kiểm tra tính hợp lệ của mặt hàng trước khi thêm vào phiếu xuất hàng
        /// </summary>
        /// <returns></returns>
        private bool IsDataGridItemValid()
        {
            // Field input checks
            if (MatHangComboBox.SelectedIndex == -1) return false;
            if (SoLuongTextBox.IsEnabled == false) return false;
            if (SoLuongTextBox.Text == "" || SoLuongTextBox.Text == string.Empty) return false;
            if (!Utilities.IsDigitsOnly(SoLuongTextBox.Text)) return false;

            // Logical checks
            System.Collections.ArrayList list = this.GetAllMaHang();
            MATHANG selected = (MATHANG)MatHangComboBox.SelectedItem;
            foreach (var item in list)
            {
                if (selected.MAHANG == (int)item)
                {
                    MessageBox.Show("Mặt hàng này đã có trong phiếu xuất hàng", "Lỗi");
                    return false;
                }
            }
            return true;
        }

        private bool IsIncreasing = false;
        private bool IsDecreasing = false;

        /// <summary>
        /// Add a product to the order from details dock panel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (IsIncreasing)
                //{
                //    DataGridRow selectedRow = MerchandiseDataGrid.GetSelectedRow();

                //}
                if (IsIncreasing) goto SkipCheck;
                if (!IsDataGridItemValid()) return;
                SkipCheck:
                MATHANG selected = (MATHANG)MatHangComboBox.SelectedItem;
                XuatHangGrid item = new XuatHangGrid
                {
                    MatHang = selected.TENHANG,
                    DonViTinh = ViewManager.Instance.GetDonViTinh(selected.MAHANG),
                    SoLuong = int.Parse(SoLuongTextBox.Text),
                    DonGia = selected.DONGIA.Value,
                    ThanhTien = decimal.Parse(ThanhTienTextBox.Text)
                };
                myDataItems.Add(item);
                if (IsIncreasing) myDataItems.Move(myDataItems.Count - 1, IncreasingIndex);
                //if (MerchandiseDataGrid.Items.Add(item) == -1) // The item couldn't be added
                //{
                //    return;
                //}
                //else // The item is added successfully
                //{
                SoLuongTextBox.Text = string.Empty;
                ThanhTienTextBox.Text = string.Empty;

                // Update Sum Money for PhieuXuatHang
                decimal sum = 0;
                for (int i = 0; i < MerchandiseDataGrid.Items.Count; i++)
                {
                    try
                    {
                        //DataGridRow row = (DataGridRow)MerchandiseDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                        //var cell = DataGridHelper.GetCell(MerchandiseDataGrid, row, 5);

                        DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, i, 5);
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
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Finished Increasing/Decreasing.
                if (IsIncreasing == true) IsIncreasing = false;
                if (IsDecreasing == true) IsDecreasing = false;
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
        /// <summary>
        /// Nếu người dùng đã nhập số tiền đại lý trả thì cập nhật số dư còn lại vào
        /// RemainderTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PaidTextBox.Text == null || PaidTextBox.Text == "") return;
            if (SumTextBox.Text == null || SumTextBox.Text == "") return;
            int sum = 0, paid = 0, remainder = 0;
            try
            {
                sum = int.Parse(SumTextBox.Text);
                paid = int.Parse(PaidTextBox.Text);
                remainder = sum - paid;
            }
            catch
            {

            }
            if (remainder < 0) PaidStatus.Visibility = Visibility.Visible;
            else PaidStatus.Visibility = Visibility.Collapsed;

            RemainderTextBox.Text = remainder.ToString();
        }
        */

        private void MerchandiseDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            DataGridRow selected = DataGridHelper.GetSelectedRow(MerchandiseDataGrid);
            if (selected == null) return;
            else
            {
                DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, selected, 0);
                TextBlock tb = cell.Content as TextBlock;
                int maHang = ViewManager.Instance.GetMaHang(tb.Text);
                MatHangComboBox.SelectedIndex = maHang - 1; //As MatHang is never deleted, this is usable. If it can be deleted -> bug.

            }
        }

        /// <summary>
        /// Update EditDockPanel (For an unified visual experience).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MerchandiseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGridRow selected = DataGridHelper.GetSelectedRow(MerchandiseDataGrid);
            if (selected == null) return;
            else
            {
                //DataGridCell cell = DataGridHelper.GetCell(MerchandiseDataGrid, selected, 0);
                //TextBlock tb = cell.Content as TextBlock;
                string selectedTenHang = DataGridHelper.GetCellContentAsString(MerchandiseDataGrid, 0);
                for (int i = 0; i < MatHangComboBox.Items.Count; i++)
                {
                    MATHANG item = (MATHANG)MatHangComboBox.Items[i];
                    string itemContent = (string)item.TENHANG;
                    if (selectedTenHang == itemContent) // Found the exact MatHang to work with
                    {
                        // Load current settings
                        MatHangComboBox.SelectedIndex = i;
                    }
                }
            }
        }

        #region Inside-DataGrid Buttons
        int IncreasingIndex = 0;
        /// <summary>
        /// Increase SoLuong of the row by one unit.
        /// </summary>
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IsIncreasing = true;
            DataGridRow selectedRow = MerchandiseDataGrid.GetSelectedRow();
            var selectedIndex = MerchandiseDataGrid.SelectedIndex;
            IncreasingIndex = MerchandiseDataGrid.SelectedIndex;

            // Set Item in Edit Dock Panel
            string selectedTenHang = DataGridHelper.GetCellContentAsString(MerchandiseDataGrid, 0);
            int selectedMaHang = ViewManager.Instance.GetMaHang(selectedTenHang);


            System.Collections.ArrayList listMaHang = GetAllMaHang();
            System.Collections.ArrayList listSoLuong = GetAllSoLuongHang();


            int selectedSoLuong = 0;
            for (int i = 0; i < listMaHang.Count; i++)
            {
                if (selectedMaHang.ToString() == listMaHang[i].ToString())
                {
                    selectedSoLuong = (int)listSoLuong[i];
                    break;
                }
            }



            for (int i = 0; i < MatHangComboBox.Items.Count; i++)
            {
                MATHANG item = (MATHANG)MatHangComboBox.Items[i];
                string itemContent = (string)item.TENHANG;
                if (selectedTenHang == itemContent) // Found the exact MatHang to work with
                {
                    // Load current settings
                    MatHangComboBox.SelectedIndex = i;
                    SoLuongTextBox.Text = selectedSoLuong.ToString();
                    SoLuongTextBox_PreviewKeyUp(sender, null);
                    // Delete the old one
                    myDataItems.RemoveAt(selectedIndex);

                    // Update the settings
                    SoLuongTextBox.Text = (++selectedSoLuong).ToString();
                    MatHangComboBox_SelectionChanged(sender, null);

                    // Increase SoLuong, Update ThanhTien
                    SoLuongTextBox_PreviewKeyUp(sender, null);

                    // Update DataGrid
                    SaveEditButton_Click(sender, e);

                }
            }
        }

        /// <summary>
        /// Decrease SoLuong of the row by one unit
        /// </summary>
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            IsDecreasing = true;
            DataGridRow selectedRow = MerchandiseDataGrid.GetSelectedRow();
            var selectedIndex = MerchandiseDataGrid.SelectedIndex;
            IncreasingIndex = MerchandiseDataGrid.SelectedIndex;

            // Set Item in Edit Dock Panel
            string selectedTenHang = DataGridHelper.GetCellContentAsString(MerchandiseDataGrid, 0);
            int selectedMaHang = ViewManager.Instance.GetMaHang(selectedTenHang);


            System.Collections.ArrayList listMaHang = GetAllMaHang();
            System.Collections.ArrayList listSoLuong = GetAllSoLuongHang();


            int selectedSoLuong = 0;
            for (int i = 0; i < listMaHang.Count; i++)
            {
                if (selectedMaHang.ToString() == listMaHang[i].ToString())
                {
                    selectedSoLuong = (int)listSoLuong[i];
                    break;
                }
            }



            for (int i = 0; i < MatHangComboBox.Items.Count; i++)
            {
                MATHANG item = (MATHANG)MatHangComboBox.Items[i];
                string itemContent = (string)item.TENHANG;
                if (selectedTenHang == itemContent) // Found the exact MatHang to work with
                {
                    // Load current settings
                    MatHangComboBox.SelectedIndex = i;
                    SoLuongTextBox.Text = selectedSoLuong.ToString();
                    SoLuongTextBox_PreviewKeyUp(sender, null);
                    // Delete the old one
                    myDataItems.RemoveAt(selectedIndex);

                    // Update the settings
                    SoLuongTextBox.Text = (--selectedSoLuong).ToString();
                    MatHangComboBox_SelectionChanged(sender, null);

                    // Increase SoLuong, Update ThanhTien
                    SoLuongTextBox_PreviewKeyUp(sender, null);

                    // Update DataGrid
                    SaveEditButton_Click(sender, e);

                }
            }
        }



        #endregion

        /// <summary>
        /// Nếu người dùng đã nhập số tiền đại lý trả thì cập nhật số dư còn lại vào
        /// RemainderTextBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaidTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (PaidTextBox.Text == null || PaidTextBox.Text == "") return;
            if (SumTextBox.Text == null || SumTextBox.Text == "") return;
            int sum = 0, paid = 0, remainder = 0;
            try
            {
                sum = int.Parse(SumTextBox.Text);
                paid = int.Parse(PaidTextBox.Text);
                remainder = sum - paid;
            }
            catch
            {

            }
            if (remainder < 0) PaidStatus.Visibility = Visibility.Visible;
            else PaidStatus.Visibility = Visibility.Collapsed;

            RemainderTextBox.Text = remainder.ToString();

            int maDL = (int)AgencySelectComboBox.SelectedValue;
            if (!xuatHangManager.IsMoreDebtsAllowed(remainder, maDL))
            {
                ConLaiStatus.Visibility = Visibility.Visible;

                DAILY daiLy = daiLyManager.Get(maDL);
                DebtInformation_ConLaiStatus.Text += "\nSố nợ hiện tại: ";
                DebtInformation_ConLaiStatus.Text += ViewManager.Instance.GetSoNoDaiLy(daiLy.LOAIDL.Value);
            }
            else
            {
                ConLaiStatus.Visibility = Visibility.Collapsed;
            }
            DebtInformation_ConLaiStatus.Text = "Số nợ hiện tại của đại lý không cho phép thực hiện phiếu xuất hàng này.";
        }
    }

    /// <summary>
    /// A template class for a grid with columns as its property
    /// </summary>
    public class XuatHangGrid : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string MatHang { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
