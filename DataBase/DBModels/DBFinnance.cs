using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.DBModels
{
    public class DBFinnance
    {
        public int FinnanceID { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public int Order_Id { get; set; }

        public string Date { get; set; }
    }
}
