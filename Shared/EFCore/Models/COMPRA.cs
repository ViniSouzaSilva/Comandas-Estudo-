using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class COMPRA : EntityBase
    {
        public int NUMERO_NF { get; set; }
        [MaxLength(3)]
        public string NUMERO_SERIE { get; set; }
        [MaxLength(2)]
        public string NF_MODELO { get; set; }

        public DateTime DATA_EMISSAO { get; set; }

        public DateTime DATA_ENTRADA { get; set; }

        public Status_Nota STATUS_NOTA { get; set; }

        public NATUREZA_OPERACAO NATUREZA_OPERACAO { get; set; }

        public PARCELAMENTO PARCELAMENTO { get; set; }

        public List<COMPRA_PAGAMENTO> COMPRA_PAGAMENTOs { get; set; }

        public int? FRETE_ID { get; set; }

        public CONTATO FRETE { get; set; }

        public List<COMPRA_ITEM> COMPRA_ITEMs { get; set; }

        public string ESPECIE { get; set; }

        public decimal QTD_VOLUMES { get; set; }

        public string INF_COMPL_EDIT { get; set; }

        public string CHAVE { get; set; }

        public bool IMPORTADO_VIA_XML { get; set; }

        #region Foreing Relations

        public int FORNECEDOR_ID { get; set; }
        public CONTATO FORNECEDOR { get; set; }
        public int COMPRADOR_ID { get; set; }
        public FUNCIONARIO COMPRADOR { get; set; }


        #endregion Foreign Relations

    }
}