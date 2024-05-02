using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.DBModels
{
    public class DBOrder
    {
        public int OrderId { get; set; }
        public string Client { get; set; }
        public string Address { get; set; }
        public string Tell { get; set; }
        public string Term { get; set; }
        public string Delivery { get; set; }
        public string Payment { get; set; }
        public double Total { get; set; }

        public List<DBProductXOrder> ProductXOrder { get; set; } = new List<DBProductXOrder>();
    }
}
