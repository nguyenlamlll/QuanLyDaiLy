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
using DAODLL;
using QuanLyDaiLy_Source.Commons.BusinessLogic;

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

            // Query-related methods
            TypeComboBox.SelectionChanged += SearchQueryChanged;
            DistrictComboBox.SelectionChanged += SearchQueryChanged;
            MonthComboBox.SelectionChanged += SearchQueryChanged;
        }

        /// <summary>
        /// Validate query options before excuting it.
        /// </summary>
        /// <returns>Return true if the query can be excuted properly.</returns>
        private bool IsQuerySuitable()
        {
            if (TypeComboBox.SelectedIndex == -1) return false;
            if (DistrictComboBox.SelectedIndex == -1) return false;
            if (MonthComboBox.SelectedIndex == -1) return false;
            return true;
        }

        private void SearchQueryChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsQuerySuitable())
            {
                try
                {
                    ComboBoxItem selected = (ComboBoxItem)MonthComboBox.SelectedItem;
                    //if (selected.Name == "comboBoxItem_January")
                }
                catch  (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Could not excute queries");
                }
            }
        }

        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        private void BaoCaoCongNo_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Báo Cáo Công Nợ";
            pageLoaded?.Invoke(this, e);

            ObservableCollection<LOAIDL> LoaiDaiLyList = new ObservableCollection<LOAIDL>();
            LoaiDaiLyList = ViewManager.Instance.GetAllLoaiDL();
            LoaiDaiLyList.Add(new DAODLL.LOAIDL() { MALOAI = 0, TENLOAI = "Tất Cả" });
            LoaiDaiLyList.Move(LoaiDaiLyList.Count - 1, 0);
            TypeComboBox.ItemsSource = LoaiDaiLyList;

            ObservableCollection<QUAN> QuanList = new ObservableCollection<QUAN>();
            QuanList = ViewManager.Instance.GetAllQuan();
            QuanList.Add(new QUAN() { MAQUAN = 0, TENQUAN = "Tất Cả" });
            QuanList.Move(LoaiDaiLyList.Count - 1, 0);
            DistrictComboBox.ItemsSource = QuanList;

        }
    }

    /// <summary>
    /// Item template for Datagrid of BaoCaoCongNo
    /// </summary>
    public class BaoCaoCongNoItem
    {
        public string TenDaiLy { get; set; }
        public decimal NoDau { get; set; }
        public decimal NoPhatSinh { get; set; }
        public decimal NoCuoi { get; set; }
    }
}
