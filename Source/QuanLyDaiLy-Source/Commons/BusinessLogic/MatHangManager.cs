using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAODLL;
using QuanLyDaiLy_Source.Models.BusinessLogic;
using System.Collections.ObjectModel;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    class MatHangManager : Manager<MATHANG>
    {
        public override bool Delete(MATHANG obj)
        {
            throw new NotImplementedException();
        }

        public override MATHANG Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<MATHANG> GetAll()
        {
            throw new NotImplementedException();
        }
        public ObservableCollection<MATHANG> GetMatHang()
        {
            ObservableCollection<MATHANG> matHang = DAOXuatHang.Instance.GetMatHang();
            return matHang;
        }

        public override bool Insert(MATHANG obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(MATHANG obj)
        {
            throw new NotImplementedException();
        }
    }
}
