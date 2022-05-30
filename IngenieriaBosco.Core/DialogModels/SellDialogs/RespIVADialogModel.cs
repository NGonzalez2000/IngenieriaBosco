using IngenieriaBosco.Core.Resources.AFIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.DialogModels.SellDialogs
{
    internal class RespIVADialogModel : INotify
    {
        private ResponsabilidadIVA selectedRespIVA;
        private char voucherType;
        private bool canContinue;
        public List<ResponsabilidadIVA> RespIVAList { get; set; }
        public bool CanContinue
        {
            get => canContinue;
            set => SetProperty(ref canContinue, value);
        }
        public char VoucherType
        {
            get => voucherType;
            set => SetProperty(ref voucherType, value);
        }
        public ResponsabilidadIVA SelectedRespIVA
        {
            get => selectedRespIVA;
            set
            {
                if(SetProperty(ref selectedRespIVA, value))
                    EvaluateResp();
            }
        }
        public RespIVADialogModel()
        {
            RespIVAList = new()
            {
                new(1,"IVA Responsable Inscripto"),
                new(2,"IVA Responsable no Inscripto"),
                new(3,"IVA no Responsable"),
                new(4,"IVA Sujeto Exento"),
                new(5,"Consumidor Final"),
                new(6,"Responsable Monotributo"),
                new(7,"Sujeto no Categorizado"),
                new(8,"Proveedor del Exterior"),
                new(9,"Cliente del Exterior"),
                new(10,"IVA Liberado – Ley Nº 19.640"),
                new(11,"IVA Responsable Inscripto – Agente de Percepción"),
                new(12,"Pequeño Contribuyente Eventual"),
                new(13,"Monotributista Social"),
                new(14,"Pequeño Contribuyente Eventual Social")
            };
            selectedRespIVA = RespIVAList[4];
            EvaluateResp();
        }
        private void EvaluateResp()
        {
            if(selectedRespIVA.Codigo == 1 ||
                selectedRespIVA.Codigo == 6 ||
                selectedRespIVA.Codigo == 13)
            {
                CanContinue = true;
                VoucherType = 'A';
                return;
            }
            if (selectedRespIVA.Codigo == 4 ||
                selectedRespIVA.Codigo == 5)
            {
                CanContinue = true;
                VoucherType = 'B';
                return;
            }
            CanContinue = false;
            VoucherType = 'X';
        }
    }
}
