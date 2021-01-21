using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using AmbiStore.Shared.Libraries.Validations;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTATO_PF : EntityBase
    {
        public string CPF { get; set; }
        public string RG { get; set; }
        public string MAE { get; set; }
        public string PAI { get; set; }
        public DateTime NASCIMENTO { get; set; }
        public int CONTATO_ID { get; set; }
        public CONTATO CONTATO { get; set; }//Contato ao qual se referem essas informações
    }
}