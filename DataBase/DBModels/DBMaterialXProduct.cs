using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.DBModels
{
    public class DBMaterialXProduct
    {
        public int MxPId { get; set; }
        public int MxPMaterialID { get; set; }
        public int MxPProductID { get; set; }
        public double MxPAmmount { get; set; }

    }
}
