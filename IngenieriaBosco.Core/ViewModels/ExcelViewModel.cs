using IngenieriaBosco.Core.DialogModels;
using IngenieriaBosco.Core.Models.Excel;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IngenieriaBosco.Core.ViewModels
{
    internal class ExcelViewModel : BaseViewModel
    {
        private List<string> asignations;
        public XlSheet? Sheet { get; set; }
        public List<string>? SheetNames { get; set; }
        public int SheetIndx { get; set; }
        private string? path;
        public string? Path
        {
            get => path;
            set => SetProperty(ref path, value);
        }
        public ICommand SelectExcelFile_Command => new RelayCommand(_ => SelectExcelFile_Execute());
        public ICommand ReadExcelFile_Command => new RelayCommand(ReadExcelFile_Execute,_ => ReadExcelFile_Enable());
        public ICommand AsignColumns_Command => new RelayCommand(AsignColumns_Execute);
        public ICommand ImportProducts_Command => new RelayCommand(_ => ImportProducts_Execute(), _ => Sheet!=null && Sheet.SelectRows_Enamble() && EnableAsignations());

        public ExcelViewModel(ISnackbarMessageQueue snackbarMessageQueue) : base(snackbarMessageQueue)
        {
            asignations = new();
            for (int i = 0; i < 9; i++) asignations.Add(string.Empty);
        }

        public override void Load()
        {
        }
        private bool EnableAsignations()
        {
            for(int i = 0; i < asignations.Count; i++)
            {
                if (!string.IsNullOrEmpty(asignations[i])) return true;
            }
            return false;
        }
        private void SelectExcelFile_Execute()
        {
            GetExcelPath();

            if (string.IsNullOrEmpty(Path)) return;
            OpenExcelFile();
        }
        private async void ReadExcelFile_Execute(object? param)
        {
            if (param == null || SheetIndx == -1) return;

            GridView grid = (GridView)param;
            grid.Columns.Clear();

            asignations = new();
            for (int i = 0; i < 9; i++) asignations.Add(string.Empty);

            Sheet = new();
            OnPropertyChanged(nameof(Sheet));

            using ExcelPackage package = new(Path);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[SheetIndx];

            Sheet.RowCount = worksheet.Dimension.End.Row;
            Sheet.ColumnCount = worksheet.Dimension.End.Column;
            
            for (int i = 1; i <= Sheet.RowCount; i++)
            {
                Sheet.AddRow(i);
                for (int j = 1; j <= Sheet.ColumnCount; j++)
                {
                    Sheet.AddCell(i - 1, worksheet.Cells[i, j].Value);
                }
            }

            grid.AllowsColumnReorder = false;

            GridViewColumn column = new();
            
            column.DisplayMemberBinding = new Binding("Id");
            grid.Columns.Add(column);
           

            

            for (int j = 0; j < Sheet.ColumnCount; j++)
            {
                column = new();
                column.DisplayMemberBinding = new Binding($"ExcelCells[{j}].Text");
                column.Header = string.Format("{0}", (char)(j + 'A'));
                grid.Columns.Add(column);
            }
            await Task.Run(() => OnPropertyChanged(nameof(Sheet)));
            await Task.Run(() => OnPropertyChanged(nameof(Sheet.Rows)));
        }
        private async void AsignColumns_Execute(object? param)
        {
            if (param == null || Sheet == null) return;
            AsignColumnDialogModel dialogModel = new(Sheet.ColumnCount);
            List<string>? Asignation = await dialogModel.GetAsignation(asignations);

            if (Asignation == null) return;

            asignations = new (Asignation);

            GridView grid = (GridView)param;

            for (int i = 0; i < grid.Columns.Count - 1; i++)
                grid.Columns[i + 1].Header = string.Format("{0}", (char)(i + 'A'));

            for (int i = 0; i < Asignation.Count; i++)
                if(!string.IsNullOrEmpty(Asignation[i]))
                    grid.Columns[Asignation[i][0] - 'A' + 1].Header = GetHeader(i);
            
               
        }
        private async void ImportProducts_Execute()
        {
            if (Sheet == null) return;
            int range = Sheet.LastRow - Sheet.FirstRow;

            List<ExcelProduct_ImportModel> products = new();

            for (int i = 0; i <= range; i++)
                products.Add(new());
            
            for(int propN = 0; propN < asignations.Count; propN++)
            {
                if (!string.IsNullOrEmpty(asignations[propN]))
                {
                    for(int rowNumber = 0; rowNumber <= range; rowNumber++)
                    {
                        if (propN == 0) await Task.Run(() => products[rowNumber].SetCode((string)Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text));
                        if (propN == 1) products[rowNumber].Product.Description = (string)Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text;
                        if (propN == 2) await Task.Run(() => products[rowNumber].SetCategory((string)Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text));
                        if (propN == 3) await Task.Run(() => products[rowNumber].SetBrand((string)Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text));
                        if (propN == 4) products[rowNumber].Product.ListingPrice = Convert.ToDecimal(Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text);
                        if (propN == 5) products[rowNumber].Product.RetailPrice = Convert.ToDecimal(Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text);
                        if (propN == 6) products[rowNumber].Product.WholesalerPrice = Convert.ToDecimal(Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text);
                        if (propN == 7) products[rowNumber].Product.Stock = int.TryParse(Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text.ToString(), out int val)? val : 0;
                        if (propN == 8) products[rowNumber].Product.WarningStock = Convert.ToInt32(Sheet.Rows![Sheet.FirstRow + rowNumber - 1].ExcelCells[asignations[propN][0] - 'A'].Text);
                    }
                }
            }

            bool flag = false;
            for(int i = 0; i < products.Count; i++)
                flag |= (products[i].ExceptionFlag);

            if (flag)
            {
                MessageBox.Show("Ocurrio un problema mientras se procesaba la informacion\n\nEl proceso se cancelará");
                return;
            }
            ExcelImportDialogModel dialogModel = new(products);

            IEnumerable<ExcelResult_ImportModel>? list = await dialogModel.Import();
            if (list is null) return;

            List<ExcelResult_ImportModel> result = new(list);
            int snew = 0;
            int sedit = 0;
            int errors = 0;
            Dictionary<string, int> errorsName = new();
            foreach (ExcelResult_ImportModel item in list)
            {
                if (item.result == null)
                {
                    errors++;
                    if(!errorsName.ContainsKey(item.Message))errorsName.Add(item.Message, 0);
                    errorsName[item.Message]++;
                    continue;
                }
                snew += (bool)item.result ? 1 : 0;
                sedit += (bool)!item.result ? 1 : 0;
            }
            string message = $"Se crearon {snew} productos con éxito\nSe editaron {sedit} productos con éxito\nOcurrieron {errors} errores.";
            if(errors > 0)
            {
                message = message + "\n\nMotivos:";
                foreach (string key in errorsName.Keys)
                {
                    message = message + $"\n{errorsName[key]} " + key;
                }
            }

            await AcceptCall(message, DialogIdentifiers.Excel_Identifier);
            
            
        }
        private bool ReadExcelFile_Enable()
            => !(SheetIndx == -1 && Path == string.Empty);
        private static string GetHeader(int propN)
        {
            return propN switch
            {
                0 => "Código",
                1 => "Descripción",
                2 => "Categoría",
                3 => "Marca",
                4 => "Precio de Lista",
                5 => "Precio Minorísta",
                6 => "Precio Mayorista",
                7 => "Stock",
                8 => "Stock Mínimo",
                _ => throw new NotImplementedException()
            };
        }


        private void GetExcelPath()
        {
            OpenFileDialog fileDialog = new();
            fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            bool? result = fileDialog.ShowDialog();

            if (result is null) return;

            if ((bool)result) Path = fileDialog.FileName;

        }
        private void OpenExcelFile()
        {
            using ExcelPackage package = new(path);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            SheetIndx = -1;
            SheetNames = new();

            foreach (ExcelWorksheet ws in package.Workbook.Worksheets)
                SheetNames.Add(ws.Name);

            if (SheetNames.Count == 0) return;

            SheetIndx = 0;

            OnPropertyChanged(nameof(SheetNames));
            OnPropertyChanged(nameof(SheetIndx));
        }
    
        
    }
}
