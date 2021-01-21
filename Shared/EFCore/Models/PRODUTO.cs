using System;
using System.ComponentModel.DataAnnotations;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PRODUTO : EntityBase
    {
        [MaxLength(18)]
        public string COD_BARRA { get; set; }
        public string REFERENCIA { get; set; }
        public decimal PRC_MEDIO { get; set; }
        public decimal QTD_ATUAL { get; set; }
        public decimal QTD_MINIMA { get; set; }
        public DateTime ULT_ENTRADA { get; set; }
        public DateTime ULT_SAIDA { get; set; }
        [MaxLength(8)]
        public string NCM { get; set; }
        public string CEST { get; set; }

        public int ESTOQUE_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }

    }
}