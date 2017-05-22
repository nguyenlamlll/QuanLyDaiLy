using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAODLL
{
    public class DAOBaoCao
    {
        /*/// <summary>
        /// Search BaoCaoDoanhSo from database and display it to GUI
        /// Load BaoCaoDoanhSo if search condition equal empty string 
        /// </summary>
        public List<vwPHIEUXUATHANG_DAILY_LOAIDL> SearchDT(string tenquan = "", string tenloai = "",int thang = 0)
        {
            List<vwPHIEUXUATHANG_DAILY_LOAIDL> li = new List<vwPHIEUXUATHANG_DAILY_LOAIDL>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (from vwbcdt in db.vwPHIEUXUATHANG_DAILY_LOAIDLs
                      
                      where vwbcdt.TENLOAI.Contains(tenloai) && vwbcdt.TENQUAN.Contains(tenquan) && vwbcdt.NGAYLAP.Value.Month.Equals(thang)
                      select new vwPHIEUXUATHANG_DAILY_LOAIDL
                      {
                          TENDL = vwbcdt.TENDL,
                          MAPHIEU = vwbcdt.MAPHIEU,
                          TONGTIEN = vwbcdt.TONGTIEN,
                          NGAYLAP = vwbcdt.NGAYLAP,


                      }
                      ).ToList();
            }
            return li;
        }
        public List<vwDAILY_QUAN_PHIEUXUAT> SearchCN(string tenquan = "", string tenloai = "", int thang = 0)
        {
            List<vwDAILY_QUAN_PHIEUXUAT> li = new List<vwDAILY_QUAN_PHIEUXUAT>();
            using (QLDLDataContext db = new QLDLDataContext())
            {
                li = (from vwbccn in db.vwDAILY_QUAN_PHIEUXUATs

                      where vwbccn.TENLOAI.Contains(tenloai) && vwbccn.TENQUAN.Contains(tenquan) && vwbccn.NGAYLAP.Value.Month.Equals(thang)
                      select new vwDAILY_QUAN_PHIEUXUAT
                      {
                          TENDL = vwbccn.TENDL,
                          TONGTIEN = vwbccn.TONGTIEN,
                          SOTIENTRA = vwbccn.SOTIENTRA,
                          CONLAI = vwbccn.CONLAI,
                          NGAYLAP = vwbccn.NGAYLAP,


                      }
                      ).ToList();
            }
            return li;
        }*/
    }
}
