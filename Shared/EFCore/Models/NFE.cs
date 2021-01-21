using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class NFE : EntityBase
    {
        [MaxLength(44)]
        public string CHAVE { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EMISSAO { get; set; }
        [MaxLength(15)]
        public string RECIBO { get; set; }
        [MaxLength(21)]
        public string PROTOCOLO { get; set; }
        [MaxLength(3)]
        public string STATUS_NFE { get; set; }
        [MaxLength(150)]
        public string DESCRICAO_STATUS { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_HORA_ENVIO  { get; set; }
        public Entrada_Saida TIPO { get; set; }
        public bool AMBIENTE_PRODUCAO { get; set; }//Servirá para saber se a nota foi homologação ou ambiente de produção
        public int NFE_VENDA_ID { get; set; }
        public VENDA NFE_VENDA { get; set; }









    }
}
