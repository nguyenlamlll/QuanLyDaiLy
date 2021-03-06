﻿using DAODLL;
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
                // Check maximum constraint
                int max = DAOView.Instance.GetSoDlToiDa(obj.MAQUAN.Value);
                int currentNum = DAOView.Instance.CountSoDaiLy(obj.MAQUAN.Value);
                if (currentNum >= max) return false;
                obj.TINHTRANG = 1; // Set current state is active (1: active, 2:inactive)
                obj.SONO = 0;
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
            return DAOView.Instance.GetDaiLy(id);
        }

        public override List<DAILY> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a DaiLy in database
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Update(DAILY obj)
        {
            return DAOTiepNhanDaiLy.Instance.Update(obj);
        }
    }
}
