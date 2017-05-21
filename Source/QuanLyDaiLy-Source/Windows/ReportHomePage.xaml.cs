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
    /// Interaction logic for ReportHomePage.xaml
    /// </summary>
    public partial class ReportHomePage : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;

        public ReportHomePage()
        {
            InitializeComponent();
            Loaded += ReportHomePage_Loaded;
        }

        private void ReportHomePage_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Lập Báo Cáo";
            pageLoaded?.Invoke(this, e);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)BusinessListView.SelectedItem;
            if (item == null) return;

            string DoanhSo = SalesListViewItem.Name.ToString();
            string ThuTien = DebtListViewItem.Name.ToString();
            /*
            if (item.Name.ToString() == DoanhSo)
            {
                try
                {
                    this.NavigationService.Navigate(new BaoCaoDoanhThu());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            */

            if (item.Name.ToString() == ThuTien)
            {
                try
                {
                    this.NavigationService.Navigate(new BaoCaoCongNo());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void SalesListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BaoCaoDoanhThu());
            SalesListViewItem.IsSelected = false;
        }
    }
}
