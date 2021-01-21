using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CHAMADO : EntityBase
    {
        public CONTATO CONTATO { get; set; }
        public DAV DAV { get; set; }
        public FUNCIONARIO ATENDENTE { get; set; }
        public FUNCIONARIO TECNICO { get; set; }
        public PrioridadeCHAMADO PRIORIDADE { get; set; }
        public string ACOMPANHAMENTO { get; set; }
        public DateTime ABERTURA { get; set; }
        public DateTime ULT_ATUALIZACAO { get; set; }
        public string CAMPO1 { get; set; }
        public string CAMPO2 { get; set; }
        public string CAMPO3 { get; set; }
        public OBJETO OBJETO { get; set; }
    }
}
