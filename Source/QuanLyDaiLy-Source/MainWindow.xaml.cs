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



namespace QuanLyDaiLy_Source
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Grid_AdvancedList.Visibility = Visibility.Visible;
            ContentFrame.Navigate(new Windows.MainContent());
            //ContentFrame.Navigate(typeof(Windows.Page1)); //Host some placeholder page - work as a MainContents page
            /*
            Utilities.SetAccentColor(Rectangle_NavigationFill_1);
            Utilities.SetAccentColor(Rectangle_NavigationFill_2);
            Utilities.SetAccentColor(Rectangle_NavigationFill_3);
            */

        }

        public object NavigationService { get; private set; }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Windows.Page1());
        }

        private void TextBlock_HomePage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Navigate(new Windows.Page1());
        }

        private void Button_List_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (Grid_AdvancedList.Visibility == Visibility.Collapsed)
                    Grid_AdvancedList.Visibility = Visibility.Visible;
                if (Grid_AdvancedList.Visibility == Visibility.Visible)
                    Grid_AdvancedList.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi" + ex.Message);
            }
            
        }

        private void Button_List_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                if (Grid_AdvancedList.Visibility == Visibility.Collapsed)
                    Grid_AdvancedList.Visibility = Visibility.Visible;
                if (Grid_AdvancedList.Visibility == Visibility.Visible)
                    Grid_AdvancedList.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi" + ex.Message);
            }
        }
    }
}
