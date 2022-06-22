using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Filters;
using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Models.Sells;
using IngenieriaBosco.Front.Windows;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class SellViewModel : BaseViewModel
    {
        public SellViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
        }

        public GridListModel<SellModel>? Sells { get; set; }
        public ICommand NewSell_Command => new RelayCommand(_ => NewSell_Execute());
        public ICommand ViewSell_Command => new RelayCommand(_ => ViewSell_Execute());
        public ICommand DeleteSell_Command => new RelayCommand(_ => DeleteSell_Execute());
        public ICommand SortCommand => new RelayCommand(_ => SortExecute());
        public ICommand FilterCommand => new RelayCommand(_ => FilterExecute());
        public SellSortModel? SellSort { get; set; }
        public SellFilterModel? SellFilter { get; set; }
        public override async void Load()
        {
            Sells = new(await DBSell.SelectAll());
            SellSort = new();
            SellFilter = new();
            OnPropertyChanged(nameof(Sells));
            OnPropertyChanged(nameof(SellSort));
            OnPropertyChanged(nameof(SellFilter));
        }
        private void NewSell_Execute()
        {
            SellWindowModel dataContext = new(snackbarMessageQueue!)
            {
                IsNewMode = true
            };
            SellWindow sellWindow = new()
            {
                DataContext = dataContext
            };
            sellWindow.Show();
        }
        private void ViewSell_Execute()
        {
            if (Sells == null || Sells.SelectedItem == null) return;
            SellWindowModel dataContext = new(snackbarMessageQueue!);
            dataContext.SetSell(Sells.SelectedItem);
            SellWindow sellWindow = new()
            {
                DataContext = dataContext
            };
            sellWindow.Show();
        }
        private async void DeleteSell_Execute()
        { 
            if(Sells == null || Sells.SelectedItem == null) return;
            if (Sells.SelectedItem.IsPayed)
            {
                await AcceptCall("No se puede eliminar la venta debido a que ya esta paga.", DialogIdentifiers.SellView_Identifier);
                return;
            }
            bool response = await AcceptCancelCall($"Seguro que desea eliminar la venta cuya factura es {Sells.SelectedItem.FactN} ?", DialogIdentifiers.SellView_Identifier);
            if (!response) return;

            try
            {
                await DBSell.Delete(Sells.SelectedItem);
            }
            catch (Exception ex)
            {
                await AcceptCall("Error al eliminar la venta de la base de datos." + ex.GetBaseException().Message, DialogIdentifiers.SellView_Identifier);
                return;
            }
            Sells.Delete(Sells.SelectedItem);

            ShowSnackbarMessage("Venta eliminada con éxito");
        }
        private void SortExecute()
            => Sells!.SortExecute(SellSort!.OrderBy());
        private void FilterExecute()
        {
            if (Sells is null) return;

            Sells.FilterExecute(SellFilter!.Filter);

            OnPropertyChanged(nameof(Sells));
        }
    }
}
