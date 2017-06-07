using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAODLL;
using System.Collections.ObjectModel;

namespace EmployeeManagerQuanLyDaiLy_Source.Models.BusinessLogic
{
    public class EmployeeManager
    {
        /// <summary>
        /// Init Singleton for BUSQLNhanVien
        /// </summary>
        private EmployeeManager() { }
        public static volatile EmployeeManager instance;
        public static EmployeeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EmployeeManager();
                return EmployeeManager.instance;
            }
        }

        public bool Insert(string ten, DateTime ngay, string dc, int macv, string acc, string pass)
        {
            if (ten == "" || dc == "")
                return false;
            if (acc == "") acc = "admin";
            if (pass == "") pass = "admin";
            return DAOQLNhanVien.Instance.Insert(ten, ngay, dc, macv, acc, pass);
        }


        public bool Delete(int manv)
        {
            return DAOQLNhanVien.Instance.Delete(manv);
        }


        public bool Update(int manv, string ten, DateTime ngay, string dc, int macv)
        {
            if (ten == "" || dc == "" || macv == 0)
                return false;
            NHANVIEN nv = new NHANVIEN();
            nv.MANV = manv;
            nv.TENNV = ten;
            nv.NGAYSINH = ngay;
            nv.DIACHI = dc;
            nv.MACHUCVU = macv;
            return DAOQLNhanVien.Instance.Update(nv);
        }

        public ObservableCollection<NHANVIEN> Search(int thang, int macv, string ten, string dc)
        {
            return DAOQLNhanVien.Instance.Search(thang, macv, ten, dc);
        }

        /// <summary>
        /// Load all records from NhanVien table.
        /// </summary>
        /// <returns>Return all records of NhanVien table in a observable collection.</returns>
        public ObservableCollection<NHANVIEN> GetAll()
        {
            return DAOQLNhanVien.Instance.GetAll();
        }

        public string GetChucVu(int maCV)
        {
            return DAOQLNhanVien.Instance.GetChucVu(maCV);
        }
    }
}
