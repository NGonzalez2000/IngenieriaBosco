using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Filters
{
    public abstract class FilterModel : INotify
    {
        public abstract bool Filter(object o);
    }
}
