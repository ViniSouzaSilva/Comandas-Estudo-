using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FRENTE_MOVIMENTO : EntityBase
    {
        [ForeignKey("ID")]
        public FRENTE_TURNO ID_TURNO { get; set; }
        public TipoMovCaixa TIPO { get; set; }
        public decimal VALOR { get; set; }
        public DateTime DATA_HORA { get; set; }
    }
}
