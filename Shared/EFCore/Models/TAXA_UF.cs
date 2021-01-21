using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class TAXA_UF
    {
        [Key]
        public string TAXA_ID { get; set; }
        [MaxLength(40)]
        public string DESCRICAO { get; set; }
        public decimal BASE_ICMS { get; set; }
        public decimal BASE_ICMSFE { get; set; }
        public decimal BASE_ICMS_ST { get; set; }
        public decimal UF_AC { get; set; }
        public decimal UF_AL { get; set; }
        public decimal UF_AM { get; set; }
        public decimal UF_AP { get; set; }
        public decimal UF_BA { get; set; }
        public decimal UF_CE { get; set; }
        public decimal UF_DF { get; set; }
        public decimal UF_ES { get; set; }
        public decimal UF_GO { get; set; }
        public decimal UF_MA { get; set; }
        public decimal UF_MG { get; set; }
        public decimal UF_MS { get; set; }
        public decimal UF_MT { get; set; }
        public decimal UF_PA { get; set; }
        public decimal UF_PB { get; set; }
        public decimal UF_PE { get; set; }
        public decimal UF_PI { get; set; }
        public decimal UF_PR { get; set; }
        public decimal UF_RJ { get; set; }
        public decimal UF_RN { get; set; }
        public decimal UF_RO { get; set; }
        public decimal UF_RR { get; set; }
        public decimal UF_RS { get; set; }
        public decimal UF_SC { get; set; }
        public decimal UF_SE { get; set; }
        public decimal UF_SP { get; set; }
        public decimal UF_TO { get; set; }
        public decimal BASE_ISS { get; set; }
        public decimal ISS { get; set; }
        public decimal POR_DIF { get; set; }
        public List<ESTOQUE> ESTOQUE_TAXA_UF { get; set; }//Estoques que usam essa taxa
        public List<ESTOQUE> ESTOQUE_TAXA_UF_CFE { get; set; }//Estoques que usam essa taxa pelo CFe
        public List<ESTOQUE> ESTOQUE_TAXA_PART { get; set; }//Estoques que usam essa taxa
        public List<ESTOQUE> ESTOQUE_TAXA_FCP { get; set; }//Estoques que usam essa taxa pelo CFe

        public List<NATUREZA_OPERACAO> NATU_OPER_TAXA { get; set; }
    }
}
