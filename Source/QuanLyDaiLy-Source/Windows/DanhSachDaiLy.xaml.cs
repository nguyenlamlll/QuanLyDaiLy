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
    /// Interaction logic for DanhSachDaiLy.xaml
    /// </summary>
    public partial class DanhSachDaiLy : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;

        public DanhSachDaiLy()
        {
            InitializeComponent();
            Loaded += DanhSachDaiLy_Loaded;
           
            AdvancededSearch.Visibility = Visibility.Collapsed;
        }

        private void DanhSachDaiLy_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Danh Sách Đại Lý";
            pageLoaded?.Invoke(this, e); //if (pageLoaded != null)
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AdvancededSearch.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            AdvancededSearch.Visibility = Visibility.Collapsed;
        }

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AgencyListView.SelectedIndex == -1) return;
            int index = AgencyListView.SelectedIndex;
        }
    }
}
