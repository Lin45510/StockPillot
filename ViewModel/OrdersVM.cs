using StockPilot.DataBase.DBModels;
using StockPilot.Model;
using StockPilot.Utilities;
using StockPilot.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class OrdersVM : ViewModelBase
    {
        private readonly OrdersModel _pageModel;
        private readonly ProductXOrderModel _pxoModel;

        #region Properties

        public List<DBOrder> Orders
        {
            get { return _pageModel.Orders; }
            set { _pageModel.Orders = value; OnPropertyChanged(); }
        }

        #endregion

        public OrdersVM()
        {
            _pageModel = new();
            _pxoModel = new();
            Orders = _pageModel.GetOrders();

            WindowOrderCommand = new RelayCommand<DBOrder>(WindowOrder);
            DeleteOrderCommand = new RelayCommand<DBOrder>(DeleteOrder);
        }

        #region Commands

        public ICommand WindowOrderCommand { get; set; }

        public ICommand DeleteOrderCommand { get; set; }

        private void WindowOrder(DBOrder order)
        {
            order ??= new DBOrder()
            {
                OrderId = 0,
                Client = "",
                Total = 0.0,
                ProductXOrder = new(),
                Address = "",
                Tell = ""
            };

            List<string> Pagamento = new() { "Boleto", "Cartão", "Pix", "Dinheiro" };
            List<string> Entrega = new() { "Retirada", "Envio" };

            var WindowOrder = new Order
            {
                DataContext = new OrderVM()
                {
                    OrderID = order.OrderId,
                    Clinte = order.Client,
                    Endereço = order.Address,
                    Telefone = order.Tell,
                    Total = order.Total,
                    Prazo = order.Term != null ? DateTime.Parse(order.Term, new CultureInfo("PT-BR")) : DateTime.Now,
                    EntregaIndex = order.OrderId != 0 ? Entrega.FindIndex(x => x == order.Delivery) : 0,
                    PagamentoIndex = order.OrderId != 0 ? Pagamento.FindIndex(x => x == order.Payment) : 0,
                    PxOList = _pxoModel.GetPxO(order.OrderId, "Orders"),
                    ordersVM = this
                }
            };
            WindowOrder.Show();
        }

        private void DeleteOrder(DBOrder order)
        {
            _pageModel.Delete(order);
            Orders = _pageModel.GetOrders();
        }

        #endregion 
    }
}
