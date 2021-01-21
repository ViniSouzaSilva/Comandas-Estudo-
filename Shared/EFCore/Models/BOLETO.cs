using System.ComponentModel.DataAnnotations;

namespace AmbiStore.Shared.EFCore.Models
{
    public class BOLETO : EntityBase
    {
        public BANCO_CONTA BANCO { get; set; }
        public string NOSSONUMERO { get; set; }
        public string VENCTO { get; set; }
        public CONTATO CLIENTE { get; set; }

    }
}