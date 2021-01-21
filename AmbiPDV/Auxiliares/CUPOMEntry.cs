using System;
using System.Collections.Generic;
using System.Text;

namespace AmbiPDV.Auxiliares
{
    public class CUPOMEntry
    {
        public int NUMITEM { get; set; }
        public string EAN13 { get; set; }
        public string DESCRICAO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public string UNIDADE { get; set; }
        public decimal VLRUNITARIO { get; set; }
        public decimal VLRTOTAL { get { return (QUANTIDADE * VLRUNITARIO); } }
        public string INFOADIC { get; set; }

        public CUPOMEntry(int nUMITEM, string eAN13, string dESCRICAO, decimal qUANTIDADE, string uNIDADE, decimal vLRUNITARIO, string iNFOADIC = null)
        {
            NUMITEM = nUMITEM;
            EAN13 = eAN13;
            DESCRICAO = dESCRICAO;
            QUANTIDADE = qUANTIDADE;
            UNIDADE = uNIDADE;
            VLRUNITARIO = vLRUNITARIO;
            INFOADIC = iNFOADIC;
        }
    }
}
