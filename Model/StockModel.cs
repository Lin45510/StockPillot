using StockPilot.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class StockModel
    {
        public List<Material> Materials = MaterialsList();

        public static List<Material> MaterialsList()
        {
            return new MaterialDAO().DataList();
        }
    }
}
