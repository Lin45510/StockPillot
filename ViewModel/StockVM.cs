using StockPilot.DataBase.Models;
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
        public List<DataBase.Models.Material> Materials
        {
            get { return _pageModel.Materials; }
            set { _pageModel.Materials = value; OnPropertyChanged(); }
        }
        public StockVM()
        {
            _pageModel = new StockModel();
            WindowMaterialCommand = new RelayCommand<object>(WindowMaterial);
        }

        #region Commnands
        public ICommand WindowMaterialCommand { get; set; }

        private void WindowMaterial(object obj)
        {
            var MaterialWindow = new View.Material
            {
                DataContext = new MaterialVM
                {
                    WindowFunc = 0
                }
            };
            MaterialWindow.Show();
        }
        #endregion
    }
}

