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
    public class ProductsVM : Utilities.ViewModelBase
    {
        private readonly ProductsModel _pageModel;

        public ProductsVM()
        {
            _pageModel = new ProductsModel();
            WindowProductCommand = new RelayCommand<object>(WindowProduct);
        }
        #region Commands
        public ICommand WindowProductCommand { get; set; }

        private void WindowProduct(object obj)
        {
            var ProductWindow = new Product
            {
                DataContext = new ProductVM
                {

                }
            };
            ProductWindow.Show();

        }
        #endregion
    }
}
