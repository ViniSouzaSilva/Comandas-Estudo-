using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PEDIDO_COMPRA : EntityBase
    {
        public CONTATO FORNECEDOR { get; set; }
        public List<PED_COMPRA_ITEM> ITENS { get; set; }
    }
}
