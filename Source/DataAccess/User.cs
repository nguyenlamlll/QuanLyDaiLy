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
                vwCHUCVU_NHANVIEN_TAIKHOAN nv = db.vwCHUCVU_NHANVIEN_TAIKHOANs.Where(p => p.TENDANGNHAP == acc 
                    && p.PASSWORD == pass).Single();
                if (nv != null)
                {
                    ten = nv.TENNV;
                    manv = nv.MANV;
                    chucvu = nv.TENCHUCVU;
                    //Login suceed
                    return true;
                }
                //login fail
                return false;
            }
        }
    }
}
