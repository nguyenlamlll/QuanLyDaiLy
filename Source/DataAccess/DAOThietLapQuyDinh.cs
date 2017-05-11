using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOThietLapQuyDinh
    {
        /// <summary>
        /// Singleton tech for class DAOThietLapQuyDinh
        /// </summary>
        private DAOThietLapQuyDinh() { }
        private static volatile DAOThietLapQuyDinh instance;
        public static DAOThietLapQuyDinh Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOThietLapQuyDinh();
                return DAOThietLapQuyDinh.instance;
            }
        }

        /// <summary>
        /// insert a record into table LOAIDL- increament number of LOAIDL
        /// </summary>
        /// <param name="tenloai"></param>
        /// <param name="sonotoida"></param>
        /// <returns></returns>
        public bool InsertLoaiDL(string tenloai, int sonotoida)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                LOAIDL dl = new LOAIDL();
                dl.TENLOAI = tenloai;
                dl.SONOTOIDA = sonotoida;
                db.LOAIDLs.InsertOnSubmit(dl);
                db.SubmitChanges();
                //insert suceed
                return true;
            }
        }

        /// <summary>
        /// delete a record into table LOAIDL - descreament number of LOAIDL
        /// </summary>
        /// <param name="loaidl"></param>
        /// <returns></returns>
        public bool DeleteLAOIDL(int loaidl)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.DAILies.Where(p => p.LOAIDL == loaidl).Count() <= 0)
                {
                    LOAIDL ldl = db.LOAIDLs.Where(p => p.MALOAI == loaidl).Single();
                    db.LOAIDLs.DeleteOnSubmit(ldl);
                    db.SubmitChanges();
                    //delete succeed
                    return true;
                }
                //delete false
                return false;
            }
        }

        /// <summary>
        /// Change max-number of DAILY in QUAN
        /// </summary>
        /// <param name="maquan"></param>
        /// <param name="sodailytoida"></param>
        /// <returns></returns>
        public bool ChangeNumOfDaily(int maquan, int sodailytoida)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                QUAN quan = db.QUANs.Where(p => p.MAQUAN == maquan).Single();
                quan.SODLTOIDA = sodailytoida;
                db.SubmitChanges();
                //change succeed
                return true;
            }
        }

        /// <summary>
        /// insert a record into table MATHANG- increament number of MATHANG
        /// </summary>
        /// <param name="tenhang"></param>
        /// <param name="tendvt"></param>
        /// <param name="dongia"></param>
        /// <returns></returns>
        public bool InsertMATHANG(string tenhang, int madvt, int dongia)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                MATHANG mh = new MATHANG();
                mh.TENHANG = tenhang;
                mh.MADVT = madvt;
                mh.DONGIA = dongia;
                db.MATHANGs.InsertOnSubmit(mh);
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// delete a record into table MATHANG - descreament number of MATHANG
        /// </summary>
        /// <param name="mamh"></param>
        /// <returns></returns>
        public bool DeleteMATHANG(int mamh)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.CTPXes.Where(p => p.MAHANG == mamh).Count() <= 0)
                {
                    MATHANG mh = db.MATHANGs.Where(p => p.MAHANG == mamh).Single();
                    db.MATHANGs.DeleteOnSubmit(mh);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// insert a record into table DVT- increament number of DVT
        /// </summary>
        /// <param name="tendvt"></param>
        /// <returns></returns>
        public bool InsertDVT(string tendvt)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                DVT dvt = new DVT();
                dvt.DVT1 = tendvt;
                db.DVTs.InsertOnSubmit(dvt);
                db.SubmitChanges();
                return true;
            }
        }

        /// <summary>
        /// delete a record into table DVT - descreament number of DVT
        /// </summary>
        /// <param name="madvt"></param>
        /// <returns></returns>
        public bool DeleteDVT(int madvt)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if (db.MATHANGs.Where(p => p.MADVT == madvt).Count() <= 0)
                {
                    DVT dvt = db.DVTs.Where(p => p.MADVT == madvt).Single();
                    db.DVTs.DeleteOnSubmit(dvt);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Change max-number of SONOTOIDA in LOAIDL
        /// </summary>
        /// <param name="maloaidl"></param>
        /// <param name="sonotoida"></param>
        /// <returns></returns>
        public bool ChangeMaxNumOfTienNo(int maloaidl, int sonotoida)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                LOAIDL loaidl = db.LOAIDLs.Where(p => p.MALOAI == maloaidl).Single();
                loaidl.SONOTOIDA = sonotoida;
                db.SubmitChanges();
                return true;
            }
        }
    }
}
