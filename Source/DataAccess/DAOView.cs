using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOView
    {
        /// <summary>
        /// Singleton tech for class DAOView
        /// </summary>
        private DAOView() { }
        private static volatile DAOView instance;
        public static DAOView Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOView();
                return DAOView.instance;
            }
        }

        /// <summary>
        /// Get all DaiLy's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDaiLyName()
        {
            List<string> li = new List<string>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.DAILies.Select(p => p.TENDL)).ToList();
                return li;
            }
            return null;
        }

        /// <summary>
        /// Get all QUAN's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllQuanName()
        {
            List<string> li = new List<string>();
            using(QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.QUANs.Select(p => p.TENQUAN)).ToList();
                return li;
            }
            return null;
        }

        /// <summary>
        /// Get all LAOIDAILY's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllLoaiDLName()
        {
            List<string> li = new List<string>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.LOAIDLs.Select(p => p.TENLOAI)).ToList();
                return li;
            }
            return null;
        }
        /*
        /// <summary>
        /// Get all DVT's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllQuanName()
        {
            List<string> li = new List<string>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.DVTs.Select(p => p.DVT1)).ToList();
                return li;
            }
            return null;
        }
        */
    }
}
