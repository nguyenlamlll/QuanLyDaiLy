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
            //AgencySelectComboBox.IsEnabled = false;
            


        }

        private void PhieuXuatHang_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Phiếu Xuất Hàng";
            pageLoaded?.Invoke(this, e);
        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            //MerchandiseDataGrid.Items.Add(new M);
        }

        private void SaveAndExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Save

            // Clear all input fields

            //Exit
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question,
               MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new Uri("/QuanLyDaiLy-Source;component/Windows/BusinessHomePage.xaml", UriKind.Relative));
            }
        }

        private void SaveAndContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Save

            // Clear all input fields
            MerchandiseDataGrid.Items.Clear();
            MerchandiseDataGrid.Items.Refresh();
        }


    }
}
