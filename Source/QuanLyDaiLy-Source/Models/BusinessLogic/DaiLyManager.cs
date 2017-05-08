using DAODLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyDaiLy_Source.Models.BusinessLogic
{
    public class DaiLyManager: Manager<DAILY>
    {
        public static void Insert(DAILY daiLy)
        {
            try
            {
                DAODLL.DAOTiepNhanDaiLy.Instance.Insert(daiLy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tham đại lý. Vui lòng kiểm tra lại.\n" + ex.ToString());
            }

        }

        public override bool Add(DAILY obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(DAILY obj)
        {
            throw new NotImplementedException();
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
