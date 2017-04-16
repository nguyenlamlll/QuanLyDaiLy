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
    /// Interaction logic for TiepNhanDaiLy.xaml
    /// </summary>
    public partial class TiepNhanDaiLy : Page
    {
        public TiepNhanDaiLy()
        {
            InitializeComponent();

            NameInputTextBox.GotFocus += RemoveText;
            NameInputTextBox.LostFocus += AddText;
            NameInputTextBox.LostFocus += NameInputFieldCheck;
        }

        private void NameInputFieldCheck(object sender, RoutedEventArgs e)
        {
            if (NameInputTextBox.Text == "Nhập tên đại lý cần lưu" || NameInputTextBox.Text == "")
            {
                NameInputStatus.Visibility = Visibility.Visible;
                NameInputStatus.Text = "Trường bắt buộc phải nhập!";
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            if (NameInputTextBox.Text == "")
            {
                //NameInputTextBox.FontStyle = FontStyles.Italic;
                NameInputTextBox.Text = "Nhập tên đại lý cần lưu";
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (NameInputTextBox.Text == "Nhập tên đại lý cần lưu")
            {
                NameInputTextBox.Text = "";
            }
        }
    }
}
