using StockPilot.Model;
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
    /// Lógica interna para Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product()
        {
            InitializeComponent();

            TxtBoxName.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Name TextBox Commandss

        private void TxtBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtBoxPrice.Focus();
            }
        }

        #endregion

        #region Price TextBox Commands

        private void TxtBoxPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBoxPrice.Text == "0")
            {
                TxtBoxPrice.SelectAll();
            }
        }

        private void TxtBoxPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtBoxPrice.Text == "0")
            {
                TxtBoxPrice.SelectAll();
            }
            if (TxtBoxPrice.Text.Length == 0)
            {
                TxtBoxPrice.Text = "0";
            }
        }

        private void TxtBoxPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TxtBoxPrice.IsFocused)
            {

                TxtBoxName.Focus();

                BtnConfirm.Command.Execute(ProductWindow);
            }
        }

        #endregion
    }
}
