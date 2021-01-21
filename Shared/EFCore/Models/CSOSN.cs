using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CSOSN
    {
        public string CSOSN_COD { get; set; }
        public string DESCRICAO { get; set; }
        [XmlIgnore]
        public List<ESTOQUE> CSOSN_NFE_ESTOQUE { get; set; }
        [XmlIgnore]
        public List<ESTOQUE> CSOSN_CFE_ESTOQUE { get; set; }
    }
}
