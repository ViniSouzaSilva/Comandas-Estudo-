using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class COMANDA : EntityBase
    {

        public DateTime DATA_CRIACAO { get; set; }

        public Status STATUS_COMANDA { get; set; }

        public List<COMANDA_HISTORICO> COMANDA_HISTORICOS { get; set; }


        

    }
}
