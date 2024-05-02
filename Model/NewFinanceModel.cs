using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class NewFinanceModel
    {
        private readonly FinnanceDAO financeDAO = new();

        #region Properties

        public int FinnanceId { get; set; }

        public double Value { get; set; }

        public string Descritption { get; set; }

        #endregion

        #region functions

        public void Insert(DBFinnance finnance)
        {
            financeDAO.Insert(finnance);
        }

        #endregion

    }
}
