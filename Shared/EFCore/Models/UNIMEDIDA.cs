using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Serialization;
using AmbiStore.Shared.Exceptions;
using AmbiStore.Shared.Libraries.Enums;

namespace AmbiStore.Shared.EFCore.Models
{
    public class UNI_MEDIDA
    {
        [Key]
        [MaxLength(4)]
        public string ABREVIATURA { get; set; }
        [MaxLength(30)]
        [Required]
        public string DESCRICAO { get; set; }
        [Required]
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal CONVERSOR { get; set; }
        [Required]
        [DefaultValue(Status.Ativo)]
        public Status STATUS { get; set; }
        [MaxLength(6)]
        [XmlIgnore]
        public string UNIDADE_EX { get; set; }//ARMAZENAR A UNIDADE DE MEDIDA UTILIZADA QUANDO VENDA FOR EXPORTACAO.
        //===================
        
        public UNI_MEDIDA()
        {

        }

        public UNI_MEDIDA(string aBREVIATURA, string dESCRICAO, decimal cONVERSOR = 1M)
        {
            ABREVIATURA = aBREVIATURA;
            DESCRICAO = dESCRICAO;
            CONVERSOR = cONVERSOR;
            STATUS = Status.Ativo;
        }
        [XmlIgnore]
        public List<ESTOQUE> ESTOQUE_UNI_MEDIDA { get; set; }
        [XmlIgnore]
        public List<VENDA_ITEM> VENDA_ITEM_UNI_MEDIDAS { get; set; }
        [XmlIgnore]
        public List<COMPRA_ITEM> COMPRA_ITEM_UNI_MEDIDA { get; set; }
    }

}
