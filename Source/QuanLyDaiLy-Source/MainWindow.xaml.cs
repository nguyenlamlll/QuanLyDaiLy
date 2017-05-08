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
            GoBackButton.Visibility = Visibility.Hidden;

            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Trang Chủ";

            ContentFrame.Navigate(new DanhSachDaiLy());
            //ContentFrame.Navigate(typeof(Windows.Page1)); //Host some placeholder page - work as a MainContents page

            MenuItems = MenuItemManager.GetMenuItems(); //ItemSource for NavigationListView
            NavigationListView.ItemsSource = MenuItems;


            BusinessHomePage.pageLoaded += new EventHandler(PageLoadCompleted);
            PhieuThuTien.pageLoaded += new EventHandler(PageLoadCompleted);
            PhieuXuatHang.pageLoaded += new EventHandler(PageLoadCompleted);
            TiepNhanDaiLy.pageLoaded += new EventHandler(PageLoadCompleted);

            ReportHomePage.pageLoaded += new EventHandler(PageLoadCompleted);
            BaoCaoCongNo.pageLoaded += new EventHandler(PageLoadCompleted);
            BaoCaoDoanhThu.pageLoaded += new EventHandler(PageLoadCompleted);

            DanhSachDaiLy.pageLoaded += new EventHandler(PageLoadCompleted);
        }
        public object NavigationService { get; private set; }


        bool mouseClicked = false;
        bool enterPressed = false;
        private void NavigationListView_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) mouseClicked = true;
        }

        private void NavigationListView_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseClicked = false;
        }

        private void NavigationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mouseClicked || enterPressed)
            {
                Models.MenuItem item = (Models.MenuItem)NavigationListView.SelectedItem;
                //MenuItemCategory category = (Models.MenuItemCategory)item;
                if (item != null)
                {
                    switch (item.Category)
                    {
                        case MenuItemCategory.Homepage:
                            {
                                ContentFrame.Navigate(new TiepNhanDaiLy());
                                break;
                            }
                        case MenuItemCategory.Lists:
                            {
                                ContentFrame.Navigate(new DanhSachDaiLy());
                                break;
                            }
                        case MenuItemCategory.Businesses:
                            {
                                ContentFrame.Navigate(new BusinessHomePage());
                                break;
                            }
                        case MenuItemCategory.Reports:
                            {
                                ContentFrame.Navigate(new ReportHomePage());
                                break;
                            }
                        case MenuItemCategory.Settings:
                            {
                                ContentFrame.Navigate(new ThayDoiQuyDinh());
                                break;
                            }
                    }
                }
            }

        }

        /// <summary>
        /// Open or close Navigation bar
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

        private void ContentFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //string currentTitle = App.Current.Properties[Models.DefaultSettings.ContentFrameTitle].ToString();
            //ContentFrameTitle.Text = currentTitle;
        }

        public void PageLoadCompleted(object sender, EventArgs e)
        {
            string currentTitle = App.Current.Properties[Models.DefaultSettings.ContentFrameTitle].ToString();
            ContentFrameTitle.Text = currentTitle;
        }
    }
}
