using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using AmbiStore.Shared.Serializador.NFe;
namespace AmbiStore.Shared.EFCore.Services
{
    class DeserializadorNFe
    {
        public DeserializadorNFe()
        {

        }
        public nfeproc Deserializa(string xmlOrig) 
        {
            if (String.IsNullOrWhiteSpace(xmlOrig)) { return null; }
            XmlRootAttribute atrib = new XmlRootAttribute();
           
            atrib.ElementName = "nfeProc";
            atrib.Namespace = "http://www.portalfiscal.inf.br/nfe";
            atrib.IsNullable = true;
            XmlSerializer xml = new XmlSerializer(typeof(nfeproc), atrib);
            using var reader = new StringReader(xmlOrig);
            using var Xreader = XmlReader.Create(reader);
            
                nfeproc a;
            try
            {
                a = (nfeproc)xml.Deserialize(Xreader);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
            
            return a;
        }
    }
}
