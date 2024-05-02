using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StockPilot.Model
{
    public class StockModel
    {
        private readonly MaterialDAO MaterialDAO = new();

        #region Window Properties

        public List<DBMaterial>? Materials;

        #endregion

        #region Functions

        public List<DBMaterial> MaterialsList()
        {
            return MaterialDAO.DataList();
        }

        public void DeleteMaterial(DBMaterial Material)
        {
            MaterialDAO.Delete(Material);
        }

        public void UpdateStock(OrderModel order, string action = "add")
        {
            ProductModel productModel = new();
            foreach (ProductXOrderModel pxo in order.PxOList)
            {
                List<DBProduct> DBproducts = productModel.GetProductsList();
                List<ProductModel> products = new();
                MxPDAO mxpDAO = new();

                foreach (DBProduct product in DBproducts)
                {
                    List<DBMaterialXProduct> mxpList = mxpDAO.DataList("SelectbyId", id: product.ProductID, caller: "Products");
                    List<MaterialXProductModel> materialXProducts = new();

                    foreach (DBMaterialXProduct materialXProductModel in mxpList)
                    {
                        materialXProducts.Add(new()
                        {
                            MxPId = materialXProductModel.MxPId,
                            MxP_MatCod = materialXProductModel.MxPMaterialID,
                            MXPAmmount = materialXProductModel.MxPAmmount,
                        });
                    }

                    products.Add(new ProductModel()
                    {
                        productName = product.ProductName,
                        productPrice = product.ProductPrice,
                        MxPList = materialXProducts,
                    });

                    foreach (DBMaterialXProduct mxp in product.MaterialXProducts)
                    {
                        double needAmmount = mxp.MxPAmmount * pxo.PxOAmmount;

                        DBMaterial material = MaterialDAO.DataList("SelectbyID", id: mxp.MxPMaterialID)[0];

                        material.MatAmmount = Convert.ToInt32(Math.Ceiling(material.MatAmmount - needAmmount));

                        MaterialDAO.Update(material);
                    }
                }

            }
        }

        #endregion
    }
}
