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
using QuanLyDaiLy_Source;
using QuanLyDaiLy_Source.Commons;
using DAODLL;
using System.Collections.ObjectModel;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using QuanLyDaiLy_Source.Commons.ViewModel;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_ThemDonViTinhWindow.xaml
    /// </summary>
    public partial class TDQD_ThemDonViTinhWindow : Window
    {
        ObservableCollection<DonViTinh_DataItem> donViTinhItems = new ObservableCollection<DonViTinh_DataItem>();
        public TDQD_ThemDonViTinhWindow()
        {
            InitializeComponent();
            InitializeDataGridItems();
            DonViTinhDataGrid.ItemsSource = donViTinhItems;
        }

        /// <summary>
        /// Update datagrid's view model.
        /// </summary>
        private void InitializeDataGridItems()
        {
            donViTinhItems.Clear();
            ObservableCollection<DVT> listDonViTinh = ViewManager.Instance.GetAllDVT();
            foreach (DVT item in listDonViTinh)
            {
                DonViTinh_DataItem itemToAdd = new DonViTinh_DataItem()
                {
                   MADVT = item.MADVT.ToString(),
                   DVT1 = item.DVT1
                };
                donViTinhItems.Add(itemToAdd);
            }
        }

        private void btnLuuDVT_Click(object sender, RoutedEventArgs e)
        {
            if (!Helper.FieldChecker.IsTextBoxFilled(txbTenDVT))
            {
                MessageBox.Show(GenericError.InputErrorContent, GenericError.InputError);
                return; 
            }
            else
            {
                if (ThietLapQuyDinh.Instance.InsertDVT(txbTenDVT.Text))
                {
                    MessageBox.Show("Đã thêm thành công");
                    txbTenDVT.Clear();
                    InitializeDataGridItems();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, vui lòng thử lại.");
                }
            }
        }
    }
}
