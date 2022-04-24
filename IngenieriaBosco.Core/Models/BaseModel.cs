using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models
{
    public abstract class BaseModel
    {
        public object ShallowCopy()
        {
            return MemberwiseClone();
        }
    }
}
