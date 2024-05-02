using StockPilot.DataBase.DBModels;
using StockPilot.Model;
using StockPilot.Utilities;
using StockPilot.View;
using StockPilot.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class StockVM : ViewModelBase
    {
        private readonly StockModel _pageModel;

        #region Properties

        public List<DBMaterial> Materials
        {
            get { return _pageModel.Materials; }
            set { _pageModel.Materials = value; OnPropertyChanged(); }
        }

        #endregion

        public StockVM()
        {
            _pageModel = new StockModel();
            Materials = _pageModel.MaterialsList();

            WindowMaterialCommand = new RelayCommand<DBMaterial>(WindowMaterial);
            DeleteMaterialCommand = new RelayCommand<DBMaterial>(DeleteMaterial);
        }

        #region Commnands
        public ICommand WindowMaterialCommand { get; set; }
        public ICommand DeleteMaterialCommand { get; set; }

        private void WindowMaterial(DBMaterial Material)
        {
            Material ??= new DBMaterial() { MatID = 0, MatName = "", MatAmmount = 0 };

            var MaterialWindow = new Material
            {
                DataContext = new MaterialVM
                {
                    Title = Material.MatID == 0 ? "Adicionar Material" : "Editar Material",
                    WindowMaterial = Material,
                    MaterialName = Material.MatName,
                    MaterialAmmount = Material.MatAmmount.ToString(),
                    StockVM = this,
                }
            };
            MaterialWindow.Show();
        }

        private void DeleteMaterial(DBMaterial Material)
        {
            _pageModel.DeleteMaterial(Material);

            Materials = _pageModel.MaterialsList();
        }
        #endregion
    }
}

