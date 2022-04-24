using IngenieriaBosco.Core.Models;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Resources;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ICommand AddProductCommand => new RelayCommand(_ => AddProductExecute());
        public GridListModel<ProductModel>? Products { get; set; }
        public ProductFilterModel? ProductFilter { get; set; } 
        public ProductSortModel? ProductSort { get; set; }
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        

        public ProductViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {

        }
        public async override void Load()
        {
            Products = new();
            ProductFilter = new ();
            ProductSort = new();
        }
        private async void AddProductExecute()
        {

        }
        private void FilterExecute()
            => Products!.FilterExecute(ProductFilter!.Filter);

        private void SortExecute()
            => Products!.SortExecute(ProductSort!.OrderBy());
    }
}
