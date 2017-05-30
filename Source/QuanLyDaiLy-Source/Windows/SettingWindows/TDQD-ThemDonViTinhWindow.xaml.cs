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

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_ThemDonViTinhWindow.xaml
    /// </summary>
    public partial class TDQD_ThemDonViTinhWindow : Window
    {
        ObservableCollection<DVT> donViTinhItems = new ObservableCollection<DVT>();
        public TDQD_ThemDonViTinhWindow()
        {
            InitializeComponent();
            donViTinhItems = ViewManager.Instance.GetAllDVT();
            DonViTinhDataGrid.ItemsSource = donViTinhItems;
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
                    donViTinhItems = ViewManager.Instance.GetAllDVT();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, vui lòng thử lại.");
                }
            }
        }
    }
}
