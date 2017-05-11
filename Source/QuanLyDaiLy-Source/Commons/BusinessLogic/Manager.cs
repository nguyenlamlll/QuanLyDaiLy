using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy_Source.Models.BusinessLogic
{
    /// <summary>
    /// Documented at Chap 4.Implementation, 4.2.2.Business Logic Layer.
    /// This is an abstract class template for database manager classes.
    /// </summary>
    /// <typeparam name="T">A Model Class that represents a database's object.</typeparam>
    public abstract class Manager<T> 
        where T : class
    {
        public abstract List<T> GetAll();
        public abstract T Get(int id);

        public abstract bool Insert(T obj);
        public abstract bool Delete(T obj);
        public abstract bool Update(T obj);
    }
}
