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
        public ThayDoiQuyDinh()
        {
            InitializeComponent();
        }

        private void AngecyRulesToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AngecyRulesToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["ContentFrameTitle"] = "Thay Đổi Quy Định";
        }
    }
}
