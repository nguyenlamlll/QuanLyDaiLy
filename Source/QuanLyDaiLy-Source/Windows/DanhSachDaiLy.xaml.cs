using QuanLyDaiLy_Source.Commons.BusinessLogic;
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

        private void TargetSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((TargetSearchComboBox.SelectedItem) as ComboBoxItem).Content.ToString() == "Loại")
            {
                SearchComboBox.ItemsSource = ViewManager.Instance.GetAllLoaiDL();
                SearchComboBox.DisplayMemberPath = "TENLOAI";
                SearchComboBox.SelectedValuePath = "MALOAI";
            }
            else
            {
                SearchComboBox.ItemsSource = ViewManager.Instance.GetAllQuan();
                SearchComboBox.DisplayMemberPath = "TENQUAN";
                SearchComboBox.SelectedValuePath = "MAQUAN";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AdvancedSearchKeyComboBox.Text == "- Chọn khóa -")
            {
                MessageBox.Show("Vui lòng chọn khóa");
            }
            else
            {
                int checkNo = -1;
                if (rbCoNo.IsChecked == true)
                    checkNo = 1;
                if (rbKoNo.IsChecked == true)
                    checkNo = 0;
                if (((AdvancedSearchKeyComboBox.SelectedItem) as ComboBoxItem).Content.ToString() == "Quận")
                {
                    AgencyListView.ItemsSource = ViewManager.Instance.SearchByQuan(txbSearch.Text, checkNo);
                }
                else if (((AdvancedSearchKeyComboBox.SelectedItem) as ComboBoxItem).Content.ToString() == "Loại Đại Lý")
                {
                    AgencyListView.ItemsSource = ViewManager.Instance.SearchByLoai(txbSearch.Text, checkNo);
                }
                else if (((AdvancedSearchKeyComboBox.SelectedItem) as ComboBoxItem).Content.ToString() == "Tên Đại Lý")
                {
                    AgencyListView.ItemsSource = ViewManager.Instance.SearchByDaiLy(txbSearch.Text, checkNo);
                }
            }
            if (rbCoNo.IsChecked == true)
                rbCoNo.IsChecked = false;
            if (rbKoNo.IsChecked == true)
                rbKoNo.IsChecked = false;
        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchComboBox.SelectedValue != null)
            {
                if (((TargetSearchComboBox.SelectedItem) as ComboBoxItem).Content.ToString() == "Quận")
                {
                    AgencyListView.ItemsSource = ViewManager.Instance.GetAllDaiLyWithLoaiDaiLy((int)SearchComboBox.SelectedValue);
                    
                }
                else
                {
                    AgencyListView.ItemsSource = ViewManager.Instance.GetAllDaiLyAccordingLoai((int)SearchComboBox.SelectedValue);
                }
            }
            else AgencyListView.ItemsSource = null;
        }
    }
}
