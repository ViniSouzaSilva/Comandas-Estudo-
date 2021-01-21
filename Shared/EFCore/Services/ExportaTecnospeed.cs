using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Extension;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Shared.Serializador.NFe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.Shared.EFCore.Services
{
    class ExportaTecnospeed
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public string erro;
        public ExportaTecnospeed()
        {

        }
        //public bool Importanfe(VENDA venda)
        //{
        //    nfeproc nfeProc = new nfeproc();
        //    // VENDA venda = new VENDA();
        //    //venda.ITENS
        //    nfeProc.versao = "4.0";
        //    EMITENTE emitente = emitenteDS.Get(1).Result;
        //    //nfeProc.NfeProc.NFe.infNFe.ide.cUF = emitente.
        //    NATUREZA_OPERACAO natoper = _context.NATUREZA_OPERACAOs
        //    .Select(x => x)
        //    .Where(x => x.CFOP.CFOP == int.Parse(nfeProc.NfeProc.NFe.infNFe.det.FirstOrDefault().prod.CFOP))
        //    .FirstOrDefault();
        //    if (natoper is null)
        //    {
        //        erro = "CFOP incorreto";
        //        return false;

        //    }

        //    venda.NATUREZA_OPERACAO = natoper;

        //    CONTATO Fornecedor = _context.CONTATOs
        //   .Select(x => x)
        //   .Where(x => x.CONTATO_PJ.CNPJ == nfeProc.NfeProc.NFe.infNFe.emit.Item || x.CONTATO_PF.CPF == nfeProc.NfeProc.NFe.infNFe.emit.Item)
        //   .FirstOrDefault();
        //    if (Fornecedor is null)
        //    {

        //        erro = "Forncededor não encontrado";
        //        return false;
        //    }
        //    else
        //    {
        //        venda.CONTATO = Fornecedor;
        //    }

        //    nfeProc.NfeProc.NFe.infNFe.ide.cNF = venda.NF_NUMERO.ToString();
        //    nfeProc.NfeProc.NFe.infNFe.ide.serie = venda.NF_SERIE.ToString();
        //    nfeProc.NfeProc.NFe.infNFe.ide.mod = (SEFAZ.NF.TMod)int.Parse(venda.NF_MODELO);
        //    nfeProc.NfeProc.NFe.infNFe.ide.dhEmi = venda.DT_EMISSAO.ToString();
        //    nfeProc.NfeProc.NFe.infNFe.ide.dhSaiEnt = venda.DT_SAIDA.ToString();
        //    nfeProc.NfeProc.NFe.infNFe.transp.vol[0].esp = venda.ESPECIE;
        //    switch (venda.TIPO_FRETE)
        //    {
        //        case "0":
        //            // venda.TIPO_FRETE = ModalidadeFrete.ContratacaoFretePorcontaDoRemetenteCIF.ToString();
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.ContratacaoFretePorcontaDoRemetenteCIF;
        //            break;
        //        case "1":
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.ContratacaoFretePorContaDestinatárioFOB;
        //            break;
        //        case "2":
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.Terceiros;
        //            break;
        //        case "3":
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.Remetente;
        //            break;
        //        case "4":
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.Destinatario;
        //            break;
        //        case "9":
        //            nfeProc.NfeProc.NFe.infNFe.transp.modFrete = (SEFAZ.NF.TNFeInfNFeTranspModFrete)ModalidadeFrete.SemFrete;
        //            break;
        //        default:
        //            break;
        //    }

        //   nfeProc.NfeProc.NFe.infNFe.transp.vol[0].pesoL = venda.PESO_LIQUIDO.ToString("F2"); 

        //    if (venda.ENTRADA_SAIDA == 0)
        //    {
        //        nfeProc.NfeProc.NFe.infNFe.ide.tpNF = (SEFAZ.NF.TNFeInfNFeIdeTpNF)Entrada_Saida.Entrada;
        //    }
        //    else
        //    {
        //        nfeProc.NfeProc.NFe.infNFe.ide.tpNF = (SEFAZ.NF.TNFeInfNFeIdeTpNF)Entrada_Saida.Saida;
        //    }
        //    nfeProc.NfeProc.NFe.infNFe.transp.vol[0].qVol = venda.QUANTIDADE_VOLUME.ToString();
        //    nfeProc.NfeProc.NFe.infNFe.transp.vol[0].nVol = venda.NUMERO_VOLUME.ToString();


        //    // venda.SOMA_FRETE = true;//TODO



        //    if ( venda.VLR_TROCO != null)
        //    {
        //       nfeProc.NfeProc.NFe.infNFe.pag.vTroco = venda.VLR_TROCO.ToString();
        //    }
        //    else
        //    {
        //        nfeProc.NfeProc.NFe.infNFe.pag.vTroco = "0";
        //    }
        //     nfeProc.NfeProc.NFe.infNFe.ide.indPres = (SEFAZ.NF.TNFeInfNFeIdeIndPres)venda.IND_PRES;
        //    switch (venda.IND_IE_DEST)
        //    {
        //        case '1':
        //            nfeProc.NfeProc.NFe.infNFe.dest.indIEDest = (SEFAZ.NF.TNFeInfNFeDestIndIEDest)IndicadorIE.ContribuinteICMS;                
        //            break;
        //        case '2':
        //            nfeProc.NfeProc.NFe.infNFe.dest.indIEDest = (SEFAZ.NF.TNFeInfNFeDestIndIEDest)IndicadorIE.ContribuinteIsentoIEICMS;
        //            break;
        //        case '9':
        //            nfeProc.NfeProc.NFe.infNFe.dest.indIEDest = (SEFAZ.NF.TNFeInfNFeDestIndIEDest)IndicadorIE.NaoContribuinte;                  
        //            break;
        //        default:
        //            break;
        //    }
        //    //venda.DESCONTO_CONDICIONAL = //TODO
             
        //    nfeProc.NfeProc.NFe.infNFe.infAdic.infCpl = venda.INFO_COMPLEMENTAR_FIXA;
        //    nfeProc.NfeProc.NFe.infNFe.infAdic.infAdFisco = venda.INFO_COMPLEMENTAR_EDITAVEL;

        //    //venda.SUBTRAI_ICMS_DESONERADO = //TODO

        //    //venda.VLR_JUROS_PARCELAMENTO = //TODO

        //    nfeProc.NfeProc.NFe.infNFe.det[0].prod.vProd = venda.VALORVENDA.ToString() ;

        //    //VENDA_PAGAMENTO vendaPag = new VENDA_PAGAMENTO();

        //       nfeProc.NfeProc.NFe.infNFe.pag.detPag[0].tPag = (SEFAZ.NF.TNFeInfNFePagDetPagTPag)int.Parse(venda.VENDA_PAGAMENTOs[0].FORMAPAGAMENTO.DESCRICAO);

             
        //}
    }
}

