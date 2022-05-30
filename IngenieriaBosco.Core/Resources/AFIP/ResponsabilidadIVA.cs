using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Resources.AFIP
{
    internal class ResponsabilidadIVA
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public ResponsabilidadIVA(int codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
    }
}
