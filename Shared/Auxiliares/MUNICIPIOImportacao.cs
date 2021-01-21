using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.Auxiliares
{
    [XmlRoot(ElementName = "municipios")]
    public class MUNICIPIOImportacao
    {
        [XmlElement(ElementName = "municipio")]
        public List<MUNICIPIO> municipios { get; set; }

        public MUNICIPIOImportacao()
        {
            this.municipios = new List<MUNICIPIO>();
        }
    }
}
