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
using QuanLyDaiLy_Source.Commons.ViewModel;
using System.Collections.ObjectModel;
using QuanLyDaiLy_Source.Commons.BusinessLogic;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_LoaiDaiLyWindow.xaml
    /// </summary>
    public partial class TDQD_LoaiDaiLyWindow : Window
    {
        ObservableCollection<LoaiDaiLyDataItem> listLoaiDaiLy = new ObservableCollection<LoaiDaiLyDataItem>();
        public TDQD_LoaiDaiLyWindow()
        {
            InitializeComponent();
            InitializeDataGridItem();
            LoaiDaiLyDataGrid.ItemsSource = listLoaiDaiLy;
            LoaiDaiLyDataGrid2.ItemsSource = listLoaiDaiLy;
            cbBoxLoaiDL.ItemsSource = listLoaiDaiLy;
        }

        /// <summary>
        /// Load (LOAIDL) Items into this page's datagrid view model.
        /// </summary>
        private void InitializeDataGridItem()
        {
            foreach (DAODLL.LOAIDL item in ViewManager.Instance.GetAllLoaiDL())
            {
                listLoaiDaiLy.Add(new LoaiDaiLyDataItem()
                {
                    MALOAI = item.MALOAI,
                    TENLOAI = item.TENLOAI,
                    SONOTOIDA = item.SONOTOIDA.Value.ToString()
                });
            }
        }

        private void btnThemLoaiDaiLy_Click(object sender, RoutedEventArgs e)
        {
            if (!Helper.FieldChecker.IsTextBoxFilled(txbLoai) || !Helper.Utilities.IsDigitsOnly(txbNo.Text))
            {
                MessageBox.Show(Commons.GenericError.InputErrorContent, Commons.GenericError.InputError);
            }
            else
            {
                int no = int.Parse(txbNo.Text);
                if (ThietLapQuyDinh.Instance.InsertLoaiDL(txbLoai.Text, no))
                {
                    //MessageBox.Show("Đã thêm loại đại lý thành công.");
                    txbLoai.Clear();
                    txbNo.Clear();

                    // Update LoaiDaiLyDataGrid
                    listLoaiDaiLy.Clear();
                    InitializeDataGridItem();
                }
                else
                {
                    MessageBox.Show("Không thể thực hiện tác vụ này. Vui lòng thử lại sau.");
                }
            }
        }

        private void btnSaveSoNoToiDa_Click(object sender, RoutedEventArgs e)
        {
            if (cbBoxLoaiDL.SelectedIndex == -1 || cbBoxLoaiDL == null ||
                txbNo_ChinhSuaSoNo.Text == "" || !Helper.Utilities.IsDigitsOnly(txbNo_ChinhSuaSoNo.Text))
            {
                MessageBox.Show(Commons.GenericError.InputErrorContent, Commons.GenericError.InputError);
            }
            else
            {
                int no = int.Parse(txbNo_ChinhSuaSoNo.Text);
                if (ThietLapQuyDinh.Instance.ChangeMaxNumOfTienNo((int)cbBoxLoaiDL.SelectedValue, no))
                {
                    //MessageBox.Show("Đã thực hiện thay đổi thành công.");
                    cbBoxLoaiDL.SelectedIndex = -1;
                    // Update LoaiDaiLyDataGrid
                    listLoaiDaiLy.Clear();
                    InitializeDataGridItem();
                }
                else
                {
                    MessageBox.Show("Không thể thực hiện tác vụ này. Vui lòng thử lại sau.");
                }
            }
        }
    }

}
