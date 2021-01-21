using System;
using System.Collections.Generic;
using System.Text;
using AmbiStore.Shared.SEFAZ.NF;
namespace AmbiStore.Shared.Serializador.NFe
{
     public class nfeproc
    {

        public TNfeProc NfeProc { get; set; }
        public TNFe NFe
        {
            get;
            set;
        }

        /// <remarks/>
        public TProtNFe protNFe
        {
            get;

            set;
            
        }

       
        public string versao
        {
            get;


            set;
           
        }
    }
}

