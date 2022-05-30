using System.ComponentModel;

namespace IngenieriaBosco.Core.Models.Filters
{
    public class ProductSortModel : SortModel
    {
        public ProductSortModel()
        {
            SortByOptions = new() { "Código", "Descripción", "Marca", "Stock" }; 
        }
        public override SortDescription OrderBy()
        {
            string selectedProperty = ParseSortBy();

            return new SortDescription(selectedProperty, IsAscending? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private string ParseSortBy()
        {
            if (SelectedSortBy == null || SelectedSortBy == SortByOptions![0]) return "Code";
            if (SelectedSortBy == SortByOptions[1]) return "Description";
            if (SelectedSortBy == SortByOptions[2]) return "Brand.Name";
            return "Stock";
        }
        
    }
}
