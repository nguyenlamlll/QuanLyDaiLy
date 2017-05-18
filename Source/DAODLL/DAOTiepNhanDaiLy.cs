using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOTiepNhanDaiLy
    {
        /// <summary>
        /// Init Singleton for TiepNhanDaily
        /// </summary>
        private DAOTiepNhanDaiLy() { }
        private static volatile DAOTiepNhanDaiLy instance;
        public static DAOTiepNhanDaiLy Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOTiepNhanDaiLy();
                return DAOTiepNhanDaiLy.instance;
            }


        }

        /// <summary>
        /// Insert Daily into database
        /// </summary>
        /// <param name="dl"></param>
        /// <returns></returns>
        public bool Insert(DAILY dl)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.DAILies.Where(p => p.MADL == dl.MADL).Count() > 0)
                    return false;
                if (db.DAILies.Where(p => p.TENDL == dl.TENDL).Count() > 0)
                    return false;

                DAILY DL = new DAILY();
                DL.MADL = dl.MADL;
                DL.TENDL = dl.TENDL;
                DL.DIENTHOAI = dl.DIENTHOAI;
                DL.DIACHI = dl.DIACHI;
                DL.NGAYTIEPNHAN = dl.NGAYTIEPNHAN;
                DL.MAQUAN = dl.MAQUAN;
                DL.LOAIDL = dl.LOAIDL;
                db.DAILies.InsertOnSubmit(DL);
                db.SubmitChanges();
                return true;

            }
        }
        /// <summary>
        /// Delete DAILy
        /// </summary>
        /// <param name="ID"></param>
        public bool Delete(int madl)

        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.PHIEUXUATHANGs.Where(p => p.MADL == madl).Count() > 0)
                    return false;
                DAILY dl = db.DAILies.Single(p=>p.MADL==madl);
                db.DAILies.DeleteOnSubmit(dl);
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// Update DAIly
        /// </summary>
        /// <param name="dl"></param>
        /// <returns></returns>
        public bool Update(DAILY dl)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.DAILies.Where(p => p.MADL == dl.MADL).Count() <= 0)
                {
                    return false;
                }
                DAILY DL = db.DAILies.Where(p => p.MADL == dl.MADL).SingleOrDefault();
                DL.TENDL = dl.TENDL;
                DL.DIENTHOAI = dl.DIENTHOAI;
                DL.DIACHI = dl.DIACHI;
                DL.NGAYTIEPNHAN = dl.NGAYTIEPNHAN;
                DL.MAQUAN = dl.MAQUAN;
                DL.LOAIDL = dl.LOAIDL;
                try
                {
                    db.SubmitChanges();
                }
                catch { return false; }
                return true;
            }

        }
        /// <summary>
        /// Search DAILY from database and display it to GUI
        /// Load if DAILY search condition equal empty string 
        /// </summary>
        public List<vwDAILY_LOAIDL_QUAN> Search(string tenloai = "" , string tenquan = "" ,int thang = 0 )
        {
            List<vwDAILY_LOAIDL_QUAN> li = new List<vwDAILY_LOAIDL_QUAN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.vwDAILY_LOAIDL_QUANs.Where(p => p.TENLOAI.Contains(tenloai)
                    || p.TENQUAN.Contains(tenquan)
                    || p.NGAYTIEPNHAN.Value.Month == thang)).ToList();
            }
            return li;
        }

    }
}
