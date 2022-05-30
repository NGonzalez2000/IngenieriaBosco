using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ProductOrderSortModel : SortModel
    {
        public ProductOrderSortModel()
        {
            SortByOptions = new() { "N°", "Proveedor", "Fecha" };
        }

        public override SortDescription OrderBy()
        {
            string selectedProperty = ParseSortBy();

            return new SortDescription(selectedProperty, IsAscending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }

        private string ParseSortBy()
        {
            if (SelectedSortBy == null || SelectedSortBy == SortByOptions![0]) return "Id";
            if (SelectedSortBy == SortByOptions[1]) return "Provider.Name";
            return "SortDate";
        }
    }
}
