using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAODLL
{
    public class DAODonViTinh
    {
        /// <summary>
        /// Singleton tech for class DAODonViTinh
        /// </summary>
        private DAODonViTinh() { }
        private static volatile DAODonViTinh instance;
        public static DAODonViTinh Instance
        {
            get
            {
                if (instance == null)
                    instance = new DAODonViTinh();
                return DAODonViTinh.instance;
            }
        }


    }
}
