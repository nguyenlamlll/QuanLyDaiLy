using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuanLyDaiLy_Source.Helper
{
    /// <summary>
    /// Provide helps when checking common WPF's controls
    /// </summary>
    public static class FieldChecker
    {
        public static bool IsTextBoxFilled(TextBox tb)
        {
            if (tb.Text == null || tb.Text == "")
            {
                return false;
            }
            return true;
        }

        public static bool IsComboBoxSelected(ComboBox box)
        {
            if (box.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }
    }
}
