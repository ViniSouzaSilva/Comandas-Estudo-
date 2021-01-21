using AmbiStore.Shared.Auxiliares;
using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AmbiStore.Shared.Libraries.Enums;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using AmbiStore.Shared.EFCore.Services;
using static AmbiStore.Shared.Libraries.Static;
using RestSharp;
using System.Xml;
using AmbiStore.Shared.SEFAZ.NF;
using System.Linq;
using AmbiStore.Shared.Serializador.NFe;
using Microsoft.EntityFrameworkCore;

namespace AmbiStore.Shared.Libraries
{
    public class Functions
    {

        public decimal ConverterParaReais(decimal valor, Moeda moeda)
        {
            WebClient client = new WebClient();
            string downloaded;
            try
            {
                downloaded = client.DownloadString($@"https://economia.awesomeapi.com.br/json/all");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            APICambio json = JsonConvert.DeserializeObject<APICambio>(downloaded);
            return valor * decimal.Parse(moeda switch
            {
                Moeda.USD => json.USD.ask,
                Moeda.USDT => json.USDT.ask,
                Moeda.CAD => json.CAD.ask,
                Moeda.AUD => json.AUD.ask,
                Moeda.EUR => json.EUR.ask,
                Moeda.GBP => json.GBP.ask,
                Moeda.ARS => json.ARS.ask,
                Moeda.JPY => json.JPY.ask,
                Moeda.CHF => json.CHF.ask,
                Moeda.CNY => json.CNY.ask,
                Moeda.ILS => json.ILS.ask,
                Moeda.BTC => json.BTC.ask,
                Moeda.LTC => json.LTC.ask,
                Moeda.ETH => json.ETH.ask,
                Moeda.XRP => json.XRP.ask,
                _ => "1"
            }, CultureInfo.InvariantCulture);
        }
        public decimal ConverterDeReais(decimal valor, Moeda moeda)
        {
            string downloaded;
            IRestResponse response;
            try
            {

                var client = new RestClient("https://economia.awesomeapi.com.br/json/all");
                client.Timeout = 3000;
                var request = new RestRequest(Method.GET);
                response = client.Execute(request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            switch (response.ResponseStatus)
            {
                case ResponseStatus.None:
                case ResponseStatus.Error:
                case ResponseStatus.Aborted:
                    return 0M;
                case ResponseStatus.Completed:
                    break;
                case ResponseStatus.TimedOut:
                    return -999M;
                default:
                    return 0M;
            }
            downloaded = response.Content;
            APICambio json = JsonConvert.DeserializeObject<APICambio>(downloaded);
            return valor / decimal.Parse(moeda switch
            {
                Moeda.USD => json.USD.bid,
                Moeda.USDT => json.USDT.bid,
                Moeda.CAD => json.CAD.bid,
                Moeda.AUD => json.AUD.bid,
                Moeda.EUR => json.EUR.bid,
                Moeda.GBP => json.GBP.bid,
                Moeda.ARS => json.ARS.bid,
                Moeda.JPY => json.JPY.bid,
                Moeda.CHF => json.CHF.bid,
                Moeda.CNY => json.CNY.bid,
                Moeda.ILS => json.ILS.bid,
                Moeda.BTC => json.BTC.bid,
                Moeda.LTC => json.LTC.bid,
                Moeda.ETH => json.ETH.bid,
                Moeda.XRP => json.XRP.bid,
                _ => "1"
            }, CultureInfo.InvariantCulture);
        }
        public (MOVIMENTO credito, MOVIMENTO debito) CriaPartidaDobrada(DateTime saida, decimal valor, string historico, PLANO_CONTA ctaCredito, PLANO_CONTA ctaDebito)
        {
            MOVIMENTO movCred = new MOVIMENTO()
            {
                DATAMOVIMENTO = saida,
                VALOR = valor,
                HISTORICO = historico,
                PLANO = ctaCredito,
                TIPO = TipoMovimento.Credito
            };
            MOVIMENTO movDeb = new MOVIMENTO()
            {
                DATAMOVIMENTO = saida,
                VALOR = valor,
                HISTORICO = historico,
                PLANO = ctaDebito,
                TIPO = TipoMovimento.Debito
            };

            return (movCred, movDeb);
        }
        public async Task<bool> VerificaCadastroSenhaAdmin()
        {
            using var context = new AmbiStoreDbContextFactory().CreateDbContext();
            if ((await context.FUNCIONARIOs.Select(x=>x).Where(x=>x.ID == -1).FirstAsync()).SENHA is null)
            {
                return false;
            }
            else return true;
        }
        public async Task<bool> VerificaUsuarioESenha(FUNCIONARIO user, string password)
        {
            using var context = new AmbiStoreDbContextFactory().CreateDbContext();
            if ((await context.FUNCIONARIOs.Select(x => x).Where(x => x.ID == user.ID).FirstAsync()).SENHA == GetMD5Hash(password)) return true;
            else return false;
        }

        public async Task<bool> VerificaSenhaPorCargo(string password, Cargo[] cargos)
        {
            using var context = new AmbiStoreDbContextFactory().CreateDbContext();
            if ((await context.FUNCIONARIOs
                .Select(x => x)
                .Include(x=>x.CARGOS)
                .Where(x => x.SENHA == GetMD5Hash(password))
                .FirstOrDefaultAsync())
                .CARGOS
                    .Select(x => x.CARGO)
                    .Intersect(cargos)
                    .Any())
            {
                return true;
            }
            else
            {
                return false;
            }               
        }

        //public COMPRA ConverteXMLParaCOMPRA(string xml)
        //{
        //    using AmbiStoreDbContext context = (new AmbiStoreDbContextFactory()).CreateDbContext();

        //    COMPRA compra = new COMPRA();
        //    var serializer = new XmlSerializer(typeof(TNFe));
        //    using var xmlEntrada = new StringReader(xml);
        //    using var xReader = XmlReader.Create(xmlEntrada);
        //    TNFe NFeEntrada = (TNFe)serializer.Deserialize(xReader);

        //    compra.NUMERO_NF = int.Parse(NFeEntrada.infNFe.ide.nNF);
        //    compra.NUMERO_SERIE = NFeEntrada.infNFe.ide.serie;
        //    compra.NF_MODELO = ((int)NFeEntrada.infNFe.ide.mod).ToString();
        //    compra.DATA_EMISSAO = DateTime.ParseExact(NFeEntrada.infNFe.ide.dhEmi, "yyyy-MM-ddTHH:mm:ss-03:00", CultureInfo.InvariantCulture);
        //    compra.DATA_ENTRADA = DateTime.Today;
        //    compra.STATUS_NOTA = Status_Nota.Emitida;
        //    var natoper = new NATUREZA_OPERACAO();
        //    CFOP_SIS cfopPrimario = context.CFOP_SISs.Select(x => x).Where(x => x.CFOP == int.Parse(NFeEntrada.infNFe.det[0].prod.CFOP)).FirstOrDefault();
        //    natoper.CFOP = cfopPrimario;
        //    compra.NATUREZA_OPERACAO = natoper;
        //    foreach (var produto in NFeEntrada.infNFe.det)
        //    {
        //        COMPRA_ITEM item = new COMPRA_ITEM();
        //        item.CFOP = context.CFOP_SISs.Select(x => x).Where(x => x.CFOP == int.Parse(produto.prod.CFOP)).FirstOrDefault();
        //        item.NUMERO_ITEM = int.Parse(produto.nItem);
        //        item.
        //    }

        //}
    }

    public class NFeEntradaFunctions
    {
        AmbiStoreDbContext _context = (new AmbiStoreDbContextFactory()).CreateDbContext();

        public TNFe Deserializa(string xml)
        {
            XmlRootAttribute atrib = new XmlRootAttribute();
            // var atrib = new XmlRootAttribute("nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = true);
            atrib.ElementName = "nfeProc";
            atrib.Namespace = "http://www.portalfiscal.inf.br/nfe";
            atrib.IsNullable = true;

            var serializer = new XmlSerializer(typeof(nfeproc), atrib);
            using var xmlEntrada = new StringReader(xml);
            //using var xReader = XmlReader.Create(xmlEntrada);
            using var xReader = new XmlTextReader(xmlEntrada);
            //xReader.Namespaces = true;
            nfeproc nota =  (nfeproc)serializer.Deserialize(xReader);
            return nota.NFe;
        }
        public CONTATO VerificaEmitente(TNFe tnfe)
        {
            CONTATO fornecedor = _context.CONTATOs
                .Select(x => x)
                .Where(x => x.CONTATO_PJ.CNPJ == tnfe.infNFe.emit.Item || x.CONTATO_PF.CPF == tnfe.infNFe.emit.Item)
                .FirstOrDefault();
            return fornecedor;
        }
        public (EstoqueParam resultado, ESTOQUE estoque) VerificaEstoque(CONTATO fornecedor, string codFornec, string descProd)
        {
            PARAMETRIZACAO paramt = _context.PARAMETRIZACAOs.Select(x => x).Where(x => x.FORNECEDOR == fornecedor && x.COD_FORNECEDOR == codFornec).FirstOrDefault();
            if (!(paramt is null))
            {
                return (EstoqueParam.Conferido, paramt.ESTOQUE);
            }
            else
            {
                return (EstoqueParam.Inexistente, null);
            }
        }
    }
}