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
using QuanLyDaiLy_Source.Models;
using QuanLyDaiLy_Source.Windows;


namespace QuanLyDaiLy_Source
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Models.MenuItem> MenuItems;
        public MainWindow()
        {
            InitializeComponent();
            //Grid_AdvancedList.Visibility = Visibility.Visible;
            this.Title.Text += "Phiếu Xuất Hàng";
            ContentFrame.Navigate(new Windows.PhieuXuatHang());
            //ContentFrame.Navigate(typeof(Windows.Page1)); //Host some placeholder page - work as a MainContents page

            MenuItems = MenuItemManager.GetMenuItems(); //ItemSource for NavigationListView
            NavigationListView.ItemsSource = MenuItems;



        }

        public object NavigationService { get; private set; }

        private void Button_List_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Button_List_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void NavigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.MenuItem item = (Models.MenuItem)NavigationListView.SelectedItem;
            //MenuItemCategory category = (Models.MenuItemCategory)item;
            if (item != null)
            {
                switch (item.Category)
                {
                    case MenuItemCategory.Homepage:
                        {
                            MessageBox.Show("Homepage clicked");
                            break;
                        }
                    case MenuItemCategory.Lists:
                        {
                            MessageBox.Show("Danh Sach clicked");
                            break;
                        }
                    case MenuItemCategory.Businesses:
                        {
                            ContentFrame.Navigate(new TiepNhanDaiLy());
                            //MessageBox.Show("Chinh Sua clicked");
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            GridLength length = new GridLength(0.25, GridUnitType.Star);
            if (NavigationMenuColumnDefinition.Width.IsStar)
            {
                NavigationMenuColumnDefinition.Width = new GridLength(60);
            }
            else
            {
                NavigationMenuColumnDefinition.Width = new GridLength(0.25, GridUnitType.Star);
            }

        }

        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void NavigationListView_MouseEnter(object sender, MouseEventArgs e)
        {
            Models.MenuItem item = (Models.MenuItem)NavigationListView.SelectedItem;

            if (item != null)
            {
                switch (item.Category)
                {
                    case MenuItemCategory.Homepage:
                        {
                            MessageBox.Show("Homepage hovered");
                            break;
                        }
                    case MenuItemCategory.Lists:
                        {
                            MessageBox.Show("Danh Sach hovered");
                            break;
                        }
                }
            }
        }

    }
}
