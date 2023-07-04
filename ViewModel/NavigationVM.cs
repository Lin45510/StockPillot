using StockPilot.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StockPilot.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object? _currentView;

        public object? CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand StockCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand FinanceCommand { get; set; }

        private void Stock(object obj) => CurrentView = new StockVM();
        private void Products(object obj) => CurrentView = new ProductsVM();
        private void Orders(object obj) => CurrentView = new OrdersVM();
        private void Finance(object obj) => CurrentView = new FinanceVM();

        public NavigationVM()
        {
            StockCommand = new RelayCommand<object>(Stock);
            ProductsCommand = new RelayCommand<object>(Products);
            OrdersCommand = new RelayCommand<object>(Orders);
            FinanceCommand = new RelayCommand<object>(Finance);

            CurrentView = new StockVM();
        }
    }
}
