using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.Auxiliares
{
    [XmlRoot(ElementName = "TB_CSOSN_SIS")]
    public class CSOSNImportacao
    {
        [XmlElement(ElementName = "CSOSN_RECORD")]
        public List<CSOSN> TB_CSOSN_SIS { get; set; }

        public CSOSNImportacao()
        {
            this.TB_CSOSN_SIS = new List<CSOSN>();
        }
    }
}
