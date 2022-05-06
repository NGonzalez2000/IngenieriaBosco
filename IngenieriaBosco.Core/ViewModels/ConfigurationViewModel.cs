using IngenieriaBosco.Core.Models.Generics;
using IngenieriaBosco.Front.Views;
using MaterialDesignThemes.Wpf;

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
