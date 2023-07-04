using StockPilot.Model;
using StockPilot.Utilities;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class OrdersVM
    {
        private readonly OrderModel _pageModel;
        public OrdersVM()
        {
            _pageModel = new OrderModel();
            WindowOrderCommand = new RelayCommand<object>(WindowOrder);
        }
        #region Commands
        public ICommand WindowOrderCommand { get; set; }

        private void WindowOrder(object obj)
        {
            var WindowOrder = new Order
            {
                DataContext = new OrderVM()

            };
            WindowOrder.Show();
        }
        #endregion 
    }
}
