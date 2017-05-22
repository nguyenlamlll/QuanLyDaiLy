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
        /// Get All DaiLy with details about LoaiDaiLy
        /// </summary>
        /// <param name="maQuan">Ma Quan can lay tat ca dai ly</param>
        /// <returns>A view table of DaiLy table and LoaiDaiLy table</returns>
        public ObservableCollection<vwDAILY_LOAIDL> GetAllDaiLyWithLoaiDaiLy(int maQuan)
        {
            ObservableCollection<vwDAILY_LOAIDL> list = new ObservableCollection<DAODLL.vwDAILY_LOAIDL>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var query = (from records in db.vwDAILY_LOAIDLs
                             where records.MAQUAN == maQuan
                             select records);
                foreach (vwDAILY_LOAIDL item in query)
                {
                    list.Add(item);
                }
                return list;
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
        /// Get All Agencies of 1 District
        /// </summary>
        /// <param name="maQuan"></param>
        /// <returns></returns>
        public ObservableCollection<DAILY> GetAllDaiLy(int maQuan)
        {
            ObservableCollection<DAILY> li = new ObservableCollection<DAILY>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.DAILies
                             where records.MAQUAN == maQuan
                             select records);
                foreach (DAILY dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }

        /// <summary>
        /// Return a DAILY of MaDL parameter.
        /// </summary>
        /// <param name="MaDL"></param>
        /// <returns></returns>
        public DAILY GetDaiLy(int maDL)
        {
            DAILY daiLy = new DAILY();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                daiLy = (from records in db.DAILies
                         where records.MADL == maDL
                         select records).Single();
                return daiLy;
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

        public int GetMaHang(string ten)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                int maHang = (from records in db.MATHANGs
                             where records.TENHANG == ten
                             select records.MAHANG).Single();
                return maHang;
            }
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

        /// <summary>
        /// Lay so dai ly toi da cua mot quan.
        /// </summary>
        /// <param name="maQuan">So quan can xem so dai ly toi da</param>
        /// <returns></returns>
        public int GetSoDlToiDa(int maQuan)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var max = (from records in db.QUANs
                           where records.MAQUAN == maQuan
                           select records).Single();
                return max.SODLTOIDA.Value;
            }
        }

        /// <summary>
        /// Dem so luong dai ly hien co trong quan, voi tinh trang dang hoat dong (TINHTRANG = 1)
        /// </summary>
        /// <param name="maQuan"></param>
        /// <returns></returns>
        public int CountSoDaiLy(int maQuan)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                int num = (from daily in db.DAILies
                           where daily.MAQUAN == maQuan && daily.TINHTRANG == 1
                           select daily).Count();
                return num;
            }
        }

        /// <summary>
        /// Lấy số nợ tối đa của một loại đại lý nào đó từ bảng LOAIDL.
        /// </summary>
        /// <param name="maLoai">Mã loại đại lý cần lấy số nợ tối đa (trong bảng LOAIDL).</param>
        /// <returns>Số nợ tối đa của loại đại lý truyền vào.</returns>
        public decimal GetSoNoDaiLy(int maLoai)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                decimal soNO = (from records in db.LOAIDLs
                            where records.MALOAI == maLoai
                            select records.SONOTOIDA.Value).Single();
                return soNO;
            }

        }

        /// <summary>
        /// Get All Agencies of 1 District
        /// </summary>
        /// <param name="maQuan"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> GetMultipleByQuan(int maQuan)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> li = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.vwDAILY_LOAIDL_QUANs
                             where records.MAQUAN == maQuan
                             select records);
                foreach (vwDAILY_LOAIDL_QUAN dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }

        /// <summary>
        /// Get All Agencies according Loai
        /// </summary>
        /// <param name="maLoai"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> GetMultipleByLoai(int maLoai)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> li = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.vwDAILY_LOAIDL_QUANs
                             where records.MALOAI == maLoai
                             select records);
                foreach (vwDAILY_LOAIDL_QUAN dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }

        #region Tìm đại lí theo tên
        /// <summary>
        /// Get All Agencies according Quan's name
        /// </summary>
        /// <param name="maLoai"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByQuan(string name)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> li = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.vwDAILY_LOAIDL_QUANs
                             where records.TENQUAN.Contains(name)
                             select records);
                foreach (vwDAILY_LOAIDL_QUAN dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }
        /// <summary>
        /// Get All Agencies according Quan's name
        /// </summary>
        /// <param name="maLoai"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByLoai(string name)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> li = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.vwDAILY_LOAIDL_QUANs
                             where records.TENLOAI.Contains(name)
                             select records);
                foreach (vwDAILY_LOAIDL_QUAN dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }
        /// <summary>
        /// Get All Agencies according Loai's name
        /// </summary>
        /// <param name="maLoai"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByDaiLy(string name)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> li = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var daiLy = (from records in db.vwDAILY_LOAIDL_QUANs
                             where records.TENDL.Contains(name)
                             select records);
                foreach (vwDAILY_LOAIDL_QUAN dl in daiLy)
                {
                    li.Add(dl);
                }
                return li;
            }
        }
        #endregion
    }
}
