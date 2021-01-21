using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTA_RECEBER_PAGTO : EntityBase
    {
        public FORMAPAGAMENTO FORMAPAGAMENTO { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VALOR { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VALOR_PAGO { get; set; }
        public DateTime VENCIMENTO { get; set; }
        public BOLETO BOLETO { get; set; }
        [ForeignKey("ID")]
        public CONTA_RECEBER CTA_RECEBER { get; set; }
    }
}