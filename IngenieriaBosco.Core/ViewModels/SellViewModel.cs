using IngenieriaBosco.Core.Models.Generics;
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

        public ICommand NewSell_Command => new RelayCommand(_ => NewSell_Execute());
        public override void Load()
        {
        }
        private void NewSell_Execute()
        {
            SellWindowModel dataContext = new(snackbarMessageQueue!);
            dataContext.IsNewMode = true;
            SellWindow sellWindow = new()
            {
                DataContext = dataContext
            };
            sellWindow.Show();
        }
    }
}
