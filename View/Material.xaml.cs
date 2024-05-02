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
    /// Lógica interna para Material.xaml
    /// </summary>
    public partial class Material : Window
    {
        public Material()
        {
            InitializeComponent();

            TxtBoxName.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Name TextBox Commands

        private void TxtBoxName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtBoxAmmount.Focus();
            }
        }

        #endregion

        #region Ammount TextBox Commands

        private void TxtBoxAmmount_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBoxAmmount.Text == "0")
            {
                TxtBoxAmmount.SelectAll();
            }
        }

        private void TxtBoxAmmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtBoxAmmount.Text == "0")
            {
                TxtBoxAmmount.SelectAll();
            }
            if (TxtBoxAmmount.Text.Length == 0)
            {
                TxtBoxAmmount.Text = "0";
            }
        }

        private void TxtBoxAmmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TxtBoxAmmount.IsFocused)
            {

                TxtBoxName.Focus();

                BtnConfirm.Command.Execute(MaterialWindow);
            }
        }

        #endregion
    }
}
