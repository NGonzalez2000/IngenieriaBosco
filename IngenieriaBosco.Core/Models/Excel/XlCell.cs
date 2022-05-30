using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Excel
{
    public class XlCell
    {
        public object Text { get; set; }
        public XlCell(object? text)
        {
            Text = string.Empty;
            if (text != null) Text = text;
        }
    }
}
