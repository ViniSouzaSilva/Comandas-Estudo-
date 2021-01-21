using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using AmbiStore.Shared.Auxiliares;
using AmbiStore.Shared.Exceptions;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CFOP_SIS //Esse objeto não segue as ID's do EntityBase
    {
        public int CFOP { get; set; }
        public string DESCRICAO { get; set; }
        public string RESUMO { get; set; }
        public string OBSERVACAO { get; set; }
        public BaixaEstoque BAIXAESTOQUE { get; set; }

        //=================
        public List<ESTOQUE> ESTOQUE_CFOP_NFE { get; set; }//Estoques que usam esse CFOP para NFE
        public List<ESTOQUE> ESTOQUE_CFOP_CFE { get; set; }//Estoques que usam esse CFOP para CFE
        public List<VENDA_ITEM> VENDA_ITEM_CFE { get; set; }
        public List<NATUREZA_OPERACAO> NATU_OPER_CFOP { get; set; }
        public List<COMPRA_ITEM> COMPRA_ITEM_CFOP { get; set; }

    }
}
