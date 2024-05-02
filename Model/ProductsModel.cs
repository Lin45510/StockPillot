using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class ProductsModel
    {
        private readonly ProductDAO productDAO = new();
        private readonly MxPDAO mxPDAO = new();

        #region Window Properties

        public List<DBProduct>? Products = new();

        #endregion

        #region Functions

        public List<DBProduct> ProductsList()
        {
            List<DBProduct> products = productDAO.DataList();
            foreach (DBProduct product in products)
            {
                product.MaterialXProducts = mxPDAO.DataList("SelectbyId", id: product.ProductID, caller: "Products");
            }

            return products ?? new();
        }

        public void DeleteProduct(DBProduct product)
        {
            productDAO.Delete(product);
        }

        #endregion
    }
}
