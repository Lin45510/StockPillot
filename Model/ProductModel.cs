using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class ProductModel
    {
        private readonly ProductDAO productDAO = new();

        private readonly MxPDAO mxPDAO = new();

        #region Properties

        public string? productName { get; set; }

        public double productPrice { get; set; }

        #endregion

        #region Window Properties

        public List<MaterialXProductModel> MxPList { get; set; }

        #endregion

        #region Functions

        public List<DBProduct> GetProductsList()
        {
            List<DBProduct> products = productDAO.DataList();
            foreach (DBProduct product in products)
            {
                product.MaterialXProducts = mxPDAO.DataList("SelectbyId", id: product.ProductID, caller: "Products");
            }

            return products ?? new();
        }

        public void Insert(DBProduct product)
        {
            productDAO.Insert(product);
        }

        public void Update(DBProduct product)
        {
            productDAO.Update(product);
        }

        #endregion
    }
}
