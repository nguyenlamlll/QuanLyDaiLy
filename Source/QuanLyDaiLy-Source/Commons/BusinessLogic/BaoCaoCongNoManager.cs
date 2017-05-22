using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DAODLL;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    public class BaoCaoCongNoManager
    {
        /// <summary>
        /// Get SoNoDauKy of a month.
        /// Documented in Report, Chapter 3., 3.4.6.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal GetSoNoDauKy(DateTime date)
        {
            decimal soNoDauKy = 0;
            ObservableCollection<PHIEUXUATHANG> listPhieuXuat =
                DAOXuatHang.Instance.GetPhieuXuatHangBeforeAMonth(date);
            ObservableCollection<PHIEUTHUTIEN> listPhieuThu =
                DAOThutien.Instance.GetPhieuThuTienBeforeAMonth(date);
            if (listPhieuXuat.Any())
            {
                foreach (PHIEUXUATHANG phieu in listPhieuXuat)
                {
                    soNoDauKy += phieu.CONLAI.Value;
                }
                if (soNoDauKy != 0) // If all PhieuXuatHang are paid. There won't be any PhieuThu.
                                    // The final result remains the same (equals 0.)
                {
                    if (listPhieuThu.Any())
                    {

                    }
                }
            }
            else //Doesn't contain any PhieuXuatHang
            {
                return soNoDauKy;
            }
            return 0;
        }
    }
}
