using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class ClientSortModel : SortModel
    {
        public ClientSortModel()
        {
            SortByOptions = new() { "Nombre", "Apellido" };
        }
        public override SortDescription OrderBy()
        {
            string selectedProperty = ParseSortBy();

            return new SortDescription(selectedProperty, IsAscending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }
        private string ParseSortBy()
        {
            if (SelectedSortBy == null || SelectedSortBy == SortByOptions![0]) return "FirstName";
            return "LastName";
        }
    }
}
