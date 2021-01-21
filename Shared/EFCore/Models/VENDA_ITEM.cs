using System.ComponentModel.DataAnnotations;

namespace AmbiStore.Shared.EFCore.Models
{
    public class VENDA_ITEM : EntityBase
    {
        public int NUM_ITEM { get; set; }
        public decimal QTD_ITEM { get; set; }
        //public decimal VLR_COMPRA { get; set; }
        /// <summary>
        /// Desconto aplicado sobre o valor unitário do produto
        /// </summary>
        public decimal VLR_DESCONTO { get; set; }

        //============
        public int VENDA_ID { get; set; }
        public VENDA VENDA { get; set; }
        public int CFOP_ID { get; set; }
        public CFOP_SIS CFOP { get; set; }
        public int ESTOQUE_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }
        public string UNI_MEDIDA_ID { get; set; }
        public UNI_MEDIDA UNI_MEDIDA { get; set; }


    }
}