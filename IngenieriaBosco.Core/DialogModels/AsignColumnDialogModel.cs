using IngenieriaBosco.Front.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels
{
    public class AsignColumnDialogModel : BaseDialogModel
    {
        public List<string> ColumnNames { get; set; }
        public List<string>? SelectedColumns { get; set; }
        public AsignColumnDialogModel(int columnCount)
        {
            ColumnNames = new();
            for (int i = 0; i < columnCount; i++)
                ColumnNames.Add( string.Format("{0}", (char)(i + 'A')));
            
        }

        public async Task<List<string>?> GetAsignation(List<string> param)
        {
            SelectedColumns = new(param);
            AsignColumnDialog columnDialog = new()
            {
                DataContext = this
            };
            bool result = await DialogHosting(columnDialog, DialogIdentifiers.Excel_Identifier);

            if(!result) return null;

            return SelectedColumns;
        }
        public override void ClosingEventHandler_Edit(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        public override void ClosingEventHandler_New(object sender, DialogClosingEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
