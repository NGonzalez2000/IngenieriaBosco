using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    public abstract class SortModel : INotify
    {
        public string? SelectedSortBy { get; set; }
        public List<string>? SortByOptions { get; set; }
        public bool IsAscending { get; set; }
        public abstract SortDescription OrderBy();
    }
}
