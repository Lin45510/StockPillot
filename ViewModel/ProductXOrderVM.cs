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
    public class ProductXOrderVM : ViewModelBase
    {
        private readonly ProductXOrderModel _pageModel = new();

        #region Properties

        public List<ProductXOrderModel> PxOList
        {
            get { return _pageModel.PxOList; }
            set { _pageModel.PxOList = value; OnPropertyChanged(); }
        }

        public string Theme
        {
            get { return _pageModel.PxOTheme; }
            set { _pageModel.PxOTheme = value; OnPropertyChanged(); }
        }

        public OrderVM OrderVM { get; set; }

        #endregion

        public ProductXOrderVM()
        {
            ConfirmCommand = new RelayCommand<Window>(Confirm);
            CheckCommand = new RelayCommand<ProductXOrderModel>(Check);
        }

        #region Functions

        public ICommand ConfirmCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        public void Confirm(Window window)
        {
            List<ProductXOrderModel> productxOrder = new();

            foreach (ProductXOrderModel pxo in PxOList)
            {
                if (pxo.PxOSelected)
                {
                    pxo.PxOAmmount = 1;
                    pxo.PxOTheme = Theme;
                    productxOrder.Add(pxo);
                }
            }

            OrderVM.PxOList = productxOrder;
            OrderVM.UpdatePrice(new());

            window.Close();
        }

        public void Check(ProductXOrderModel product)
        {
            product.PxOSelected = !product.PxOSelected;
        }


        #endregion

    }
}
