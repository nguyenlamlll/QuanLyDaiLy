using DAODLL;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.Loaded += LoadCurrentUser;
        }

        /// <summary>
        /// Load account information that assosicates with current working session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadCurrentUser(object sender, RoutedEventArgs e)
        {
            try
            {
                UsernameTextBlock.Text = User.Instance.Ten;
                PositionTextBlock.Text = User.Instance.Chucvu;
            }
            catch
            {

            }

            // If for some reasons we cannot load information, it'd better we hide the placeholders also.
            if (UsernameTextBlock.Text == "" || PositionTextBlock.Text == "")
            {
                InformationStackPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                InformationStackPanel.Visibility = Visibility.Visible;
            }

        }
    }
}
