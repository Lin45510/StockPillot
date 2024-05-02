using StockPilot.DataBase.DBModels;
using StockPilot.Model;
using StockPilot.Utilities;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class FinanceVM : ViewModelBase
    {
        private readonly FinanceModel _pageModel;

        #region Properties

        public double Saldo
        {
            get { return _pageModel.Saldo; }
            set { _pageModel.Saldo = value; OnPropertyChanged(); }
        }

        public List<DBFinnance> finnancesList
        {
            get { return _pageModel.financces; }
            set { _pageModel.financces = value; OnPropertyChanged(); }
        }

        #endregion

        public FinanceVM()
        {
            _pageModel = new FinanceModel();
            finnancesList = _pageModel.GetFinnaces();
            Saldo = _pageModel.GetSaldo(finnancesList);

            NewFinnanceWindowCommand = new RelayCommand<object>(NewFinnanceWindow);
        }

        #region Commands

        public ICommand NewFinnanceWindowCommand { get; set; }

        private void NewFinnanceWindow(object obj)
        {
            NewFinance newFinanceWindow = new()
            {
                DataContext = new NewFinnanceVM()
                {

                }
            };

            newFinanceWindow.Show();

        }

        #endregion
    }
}
