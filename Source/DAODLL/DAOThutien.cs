using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOThutien
    {
        /// <summary>
        /// Singleton tech for class DAOTiepNhanDaiLy
        /// </summary>
        private DAOThutien() { }
        private static volatile DAOThutien instance;
        public static DAOThutien Instance
        {
            get 
            {
                if (instance == null)
                    instance = new DAOThutien();
                return DAOThutien.instance; 
            }
        }

        /// <summary>
        /// Search PHIEUTHUTIEN from database and display it to GUI
        /// Load PHIEUTHUTIEN if search condition equal empty string 
        /// </summary>
        public List<PHIEUTHUTIEN> Search(string tendaily="", string tennhanvien="")
        {
            List<PHIEUTHUTIEN> li = new List<PHIEUTHUTIEN>();
            using(QLDLDataContext db = new QLDLDataContext())
            {
                li = (from vwphieuthu in db.vwDAILY_PHIEUTHUTIEN_NHANVIENs
                      where vwphieuthu.TENDL.Contains(tendaily) && vwphieuthu.TENNV.Contains(tennhanvien)
                      select new PHIEUTHUTIEN
                      {
                          MAPHIEU = vwphieuthu.MAPHIEU,
                          MADL = vwphieuthu.MADL,
                          NGAYTHUTIEN = vwphieuthu.NGAYTHUTIEN,
                          SOTIEN = vwphieuthu.SOTIEN,
                          NGUOITHU = vwphieuthu.NGUOITHU
                      }
                      ).ToList();
            }
            return li;
        }

        /// <summary>
        /// Insert one PhieuThuTien into database and update SoNo of DaiLy,
        /// </summary>
        /// <param name="tendaily"></param>
        /// <param name="dienthoai"></param>
        /// <param name="diachi"></param>
        /// <param name="sotien"></param>
        /// <param name="ngaythu"></param>
        /// <returns></returns>
        public bool Insert(string tendaily, string dienthoai, string diachi, decimal? sotien, DateTime? ngaythu)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                PHIEUTHUTIEN ptt = new PHIEUTHUTIEN();
                //check if dl exist or not
                DAILY dl = db.DAILies.Single(p => p.TENDL == tendaily 
                    && p.DIENTHOAI == dienthoai 
                    && p.DIACHI == diachi);
                if (dl == null)
                    //dl doesn't exist
                    return false;
                //dl exist
                ptt.MADL = dl.MADL;
                ptt.NGAYTHUTIEN = ngaythu;
                ptt.SOTIEN = sotien;
                ptt.NGUOITHU = User.Instance.Manv;

                dl.SONO = dl.SONO - ptt.SOTIEN;
                
                //insert phieuthutien into database
                db.PHIEUTHUTIENs.InsertOnSubmit(ptt);
                db.SubmitChanges();
                //insert succeed
                return true;   
            }
        }

        /// <summary>
        /// delete a row of table PHIEUTHUTIEN in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                PHIEUTHUTIEN phieuthutien = db.PHIEUTHUTIENs.Where(p => p.MAPHIEU == id).Single();
                db.PHIEUTHUTIENs.DeleteOnSubmit(phieuthutien);
                db.SubmitChanges();
                //delete sucees;
                return true;
            }
            //delete fail
        }

        /// <summary>
        /// Updelete a row in table PHIEUTHUTIEN
        /// </summary>
        /// <param name="ptt"></param>
        /// <returns></returns>
        public bool Update(int maphieuthutien, string tendaily, string dienthoai, string diachi, int sotien, DateTime ngaythu)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                PHIEUTHUTIEN ptt = db.PHIEUTHUTIENs.Where(p => p.MAPHIEU == maphieuthutien).Single();
                //check if dl exist or not
                DAILY dl = db.DAILies.Single(p => p.TENDL == tendaily
                    && p.DIENTHOAI == dienthoai
                    && p.DIACHI == diachi);
                if (dl == null)
                    //dl doesn't exist
                    return false;
                //dl exist
                ptt.MADL = dl.MADL;
                ptt.NGAYTHUTIEN = ngaythu;
                ptt.SOTIEN = sotien;
                //update phieuthutien into database
                db.SubmitChanges();
                //update succeed
                return true;
            }
        }


        /// <summary>
        /// Get all PhieuThuTien before a month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public ObservableCollection<PHIEUTHUTIEN> GetPhieuThuTienBeforeAMonth(DateTime date)
        {
            ObservableCollection<PHIEUTHUTIEN> list = new ObservableCollection<PHIEUTHUTIEN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var query = (from phieu in db.PHIEUTHUTIENs
                             where phieu.NGAYTHUTIEN.Value.Month <= date.Month
                             && phieu.NGAYTHUTIEN.Value.Year <= date.Year
                             select phieu);
                foreach (var item in query)
                {
                    list.Add(query as PHIEUTHUTIEN);
                }
                return list;
            }
        }
    }
}
