using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    using DAODLL;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace BUS
    {
        public class ViewManager
        {
            /// <summary>
            /// Init Singleton for BUSView
            /// </summary>
            private ViewManager() { }
            public static volatile ViewManager instance;
            public static ViewManager Instance
            {
                get
                {
                    if (instance == null)
                        instance = new ViewManager();
                    return ViewManager.instance;
                }
            }


            public ObservableCollection<DAILY> GetAllDaiLy()
            {
                return DAOView.Instance.GetAllDaiLy();
            }

            public ObservableCollection<QUAN> GetAllQuan()
            {
                return DAOView.Instance.GetAllQuan();
            }

            public ObservableCollection<LOAIDL> GetAllLoaiDL()
            {
                return DAOView.Instance.GetAllLoaiDL();
            }

            public ObservableCollection<DVT> GetAllDVT()
            {
                return DAOView.Instance.GetAllDVT();
            }

            public ObservableCollection<CHUCVU> GetAllCV()
            {
                return DAOView.Instance.GetAllCV();
            }
        }
    }

}
