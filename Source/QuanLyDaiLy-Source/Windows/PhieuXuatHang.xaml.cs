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

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for PhieuXuatHang.xaml
    /// </summary>
    public partial class PhieuXuatHang : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        public PhieuXuatHang()
        {
            InitializeComponent();
            Loaded += PhieuXuatHang_Loaded;


            //List<Models.DataGridTestItem> dataList = new List<Models.DataGridTestItem>();
            //MerchandiseDataGrid.ItemsSource = dataList;

        }

        private void PhieuXuatHang_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Phiếu Xuất Hàng";
            pageLoaded?.Invoke(this, e);
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            Models.DataGridTestItem data1 = new Models.DataGridTestItem { Test1 = "AAA", Test2 = "BBB" };
            Models.DataGridTestItem data2 = new Models.DataGridTestItem { Test1 = "111", Test2 = "222" };
            List<Models.DataGridTestItem> list = new List<Models.DataGridTestItem>();
            list.Add(data1);
            list.Add(data2);
            foreach (Models.DataGridTestItem item in list)
            {
                MerchandiseDataGrid.Items.Add(item);
            }

        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
