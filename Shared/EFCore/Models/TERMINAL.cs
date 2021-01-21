using AmbiStore.Shared.Libraries.Enums;
using System.ComponentModel.DataAnnotations;

namespace AmbiStore.Shared.EFCore.Models
{
    public class TERMINAL : EntityBase
    {
        public string HD_SERIAL { get; set; }
        public short NO_CAIXA { get; set; }
        public bool SAT_USA { get; set; }
        public Pede_Info VENDA_PEDE_CPF { get; set; }
        public Sat_Modelo SAT_MODELO { get; set; }
        public string SAT_COD_ATIV { get; set; }
        public string SAT_SIGN_AC { get; set; }
        public string VENDA_IMPRESSORA_USB { get; set; }
        public bool SYS_EMITECOMPROVANTE { get; set; }
        public bool WHATS_USA { get; set; }
        public bool TEF_USA { get; set; }
        [RegularExpression("\\d{1,3}[.]\\d{1,3}[.]\\d{1,3}[.]\\d{1,3}", ErrorMessage = "Endereço de IP inválido")]
        public string TEF_IP { get; set; }
        [RegularExpression("\\d{8}", ErrorMessage = "Numero de Loja TEF inválido")]
        public string TEF_NUMLOJA { get; set; }
        [RegularExpression("[\\D{2}\\d{6}", ErrorMessage = "Número do Terminal TEF inválido")]
        public string TEF_NUM_TERMINAL { get; set; }
        public bool TEF_PEDECPFPELOPINPAD { get; set; }
        public short BAL_PORTA { get; set; }
        public short BAL_BITS { get; set; }
        public int BAL_BAUD { get; set; }
        public Parity BAL_PARITY { get; set; }
        public Balanca_Modelo BAL_MODELO { get; set; }
        public short AC_FILLPREFIX { get; set; }
        public AutoCompleteFilterMode AC_FILLMODE { get; set; }
        public bool AC_USAREFERENCIA { get; set; }
        public int AC_FILLDELAY { get; set; }
        public Pede_Info VENDA_PERGUNTA_VENDEDOR { get; set; }
        public int SAT_SERVTIMEOUT { get; set; }
        public int SAT_LIFESIGNINTERVAL { get; set; }
        public bool VENDA_PERMITEPARCELAS { get; set; }
    }
}
