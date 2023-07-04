using StockPilot.Model;
using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StockPilot.ViewModel
{
    public class FinanceVM : ViewModelBase
    {
        private readonly FinanceModel _pageModel;
        public float Saldo
        {
            get { return _pageModel.Saldo; }
            set { _pageModel.Saldo = value; OnPropertyChanged(); }
        }
        public FinanceVM()
        {
            _pageModel = new FinanceModel();
        }
    }
}
