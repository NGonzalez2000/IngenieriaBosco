using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Excel
{
    public class XlRow : INotify
    {
        private bool isSelected;
        public int Id { get; set; }
        public List<XlCell> ExcelCells { get; set; }
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
        public XlRow(int RowNumber)
        {
            ExcelCells = new();
            Id = RowNumber;
        }
    }
}
