using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class VENDA : EntityBase
    {
        public int NF_NUMERO { get; set; }
        [MaxLength(3)]
        public string NF_SERIE { get; set; }
        [MaxLength(2)]
        public string NF_MODELO { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DT_EMISSAO { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DT_SAIDA { get; set; }
        [MaxLength(15)]
        public string ESPECIE { get; set; }//ESPECIE DA NOTA FISCAL
        [MaxLength(2)]
        public string TIPO_FRETE { get; set; }//TIPO DE FRETE(0:CONTRATADO POR CONTA REMETENTE(CIF) / 1:CONTRATADO POR CONTA DESTINATARIO(FOB) / 2:CONTRATADO POR CONTA DE TERCEIROS / 3:PROPRIO POR CONTA REMETENTE / 4:PROPRIO POR CONTA DESTINATARIO / 9:SEM FRETE)
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal PESO_LIQUIDO { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal PESO_BRUTO { get; set; }
        public Status STATUS { get; set; }
        public Entrada_Saida ENTRADA_SAIDA { get; set; }
        [MaxLength(15)]
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal QUANTIDADE_VOLUME { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal NUMERO_VOLUME { get; set; }
        public bool PRODUTO_REVENDA { get; set; }
        public bool SOMA_FRETE { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VLR_TROCO { get; set; }
        public char IND_PRES { get; set; }// Indicação de presença 
        public char IND_IE_DEST { get; set; }
        public bool DESCONTO_CONDICIONAL {get; set;}
        public string INFO_COMPLEMENTAR_FIXA { get; set; }
        public string INFO_COMPLEMENTAR_EDITAVEL { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ENVIO_API { get; set;}
        public bool SUBTRAI_ICMS_DESONERADO { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal VLR_JUROS_PARCELAMENTO { get; set; }
        //=======================
        public int NATUREZA_OPERACAO_ID { get; set; }
        public NATUREZA_OPERACAO NATUREZA_OPERACAO { get; set; }
        public List<VENDA_ITEM> VENDA_ITEMs { get; set; } = new List<VENDA_ITEM>();
        public List<VENDA_PAGAMENTO> VENDA_PAGAMENTOs { get; set; } = new List<VENDA_PAGAMENTO>();
        public int TRANSPORTADORA_ID { get; set; }
        public CONTATO TRANSPORTADORA { get; set; }
        public int VENDEDOR_ID { get; set; }
        public FUNCIONARIO VENDEDOR { get; set; }
        public int CLIENTE_ID { get; set; }
        public CONTATO CLIENTE { get; set; }
        public int PLANO_CONTA_ID { get; set; }
        public PLANO_CONTA PLANO_CONTA { get; set; }
        public NFE NFE { get; set; }
    }
}
