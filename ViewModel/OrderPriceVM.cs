using StockPilot.Model;
using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class OrderPriceVM : ViewModelBase
    {
        private readonly OrderPriceModel _pageModel;

        #region Properties

        public double Price
        {
            get { return _pageModel.Price; }
            set { _pageModel.Price = value; OnPropertyChanged(); }
        }

        #endregion

        #region Window Properties

        public OrderVM orderVM { get; set; }

        #endregion

        public OrderPriceVM()
        {
            _pageModel = new();

            PageFunctionCommand = new RelayCommand<Window>(PageFunction);
        }

        #region Commands

        public ICommand PageFunctionCommand { get; set; }

        private void PageFunction(Window window)
        {
            orderVM.Total = Price;

            window.Close();
        }

        #endregion
    }
}
