using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.Interfaces
{
    interface IDAO<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        SQLiteDataReader Select();
        SQLiteDataReader Search(string search);
        SQLiteDataReader SelectbyID(int id, string caller);
        List<T> DataList(string selectmode, int id, string search, string caller);
    }
}
