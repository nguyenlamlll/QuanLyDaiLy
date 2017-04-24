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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for BusinessHomePage.xaml
    /// </summary>
    public partial class BusinessHomePage : Page
    {
        public BusinessHomePage()
        {
            InitializeComponent();
            App.Current.Properties["ContentFrameTitle"] = "Nghiệp Vụ Đại Lý";
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)BusinessListView.SelectedItem;
            Test.Text = item.Name;

            string XuatHang = PhieuXuatHangListViewItem.Name.ToString();
            string ThuTien = PhieuThuTienListViewItem.Name.ToString();

            if (item.Name.ToString() == XuatHang)
            {
                try
                {
                    this.NavigationService.Navigate(new PhieuXuatHang());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            if (item.Name.ToString() == ThuTien)
            {
                try
                {
                    this.NavigationService.Navigate(new PhieuThuTien());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
    }
}
