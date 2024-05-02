using StockPilot.DataBase.DBModels;
using StockPilot.Model;
using StockPilot.Utilities;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class ProductVM : ViewModelBase
    {
        private readonly ProductModel _pageModel;
        private readonly MaterialXProductModel _mxpModel = new();

        #region Properties

        public string Title { get; set; }

        public ProductsVM ProductsVM { get; set; } = new ProductsVM();

        public DBProduct Product { get; set; }

        public string ProductName
        {
            get { return _pageModel.productName; }
            set { _pageModel.productName = value; OnPropertyChanged(); }
        }

        public string ProductPrice
        {
            get { return _pageModel.productPrice.ToString(); }
            set { _pageModel.productPrice = double.Parse(value); OnPropertyChanged(); }
        }

        public List<MaterialXProductModel> MaterialXProductsList
        {
            get { return _pageModel.MxPList; }
            set { _pageModel.MxPList = value; OnPropertyChanged(); }
        }

        #endregion

        public ProductVM()
        {
            _pageModel = new();

            PageFunctionCommand = new RelayCommand<Window>(PageFunction);
            WindowMxPCommnad = new RelayCommand<object>(WindowMxP);
            RemoveMaterialCommand = new RelayCommand<MaterialXProductModel>(RemoveMaterial);
        }

        #region Commands

        public ICommand PageFunctionCommand { get; set; }

        public ICommand WindowMxPCommnad { get; set; }

        public ICommand RemoveMaterialCommand { get; set; }

        private void PageFunction(Window window)
        {
            if (double.TryParse(ProductPrice, out double price))
            {
                List<DBMaterialXProduct> mxpList = new();

                foreach (MaterialXProductModel material in MaterialXProductsList)
                {
                    mxpList.Add(new()
                    {
                        MxPId = material.MxPId,
                        MxPMaterialID = material.MxP_MatCod,
                        MxPProductID = Product.ProductID,
                        MxPAmmount = material.MXPAmmount,
                    });
                }

                DBProduct product = new()
                {
                    ProductID = Product.ProductID,
                    ProductName = ProductName,
                    ProductPrice = price,
                    MaterialXProducts = mxpList
                };

                if (Product.ProductID == 0)
                {
                    _pageModel.Insert(product);

                    ProductName = "";
                    ProductPrice = "0";
                    MaterialXProductsList = new();

                    ProductsVM.Products = _pageModel.GetProductsList();
                }
                else if (Product.ProductID > 0)
                {
                    _pageModel.Update(product);

                    ProductsVM.Products = _pageModel.GetProductsList();
                    window.Close();
                }
            }
            else
            {
                MessageBox.Show("A Quantidade Deve Ser Um Numero");
            }
        }

        private void WindowMxP(object obj)
        {
            var MxPWindow = new MaterialXProduct
            {
                DataContext = new MaterialXProductVM
                {
                    MxPList = _mxpModel.GetMxPList(MaterialXProductsList),
                    ProductVM = this
                }
            };
            MxPWindow.Show();
        }

        private void RemoveMaterial(MaterialXProductModel mxp)
        {
            List<MaterialXProductModel> mxpList = new();

            foreach (MaterialXProductModel item in MaterialXProductsList)
            {
                mxpList.Add(item);
            }

            mxpList.Remove(mxp);

            MaterialXProductsList = mxpList;
        }

        #endregion
    }
}
