using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.EFCore.Models
{
    public class CST
    {
        public string COD_CST { get; set; }
        public string DESCRICAO { get; set; }
        [XmlIgnore]
        public List<ESTOQUE> CST_NFE_ESTOQUE { get; set; }
        [XmlIgnore]
        public List<ESTOQUE> CST_CFE_ESTOQUE { get; set; }
    }
}
