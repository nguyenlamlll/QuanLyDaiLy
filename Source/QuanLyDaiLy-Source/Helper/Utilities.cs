using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        /// <summary>
        /// Initialize a DateTime variable from a MonthComboBox. 
        /// The method checks using month names in Vietnamese. Eg: "Tháng Một".
        /// </summary>
        /// <param name="time"></param>
        /// <param name="cmb">A ComboBox storing all the months from January to December (in Vietnamese)</param>
        /// <returns>Return a DateTime set in current year, 1st day of the selected month in ComboBox</returns>
        public static DateTime GetDateTimeFromMonthComboBox(out DateTime time, ComboBox cmb)
        {
            ComboBoxItem selectedMonth = (ComboBoxItem)cmb.SelectedItem;
            switch (selectedMonth.Content.ToString())
            {
                case "Tháng Một":
                    {
                        time = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                    }
                case "Tháng Hai":
                    {
                        time = new DateTime(DateTime.Now.Year, 2, 1);
                        break;
                    }
                case "Tháng Ba":
                    {
                        time = new DateTime(DateTime.Now.Year, 3, 1);
                        break;
                    }
                case "Tháng Bốn":
                    {
                        time = new DateTime(DateTime.Now.Year, 4, 1);
                        break;
                    }
                case "Tháng Năm":
                    {
                        time = new DateTime(DateTime.Now.Year, 5, 1);
                        break;
                    }
                case "Tháng Sáu":
                    {
                        time = new DateTime(DateTime.Now.Year, 6, 1);
                        break;
                    }
                case "Tháng Bảy":
                    {
                        time = new DateTime(DateTime.Now.Year, 7, 1);
                        break;
                    }
                case "Tháng Tám":
                    {
                        time = new DateTime(DateTime.Now.Year, 8, 1);
                        break;
                    }
                case "Tháng Chín":
                    {
                        time = new DateTime(DateTime.Now.Year, 9, 1);
                        break;
                    }
                case "Tháng Mười":
                    {
                        time = new DateTime(DateTime.Now.Year, 10, 1);
                        break;
                    }
                case "Tháng Mười Một":
                    {
                        time = new DateTime(DateTime.Now.Year, 11, 1);
                        break;
                    }
                case "Tháng Mười Hai":
                    {
                        time = new DateTime(DateTime.Now.Year, 12, 1);
                        break;
                    }
                default:
                    {
                        time = new DateTime();
                        break;
                    }
            }
            return time;
        }
    }
}
