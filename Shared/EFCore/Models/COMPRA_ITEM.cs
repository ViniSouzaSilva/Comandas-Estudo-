using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class COMPRA_ITEM : EntityBase
    {
        public int NUMERO_ITEM { get; set; }
        public decimal QUANTIDADE_ITEM { get; set; }
        public decimal VALOR_ITEM { get; set; }
        public decimal VALOR_DESCONTO { get; set; }
        public decimal VALOR_FRETE { get; set; }
        public decimal VALOR_SEGURO { get; set; }
        public decimal VALOR_DESPESA { get; set; }
        public decimal VALOR_RETENCAO_PIS_COF_CSLL { get; set; }
        [MaxLength(3)]
        public string CSOSN { get; set; }
        public int VALOR_ICM_DESONERADO { get; set; }




        #region Foreign Relations
        public int ESTOQUE_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }

        public string UNIDADE_MEDIDA_ID { get; set; }
        public UNI_MEDIDA UNIDADE_MEDIDA { get; set; }

        public int CFOP_ID { get; set; }
        public CFOP_SIS CFOP { get; set; }


        #endregion Foreign Relations
        #region Helper Properties
        [NotMapped]
        public decimal VALOR_TOTAL { get { return QUANTIDADE_ITEM * VALOR_ITEM; } }
        #endregion
    }
}