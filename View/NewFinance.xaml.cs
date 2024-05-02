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
    /// Lógica interna para NewFinance.xaml
    /// </summary>
    public partial class NewFinance : Window
    {
        public NewFinance()
        {
            InitializeComponent();
            TxtValue.Focus();
        }

        #region TxtValue Functions

        private void TxtValue_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtDescricao.Focus();
            }
        }

        #endregion

        #region TxtDescricao Functions

        private void TxtDescricao_KeyUp(object sender, KeyEventArgs e)
        {

        }

        #endregion

        #region BtnClose Functions

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}
