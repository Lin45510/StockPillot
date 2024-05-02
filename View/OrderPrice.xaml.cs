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
using System.Windows.Shapes;

namespace StockPilot.View
{
    /// <summary>
    /// Interaction logic for OrderPrice.xaml
    /// </summary>
    public partial class OrderPrice : Window
    {
        public OrderPrice()
        {
            InitializeComponent();
            TxtPrice.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TxtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnConfirm.Focus();
                BtnConfirm.Command.Execute(OrderPriceWindow);
            }
        }

        private void TxtPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPrice.Text == "0")
            {
                TxtPrice.SelectAll();
            }
        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPrice.Text == "0")
            {
                TxtPrice.SelectAll();
            }
            if (TxtPrice.Text.Length == 0)
            {
                TxtPrice.Text = "0";
            }
        }
    }
}
