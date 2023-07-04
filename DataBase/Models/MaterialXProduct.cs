using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.Models
{
    public class MaterialXProduct
    {
        public int MxPId { get; set; }
        public int MxPMaterialID { get; set; }
        public int MxPProductID { get; set; }
        public string MxPName { get; set; }
        public double MxPAmmount { get; set; }

    }
}
