using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductTheme { get; set; }
        public int PreviousOrderAmmount { get; set; }
        public List<MaterialXProduct> MaterialXProducts { get; set; } = new List<MaterialXProduct>();
    }
}
