using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Shared.Libraries.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
    public class EMITENTE : EntityBase
    {
        [Required]
        [MaxLength(80)]
        public string NOME_EMITENTE { get; set; }

        [Required]
        [MaxLength(40)]
        public string NOME_FANTASIA { get; set; }

        [Required]
        [MaxLength(40)]
        public string CONTATO_RESPONSAVEL { get; set; }

        [Required]
        [MaxLength(18)]
        [MinLength(18)]
        [CPForCPNPJ]
        public string CNPJ { get; set; }

        [Required]
        [MaxLength(9)]
        [MinLength(9)]
        public string ENDERECO_CEP { get; set; }

        [Required]
        public TipoLograd ENDERECO_TIPO { get; set; }

        [Required]
        [MaxLength(40)]
        public string ENDERECO_LOGRAD { get; set; }

        [Required]
        [MaxLength(5)]
        public string ENDERECO_NUMERO { get; set; }

        [MaxLength(15)]
        public string ENDERECO_COMPL { get; set; }

        [Required]
        [MaxLength(35)]
        public string ENDERECO_BAIRRO { get; set; }

        [Required]
        [MaxLength(2)]
        public string DDD_COMERCIAL { get; set; }

        [Required]
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

        [Required]
        [MaxLength(16)]
        public string INSCRICAO_ESTADUAL { get; set; }

        [MaxLength(16)]
        public string INSCRICAO_MUNICIPAL { get; set; }

        [Required]
        [MaxLength(17)]
        public string CNAE { get; set; }

        public bool SIMPLES_NACIONAL { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DT_COMPRA_SISTEMA { get; set; }

        public string RAMO_ATIVIDADE { get; set; }

        public string EMAIL { get; set; }
        public string SITE { get; set; }
        public bool RT_EXCEDE_SUBLIMITE { get; set; }

        //==================
        public int END_MUNICIPIO_ID { get; set; }
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
        }
    }
}
