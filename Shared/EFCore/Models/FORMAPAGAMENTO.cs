using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class FORMAPAGAMENTO : EntityBase
    {
        public string DESCRICAO { get; set; }
        public Status STATUS { get; set; }
        public UsoFMAPAGTO UTILIZACAO { get; set; }
        

        public int PARCELAMENTO_ID { get; set; }
        public PARCELAMENTO PARCELAMENTO { get; set; }

        public List<VENDA_PAGAMENTO> VENDA_PAGAMENTOS { get; set; }

    }
}