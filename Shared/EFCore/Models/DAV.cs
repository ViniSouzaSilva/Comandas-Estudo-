using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class DAV : EntityBase
    {
        public CONTATO CLIENTE { get; set; }
        public FUNCIONARIO VENDEDOR { get; set; }
        public DateTime TS_PEDIDO { get; set; }
        public DateTime VALIDADE { get; set; }
        public string OBSERVACAO { get; set; }
        public List<DAV_ITEM> ITENS { get; set; }
        public CONTATO TRANSPORTADORA { get; set; }
        public decimal VLR_FRETE { get; set; }
        public PARCELAMENTO PARCELAMENTO { get; set; }
        public DAV_STATUS STATUS { get; set; }
        public TipoDAV TIPO { get; set; }
    }
}
