using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class REMESSA : EntityBase
    {
        public FILIAL DESTINO { get; set; }
        public MotivoRemessa MOTIVO { get; set; }
        public List<REMESSA_ITEM> ITENS { get; set; }
    }
}
