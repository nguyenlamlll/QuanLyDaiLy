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
using QuanLyDaiLy_Source.Models;

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
            Loaded += BusinessHomePage_Loaded;
            
        }
        public static event EventHandler pageLoaded;
        private void BusinessHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[DefaultSettings.ContentFrameTitle] = "Nghiệp Vụ Đại Lý";
            pageLoaded?.Invoke(this, e);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)BusinessListView.SelectedItem;
            //Test.Text = item.Name;

            string XuatHang = PhieuXuatHangListViewItem.Name.ToString();
            string ThuTien = PhieuThuTienListViewItem.Name.ToString();
            string TiepNhan = TiepNhanDaiLyListViewItem.Name.ToString();

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
            if (item.Name.ToString() == TiepNhan)
            {
                try
                {
                    this.NavigationService.Navigate(new TiepNhanDaiLy());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
    }
}
