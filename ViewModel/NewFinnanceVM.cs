using StockPilot.Model;
using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.ViewModel
{
    public class NewFinnanceVM : ViewModelBase
    {
        private NewFinanceModel _pageModel;

        #region Page Properties

        public int OperationType { get; set; }

        public string PageTittle
        {
            get { return OperationType == 0 ? "Nova Entrada" : OperationType == 1 ? "Nova Saida" : ""; }
            set { }
        }

        #endregion

        #region Porperties

        public int FinnanceID
        {
            get { return _pageModel.FinnanceId; }
            set { _pageModel.FinnanceId = value; OnPropertyChanged(); }
        }

        public double Value
        {
            get { return _pageModel.Value; }
            set { _pageModel.Value = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get { return _pageModel.Descritption; }
            set { _pageModel.Descritption = value; OnPropertyChanged(); }
        }

        #endregion

        public NewFinnanceVM()
        {
            _pageModel = new();
        }
    }
}
