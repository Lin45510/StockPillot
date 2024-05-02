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
    public class OrderVM : ViewModelBase
    {
        private readonly OrderModel _pagemodel = new();
        private readonly ProductXOrderModel _pxomodel = new();
        private readonly StockModel _stockmodel = new();

        #region Properties

        public int OrderID
        {
            get { return _pagemodel.OrderID; }
            set { _pagemodel.OrderID = value; OnPropertyChanged(); }
        }

        public string Clinte
        {
            get { return _pagemodel.Client; }
            set { _pagemodel.Client = value; OnPropertyChanged(); }
        }

        public string Endereço
        {
            get { return _pagemodel.Address; }
            set { _pagemodel.Address = value; OnPropertyChanged(); }
        }

        public string Telefone
        {
            get { return _pagemodel.Tell; }
            set { _pagemodel.Tell = value; OnPropertyChanged(); }
        }

        public DateTime Prazo
        {
            get { return _pagemodel.Term; }
            set { _pagemodel.Term = value; OnPropertyChanged(); }
        }

        private string[] Pagamento = { "Boleto", "Cartão", "Pix", "Dinheiro" };
        private string[] Entrega = { "Retirada", "Envio" };

        public double Total
        {
            get { return _pagemodel.Total; }
            set { _pagemodel.Total = value; OnPropertyChanged(); }
        }

        public List<ProductXOrderModel> PxOList
        {
            get { return _pagemodel.PxOList; }
            set { _pagemodel.PxOList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Window Properties

        public int EntregaIndex
        {
            get { return _pagemodel.EntregaIndex; }
            set { _pagemodel.EntregaIndex = value; OnPropertyChanged(); }
        }

        public int PagamentoIndex
        {
            get { return _pagemodel.PagamentoIndex; }
            set { _pagemodel.PagamentoIndex = value; OnPropertyChanged(); }
        }

        public OrdersVM ordersVM { get; set; }

        #endregion

        public OrderVM()
        {
            InsertCommand = new RelayCommand<Window>(Insert);
            WindowPxOCommand = new RelayCommand<object>(WindowPxO);
            RemoveProductCommand = new RelayCommand<ProductXOrderModel>(RemoveProduct);
            UpdatePriceCommand = new RelayCommand<object>(UpdatePrice);
            OrderPriceWindowCommand = new RelayCommand<object>(OrderPriceWindow);

            Prazo = DateTime.Now;
        }

        #region Commands

        public ICommand InsertCommand { get; set; }

        public ICommand WindowPxOCommand { get; set; }

        public ICommand RemoveProductCommand { get; set; }

        public ICommand UpdatePriceCommand { get; set; }

        public ICommand OrderPriceWindowCommand { get; set; }

        private void Insert(Window window)
        {
            List<DBProductXOrder> pxoList = new();

            foreach (ProductXOrderModel pxo in PxOList)
            {
                pxoList.Add(new()
                {
                    PxOID = pxo.PxOID,
                    PxOProductID = pxo.PxO_ProductCode,
                    PxOOrderID = OrderID,
                    PxOName = pxo.PxOName,
                    PxOAmmount = pxo.PxOAmmount,
                    PxOTheme = pxo.PxOTheme ?? "",
                });
            }

            DBOrder order = new()
            {
                OrderId = OrderID,
                Client = Clinte ?? "",
                Address = Endereço ?? "",
                Tell = Telefone ?? "",
                Term = DateOnly.FromDateTime(Prazo).ToString(),
                Delivery = Entrega[EntregaIndex],
                Payment = Pagamento[PagamentoIndex],
                Total = Total,
                ProductXOrder = pxoList
            };

            if (OrderID == 0)
            {
                _pagemodel.Insert(order);

                OrderModel Order = new()
                {
                    PxOList = PxOList,
                };

                _stockmodel.UpdateStock(Order);
                Clinte = "";
                Endereço = "";
                Telefone = "";
                Prazo = DateTime.Now;
                EntregaIndex = 0;
                PagamentoIndex = 0;
                Total = 0.0;

                PxOList = new() { };

                ordersVM.Orders = _pagemodel.GetOrders();
            }


            if (OrderID != 0)
            {
                _pagemodel.Update(order);
                ordersVM.Orders = _pagemodel.GetOrders();
                window.Close();
            }
        }

        private void WindowPxO(object obj)
        {

            var PxOWindow = new ProductXOrder
            {
                DataContext = new ProductXOrderVM
                {
                    PxOList = _pxomodel.GetPxOList(PxOList),
                    OrderVM = this
                }
            };
            PxOWindow.Show();
        }

        private void RemoveProduct(ProductXOrderModel pxo)
        {
            List<ProductXOrderModel> pxoList = new();

            foreach (ProductXOrderModel item in PxOList)
            {
                pxoList.Add(item);
            }

            pxoList.Remove(pxo);

            PxOList = pxoList;
        }

        private void OrderPriceWindow(object obj)
        {
            OrderPrice orderPrice = new()
            {
                DataContext = new OrderPriceVM()
                {
                    Price = Total,
                    orderVM = this
                }
            };


            orderPrice.Show();
        }

        public void UpdatePrice(object obj)
        {
            double price = 0.0;

            foreach (ProductXOrderModel product in PxOList)
            {
                price += product.PxOProductPrice * product.PxOAmmount;
            }

            Total = price;
        }

        #endregion

    }
}
