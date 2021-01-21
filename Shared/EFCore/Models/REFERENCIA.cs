using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AmbiStore.Shared.EFCore.Models
{
   public  class REFERENCIA : EntityBase
    {
        /// <summary>
        /// Nome da pessoa cuja referenciou o cliente em questão (Exemplo: Fiador)
        /// </summary>
        [Required(ErrorMessage = "Informe um nome")]
        [MinLength(4, ErrorMessage = "Informe um nome válido")]
        public string NOME { get; set; }
        /// <summary>
        /// Telefone para contato da referência,no formato DDNNNNNNNNN
        /// </summary>
        [Required(ErrorMessage = "Informe um número")]
        [RegularExpression("\\d{10,11}")]
        public string FONE { get; set; }
        #region Foreign Relations
        
        public CONTATO CONTATO { get; set; }
        public int CONTATO_ID { get; set; }
        #endregion Foreign Relations

    }
}
