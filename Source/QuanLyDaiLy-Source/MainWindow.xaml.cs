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
using System.Threading;
using QuanLyDaiLy_Source.Commons;

namespace QuanLyDaiLy_Source
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// View model for Navigation ListView.
        /// </summary>
        private List<Models.MenuItem> MenuItems;

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;


            GoBackButton.Visibility = Visibility.Hidden;

            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Trang Chủ";

            ContentFrame.Navigate(new HomePage());

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
            ThayDoiQuyDinh.pageLoaded += new EventHandler(PageLoadCompleted);
        }

        [STAThread]
        /// <summary>
        /// Handle events when closing this Main Window (also, closing this application)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Let user confirm one more time before actually closing the application
            if (MessageBox.Show("Bạn có chắc chắn là muốn thoát khỏi chương trình?", "Tắt chương trình", MessageBoxButton.YesNo)
                != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                //Environment.Exit(0);

            }

        }

        public object NavigationService { get; private set; }

        /// <summary>
        /// Check either mouse is clicked or not.
        /// </summary>
        bool mouseClicked = false;

        /// <summary>
        /// Check either user pressed enter or not.
        /// </summary>
        bool IsEnterPressed = false;

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
            if (mouseClicked || IsEnterPressed)
            {
                Models.MenuItem item = (Models.MenuItem)NavigationListView.SelectedItem;
                //MenuItemCategory category = (Models.MenuItemCategory)item;
                if (item != null)
                {
                    switch (item.Category)
                    {
                        case MenuItemCategory.Homepage:
                            {
                                ContentFrame.Navigate(new HomePage());
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

        /// <summary>
        /// Change the title each time a new page is successfully loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PageLoadCompleted(object sender, EventArgs e)
        {
            string currentTitle = App.Current.Properties[Models.DefaultSettings.ContentFrameTitle].ToString();
            ContentFrameTitle.Text = currentTitle;
        }

        /// <summary>
        /// Hide this main Window until user enters correct credentials.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow.LoginSucceed += new EventHandler(Window_LoginSucceed);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = Window.GetWindow(this);
            loginWindow.Show();
        }

        /// <summary>
        /// User successfully log in. Return the main window to the surface.
        /// </summary>
        private void Window_LoginSucceed(object sender, EventArgs e)
        {
            this.Show();
        }


        /// <summary>
        /// Excute the command: Go to HomePage.
        /// </summary>
        private void CommandBinding_TrangChu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.Navigate(new HomePage());
        }

        /// <summary>
        /// Excute the command: Go to DanhSachDaiLy page.
        /// </summary>
        private void CommandBinding_DanhSach_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.Navigate(new DanhSachDaiLy());
        }

        /// <summary>
        /// Excute the command: Go to BusinessHomePage.
        /// </summary>
        private void CommandBinding_NghiepVu_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.Navigate(new BusinessHomePage());
        }

        /// <summary>
        /// Excute the command: Go to ReportHomePage.
        /// </summary>
        private void CommandBinding_BaoCao_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.Navigate(new ReportHomePage());
        }

        /// <summary>
        /// Excute the command: Go to Settings page.
        /// </summary>
        private void CommandBinding_CaiDat_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.Navigate(new ThayDoiQuyDinh());
        }


        private bool IsAltPressed = false;
        /// <summary>
        /// If user presses Alt to use shortcuts for MenuItem, underline all the command letters.
        /// If not, turn off those underlines.
        /// </summary>
        private void CheckAltCommandsInvocation()
        {
            if (IsAltPressed)
            {
                foreach (var textBlock in Helper.VisualChildren.FindVisualChildren<TextBlock>(NavigationListView))
                {
                    if (textBlock.Name == "TextBlock_CommandLetter")
                    {
                        textBlock.TextDecorations = TextDecorations.Underline;
                    }
                }
            }
            else //Alt key isn't pressed
            {
                foreach (var textBlock in Helper.VisualChildren.FindVisualChildren<TextBlock>(NavigationListView))
                {
                    if (textBlock.Name == "TextBlock_CommandLetter")
                    {
                        textBlock.TextDecorations = null;
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = (e.Key == Key.System ? e.SystemKey : e.Key);
            if (key == Key.LeftAlt || key == Key.RightAlt)
            {
                IsAltPressed = true;
            }
            if (e.Key == Key.Enter)
            {
                IsEnterPressed = true;
            }
            CheckAltCommandsInvocation();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            IsAltPressed = false;

            IsEnterPressed = false;

            CheckAltCommandsInvocation();
        }

        private void ContentFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            NavigationState state = e.ExtraData as NavigationState;
            if (state != null && state.WillNavigatingMethodOfParentsBeSkipped == true) return;
            Page page = ContentFrame.Content as Page;
            if (page == null) return;
            if (page.Title == "TiepNhanDaiLy" || page.Title == "PhieuXuatHang" || page.Title == "PhieuThuTien" ||
                page.Title == "BaoCaoCongNo" || page.Title == "BaoCaoDoanhThu" || page.Title == "DanhSachDaiLy")
                if (MessageBox.Show("Bạn có chắc chắn là muốn thoát khỏi tác vụ hiện tại?", "Thoát tác vụ hiện tại", MessageBoxButton.YesNo)
                    != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
        }
    }
}
