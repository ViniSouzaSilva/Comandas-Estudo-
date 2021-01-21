using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class ESTOQUE : EntityBase
    {
        /// <summary>
        /// Descrição do estoque, com até 120 caracteres.
        /// </summary>
        [Required]
        [MaxLength(120)]
        public string DESCRICAO { get; set; }

        /// <summary>
        /// Quantidade atual no estoque
        /// </summary>
        [Required]
        [Column(TypeName = "NUMERIC(18,4)")]
        [DefaultValue(0)]
        public decimal QUANTIDADE { get; set; }

        /// <summary>
        /// Determina se o item irá aparecer nos outros módulos além do cadastro de estoque
        /// </summary>
        [Required]
        [DefaultValue(Status.Ativo)]
        public Status STATUS { get; set; }

        /// <summary>
        /// Data na qual o estoque foi cadastrado
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DATA_CADASTRO { get; set; }

        /// <summary>
        /// Preço de venda unitário padrão do estoque
        /// </summary>
        [Required]
        [Column(TypeName = "NUMERIC(18,4)")]
        [DefaultValue(0.01)]
        public decimal PRECO_VENDA { get; set; } = 0.01M;

        /// <summary>
        /// Preço de venda unitário padrão do estoque em dólares
        /// </summary>
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal PRECO_DOLAR { get; set; }

        /// <summary>
        /// Preço de custo médio do estoque
        /// </summary>
        [Required]
        [Column(TypeName = "NUMERIC(18,4)")]
        [DefaultValue(0.01)]
        public decimal PRECO_CUSTO { get; set; } = 0.01M;
        
        /// <summary>
        /// Data na qual ocorreu a última venda desse estoque
        /// </summary>
        public DateTime ULTIMA_VENDA { get; set; }

        /// <summary>
        /// Data na qual ocorreu a última compra desse estoque
        /// </summary>
        public DateTime ULTIMA_COMPRA { get; set; }
        /// <summary>
        /// Margem de Valor Agregado
        /// </summary>
        [NotMapped]
        public decimal MVA { get; set; }

        /// <summary>
        /// Porcentagem da comissão do estoque
        /// </summary>
        public decimal PORCEM_COMISSAO_PRODUTO { get; set; }


        /// <summary>
        /// Tipo do item de estoque
        /// </summary>
        [Required]
        [DefaultValue(TipoItem.Outros)]
        public TipoItem TIPO_ITEM { get; set; }

        /// <summary>
        /// Código da Substituição Tributária PIS
        /// </summary>
        public CST_PIS_COFINS COD_SUBSTITUICAO_TRIBUTARIA_PIS { get; set; }

        /// <summary>
        /// Código da Substituição Tributária COFINS
        /// </summary>
        public CST_PIS_COFINS COD_SUBSTITUICAO_TRIBUTARIA_COFINS { get; set; }

        /// <summary>
        /// Percentual do valor de venda correspondente ao PIS 
        /// </summary>
        public decimal PIS { get; set; }

        /// <summary>
        /// Percentual do valor de venda correspondente ao COFINS
        /// </summary>
        public decimal COFINS { get; set; }


        public decimal MARGEM_PV { get; set; }

        /// <summary>
        /// Observações extras
        /// </summary>
        public string OBSERVACAO { get; set; }

        /// <summary>
        /// Natureza da Receita da venda desse Estoque
        /// </summary>
        public int NAT_RECEITA { get; set; }

        /// <summary>
        /// Preço unitário de venda do estoque quando a quantidade de atacado é atingida
        /// </summary>
        public decimal PRECO_ATACADO { get; set; } = 0M;

        /// <summary>
        /// Quantidade a partir da qual o valor de venda unitário deve ser substituído pelo valor de atacado
        /// </summary>
        public decimal QTD_ATACADO { get; set; }//QUANTIDADE DE ATACADO 
        
        /// <summary>
        /// Informação do primeiro campo customizável
        /// </summary>
        [NotMapped]
        public string CAMPO1 { get; set; }

        /// <summary>
        /// Informação do segundo campo customizável
        /// </summary>
        [NotMapped]
        public string CAMPO2 { get; set; }

        /// <summary>
        /// Quantidade a partir da qual o estoque aparece em vermelho na listagem
        /// </summary>
        public decimal QTD_MINIMA { get; set; }

        /// <summary>
        /// Quantidade reservada 
        /// </summary>
        public decimal QTD_RESERVA { get; set; }
        public decimal PESO { get; set; }
        public decimal ALIQUOTAINTERNA { get; set; }

        private TipoItemCadastro _tipoItemCadastro;

        public TipoItemCadastro TIPOITEMCADASTRO
        {
            get { return _tipoItemCadastro; }
            set { _tipoItemCadastro = value; }
        }


        #region Foreign Key//=========================


        public string TAXA_UF_ID { get; set; }
        public TAXA_UF TAXA_UF { get; set; }
        public string TAXA_UF_CFE_ID { get; set; }
        public TAXA_UF TAXA_UF_CFE { get; set; }
        public string TAXA_ICMS_PART_ID { get; set; }

        public TAXA_UF TAXA_ICMS_PART { get; set; }
        public string TAXA_ICMS_FCP_ID { get; set; }
        public TAXA_UF TAXA_ICMS_FCP { get; set; }//TAXA ATUALIZADA PARA CALCULO DO FUNDO DE COMBATE A POBREZA


        public string UNI_MEDIDA_ID { get; set; }
        [Required]
        public UNI_MEDIDA UNI_MEDIDA { get; set; }


        public int? CFOP_NF_ID { get; set; }
        [Required]
        public CFOP_SIS CFOP_NF { get; set; }

        public int? CFOP_CFE_ID { get; set; }
        [Required]
        public CFOP_SIS CFOP_CFE { get; set; }

        public int? ULTIMO_FORNECEDOR_ID { get; set; }
        public CONTATO ULTIMO_FORNECEDOR { get; set; }
        public int? FORNECEDOR_PREFERENCIAL_ID { get; set; }
        public CONTATO FORNECEDOR_PREFERENCIAL { get; set; }

        public int? GRUPO_ID { get; set; }
        public GRUPO GRUPO { get; set; }

        public PRODUTO PRODUTO_ESTOQUE { get; set; }

        public SERVICO SERVICO_ESTOQUE { get; set; }

        public string CST_NFE_ID { get; set; }
        public CST CST_NFE { get; set; }
        public string CST_CFE_ID { get; set; }
        public CST CST_CFE { get; set; }
        public string CSOSN_NFE_ID { get; set; }
        public CSOSN CSOSN_NFE { get; set; }
        public string CSOSN_CFE_ID { get; set; }
        public CSOSN CSOSN_CFE { get; set; }
        public List<VENDA_ITEM> VENDA_ITEM_ESTOQUES { get; set; }
        public List<PARAMETRIZACAO> PARAMETRIZACAO_ESTOQUE { get; set; }
        public List<COMPRA_ITEM> COMPRA_ITEM_ESTOQUE { get; set; }

        public List<COMANDA_HISTORICO> COMANDA_HISTORICO { get; set; }

        #endregion
    }
}
