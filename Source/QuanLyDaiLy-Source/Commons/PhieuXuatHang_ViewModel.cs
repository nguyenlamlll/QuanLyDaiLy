using DAODLL;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons
{
    public class PhieuXuatHang_ViewModel
    {
        public ObservableCollection<CTPX> CTPXs { get; set; }
        public ObservableCollection<MATHANG> MatHangs { get; set; }
        public ObservableCollection<DVT> DVTs { get; set; }
        public PhieuXuatHang_ViewModel()
        {
            MatHangManager manager = new MatHangManager();
            MatHangs = manager.GetMatHang();

            DVTs = ViewManager.Instance.GetAllDVT();

            CTPXs = ViewManager.Instance.GetAllCTPX();
        }
    }
}
