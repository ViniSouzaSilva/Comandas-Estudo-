using System.ComponentModel.DataAnnotations;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class BOLETO_CONFIG : EntityBase
    {
        public BANCO_CONTA BANCO { get; set; }
        public string TIPO_CART { get; set; }
        public string COD_CEDENTE { get; set; }
        public string ESPECIE { get; set; }
        public bool ACEITE { get; set; }
        public string LOCAL_PAGTO { get; set; }
        public int FAIXA_INI { get; set; }
        public int FAIXA_FIM { get; set; }
        public int FAIXA_ATU { get; set; }
        public string INSTRUCOES { get; set; }
        public int NUM_REMESSA { get; set; }
        public decimal JUROS { get; set; }
        public decimal MULTA { get; set; }
        public int DIAS_PROTESTO { get; set; }
        public TipoJuros TIPO_JUROS { get; set; }
        public TipoProtesto TIPO_PROTESTO { get; set; }
        public string CNAB { get; set; }
    }
}