using DAODLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyDaiLy_Source.Models.BusinessLogic
{
    public class DaiLyManager : Manager<DAILY>
    {
        public override bool Insert(DAILY obj)
        {
            try
            {
                DAODLL.DAOTiepNhanDaiLy.Instance.Insert(obj);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm đại lý. Vui lòng kiểm tra lại.\n" + ex.ToString());
            }
            return false;
        }

        public override bool Delete(DAILY obj)
        {
            try
            {
                DAODLL.DAOTiepNhanDaiLy.Instance.Delete(obj.MADL);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa đại lý. Vui lòng kiểm tra lại.\n" + ex.ToString());
            }
            return false;
        }

        public override DAILY Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<DAILY> GetAll()
        {
            throw new NotImplementedException();
        }

        public override bool Update(DAILY obj)
        {
            throw new NotImplementedException();
        }
    }
}
