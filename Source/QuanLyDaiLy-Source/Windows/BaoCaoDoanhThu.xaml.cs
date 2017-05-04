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
    /// Interaction logic for BaoCaoDoanhThu.xaml
    /// </summary>
    public partial class BaoCaoDoanhThu : Page
    {
        public BaoCaoDoanhThu()
        {
            InitializeComponent();
            App.Current.Properties["ContentFrameTitle"] = "Báo Cáo Doanh Số";
        }
    }
}
