using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.Auxiliares
{
    public class PLANOCTAImportacao
    {

        public List<DATA_RECORD> TB_CSOSN_SIS { get; set; }

        public PLANOCTAImportacao()
        {
            this.TB_CSOSN_SIS = new List<DATA_RECORD>();
        }

        public List<PLANO_CONTA> listaPlanoContas()
        {
            List<PLANO_CONTA> lista = new List<PLANO_CONTA>();
            foreach (DATA_RECORD dr in TB_CSOSN_SIS)
            {
                lista.Add(new PLANO_CONTA() {ID = dr.ID_CTAPLA, DESCRICAO = dr.DESCRICAO, COD_CONTA = dr.COD_CONTA, COD_PAI = dr.COD_PAI });
            }
            return lista;
        }
       
    }
    public class DATA_RECORD
    {
        public int ID_CTAPLA { get; set; }
        public string DESCRICAO { get; set; }
        public string COD_CONTA { get; set; }
        public string COD_PAI { get; set; }

    }
}
