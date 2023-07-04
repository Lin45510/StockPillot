using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Client { get; set; }
        public double Total { get; set; }
        public string Delivery { get; set; }
        public DateTime Term { get; set; }
        public string Payment { get; set; }
        public string Tell { get; set; }
        public List<ProductXOrder> ProductXOrder { get; set; } = new List<ProductXOrder>();
    }
}
