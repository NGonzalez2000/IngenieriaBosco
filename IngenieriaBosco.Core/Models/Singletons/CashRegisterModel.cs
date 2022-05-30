using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieriaBosco.Core.Models.Singletons
{
    public class CashRegisterModel : INotify
    {
        public decimal USD { get; set; }
        public decimal ARG { get; set; }
        public void IncreaseUsd()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
