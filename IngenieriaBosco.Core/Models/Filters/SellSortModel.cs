using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    internal class SellSortModel : SortModel
    {
        public SellSortModel()
        {
            SortByOptions = new() { "Nº Factura", "Fecha", "Estado" };
        }
        public override SortDescription OrderBy()
        {
            string selectedProperty = ParseSortBy();

            return new SortDescription(selectedProperty, IsAscending ? ListSortDirection.Ascending : ListSortDirection.Descending);
        }
        private string ParseSortBy()
        {
            if (SelectedSortBy == null || SelectedSortBy == SortByOptions![0]) return "FactN";
            if (SelectedSortBy == "Fecha") return "Date";
            return "IsPayed";
        }
    }
}
