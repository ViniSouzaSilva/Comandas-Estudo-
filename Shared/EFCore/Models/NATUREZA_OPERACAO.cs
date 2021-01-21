using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class NATUREZA_OPERACAO : EntityBase
    {   
        [MaxLength(60)]
        public string DESCRICAO { get; set; }
        public bool RETEM_PIS_COFINS_CSLL { get; set; }//Retem PIS COFINS CSLL (SIM OU NÃO)
        public bool RETEM_INSS { get; set; }
        public bool RETEM_IRRF { get; set; }
        public bool RETEM_PIS_COFINS { get; set; }
        public Status STATUS { get; set; }
        public bool GERA_FINANCEIRO { get; set; }
        public bool GERA_REMESSA { get; set; }
        public string OBSERVACAO { get; set; }
        public decimal BASE_COMISSAO { get; set; }
        public bool CALCULA_IPI { get; set; }

        //====================
        public List<VENDA> VENDA_NATOPER { get; set; }
        public int CFOP_ID { get; set; }
        public CFOP_SIS CFOP { get; set; }
        public string TAXA_UF_ID { get; set; }
        public TAXA_UF TAXA_UF { get; set; }//código de taxas interno

    }
}
