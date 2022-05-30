using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Core.Models.Sells;
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
        public GridListModel<SellOrderModel>? Sells { get; set; }
        public override void Load()
        {
            Sells = new();
        }
        private void NewSell_Execute()
        {

        }
    }
}
