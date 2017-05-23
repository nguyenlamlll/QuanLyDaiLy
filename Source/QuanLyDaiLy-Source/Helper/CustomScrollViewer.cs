using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLy_Source.Helper
{
    public class CustomScrollViewer : ScrollViewer
    {
        static CustomScrollViewer()
        {

        }

        private bool canScrollUp
        {
            get
            {
                return this.ScrollableHeight > 0 && this.VerticalOffset > 0;
            }
        }

        private bool canScrollDown
        {
            get
            {
                return this.ScrollableHeight > 0 &&
                  this.VerticalOffset + this.ViewportHeight < this.ExtentHeight;
            }
        }

        private bool canScrollLeft
        {
            get
            {
                return this.ScrollableWidth > 0 && this.HorizontalOffset > 0;
            }
        }

        private bool canScrollRight
        {
            get
            {
                return this.ScrollableWidth > 0 &&
                this.HorizontalOffset + this.ViewportWidth < this.ExtentWidth;
            }
        }

        public bool CanScroll
        {
            get
            {
                if (canScrollUp || canScrollDown || canScrollLeft || canScrollRight)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                if (!canScrollUp)
                {
                    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Down)
            {
                if (!canScrollDown)
                {
                    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Left)
            {
                if (!canScrollLeft)
                {
                    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                    e.Handled = true;
                }
            }
            if (e.Key == Key.Right)
            {
                if (!canScrollRight)
                {
                    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Right));
                    e.Handled = true;
                }
            }
        }
    }
}
