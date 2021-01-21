using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FUNC_MOD_COLUNA : EntityBase
    {
        public string COLUNA { get; set; }
        public decimal LARGURA { get; set; }
        public bool VISIVEL { get; set; }
        public int FUNCIONARIO_ID { get; set; }
        public FUNCIONARIO FUNCIONARIO { get; set; }
        public Modulo MODULO { get; set; }
    }
}
