using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Views;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.ViewModels
{
    public class ConfigurationViewModel : BaseViewModel
    {
        public GridListModel<ItemModel>? Items { get; set; }
        public ConfigurationViewModel(ISnackbarMessageQueue snackbarMessageQueue):base(snackbarMessageQueue)
        {
        }
        public override void Load()
        {
            Items = new();
            Items.Insert(new("Interfaz",typeof( InterfaceView ), new InterfaceViewModel(snackbarMessageQueue!)));
        }
    }
}
