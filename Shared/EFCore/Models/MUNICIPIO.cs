using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.EFCore.Models
{
    public class MUNICIPIO
    {
        [Key]
        [XmlElement("ID_Municipio")]
        public int ID_MUNICIPIO { get; set; }
        [XmlElement("Mun_Desc")]
        public string MUN_DESC { get; set; }
        [MaxLength(2)]
        [XmlElement("UF")]
        public string UF { get; set; }
        [MaxLength(4)]
        [XmlIgnore]
        public string CODIGO_SIAFI { get; set; }

        //==========================
        [XmlIgnore]
        public List<CONTATO> CONTATO_MUNICIPIO { get; set; }
        [XmlIgnore]
        public EMITENTE CONTATO_EMITENTE { get; set; }

    }
}
