using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class User
    {
        private string ten;

        public string Ten
        {
            get { return ten; }
            set { ten = value; }
        }
        private int manv;

        public int Manv
        {
            get { return manv; }
            set { manv = value; }
        }
        private string chucvu;

        public string Chucvu
        {
            get { return chucvu; }
            set { chucvu = value; }
        }

        /// <summary>
        /// Singleton tech for class User
        /// </summary>
        private User() { }
        private static volatile User instance;
        public static User Instance
        {
            get
            {
                if (instance == null)
                    instance = new User();
                return User.instance;
            }
        }

        /// <summary>
        /// Pass account- password pair to check Login
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool Dangnhap(string acc, string pass)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                //vwCHUCVU_NHANVIEN_TAIKHOAN nv = db.vwCHUCVU_NHANVIEN_TAIKHOANs.Where(p => p.TENDANGNHAP.Contains(acc) 
                //    && p.PASSWORD.Contains(pass)).Single();
                TAIKHOAN nv = null;
                try
                {
                    nv = (from account in db.TAIKHOANs
                                  //join employee in db.NHANVIENs on account.MANV equals employee.MANV
                                  //join position in db.CHUCVUs on employee.MACHUCVU equals position.MACHUCVU
                              where account.TENDANGNHAP.Contains(acc)
                              && account.PASSWORD.Contains(pass)
                              select account).Single();
                }
                catch (Exception)
                {
                    nv = null;
                }

                if (nv != null)
                {
                    /*
                    ten = nv.TENNV;
                    manv = nv.MANV;
                    chucvu = nv.TENCHUCVU;
                    */
                    ten = nv.NHANVIEN.TENNV;
                    manv = (int)nv.MANV;

                    var query = (from position in db.CHUCVUs
                                where nv.NHANVIEN.MACHUCVU == position.MACHUCVU
                                select position.TENCHUCVU).Single();
                    chucvu = query;

                    //Login suceed
                    return true;
                }
                //login fail
                return false;
            }
        }
    }
}
