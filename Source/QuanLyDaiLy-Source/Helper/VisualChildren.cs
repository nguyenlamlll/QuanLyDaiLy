using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QuanLyDaiLy_Source.Helper
{
    /// <summary>
    /// Provide methods to help with finding children elements.
    /// </summary>
    public static class VisualChildren
    {
        /// <summary>
        /// Find all visual children of a DepedencyObject.
        /// </summary>
        /// <typeparam name="T">Type of visual children you want to find.</typeparam>
        /// <param name="depObj">The parent Dependency Object.</param>
        /// <returns>Return a IEnumerable list (to use in foreach loops).</returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
    }
}
