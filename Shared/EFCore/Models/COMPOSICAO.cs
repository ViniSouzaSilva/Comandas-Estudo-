using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class COMPOSICAO : EntityBase
    {
        public ESTOQUE ITEMSAIDA { get; set; }
        public ESTOQUE COMPONENTE{ get; set; }
        public decimal QTD_COMPONENTE { get; set; }


    }
}
