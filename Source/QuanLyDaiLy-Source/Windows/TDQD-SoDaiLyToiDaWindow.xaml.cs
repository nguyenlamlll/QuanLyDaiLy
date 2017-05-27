using QuanLyDaiLy_Source.Commons.BusinessLogic;
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
using System.Windows.Shapes;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for TDQD_SoDaiLyToiDaWindow.xaml
    /// </summary>
    public partial class TDQD_SoDaiLyToiDaWindow : Window
    {
        ObservableCollection<DAODLL.QUAN> DataGridItemms = new ObservableCollection<DAODLL.QUAN>();
        public TDQD_SoDaiLyToiDaWindow()
        {
            InitializeComponent();
            DataGridItemms = ViewManager.Instance.GetAllQuan();
            DaiLyToiDaDataGrid.ItemsSource = DataGridItemms;
            cbBoxQuan.ItemsSource = ViewManager.Instance.GetAllQuan();

            
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
