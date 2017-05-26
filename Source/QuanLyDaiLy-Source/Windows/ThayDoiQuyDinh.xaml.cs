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
    /// Interaction logic for ThayDoiQuyDinh.xaml
    /// </summary>
    public partial class ThayDoiQuyDinh : Page
    {
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;

        public ThayDoiQuyDinh()
        {
            InitializeComponent();
            Loaded += ThayDoiQuyDinh_Loaded;
        }

        private void ThayDoiQuyDinh_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Thay Đổi Quy Định";
            pageLoaded?.Invoke(this, e);
        }

        private void AngecyRulesToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AngecyRulesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void DonViTinhWindowButton_Click(object sender, RoutedEventArgs e)
        {
            TDQD_ThemDonViTinhWindow themDVTWindow = new TDQD_ThemDonViTinhWindow();
            themDVTWindow.Owner = Window.GetWindow(this);
            themDVTWindow.ShowDialog();
        }
    }
}
