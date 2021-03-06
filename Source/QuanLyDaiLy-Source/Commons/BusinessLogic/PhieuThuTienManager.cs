﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAODLL;
using QuanLyDaiLy_Source.Models.BusinessLogic;
using System.Windows;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{
    public class PhieuThuTienManager : Manager<PHIEUTHUTIEN>
    {
        public override bool Delete(PHIEUTHUTIEN obj)
        {
            throw new NotImplementedException();
        }

        public override PHIEUTHUTIEN Get(int id)
        {
            throw new NotImplementedException();
        }

        public override List<PHIEUTHUTIEN> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">PHIEUTHUTIEN needs obj.DAILY.TENDL, obj.DAILY.DIENTHOAI, obj.DAILY.DIACHI, obj.SOTIEN, obj.NGAYTHUTIEN</param>
        public override bool Insert(PHIEUTHUTIEN obj)
        {
            try
            {
                //Check SoNo
                DaiLyManager daiLyManager = new DaiLyManager();
                DAILY daiLy = daiLyManager.Get(obj.MADL.Value);
                if (daiLy.SONO.Value < obj.SOTIEN) return false;
                // Insert PhieuThuTien first. If succeed, proceed to update SoNo of DaiLy in DAILY table.
                if (DAOThutien.Instance.Insert(obj.DAILY.TENDL, obj.DAILY.DIENTHOAI, obj.DAILY.DIACHI,
                    obj.SOTIEN, obj.NGAYTHUTIEN))
                {

                    return true;
                }
                else
                {
                    MessageBox.Show("Không thể thêm phiếu thu tiền.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm phiếu thu tiền. Vui lòng kiểm tra lại.\n" + ex.ToString());
            }
            return false;
        }

        public override bool Update(PHIEUTHUTIEN obj)
        {
            throw new NotImplementedException();
        }
    }
}
