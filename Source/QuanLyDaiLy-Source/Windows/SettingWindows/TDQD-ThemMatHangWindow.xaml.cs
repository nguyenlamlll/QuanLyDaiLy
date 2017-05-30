using QuanLyDaiLy_Source.Commons;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using QuanLyDaiLy_Source.Helper;
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
using System.Windows.Shapes;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_ThemMatHangWindow.xaml
    /// </summary>
    public partial class TDQD_ThemMatHangWindow : Window
    {
        /// <summary>
        /// View model for MatHangDataGrid
        /// </summary>
        ObservableCollection<MatHangGridDataItem> matHangItems = new ObservableCollection<MatHangGridDataItem>();

        public TDQD_ThemMatHangWindow()
        {
            InitializeComponent();
            cbDVT.ItemsSource = ViewManager.Instance.GetAllDVT();
            InitializeDataGridItems();
            MatHangDataGrid.ItemsSource = matHangItems;
        }

        /// <summary>
        /// Update datagrid's view model.
        /// </summary>
        private void InitializeDataGridItems()
        {
            matHangItems.Clear();
            ObservableCollection<DAODLL.MATHANG> listMatHang = ViewManager.Instance.GetAllMatHang();
            foreach (DAODLL.MATHANG item in listMatHang)
            {
                MatHangGridDataItem itemToAdd = new MatHangGridDataItem()
                {
                    MAHANG = item.MAHANG.ToString(),
                    TENHANG = item.TENHANG,
                    DVT = ViewManager.Instance.GetDonViTinh(item.MAHANG),
                    DONGIA = item.DONGIA.ToString()
                };
                matHangItems.Add(itemToAdd);
            }
        }

        /// <summary>
        /// Thêm mặt hàng mới vào database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuuMatHang_Click(object sender, RoutedEventArgs e)
        {
            if (!FieldChecker.IsTextBoxFilled(txbTenMH) || !Utilities.IsDigitsOnly(txbDonGia.Text) || cbDVT.SelectedValue == null)
            {
                MessageBox.Show(GenericError.InputErrorContent, GenericError.InputError);
            }
            else
            {
                int donGia = int.Parse(txbDonGia.Text);
                if (ThietLapQuyDinh.Instance.InsertMATHANG(txbTenMH.Text, (int)cbDVT.SelectedValue, donGia))
                {
                    MessageBox.Show("Đã thêm thành công.");
                    txbTenMH.Clear();
                    txbDonGia.Clear();
                    InitializeDataGridItems();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, vui lòng thử lại.");
                }
            }
        }
    }

    public class MatHangGridDataItem
    {
        public string MAHANG { get; set; }
        public string TENHANG { get; set; }
        public string DVT { get; set; }
        public string DONGIA { get; set; }

    }
}
