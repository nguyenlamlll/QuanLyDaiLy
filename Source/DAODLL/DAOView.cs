using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static volatile DAOView instance;
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
        public ObservableCollection<DAILY> GetAllDaiLy()
        {
            ObservableCollection<DAILY> li = new ObservableCollection<DAILY>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.DAILies.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as DAILY);
                }
                return li;
            }
        }

        /// <summary>
        /// Get all QUAN's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<QUAN> GetAllQuan()
        {
            ObservableCollection<QUAN> li = new ObservableCollection<QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.QUANs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as QUAN);
                }
                return li;
            }
        }

        /// <summary>
        /// Get all LAOIDAILY's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<LOAIDL> GetAllLoaiDL()
        {
            ObservableCollection<LOAIDL> li = new ObservableCollection<LOAIDL>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.LOAIDLs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as LOAIDL);
                }
                return li;
            }

        }

        /// <summary>
        /// Get all DVT's name and show upto ComboBox
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<DVT> GetAllDVT()
        {
            ObservableCollection<DVT> li = new ObservableCollection<DVT>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.DVTs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as DVT);
                }
                return li;
            }

        }


        /// <summary>
        /// get all CHUC VU
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<CHUCVU> GetAllCV()
        {
            ObservableCollection<CHUCVU> li = new ObservableCollection<CHUCVU>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.CHUCVUs.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as CHUCVU);
                }
                return li;
            }
        }

        public ObservableCollection<CTPX> GetAllCTPX()
        {
            ObservableCollection<CTPX> li = new ObservableCollection<CTPX>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.CTPXes.Select(p => p);
                foreach (var item in l)
                {
                    li.Add(item as CTPX);
                }
                return li;
            }
        }

        public ObservableCollection<MATHANG> GetAllMatHang()
        {
            ObservableCollection<MATHANG> matHang = new ObservableCollection<MATHANG>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.MATHANGs.Select(p => p);
                foreach (var item in l)
                {
                    matHang.Add(item as MATHANG);
                }
            }
            return matHang;
        }


        /// <summary>
        /// Get unit of a product, using its ID
        /// </summary>
        /// <param name="maHang"></param>
        /// <returns>Return the display name of that unit</returns>
        public string GetDonViTinh(int maHang)
        {
            string donVi = null;
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var product = (from products in db.MATHANGs
                                 where products.MAHANG == maHang
                                 select products).Single();
                donVi = (from records in db.DVTs
                         where records.MADVT == product.MADVT
                         select records.DVT1).Single();
                return donVi;
            }
        }

        public decimal GetDonGia(int maHang)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var product = (from products in db.MATHANGs
                               where products.MAHANG == maHang
                               select products).Single();
                return product.DONGIA.Value;
            }
        }

        public QUAN GetQuan(int maQuan)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var district = (from districts in db.QUANs
                                where districts.MAQUAN == maQuan
                                select districts).Single();
                return district;
            }
        }
    }
}
