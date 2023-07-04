using StockPilot.Utilities;
using StockPilot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StockPilot.View;

namespace StockPilot.ViewModel
{
    public class MaterialVM : ViewModelBase
    {
        private readonly MaterialModel _pageModel;
        public int? WindowFunc { get; set; }
        public string? Title { get { return WindowFunc == 0 ? "Novo Material" : WindowFunc == 1 ? "Editar Material" : ""; } set {; } }
        public string? BtnContent { get { return WindowFunc == 0 ? "Adicionar" : WindowFunc == 1 ? "Editar" : ""; } set {; } }
        public MaterialVM()
        {
            _pageModel = new MaterialModel();
        }
        #region Functions
        private void PageFction()
        {
            MessageBox.Show(WindowFunc.ToString());
        }
        #endregion
    }
}
