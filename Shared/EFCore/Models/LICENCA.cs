using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class LICENCA : EntityBase
    {
        public string STORE_KEY { get; set; }
        public string TECH_KEY { get; set; }
        public string MAITRE_KEY { get; set; }
        public string DELIVERY_KEY { get; set; }
        public string WEBSTORE_KEY { get; set; }
        public string VERSAO_EXE { get; set; }
        public string VERSAO_BD { get; set; }
        public DateTime TS_INSTALACAO { get; set; }
        public DateTime ULT_VERIFICACAO { get; set; }
        public DateTime ULT_ATUALIZACAO { get; set; }
    }
}
