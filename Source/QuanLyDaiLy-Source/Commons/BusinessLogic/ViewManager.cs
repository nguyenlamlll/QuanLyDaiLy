﻿using DAODLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Commons.BusinessLogic
{

    public class ViewManager
    {
        /// <summary>
        /// Init Singleton for BUSView
        /// </summary>
        private ViewManager() { }
        public static volatile ViewManager instance;
        public static ViewManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ViewManager();
                return ViewManager.instance;
            }
        }


        public ObservableCollection<MATHANG> GetAllMatHang()
        {
            return DAOView.Instance.GetAllMatHang();
        }

        /// <summary>
        /// Get all DaiLy in database and load them into an observable collection
        /// </summary>
        /// <returns>Return a observable collection of all DaiLy</returns>
        public ObservableCollection<DAILY> GetAllDaiLy()
        {
            return DAOView.Instance.GetAllDaiLy();
        }

        public ObservableCollection<QUAN> GetAllQuan()
        {
            return DAOView.Instance.GetAllQuan();
        }

        public ObservableCollection<LOAIDL> GetAllLoaiDL()
        {
            return DAOView.Instance.GetAllLoaiDL();
        }

        public ObservableCollection<DVT> GetAllDVT()
        {
            return DAOView.Instance.GetAllDVT();
        }

        public ObservableCollection<CHUCVU> GetAllCV()
        {
            return DAOView.Instance.GetAllCV();
        }

        public ObservableCollection<CTPX> GetAllCTPX()
        {
            return DAOView.Instance.GetAllCTPX();
        }

        public string GetDonViTinh(int maHang)
        {
            return DAOView.Instance.GetDonViTinh(maHang);
        }

        public decimal GetDonGia(int maHang)
        {
            return DAOView.Instance.GetDonGia(maHang);
        }

        public QUAN GetQuan(int maQuan)
        {
            return DAOView.Instance.GetQuan(maQuan);
        }

        /// <summary>
        /// Get All DaiLy that belongs to QUAN (maQuan)
        /// </summary>
        /// <param name="maQuan">Mã Quận cần lấy tât cả đại lý</param>
        /// <returns></returns>
        public ObservableCollection<DAILY> GetAllDaiLy(int maQuan)
        {
            return DAOView.Instance.GetAllDaiLy(maQuan);
        }


        public int GetMaHang(string ten)
        {
            return DAOView.Instance.GetMaHang(ten);
        }

        public decimal GetSoNoDaiLy(int maLoai)
        {
            return DAOView.Instance.GetSoNoDaiLy(maLoai);
        }

        /// <summary>
        /// Enable condition checkNo or checkKoNo or both are disable
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        /// <param name="check"></param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> Test(ObservableCollection<vwDAILY_LOAIDL_QUAN> src, ObservableCollection<vwDAILY_LOAIDL_QUAN> des, int check)
        {
            if (src != null)
            {
                if (check == 1)
                {
                    foreach (vwDAILY_LOAIDL_QUAN dl in src)
                    {
                        if (dl.SONO > 0)
                            des.Add(dl);
                    }
                    return des;
                }
                else if (check == 0)
                {
                    foreach (vwDAILY_LOAIDL_QUAN dl in src)
                    {
                        if (dl.SONO == 0 || dl.SONO == null)
                            des.Add(dl);
                    }
                    return des;
                }
                else
                {
                    return src;
                }
            }
            return null;
        }

        #region Tìm đại lí theo tên
        /// <summary>
        /// Get All DaiLy that belongs to QUAN (maQuan)
        /// </summary>
        /// <param name="maQuan">Mã Quận cần lấy tât cả đại lý</param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByQuan(string name, int check)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiSrc = DAOView.Instance.SearchByQuan(name);
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiDes = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            return Test(dailiSrc, dailiDes, check);
        }

        /// <summary>
        /// Get All DaiLy that belongs according LOAIDL (maQuan)
        /// </summary>
        /// <param name="maLoai">Mã Loại cần lấy tât cả đại lý</param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByLoai(string name, int check)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiSrc = DAOView.Instance.SearchByLoai(name);
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiDes = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            return Test(dailiSrc, dailiDes, check);
        }

        /// <summary>
        /// Get All DaiLy that belongs according LOAIDL (maQuan)
        /// </summary>
        /// <param name="maLoai">Mã Loại cần lấy tât cả đại lý</param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> SearchByDaiLy(string name, int check)
        {
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiSrc = DAOView.Instance.SearchByDaiLy(name);
            ObservableCollection<vwDAILY_LOAIDL_QUAN> dailiDes = new ObservableCollection<vwDAILY_LOAIDL_QUAN>();
            return Test(dailiSrc, dailiDes, check);
        }
        #endregion

        /// <summary>
        /// Get All DaiLy that belongs according LOAIDL (maQuan)
        /// </summary>
        /// <param name="maLoai">Mã Loại cần lấy tât cả đại lý</param>
        /// <returns></returns>
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> GetAllDaiLyAccordingLoai(int maLoai)
        {
            if (maLoai == 0) return DAOView.Instance.GetMultipleViewByLoai();
            return DAOView.Instance.GetMultipleViewByLoai(maLoai);
        }

        public ObservableCollection<vwDAILY_LOAIDL> GetAllDaiLyWithLoaiDaiLy(int maQuan)
        {
            if (maQuan == 0)
            {
                return DAOView.Instance.GetAllDaiLyWithLoaiDaiLy();
            }
            return DAOView.Instance.GetAllDaiLyWithLoaiDaiLy(maQuan);
        }

        public ObservableCollection<DAILY> GetAllDaiLyByMaLoai(int maLoai)
        {
            return DAOView.Instance.GetAllDaiLyByMaLoai(maLoai);
        }

        public ObservableCollection<DAILY> GetAllDaiLy(int maQuan, int maLoai)
        {
            return DAOView.Instance.GetAllDaiLy(maQuan, maLoai);
        }

        /// <summary>
        /// Lấy số đại lý tối đa của một quận (mã quận).
        /// </summary>
        /// <param name="maQuan">Mã quận cần lấy số đại lý tối đa</param>
        /// <returns>Số đại lý tối đa.</returns>
        public int GetSoDlToiDa(int maQuan)
        {
            return DAOView.Instance.GetSoDlToiDa(maQuan);
        }

    }


}
