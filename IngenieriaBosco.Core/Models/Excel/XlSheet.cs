using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace IngenieriaBosco.Core.Models.Excel
{
    public class XlSheet : INotify, IDataErrorInfo
    {
        /// <summary>
        /// Rows are indexed with base 1
        /// </summary>
        public List<XlRow>? Rows { get; set; }
        private int firstRow;
        private int lastRow;
        private int rowCount;

        public int FirstRow
        {
            get { return firstRow; }
            set { SetProperty(ref firstRow, value); }
        }
        public int LastRow
        {
            get { return lastRow; }
            set { SetProperty(ref lastRow, value); }
        }
        public int RowCount
        {
            get { return rowCount; }
            set
            {
                rowCount = value;
                LastRow = value;
                FirstRow = 1;
            }
        }
        public int ColumnCount { get; set; }
        public ICommand SelectRowsCommand => new RelayCommand(SelectRowsExecute,_ => SelectRows_Enamble());
        public void AddRow(int RowIndex)
        {
            if (Rows == null) Rows = new();
            Rows.Add(new(RowIndex));
        }
        public void AddCell(int row,object? content)
        {
            Rows![row].ExcelCells.Add(new(content));
        }
        private void SelectRowsExecute(object? param)
        {
            if(Rows is null) return;

            for (int i = 0; i < RowCount; i++)
                Rows[i].IsSelected = IsInRange(i + 1);

            if (param is null) return;
            ListView listView = (ListView)param;
            listView.ScrollIntoView(Rows[firstRow - 1]);
        }
        public bool SelectRows_Enamble()
            => Rows != null &&
                this[nameof(LastRow)] == string.Empty && 
                this[nameof(FirstRow)] == string.Empty;
        private bool IsInRange(int indx)
            => !(indx < firstRow || indx > lastRow);

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                if(columnName == nameof(FirstRow))
                {
                    if (FirstRow <= 0) return "El valor debe ser mayor a 0";
                    if (FirstRow > LastRow) return "El valor de comienzo debe ser menor al de finalizar";
                    if (FirstRow > RowCount) return "El valor esta fuera de rango";
                }
                if (columnName == nameof(LastRow))
                {
                    if (LastRow < 0) return "El valor debe ser mayor a 0";
                    if (FirstRow > LastRow) return "El valor de finalizar debe ser mayor al de comienzo";
                    if (LastRow > RowCount) return "El valor esta fuera de rango";
                }
                return string.Empty;
            }
        }


    }
}
