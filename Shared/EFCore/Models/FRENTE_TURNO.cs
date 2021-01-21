using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FRENTE_TURNO : EntityBase
    {
        public TERMINAL TERMINAL { get; set; }
        public DateTime TS_ABERTURA { get; set; }
        public DateTime TS_FECHAMENTO { get; set; }
        public FUNCIONARIO OPERADOR { get; set; }
        public bool TURNO_ABERTO { get; set; }
        public List<FRENTE_MOVIMENTO> MOVIMENTOs { get; set; }
        public List<FRENTE_FECHAMENTO> FRENTE_FECHAMENTOs { get; set; }
    }
}
