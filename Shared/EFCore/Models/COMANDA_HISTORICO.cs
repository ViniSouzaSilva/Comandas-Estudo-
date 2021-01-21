using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class COMANDA_HISTORICO : EntityBase
    {

        public DateTime DATA_COMANDA { get; set; }
        public DateTime DATA_COMANDA_FECHAMENTO { get; set; }
        public Status STATUS { get; set; }
        public bool ISDELETED { get; set; }
        public decimal QTD { get; set; }

        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VLR_TOTAL { get; set; }




        #region Foreign Key
        public COMANDA COMANDA { get; set; }
        public int COMANDA_ID { get; set; }
        public ESTOQUE ESTOQUE { get; set; }
        public int ESTOQUE_ID { get; set; }
        #endregion
    }
}
