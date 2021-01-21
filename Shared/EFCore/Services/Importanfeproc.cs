using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Serializador.NFe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmbiStore.Shared.Libraries.Enums;
using System.Reflection.Metadata;
using System.Threading;

namespace AmbiStore.Shared.EFCore.Services
{
    public class Importanfeproc
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public string erro;
        public Importanfeproc()
        {

        }
        public bool Validanfeproc(nfeproc nfeProc)
        {
            // verifica se o cnpj corresponde ao EMITENTE.CNPJ

            if (nfeProc.NfeProc.NFe.infNFe.dest.ItemElementName != SEFAZ.NF.ItemChoiceType3.CNPJ)
            {
                erro = "Nota não emitida para pessoa juridica";
                return false;
            }
            EMITENTE emit = _context.EMITENTEs
            .Select(x => x)
            .Where(x => x.CNPJ == nfeProc.NfeProc.NFe.infNFe.dest.Item)
            .FirstOrDefault();

            if (emit is null)
            {
                erro = "CNPJ do destinatário não corresponde ao CNPJ atual";
                return false;
            }

            //mais verif

            erro = string.Empty;
            return true;

        }
        //public bool Importanfe(nfeproc nfeProc, Status status, bool ProduRevenda,bool SomaFrete) 
        //{
        //    if (!Validanfeproc(nfeProc)) 
        //    {
        //        return false;

        //    }
        //    VENDA venda = new VENDA();

           

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

        //    venda.NF_NUMERO = int.Parse(nfeProc.NfeProc.NFe.infNFe.ide.cNF);
        //    venda.NF_SERIE = nfeProc.NfeProc.NFe.infNFe.ide.serie;
        //    venda.NF_MODELO = nfeProc.NfeProc.NFe.infNFe.ide.mod.ToString();
        //    venda.DT_EMISSAO = DateTime.Parse(nfeProc.NfeProc.NFe.infNFe.ide.dhEmi);
        //    venda.DT_SAIDA = DateTime.Parse(nfeProc.NfeProc.NFe.infNFe.ide.dhSaiEnt);
        //    venda.ESPECIE = nfeProc.NfeProc.NFe.infNFe.transp.vol[0].esp;
        //    switch ((int)nfeProc.NfeProc.NFe.infNFe.transp.modFrete)
        //    {
        //        case 0 :
        //            venda.TIPO_FRETE = ModalidadeFrete.ContratacaoFretePorcontaDoRemetenteCIF.ToString();
        //            break;
        //        case 1 :
        //            venda.TIPO_FRETE = ModalidadeFrete.ContratacaoFretePorContaDestinatárioFOB.ToString();
        //            break;
        //        case 2 :
        //            venda.TIPO_FRETE = ModalidadeFrete.Terceiros.ToString();
        //            break;
        //        case 3 :
        //            venda.TIPO_FRETE = ModalidadeFrete.Remetente.ToString();
        //            break;
        //        case 4 :
        //            venda.TIPO_FRETE = ModalidadeFrete.Destinatario.ToString();
        //            break;
        //        case 9:
        //            venda.TIPO_FRETE = ModalidadeFrete.SemFrete.ToString();
        //            break;
        //        default:
        //            break;
        //    }
            
        //    venda.PESO_LIQUIDO = decimal.Parse(nfeProc.NfeProc.NFe.infNFe.transp.vol[0].pesoL);
         
        //    if (nfeProc.NfeProc.NFe.infNFe.ide.tpNF == 0)
        //    {
        //        venda.ENTRADA_SAIDA = Entrada_Saida.Entrada;
        //    }
        //    else 
        //    {
        //        venda.ENTRADA_SAIDA = Entrada_Saida.Saida;
        //    }
        //    venda.QUANTIDADE_VOLUME = decimal.Parse(nfeProc.NfeProc.NFe.infNFe.transp.vol[0].qVol);
        //    venda.NUMERO_VOLUME = decimal.Parse(nfeProc.NfeProc.NFe.infNFe.transp.vol[0].nVol);


        //    // venda.SOMA_FRETE = true;//TODO



        //    if (nfeProc.NfeProc.NFe.infNFe.pag.vTroco != "" || nfeProc.NfeProc.NFe.infNFe.pag.vTroco != null)
        //    {
        //        venda.VLR_TROCO = decimal.Parse(nfeProc.NfeProc.NFe.infNFe.pag.vTroco);
        //    }
        //    else 
        //    {
        //        venda.VLR_TROCO = 0;
        //    }
        //    venda.IND_PRES = (char)nfeProc.NfeProc.NFe.infNFe.ide.indPres;
        //    switch ((int)nfeProc.NfeProc.NFe.infNFe.dest.indIEDest)
        //    {
        //        case 1 :
        //            venda.IND_IE_DEST = (char)IndicadorIE.ContribuinteICMS; 
        //            break;
        //        case 2 :
        //            venda.IND_IE_DEST = (char)IndicadorIE.ContribuinteIsentoIEICMS;
        //            break;
        //        case 9 :
        //            venda.IND_IE_DEST = (char)IndicadorIE.NaoContribuinte;  
        //            break;
        //        default:
                   
        //            break;
        //    }
        //    //venda.DESCONTO_CONDICIONAL = //TODO

        //    venda.INFO_COMPLEMENTAR_FIXA = nfeProc.NfeProc.NFe.infNFe.infAdic.infCpl;
        //    venda.INFO_COMPLEMENTAR_EDITAVEL = nfeProc.NfeProc.NFe.infNFe.infAdic.infAdFisco;

        //    //venda.SUBTRAI_ICMS_DESONERADO = //TODO

        //    //venda.VLR_JUROS_PARCELAMENTO = //TODO

        //    venda.VALORVENDA = decimal.Parse(nfeProc.NfeProc.NFe.infNFe.det[0].prod.vProd);
            
        //    VENDA_PAGAMENTO vendaPag = new VENDA_PAGAMENTO();


        //    //vendaPag.FORMAPAGAMENTO = 
        //}
    }
}
