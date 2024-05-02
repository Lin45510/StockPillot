using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class ProductXOrderModel
    {
        private readonly ProductDAO productDAO = new();
        private readonly PxODAO pxODAO = new();

        #region Properties

        public int PxOID { get; set; }

        public bool PxOSelected { get; set; }

        public int PxOAmmount { get; set; }

        public string PxOName { get; set; }

        public int PxO_ProductCode { get; set; }

        public int PxO_OrderCode { get; set; }

        public string PxOTheme { get; set; }

        public double PxOProductPrice { get; set; }

        public List<DBProduct> ProductsList { get; set; }

        public List<ProductXOrderModel> PxOList { get; set; }

        #endregion

        #region Functions

        public List<ProductXOrderModel> GetPxOList(List<ProductXOrderModel> pxo)
        {
            pxo ??= new();

            List<ProductXOrderModel> PxOList = new();

            List<DBProduct> ProductsList = productDAO.DataList();

            foreach (DBProduct product in ProductsList)
            {
                PxOList.Add(new()
                {
                    PxOID = product.ProductID,
                    PxOSelected = pxo.Exists(x => x.PxO_ProductCode == product.ProductID),
                    PxO_ProductCode = product.ProductID,
                    PxOName = product.ProductName,
                    PxOAmmount = product.PreviousOrderAmmount,
                    PxOTheme = product.ProductTheme,
                    PxOProductPrice = product.ProductPrice,
                });
            }

            return PxOList;
        }

        public List<ProductXOrderModel> GetPxO(int itemId, string itemCaller)
        {
            List<ProductXOrderModel> Prods = new();
            List<DBProductXOrder> productxOrders = pxODAO.DataList("SelectbyId", id: itemId, caller: itemCaller);
            List<DBProduct> products = productDAO.DataList();

            foreach (DBProductXOrder product in productxOrders)
            {
                if (products.Exists(x => x.ProductID == product.PxOProductID))
                {
                    Prods.Add(new()
                    {
                        PxOID = product.PxOID,
                        PxO_ProductCode = product.PxOProductID,
                        PxO_OrderCode = product.PxOOrderID,
                        PxOName = products.Find(x => x.ProductID == product.PxOProductID).ProductName,
                        PxOAmmount = product.PxOAmmount,
                        PxOTheme = product.PxOTheme,
                        PxOProductPrice = products.Find(x => x.ProductID == product.PxOProductID).ProductPrice,
                    });
                }
            }

            return Prods;
        }

        #endregion
    }
}
