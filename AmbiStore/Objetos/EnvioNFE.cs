using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiStore.Objetos
{
    class EnvioNFE
    {
        public string CodNota { get; set; }
        public string ChaveNFE { get; set; }
        public string CStat { get; set; }
        public string MsgSefaz { get; set; }

        public EnvioNFE(string retorno)
        {
            string[] retornoArray = retorno.Split(',');
            CodNota = retornoArray[0];
            ChaveNFE = retornoArray[1];
            CStat = retornoArray[2];
            MsgSefaz = retornoArray[3] ?? throw new ArgumentNullException();
        }
    }
    class ErroNFE    
    {
        public string PalavraException { get; set; }
        public string ClasseException { get; set; }
        public string MSGException { get; set; }

        public ErroNFE(string Exception)
        {
            string[] retornoArray = Exception.Split(',');
            PalavraException = retornoArray[0] ?? throw new ArgumentNullException();
            ClasseException = retornoArray[1] ?? throw new ArgumentNullException();
            MSGException = retornoArray[2] ?? throw new ArgumentNullException();
        }
    }
}
