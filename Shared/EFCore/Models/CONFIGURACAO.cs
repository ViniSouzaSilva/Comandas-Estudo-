using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONFIGURACAO : EntityBase
    {
        public string SIGN_AC { get; set; }
        public decimal DESC_MAX_OP { get; set; }
        public bool SYS_USARECARGAS { get; set; }
        public bool SYS_DETALHADESCONTO { get; set; }
        public int SYS_COD10PORCENTO { get; set; }
        public bool DESCRICAO_REDUZIDA { get; set; }
        public bool SYS_USACOMANDA { get; set; }
        public bool SYS_PEDESENHAAOCANCELAR { get; set; }
    }
}
