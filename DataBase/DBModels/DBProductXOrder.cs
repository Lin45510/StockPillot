using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.DBModels
{
    public class DBProductXOrder
    {
        public int PxOID { get; set; }
        public int PxOProductID { get; set; }
        public int PxOOrderID { get; set; }
        public string PxOName { get; set; }
        public int PxOAmmount { get; set; }
        public string PxOTheme { get; set; }
        public double PxOPrice { get; set; }
    }
}
