using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ProviderSortModel : SortModel
    {
        public ProviderSortModel()
        {
            SortByOptions = new () { "Nombre" };
        }
        public override SortDescription OrderBy()
        {
            string selectedProperty = ParseSortBy();

            return new SortDescription(selectedProperty, IsAscending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }
        private string ParseSortBy()
        {
            return "Name";
        }
    }
}
