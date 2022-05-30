using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    public abstract class BaseModel : INotify
    {
        public object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
