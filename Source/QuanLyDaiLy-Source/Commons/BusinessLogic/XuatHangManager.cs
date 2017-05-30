using DAODLL;
using QuanLyDaiLy_Source.Models.BusinessLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    class XuatHangManager : Manager<PHIEUXUATHANG>
    {
        DaiLyManager daiLyManager = new DaiLyManager();

        public override bool Delete(PHIEUXUATHANG obj)
        {
            throw new NotImplementedException();
        }

        public override PHIEUXUATHANG Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<PHIEUXUATHANG> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool Insert(PHIEUXUATHANG obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Thêm phiếu vào database (chỉnh sửa bao gồm bảng PHIEUXUATHANG và CTPX)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="maHang"></param>
        /// <param name="soLuong"></param>
        /// <returns></returns>
        public bool Insert(PHIEUXUATHANG obj, ArrayList maHang, ArrayList soLuong)
        {
            // Check for current Debt.
            DAILY daiLy = daiLyManager.Get(obj.MADL.Value);
            decimal soNoToiDa = ViewManager.Instance.GetSoNoDaiLy(daiLy.LOAIDL.Value);
            if (daiLy.SONO.Value >= soNoToiDa || (daiLy.SONO.Value + obj.CONLAI.Value) >= soNoToiDa) return false;

            if ((int)obj.CONLAI.Value < 0)
            {
                return false;
            }

            // Add PhieuXuatHang into PHIEUXUATHANG
            if (DAOXuatHang.Instance.Insert(obj.MADL.Value, obj.NGAYLAP.Value,
                (int)obj.TONGTIEN.Value, (int)obj.SOTIENTRA.Value,
                (int)obj.CONLAI.Value, maHang, soLuong))
            {
                //Update SoNo in DAILY
                daiLy.SONO = daiLy.SONO + obj.CONLAI.Value;
                if (daiLyManager.Update(daiLy))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra số nợ đại lý hiện có, liệu có được phép để tiếp tục cộng dồn thêm nợ hay không.
        /// </summary>
        /// <param name="debt"></param>
        /// <returns></returns>
        public bool IsMoreDebtsAllowed(int debt, int maDL)
        {
            DAILY daiLy = daiLyManager.Get(maDL);
            decimal soNoToiDa = ViewManager.Instance.GetSoNoDaiLy(daiLy.LOAIDL.Value);
            if (daiLy.SONO.Value >= soNoToiDa || (daiLy.SONO.Value + debt) >= soNoToiDa) return false;

            return true;
        }

        public bool IsPayingMoneyGood(PHIEUXUATHANG obj)
        {
            if (!obj.CONLAI.HasValue) return false;
            if (obj.CONLAI.Value < 0) return false;
            return true;
        }


        public override bool Update(PHIEUXUATHANG obj)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PHIEUXUATHANG> GetAllPhieuXuatHang(int maDL)
        {
            return DAOXuatHang.Instance.GetAllPhieuXuatHang(maDL);
        }
    }
}
