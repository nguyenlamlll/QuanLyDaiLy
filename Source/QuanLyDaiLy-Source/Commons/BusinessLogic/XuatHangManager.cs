using DAODLL;
using QuanLyDaiLy_Source.Models.BusinessLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    class XuatHangManager : Manager<PHIEUXUATHANG>
    {
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
            return DAOXuatHang.Instance.Insert(obj.MADL.Value, obj.NGAYLAP.Value, (int)obj.TONGTIEN.Value, (int)obj.SOTIENTRA.Value,
                (int)obj.CONLAI.Value, maHang, soLuong);
        }

        public override bool Update(PHIEUXUATHANG obj)
        {
            throw new NotImplementedException();
        }
    }
}
