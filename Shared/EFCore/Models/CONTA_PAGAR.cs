using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTA_PAGAR : EntityBase
    {
        public CONTATO FORNECEDOR { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VALOR_TOTAL { get; set; }
        public COMPRA COMPRA_REFERENTE { get; set; }
        public List<CONTA_PAGAR_PAGTO> PAGAMENTOS { get; set; }
    }
}
