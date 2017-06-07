using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOQLNhanVien
    {
        /// <summary>
        /// Init Singleton for DAOQLNhanVien
        /// </summary>
        private DAOQLNhanVien() { }
        private static volatile DAOQLNhanVien instance;
        public static DAOQLNhanVien Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAOQLNhanVien();
                return DAOQLNhanVien.instance;
            }
        }

        /// <summary>
        /// Insert Nhanvien into database
        /// </summary>
        /// <param name="dl"></param>
        /// <returns></returns>
        public bool Insert(string ten, DateTime ngay, string dc, int macv, string acc, string pass)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                NHANVIEN nv = new NHANVIEN();
                nv.TENNV = ten;
                nv.NGAYSINH = ngay;
                nv.DIACHI = dc;
                nv.MACHUCVU = macv;
                db.NHANVIENs.InsertOnSubmit(nv);
                db.SubmitChanges();

                TAIKHOAN tk = new TAIKHOAN();
                tk.TENDANGNHAP = acc;
                tk.PASSWORD = pass;
                tk.MANV = db.NHANVIENs.Single(p => p.TENNV == ten && p.NGAYSINH == ngay && p.MACHUCVU == macv).MANV;
                db.TAIKHOANs.InsertOnSubmit(tk);
                db.SubmitChanges();
                return true;
            }

        }

        /// <summary>
        /// Delete Nhan vien
        /// </summary>
        /// <param name="ID"></param>
        public bool Delete(int manv)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if ((db.PHIEUXUATHANGs.Where(p => p.NGUOIXUAT == manv).Count() > 0)
                    || (db.PHIEUTHUTIENs.Where(p => p.NGUOITHU == manv).Count() > 0))
                    return false;
                else
                {
                    List<TAIKHOAN> li = db.TAIKHOANs.Where(p => p.MANV == manv).ToList();
                    db.TAIKHOANs.DeleteAllOnSubmit(li);
                    db.SubmitChanges();

                    NHANVIEN nv = db.NHANVIENs.Single(p => p.MANV == manv);
                    db.NHANVIENs.DeleteOnSubmit(nv);
                    db.SubmitChanges();
                    return true;
                }
            }
        }

        /// <summary>
        /// Update NHANVIEN
        /// </summary>
        /// <param name="dl"></param>
        /// <returns></returns>
        public bool Update(NHANVIEN nhanvien)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                NHANVIEN nv = db.NHANVIENs.Where(p => p.MANV == nhanvien.MANV).Single();
                nv.TENNV = nhanvien.TENNV;
                nv.NGAYSINH = nhanvien.NGAYSINH;
                nv.DIACHI = nhanvien.DIACHI;
                nv.MACHUCVU = nhanvien.MACHUCVU;
                db.SubmitChanges();
                return true;
            }


        }
        /// <summary>
        /// Search NHan vien from database and display it to GUI
        /// Load if nhanvien's search condition equal empty string 
        /// </summary>
        public ObservableCollection<NHANVIEN> Search(int thang, int macv, string ten, string dc)
        {
            ObservableCollection<NHANVIEN> li = new ObservableCollection<NHANVIEN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var l = db.NHANVIENs.Where(p => p.MACHUCVU == macv && p.TENNV.Contains(ten)
                      && p.NGAYSINH.Value.Month == thang && p.DIACHI.Contains(dc));
                foreach (var item in l)
                {
                    li.Add(item as NHANVIEN);
                }
                return li;
            }

        }

        /// <summary>
        /// Load all records from NhanVien table.
        /// </summary>
        /// <returns>Return all records of NhanVien table in a observable collection.</returns>
        public ObservableCollection<NHANVIEN> GetAll()
        {
            ObservableCollection<NHANVIEN> list = new ObservableCollection<NHANVIEN>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                var nhanVien = (from records in db.NHANVIENs
                                select records);
                foreach (var item in nhanVien)
                {
                    list.Add(item);
                }
                return list;
            }
        }

        public string GetChucVu(int maCV)
        {
            string chucVu = string.Empty;
            using (QLDLDataContext db = new QLDLDataContext())
            {
                chucVu = (from positions in db.CHUCVUs
                          where positions.MACHUCVU == maCV
                          select positions.TENCHUCVU).Single();
                return chucVu;
            }
        }
    }
}
