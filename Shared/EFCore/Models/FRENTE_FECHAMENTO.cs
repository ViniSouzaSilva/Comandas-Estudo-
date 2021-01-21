using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FRENTE_FECHAMENTO : EntityBase
    {
        [ForeignKey("ID")]
        public FRENTE_TURNO ID_TURNO { get; set; }
        public FORMAPAGAMENTO FORMAPAGAMENTO { get; set; }
        public decimal VALOR_INFORMADO { get; set; }
    }
}
