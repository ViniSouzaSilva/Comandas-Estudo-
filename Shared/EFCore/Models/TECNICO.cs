using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class TECNICO : FUNCIONARIO
    {
        public FUNCIONARIO FUNCIONARIO { get; set; }
        public SETORTECNICO SETOR { get; set; }

    }
}
