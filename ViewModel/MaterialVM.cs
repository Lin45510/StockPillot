using StockPilot.Utilities;
using StockPilot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StockPilot.View;
using StockPilot.DataBase.DBModels;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class MaterialVM : ViewModelBase
    {
        private readonly MaterialModel _pageModel;

        #region Page Properties

        public DBMaterial WindowMaterial { get; set; }
        public string? Title { get; set; }
        public string? MaterialName { get { return _pageModel.MaterialName; } set {; _pageModel.MaterialName = value; OnPropertyChanged(); } }
        public string? MaterialAmmount { get { return _pageModel.MaterialAmount.ToString(); } set { _pageModel.MaterialAmount = int.Parse(value); OnPropertyChanged(); } }

        public StockVM StockVM { get; set; }

        #endregion

        public MaterialVM()
        {
            _pageModel = new MaterialModel();

            PageFunctionCommand = new RelayCommand<Window>(PageFunction);
        }
        #region Commnands

        public ICommand PageFunctionCommand { get; set; }

        private void PageFunction(Window window)
        {
            if (int.TryParse(MaterialAmmount, out int ammount))
            {
                DBMaterial Material = new()
                {
                    MatID = WindowMaterial.MatID,
                    MatName = MaterialName,
                    MatAmmount = ammount,
                };

                if (WindowMaterial.MatID == 0)
                {
                    _pageModel.Insert(Material);

                    MaterialName = "";
                    MaterialAmmount = "0";

                    StockVM.Materials = _pageModel.GetMaterialsList();
                }
                else if (WindowMaterial.MatID > 0)
                {
                    _pageModel.Update(Material);

                    StockVM.Materials = _pageModel.GetMaterialsList();
                    window.Close();
                }
            }
            else
            {
                MessageBox.Show("A Quantidade Deve Ser Um Numero");
            }
        }

        #endregion
    }
}
