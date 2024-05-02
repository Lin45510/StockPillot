using StockPilot.DataBase.DBModels;
using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.ViewModel
{
    public class ViewOrderVM : ViewModelBase
    {
        public DBOrder order { get; set; }

        public ViewOrderVM()
        {

        }
    }
}
