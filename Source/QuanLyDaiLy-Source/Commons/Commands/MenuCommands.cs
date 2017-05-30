using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyDaiLy_Source.Commons.Commands
{
    /// <summary>
    /// Custom commands for MenuItem in MainWindow.
    /// </summary>
    static class MenuCommands
    {
        /// <summary>
        /// Go to HomePage.
        /// </summary>
        public static RoutedCommand TrangChu = new RoutedCommand();


        public static RoutedCommand DanhSach = new RoutedCommand();


        public static RoutedCommand BaoCao = new RoutedCommand();


        public static RoutedCommand NghiepVu = new RoutedCommand();


        public static RoutedCommand CaiDat = new RoutedCommand();
    }
}
