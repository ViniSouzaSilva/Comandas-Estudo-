using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using CfeRecepcao_0007;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiPDV.Services.SAT
{
    public class FuncoesGerais
    {

        public void EnviaDadosParaSAT(VENDA VENDA_EM_CURSO)
        {
            envCFeCFe cFe = ConverteVendaEmCFe(VENDA_EM_CURSO);
            string xmlASerEnviado = ConverteCFeEmXML(cFe);
        }

        private envCFeCFe ConverteVendaEmCFe(VENDA vENDA_EM_CURSO)
        {
            AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            TERMINAL terminalInfo = _context.TERMINALs.Select(x => x).Where(x => x.HD_SERIAL == GetSerialHexNumberFromExecDisk()).First();
            EMITENTE emitenteInfo = _context.EMITENTEs.Select(x => x).First();


            envCFeCFe _Cfe = new envCFeCFe();
            envCFeCFeInfCFe _infCfe = new envCFeCFeInfCFe() { versaoDadosEnt = "0.07" };
            _infCfe.ide = new envCFeCFeInfCFeIde() { CNPJ = "22141365000179", signAC = terminalInfo.SAT_SIGN_AC, numeroCaixa = terminalInfo.NO_CAIXA.ToString() };
            _infCfe.emit = new envCFeCFeInfCFeEmit()
            {
                CNPJ = emitenteInfo.CNPJ,
                IE = emitenteInfo.INSCRICAO_ESTADUAL,
                cRegTribISSQN = "1",
                indRatISSQN = "N",
                IM = emitenteInfo.INSCRICAO_MUNICIPAL
            };

            if (!(vENDA_EM_CURSO.CLIENTE?.CONTATO_PF?.CPF is null))
            {
                _infCfe.dest = new envCFeCFeInfCFeDest()
                {
                    ItemElementName = ItemChoiceType.CPF,
                    Item = vENDA_EM_CURSO.CLIENTE.CONTATO_PF.CPF
                };
            }
            if (!(vENDA_EM_CURSO.CLIENTE?.CONTATO_PJ?.CNPJ is null))
            {
                _infCfe.dest = new envCFeCFeInfCFeDest()
                {
                    ItemElementName = ItemChoiceType.CNPJ,
                    Item = vENDA_EM_CURSO.CLIENTE.CONTATO_PJ.CNPJ
                };
            }

            List<envCFeCFeInfCFeDet> _listaDets = new List<envCFeCFeInfCFeDet>();
            int nItemCupom = 1;
            foreach (VENDA_ITEM item in vENDA_EM_CURSO.VENDA_ITEMs)
            {
                envCFeCFeInfCFeDet _det = new envCFeCFeInfCFeDet();
                envCFeCFeInfCFeDetProd _prod = new envCFeCFeInfCFeDetProd();
                envCFeCFeInfCFeDetImposto _imposto = new envCFeCFeInfCFeDetImposto();
                _det.nItem = nItemCupom.ToString();
                nItemCupom++;
                _prod.cProd = item.ESTOQUE.ID.ToString();
                _prod.CFOP = item.CFOP_ID.ToString();
                _prod.cEAN = item.ESTOQUE.PRODUTO_ESTOQUE.COD_BARRA;
                _prod.indRegra = "A";
                _prod.NCM = item.ESTOQUE.PRODUTO_ESTOQUE.NCM;
                _prod.qCom = item.QTD_ITEM.ToString("N4", CultureInfo.InvariantCulture);
                _prod.uCom = item.ESTOQUE.UNI_MEDIDA_ID;
                _prod.vUnCom = (item.QTD_ITEM >= item.ESTOQUE.QTD_ATACADO) switch
                { 
                    true => item.ESTOQUE.PRECO_ATACADO.ToString("N2"),
                    false => item.ESTOQUE.PRECO_VENDA.ToString("N2")

                };
                _prod.vDesc = ((item.ESTOQUE.PRECO_VENDA - item.VLR_DESCONTO) * item.QTD_ITEM).ToString("N2");
                _prod.xProd = item.ESTOQUE.DESCRICAO;
                _det.prod = _prod;

            }

            envCFeCFeInfCFeDetImpostoICMS _ICMS;
            envCFeCFeInfCFeDetImpostoICMSICMS00 _ICMS00;
            envCFeCFeInfCFeDetImpostoICMSICMS40 _ICMS40;
            envCFeCFeInfCFeDetImpostoICMSICMSSN102 _ICMS102;
            envCFeCFeInfCFeDetImpostoICMSICMSSN900 _ICMS900;
            envCFeCFeInfCFeDetImpostoPIS _PIS;
            envCFeCFeInfCFeDetImpostoPISPISAliq _PISAliq;
            envCFeCFeInfCFeDetImpostoPISPISQtde _PISQtde;
            envCFeCFeInfCFeDetImpostoPISPISNT _PISNT;
            envCFeCFeInfCFeDetImpostoPISPISSN _PISSN;
            envCFeCFeInfCFeDetImpostoPISPISOutr _PISOutr;
            envCFeCFeInfCFeDetImpostoPISST _PISST;
            envCFeCFeInfCFeDetImpostoCOFINS _COFINS;
            envCFeCFeInfCFeDetImpostoCOFINSCOFINSAliq _COFINSAliq;
            envCFeCFeInfCFeDetImpostoCOFINSCOFINSQtde _COFINSQtde;
            envCFeCFeInfCFeDetImpostoCOFINSCOFINSNT _COFINSNT;
            envCFeCFeInfCFeDetImpostoCOFINSCOFINSSN _COFINSSN;
            envCFeCFeInfCFeDetImpostoCOFINSCOFINSOutr _COFINSOutr;
            envCFeCFeInfCFeDetImpostoCOFINSST _COFINSST;
            envCFeCFeInfCFeDetImpostoISSQN _ISSQN;
            envCFeCFeInfCFeTotal _Total;
            List<envCFeCFeInfCFePgtoMP> _listaPagamentos;
            envCFeCFeInfCFePgtoMP _MP;
            envCFeCFeInfCFeDetProdObsFiscoDet _obsFiscoDet;
            List<envCFeCFeInfCFeDetProdObsFiscoDet> _listaObsFiscoDet;


            //Preenche cabeçalho do XML do SAT
            _Cfe.infCFe = _infCfe;



            _listaDets = new List<envCFeCFeInfCFeDet>();


            return _Cfe;
        }

        private string ConverteCFeEmXML(envCFeCFe cfeAConverter)
        {
            var settings = new XmlWriterSettings() { Encoding = new UTF8Encoding(true), OmitXmlDeclaration = false, Indent = false };
            var XmlFinal = new StringBuilder();
            var serializer = new XmlSerializer(typeof(envCFeCFe));
            using (var xwriter2 = XmlWriter.Create(XmlFinal, settings))
            {
                var xns = new XmlSerializerNamespaces();
                xns.Add(string.Empty, string.Empty);
                Directory.CreateDirectory(@"SAT_LOG");
                serializer.Serialize(xwriter2, cfeAConverter, xns); //Popula o stringbuilder para ser enviado para o SAT.
            }
            return XmlFinal.ToString();
        }
    }
}
