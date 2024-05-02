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
using System.Windows.Shapes;

namespace StockPilot.View
{
    /// <summary>
    /// Lógica interna para Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        public Order()
        {
            InitializeComponent();
            TxtCliente.Focus();
        }

        #region TxtCliente

        private void TxtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtTelefone.Focus();
            }
        }

        #endregion

        #region TxtTelefone

        private void TxtTelefone_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TxtEndereco.Focus();
            }
        }

        #endregion

        #region Datagrid

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            UpdatePriceBtn.Focus();
            UpdatePriceBtn.Command.Execute(null);
        }

        #endregion

    }
}
