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
    class OrdersModel
    {
        private OrderDAO orderDAO = new();
        private PxODAO pxODAO = new();

        #region Properties

        public List<DBOrder> Orders { get; set; }

        #endregion

        #region Functions

        public List<DBOrder> GetOrders()
        {
            List<DBOrder> orders = orderDAO.DataList();
            foreach (DBOrder order in orders)
            {
                order.ProductXOrder = pxODAO.DataList("SelectbyId", id: order.OrderId, caller: "Orders");
            }

            return orders;
        }

        public void Delete(DBOrder order)
        {
            orderDAO.Delete(order);
        }

        #endregion
    }
}
