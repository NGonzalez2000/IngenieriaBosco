using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Views;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    public class MainWindowsViewModel
    {
        public MainWindowsViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            Items = new();
            Items.Insert(new("Inicio", typeof(DashboardView), new DashboardViewModel(snackbarMessageQueue)));

            foreach (ItemModel item in GenerateItems(snackbarMessageQueue).ToList().OrderBy(x => x.Name))
            {
                Items.Insert(item);
            }
            Items.Insert(new("Configuración", typeof(ConfigurationView), new ConfigurationViewModel(snackbarMessageQueue)));

            Items.OnSelectionChanged = OnSelectionChanged;
            Items.SelectedItem = Items.Collection[0];
        }
        public GridListModel<ItemModel> Items { get; }
        public ICommand RefreshCommand => new RelayCommand(_ => RefreshView(), _ => Items.SelectedItem != null);
        public ICommand HomeCommand => new RelayCommand(_ => Items.SelectedItem = Items.Collection[0], _ => Items.Collection.Count > 0);
        private void RefreshView()
        {
            if (Items.SelectedItem!.Content is FrameworkElement view)
            {
                (view.DataContext as BaseViewModel)!.Load();
            }
        }
        private void OnSelectionChanged(object item)
        {
            if((item as ItemModel)!.Content is FrameworkElement view)
            {
                (view.DataContext as BaseViewModel)!.Load();
            }
        }

        private static IEnumerable<ItemModel> GenerateItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            yield return new ItemModel("Productos", typeof(ProductView), new ProductViewModel(snackbarMessageQueue));
            yield return new ItemModel("Categorías y marcas", typeof(CategoryView), new CategoryViewModel(snackbarMessageQueue));
        }
    }
}
