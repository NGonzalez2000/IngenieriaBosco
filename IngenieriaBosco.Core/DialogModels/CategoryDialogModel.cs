using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    public class CategoryDialogModel : BaseDialogModel
    {
        public CategoryModel Category { get; set; }
        public CategoryDialogModel(CategoryModel? category = null)
        {
            Category = new ();
            if (category != null) Category = (CategoryModel)category.ShallowCopy();
        }
        public async Task<CategoryModel?> NewCategory()
        {
            CategoryDialog dialog = new ("Nueva Categoría")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog,DialogIdentifiers.Category_Identifier,closingEventHandler: ClosingEventHandler_New);
            if(result) return Category;
            return null;
        }
        public async Task<CategoryModel?> EditCategory()
        {
            CategoryDialog dialog = new ("Editar Categoría")
            {
                DataContext = this
            };
            bool result = await DialogHosting(dialog, DialogIdentifiers.Category_Identifier, closingEventHandler: ClosingEventHandler_Edit);
            if (result) return Category;
            return null;
        }
        public void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;
            if(Category["Name"] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Category));
        }
        public void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                    parameter == false) return;
            if (Category["Name"] == string.Empty) return;

            eventArgs.Cancel();

            OnPropertyChanged(nameof(Category));
        }

    }
}
