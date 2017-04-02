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

            /*
            Utilities.SetAccentColor(Rectangle_NavigationFill_1);
            Utilities.SetAccentColor(Rectangle_NavigationFill_2);
            Utilities.SetAccentColor(Rectangle_NavigationFill_3);
            */

        }

        public object NavigationService { get; private set; }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();

        }

        private void TextBlock_HomePage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
