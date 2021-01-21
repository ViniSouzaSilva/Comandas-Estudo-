using AmbiStore.Shared.Libraries.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTATO_PJ : EntityBase
    {
        public string CNPJ { get; set; }
        public string IE { get; set; }
        public string IM { get; set; }
        public int CONTATO_ID { get; set; }
        public CONTATO CONTATO { get; set; }//Contato ao qual se referem essas informações
    }
}