using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CONTATO : EntityBase
    {
        [Required]
        public string NOME_JURIDICO { get; set; }
        public string NOME_FANTASIA { get; set; }
        public Status STATUS { get; set; }
        public string CONTATO_ESP { get; set; }
        [MaxLength(9)]
        public string ENDERECO_CEP { get; set; }
        public TipoLograd ENDERECO_TIPO { get; set; }
        [MaxLength(40)]
        public string ENDERECO_LOGRAD { get; set; }
        [MaxLength(5)]
        public string ENDERECO_NUMERO { get; set; }
        [MaxLength(15)]
        public string ENDERECO_COMPL { get; set; }
        [MaxLength(35)]
        public string ENDERECO_BAIRRO { get; set; }
        [MaxLength(2)]
        public string DDD_COMERCIAL { get; set; }
        [RegularExpression("\\d{8,9}", ErrorMessage = "Insira um valor válido.")]
        public string TEL_COMERCIAL { get; set; }
        [MaxLength(2)]
        public string DDD_CELULAR1 { get; set; }
        [RegularExpression("\\d{8,9}", ErrorMessage = "Insira um valor válido.")]
        public string TEL_CELULAR1 { get; set; }
        [MaxLength(2)]
        public string DDD_CELULAR2 { get; set; }
        [RegularExpression("\\d{8,9}", ErrorMessage = "Insira um valor válido.")]
        public string TEL_CELULAR2 { get; set; }

        public string PAIS { get; set; }
        [EmailAddress]
        public string EMAIL1 { get; set; }
        [EmailAddress]
        public string EMAIL2 { get; set; }
        [EmailAddress]
        public string EMAILNFE { get; set; }
        public string OBSERVACAO { get; set; }
        public string ALERTA { get; set; }
        public DateTime DT_CADASTRO { get; set; }
        public DateTime DT_PRI_COMPRA { get; set; }
        public DateTime DT_ULT_COMPRA { get; set; }
        private StatusContribuicao cONTRIBUINTE;
        public StatusContribuicao CONTRIBUINTE { 
            get
            {
                return cONTRIBUINTE;
            }
            set
            {
                cONTRIBUINTE = value;
                OnPropertyChanged("CONTRIBUINTE");
            }
        }
        public decimal LIM_CREDITO { get; set; }
        public int DIA_VECTO { get; set; }

        #region Foreign Relations

        public CONTATO_PF CONTATO_PF { get; set; } //Informações de Contato de Pessoa Física
        public CONTATO_PJ CONTATO_PJ { get; set; } //Informações de Contato de Pessoa Jurídica

        public int? CONTATO_VENDEDOR_PREF_ID { get; set; }
        public FUNCIONARIO VENDEDOR_PREF { get; set; } //Funcionário que é o vendedor preferencial desse contato

        public FUNCIONARIO FUNCIONARIO { get; set; } //Funcionário ao qual esse contato corresponde

        public int? END_MUNICIPIO_ID { get; set; }
        [DefaultValue(null)]
        private MUNICIPIO mUNICIPIO;
        public MUNICIPIO END_MUNICIPIO
        {
            get { return mUNICIPIO; }
            set
            {
                mUNICIPIO = value;
                OnPropertyChanged("END_MUNICIPIO");
            }
        }//Município desse Contato

        public List<ESTOQUE> FORNECEDOR_PREF_ESTOQUE { get; set; } //Lista de Estoques dos quais esse contato é o Fornecedor Preferencial
        public List<ESTOQUE> ULTIMO_FORNECEDOR_ESTOQUE { get; set; } //Lista de Estoques dos quais esse contato é o ùltimo Fornecedor
        public List<VENDA> TRANSPORTADORA_CONTATO { get; set; }
        public List<VENDA> CLIENTE_CONTATO { get; set; }
        public List<PARAMETRIZACAO> PARAMETRIZACAO_FORNECEDOR { get; set; }
        public List<COMPRA> COMPRA_FORNECEDOR { get; set; }
        public List<REFERENCIA> REFERENCIAS { get; set; } = new List<REFERENCIA>();
        #endregion
        #region Helper Properties
        [NotMapped]
        public string TEL_COMERCIAL_FULL { get { return $"{DDD_COMERCIAL} {TEL_COMERCIAL}"; } }
        [NotMapped]
        public string TEL_CELULAR1_FULL { get { return $"{DDD_CELULAR1} {TEL_CELULAR1}"; } }
        [NotMapped]
        public string TEL_CELULAR2_FULL { get { return $"{DDD_CELULAR2} {TEL_CELULAR2}"; } }

        #endregion
    }
}
