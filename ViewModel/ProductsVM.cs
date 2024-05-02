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
    public class ProductsVM : ViewModelBase
    {
        private readonly ProductsModel _pageModel;
        private readonly MaterialXProductModel _mxpModel = new();

        #region Properties

        public List<DBProduct> Products
        {
            get { return _pageModel.Products; }
            set { _pageModel.Products = value; OnPropertyChanged(); }
        }

        #endregion

        public ProductsVM()
        {
            _pageModel = new ProductsModel();

            Products = _pageModel.ProductsList();

            WindowProductCommand = new RelayCommand<DBProduct>(WindowProduct);
            DeleteProductCommand = new RelayCommand<DBProduct>(DeleteProduct);
        }

        #region Commands

        public ICommand WindowProductCommand { get; set; }

        public ICommand DeleteProductCommand { get; set; }

        private void WindowProduct(DBProduct product)
        {
            product ??= new DBProduct() { ProductID = 0, ProductName = "", ProductPrice = 0.0, MaterialXProducts = new() };

            var ProductWindow = new Product
            {
                DataContext = new ProductVM
                {
                    Title = product.ProductID == 0 ? "Adicionar Produto" : "Editar Produto",
                    Product = product,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice.ToString(),
                    MaterialXProductsList = _mxpModel.GetMxP(product.ProductID, "Products"),
                    ProductsVM = this
                }
            };
            ProductWindow.Show();
        }

        private void DeleteProduct(DBProduct product)
        {
            _pageModel.DeleteProduct(product);

            Products = _pageModel.ProductsList();
        }

        #endregion
    }
}
