using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Resources;
using IngenieriaBosco.Front.Views;
using MaterialDesignThemes.Wpf;
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
            Items.Insert(new("Configuración", typeof(ConfigurationView), new ConfigurationViewModel(snackbarMessageQueue)));
            Items.SelectedItem = Items.Collection[0];
            Items.OnSelectionChanged = OnSelectionChanged;
        }
        public GridListModel<ItemModel> Items { get; }
        public ICommand RefreshCommand => new RelayCommand(_ => RefreshView(), _ => Items.SelectedItem != null);
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
    }
}
