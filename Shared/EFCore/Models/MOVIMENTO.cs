using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class MOVIMENTO : EntityBase
    {
        public DateTime DATAMOVIMENTO { get; set; }
        public string HISTORICO { get; set; }
        public TipoMovimento TIPO { get; set; }
        public decimal VALOR { get; set; }
        public int PLANO_CONTA_ID { get; set; }
        public PLANO_CONTA PLANO { get; set; }
    }
}
