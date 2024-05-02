using StockPilot.DataBase.DBModels;
using StockPilot.Model;
using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    public class MaterialXProductVM : ViewModelBase
    {
        private readonly MaterialXProductModel _pageModel = new();

        #region Properties

        public List<MaterialXProductModel> MxPList
        {
            get { return _pageModel.MxPList; }
            set { _pageModel.MxPList = value; OnPropertyChanged(); }
        }

        public ProductVM ProductVM { get; set; }

        #endregion

        public MaterialXProductVM()
        {

            ConfirmCommand = new RelayCommand<Window>(Confirm);
            CheckCommand = new RelayCommand<MaterialXProductModel>(Check);
        }

        #region Functions

        public ICommand ConfirmCommand { get; set; }

        public ICommand CheckCommand { get; set; }

        public void Confirm(Window window)
        {
            List<MaterialXProductModel> materialXProductModels = new();

            foreach (MaterialXProductModel mxp in MxPList)
            {
                if (mxp.MxPSelected)
                {
                    mxp.MXPAmmount = 1;
                    materialXProductModels.Add(mxp);
                }
            }

            ProductVM.MaterialXProductsList = materialXProductModels;

            window.Close();
        }

        public void Check(MaterialXProductModel material)
        {
            material.MxPSelected = !material.MxPSelected;
        }

        #endregion
    }
}
