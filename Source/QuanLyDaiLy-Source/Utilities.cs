using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyDaiLy_Source
{
    public partial class Utilities
    {
        private bool isUtilityUsed = false;
        public Utilities()
        {
            isUtilityUsed = true;
        }
        static public void SetAccentColor(System.Windows.Window win)
        {
            win.Background = SystemParameters.WindowGlassBrush;
        }
        static public void SetAccentColor(System.Windows.Shapes.Rectangle rect)
        {
            rect.Fill = SystemParameters.WindowGlassBrush;
        }
    }
}
