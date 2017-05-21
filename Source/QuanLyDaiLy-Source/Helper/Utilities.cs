using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyDaiLy_Source.Helper
{
    public static partial class Utilities
    {
        static public void SetAccentColor(System.Windows.Window win)
        {
            win.Background = SystemParameters.WindowGlassBrush;
        }
        static public void SetAccentColor(System.Windows.Shapes.Rectangle rect)
        {
            rect.Fill = SystemParameters.WindowGlassBrush;
        }
        static public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
