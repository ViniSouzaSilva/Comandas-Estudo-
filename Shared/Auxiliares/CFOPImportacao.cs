using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Exceptions;
using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace AmbiStore.Shared.Auxiliares
{
    [XmlRoot(ElementName = "TB_CFOP_SIS")]
    public class CFOPImportacao
    {
        [XmlElement(ElementName = "DATA_RECORD")]
        public List<CFOPXML> cfopsxmls { get; set; }
        public CFOPImportacao()
        {
            this.cfopsxmls = new List<CFOPXML>();
        }
        public List<CFOP_SIS> deserializado()
        {

            List<CFOP_SIS> cfops = new List<CFOP_SIS>();
            foreach (CFOPXML item in cfopsxmls)
            {
                cfops.Add(new CFOP_SIS() 
                {
                    CFOP = int.Parse(item.CFOP),
                    DESCRICAO = item.DESCRICAO,
                    RESUMO = item.RESUMO,
                    OBSERVACAO = item.OBSERVACAO,
                    BAIXAESTOQUE = item.EST_BX.ToUpper() switch
                    {
                        "A" => BaixaEstoque.Ambos,
                        "S" => BaixaEstoque.Sim,
                        "N" => BaixaEstoque.Nao,
                        _ => throw new InvalidValueException($"CFOP.EST_BX - {item.CFOP} - {item.EST_BX}")

                    }
                });
            }
            return cfops;
        }
    }
    public class CFOPXML
    {
        public string CFOP { get; set; }
        public string DESCRICAO { get; set; }
        public string RESUMO { get; set; }
        public string OBSERVACAO { get; set; }
        public string EST_BX { get; set; }

    }
}
