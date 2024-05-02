using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class FinanceModel
    {
        private readonly FinnanceDAO finnanceDAO = new();

        public double Saldo { get; set; }

        public List<DBFinnance> financces { get; set; }

        #region Commands

        public List<DBFinnance> GetFinnaces()
        {
            return finnanceDAO.DataList();
        }

        public double GetSaldo(List<DBFinnance> finnancesList)
        {
            double saldo = 0;
            foreach (DBFinnance finnance in finnancesList)
            {
                saldo += finnance.Value;
            }
            return saldo;
        }

        #endregion
    }
}
