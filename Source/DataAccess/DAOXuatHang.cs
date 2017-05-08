﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DAODLL
{
    public class DAOXuatHang
    {
        /// <summary>
        /// Singleton tech for class DAOXuatHang
        /// </summary>
        private DAOXuatHang() { }
        private static volatile DAOXuatHang instance;
        public static DAOXuatHang Instance
        {
            get 
            {
                if (instance == null)
                    instance = new DAOXuatHang();
                return DAOXuatHang.instance; 
            }
        }

        /// <summary>
        /// Search PHIEUTHUTIEN from database and display it to GUI
        /// Load PHIEUTHUTIEN if search condition equal empty string 
        /// </summary>
        public List<vw_SEARCH_PHIEUXUATHANG> Search(string tendl="", string tenquan="")
        {
            List<vw_SEARCH_PHIEUXUATHANG> li = new List<vw_SEARCH_PHIEUXUATHANG>();
            using(QLDLDataContext db = new QLDLDataContext())
            {
                li = (db.vw_SEARCH_PHIEUXUATHANGs.Where(p => p.TENDL.Contains(tendl)
                    && p.TENQUAN.Contains(tenquan))).ToList();
            }
            return li;
        }

        /// <summary>
        /// insert phieuxuat
        /// </summary>
        /// <param name="madl"></param>
        /// <param name="ngay"></param>
        /// <param name="tong"></param>
        /// <param name="tra"></param>
        /// <param name="conlai"></param>
        /// <param name="mahang"></param>
        /// <param name="sl"></param>
        /// <returns></returns>
        public bool Insert(int madl, DateTime ngay, int tong, int tra, int conlai, ArrayList mahang, ArrayList sl)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                //insert into PHIEUXUATHANG
                PHIEUXUATHANG px = new PHIEUXUATHANG();
                px.MADL = madl;
                px.NGAYLAP = ngay;
                px.TONGTIEN = tong;
                px.SOTIENTRA=tra;
                px.CONLAI = conlai;
                px.NGUOIXUAT = User.Instance.Manv;
                //insert PHIEUXUATHANG into database
                db.PHIEUXUATHANGs.InsertOnSubmit(px);
                db.SubmitChanges();

                //Get MAPX 
                int mapx=db.PHIEUXUATHANGs.Single(p=>p.MADL==madl && p.NGAYLAP==ngay).MAPHIEU;

                //insert into ctphieuxaut
                for(int i=0;i< mahang.Count;i++)
                {
                    CTPX ctpx = new CTPX();
                    ctpx.MAPHIEU = mapx;
                    ctpx.MAHANG = (int)mahang[i];
                    ctpx.SOLUONG = (int)sl[i];
                }
                
                //insert succeed
                return true;   
            }
            //insert fail;
            return false;
        }

        /// <summary>
        /// delete phieuxuat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                if(db.CTPXes.Where(p=> p.MAPHIEU == id).Count()>0) return false;
                else{
                    PHIEUXUATHANG px = db.PHIEUXUATHANGs.Where(p=>p.MAPHIEU == id).First();
                    db.PHIEUXUATHANGs.DeleteOnSubmit(px);
                     db.SubmitChanges();
                    //delete sucees;
                    return true;
                }
            }
            //delete fail
            return false;
        }

        /// <summary>
        /// Update phieuxuat
        /// </summary>
        /// <param name="ptt"></param>
        /// <returns></returns>
        public bool Update(int maphieu, int madl, DateTime ngay, int tong, int tra, int conlai, ArrayList mahang, ArrayList sl)
        {
            using (QLDLDataContext db = new QLDLDataContext())
            {
                //update from PHIEUXUATHANG
                PHIEUXUATHANG px = db.PHIEUXUATHANGs.Single(p=>p.MAPHIEU == maphieu);
                px.MADL = madl;
                px.NGAYLAP = ngay;
                px.TONGTIEN = tong;
                px.SOTIENTRA = tra;
                px.CONLAI = conlai;
                //update phieuxuat from PHIEUXUATHANG
                db.SubmitChanges();

                //update from ctphieuxaut
                for (int i = 0; i < mahang.Count; i++)
                {
                    CTPX ctpx = db.CTPXes.Where(p=>p.MAPHIEU == maphieu && p.MAHANG == (int)mahang[i]).First();
                    ctpx.SOLUONG = (int)sl[i];
                }

                //insert succeed
                return true;
            }
            //insert fail;
            return false;
        }
    }
}
