using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.DataBase.Models
{
    public class Material
    {
        public int MatID { get; set; }
        public string MatName { get; set; }
        public int MatAmmount { get; set; }
        public int MatNeed { get; set; }
    }
}
