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
using System.Windows.Shapes;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_SoDaiLyToiDaWindow.xaml
    /// </summary>
    public partial class TDQD_SoDaiLyToiDaWindow : Window
    {
        ObservableCollection<QuanDataGridItem> DataGridItemms = new ObservableCollection<QuanDataGridItem>();
        public TDQD_SoDaiLyToiDaWindow()
        {
            InitializeComponent();
            //DataGridItemms = ViewManager.Instance.GetAllQuan();
            InitializeDaiLyToiDaDataGrid();
            DaiLyToiDaDataGrid.ItemsSource = DataGridItemms;
            cbBoxQuan.ItemsSource = ViewManager.Instance.GetAllQuan();


        }

        private void InitializeDaiLyToiDaDataGrid()
        {
            foreach (DAODLL.QUAN item in ViewManager.Instance.GetAllQuan())
            {
                DataGridItemms.Add(new QuanDataGridItem()
                {
                    TENQUAN = item.TENQUAN,
                    SODLTOIDA = item.SODLTOIDA.Value.ToString()
                });
            }
        }

        /// <summary>
        /// Thay doi so Dl toi da trong 1 quan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (!Helper.Utilities.IsDigitsOnly(SoDLTextBox.Text) || cbBoxQuan.SelectedValue == null)
            {
                MessageBox.Show(GenericError.InputErrorContent, GenericError.InputError);
            }
            else
            {
                int dl = int.Parse(SoDLTextBox.Text);
                if (ThietLapQuyDinh.Instance.ChangeNumOfDaily((int)cbBoxQuan.SelectedValue, dl))
                {
                    MessageBox.Show("Đã thay đổi thành công.");
                    DataGridItemms.Clear();
                    InitializeDaiLyToiDaDataGrid(); //Update the DataGrid
                }
                else
                {
                    MessageBox.Show("Thay đổi thất bại. Vui lòng thử lại sau.");
                }
            }
        }

        /// <summary>
        /// Load số loại đại lý tối đa lên textBox Số loại ĐL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBoxQuan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBoxQuan.SelectedIndex == -1)
            {
                SoDLTextBox.IsEnabled = false;
                return;
            }
            try
            {
                SoDLTextBox.Text = ViewManager.Instance.GetSoDlToiDa((int)cbBoxQuan.SelectedValue).ToString();
                SoDLTextBox.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thực hiện tác vụ này. Vui lòng kiểm tra lại lựa chọn quận hiện tại.\n"
                    + ex.ToString());
            }

        }
    }
    public class QuanDataGridItem
    {
        public string TENQUAN { get; set; }
        public string SODLTOIDA { get; set; }
    }
}
