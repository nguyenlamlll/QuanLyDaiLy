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
    /// Interaction logic for BaoCaoCongNo.xaml
    /// </summary>
    public partial class BaoCaoCongNo : Page
    {
        public BaoCaoCongNo()
        {
            InitializeComponent();
            Loaded += BaoCaoCongNo_Loaded;

        }
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        private void BaoCaoCongNo_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Báo Cáo Công Nợ";
            pageLoaded?.Invoke(this, e);
        }
    }
}
