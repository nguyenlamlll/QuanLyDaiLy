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
        /// Get SoNoDauKy of a month by retrieving all previous PhieuXuatHang and PhieuThuTien.
        /// SoNoDauKy equals total debt from PhieuXuats subtracting collected money from PhieuThues
        /// Documented in Report, Chapter 3., 3.4.6.
        /// </summary>
        /// <param name="date">The month where SoNoDauKy is calculated</param>
        /// <returns>Return SoNoDauKy (sum of all debts from previous months.)</returns>
        public decimal GetSoNoDauKy(DateTime date, int maDL)
        {
            decimal soNoDauKy = 0;
            ObservableCollection<PHIEUXUATHANG> listPhieuXuat =
                DAOXuatHang.Instance.GetPhieuXuatHangBeforeAMonth(date, maDL);
            ObservableCollection<PHIEUTHUTIEN> listPhieuThu =
                DAOThutien.Instance.GetPhieuThuTienBeforeAMonth(date, maDL);
            if (listPhieuXuat.Any())
            {
                foreach (PHIEUXUATHANG phieu in listPhieuXuat)
                    if (phieu.CONLAI.HasValue) soNoDauKy += phieu.CONLAI.Value;

                if (soNoDauKy != 0) // If all PhieuXuatHang are paid. There won't be any PhieuThu.
                                    // The final result remains the same (equals 0.)
                {
                    if (listPhieuThu.Any())
                    {
                        foreach (PHIEUTHUTIEN phieu in listPhieuThu)
                            if (phieu.SOTIEN.HasValue) soNoDauKy -= phieu.SOTIEN.Value;
                    }
                }
            }
            else //Doesn't contain any PhieuXuatHang, which means the agency has no debt.
            {
                return soNoDauKy;
            }
            return soNoDauKy;
        }


        /// <summary>
        /// Get SoNoPhatSinh in a month. 
        /// The result is positive if DaiLy makes more debts than paying.
        /// The result is negative if Daily pays its debt more in that month.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>SoNoPhatSinh in a month (Whether a Daily pays its debt or loan more.)</returns>
        public decimal GetSoNoPhatSinh(DateTime date, int maDL)
        {
            decimal soNoPhatSinh = 0;
            ObservableCollection<PHIEUXUATHANG> listPhieuXuat =
                DAOXuatHang.Instance.GetPhieuXuatHangInAMonth(date, maDL);
            ObservableCollection<PHIEUTHUTIEN> listPhieuThu =
                DAOThutien.Instance.GetPhieuThuTienInAMonth(date, maDL);

            if (!listPhieuThu.Any() && !listPhieuXuat.Any()) return soNoPhatSinh;
            if (listPhieuXuat.Any())
                foreach (PHIEUXUATHANG phieu in listPhieuXuat)
                    if (phieu.CONLAI.HasValue) soNoPhatSinh += phieu.CONLAI.Value;
            if (listPhieuThu.Any())
                foreach (PHIEUTHUTIEN phieu in listPhieuThu)
                    if (phieu.SOTIEN.HasValue) soNoPhatSinh -= phieu.SOTIEN.Value;

            return soNoPhatSinh;
        }
    }
}
