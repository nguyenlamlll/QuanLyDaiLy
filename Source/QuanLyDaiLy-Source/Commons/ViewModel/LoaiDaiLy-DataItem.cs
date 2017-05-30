using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons.ViewModel
{
    /// <summary>
    /// Data View Model for datagrid/list box/... that needs the class' properties to be shown onto itself.
    /// </summary>
    public class LoaiDaiLyDataItem
    {
        public int MALOAI { get; set; }
        public string TENLOAI { get; set; }
        public string SONOTOIDA { get; set; }
    }
}
