using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class PERMISSAO_FUNCIONARIO : EntityBase
    {
        public int FUNCIONARIO_ID { get; set; }
        public FUNCIONARIO FUNCIONARIO { get; set; }
        public Modulo MODULO { get; set; }
        public bool PERMITIDO { get; set; }
    }
}
