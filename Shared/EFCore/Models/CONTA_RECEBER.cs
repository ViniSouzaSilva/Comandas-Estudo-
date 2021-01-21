using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTA_RECEBER : EntityBase
    {
        public CONTATO CLIENTE { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VALOR_TOTAL { get; set; }
        public VENDA VENDA_REFERENTE { get; set; }
        public List<CONTA_RECEBER_PAGTO> PAGAMENTOS { get; set; }
    }
}
