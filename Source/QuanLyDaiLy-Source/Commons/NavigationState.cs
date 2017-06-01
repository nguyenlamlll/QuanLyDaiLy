using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace QuanLyDaiLy_Source.Commons
{
    /// <summary>
    /// A class to pass information while navigating between pages.
    /// </summary>
    public class NavigationState : CustomContentState
    {
        /// <summary>
        /// Indicating that whether Navigation Method in MainWindow.xaml.cs should be skipped
        /// </summary>
        public bool WillNavigatingMethodOfParentsBeSkipped { get; set; }

        public override void Replay(NavigationService navigationService, NavigationMode mode) { }
    }
}
