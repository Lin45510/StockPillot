﻿using StockPilot.DataBase.DBModels;
using StockPilot.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockPilot.View
{
    /// <summary>
    /// Interação lógica para Orders.xam
    /// </summary>
    public partial class Orders : UserControl
    {
        public Orders()
        {
            InitializeComponent();
        }

        private void DGOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<DBOrder> order = DGOrders.ItemsSource.OfType<DBOrder>().ToList();

            ViewOrder viewOrder = new ViewOrder()
            {
                DataContext = new ViewOrderVM()
                {
                    order = order[DGOrders.SelectedIndex]
                }
            };

            viewOrder.Show();
        }
    }
}
