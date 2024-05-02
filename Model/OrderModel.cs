using StockPilot.DataBase.DAO;
using StockPilot.DataBase.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPilot.Model
{
    public class OrderModel
    {
        OrderDAO orderDAO = new();
        PxODAO pxoDAO = new();

        #region Properties

        public int OrderID { get; set; }

        public string Client { get; set; }

        public string Address { get; set; }

        public string Tell { get; set; }

        public DateTime Term { get; set; }

        public string Delivery { get; set; }

        public string Payment { get; set; }

        public double Total { get; set; }

        public int EntregaIndex { get; set; }

        public int PagamentoIndex { get; set; }

        #endregion

        #region Window Properties

        public List<ProductXOrderModel> PxOList { get; set; }

        #endregion

        #region Functions

        public List<DBOrder> GetOrders()
        {
            List<DBOrder> orders = orderDAO.DataList();
            foreach (DBOrder order in orders)
            {
                order.ProductXOrder = pxoDAO.DataList("SelectbyId", id: order.OrderId, caller: "Orders");
            }

            return orders;
        }

        public void Insert(DBOrder order)
        {
            orderDAO.Insert(order);
        }

        public void Update(DBOrder order)
        {
            orderDAO.Update(order);
        }

        #endregion
    }
}
