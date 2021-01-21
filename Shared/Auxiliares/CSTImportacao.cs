using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace AmbiStore.Shared.Auxiliares
{
    [XmlRoot(ElementName = "TB_CST_SIS")]
    public class CSTImportacao
    {
        [XmlElement(ElementName = "CST_RECORD")]
        public List<CST> TB_CST_SIS { get; set; }

        public CSTImportacao()
        {
            this.TB_CST_SIS = new List<CST>();
        }
    }
}
