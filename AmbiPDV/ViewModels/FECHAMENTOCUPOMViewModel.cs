using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmbiPDV.ViewModels
{
    public class FECHAMENTOCUPOMViewModel : ViewModelBase
    {
        #region NÃO FISCAL - HOMOLOGAÇÃO
        bool fiscal = true;
        #endregion NÃO FISCAL - HOMOLOGAÇÃO
        public List<FORMAPAGAMENTO> FORMASATIVAS { get; set; }
        public List<VENDA_PAGAMENTO> PAGAMENTOS_FEITOS { get; set; }
        internal FORMAPAGAMENTO FORMASELECIONADA { get; set; }
        public VENDA VENDA_EM_CURSO { get; set; }
        public decimal VALOR_DA_VENDA
        {
            get
            {
                decimal valorDaVenda = 0M;
                foreach (VENDA_ITEM item in VENDA_EM_CURSO.VENDA_ITEMs)
                {
                    valorDaVenda += (item.ESTOQUE.PRECO_VENDA - item.VLR_DESCONTO) * item.QTD_ITEM;
                }
                return valorDaVenda;
            }
        }
        public int FORMAPAGTO
        {
            set
            {
                FORMASELECIONADA = FORMASATIVAS.Select(x => x).Where(x => x.ID == value).FirstOrDefault();
            }
        }
        public decimal VALORPAGTO { get; set; }
        public int PARCELAS { get; set; } = 1;
        public decimal VALORPAGO { get; set; } = 0;
        public decimal DESCONTO { get; set; } = 0;
        public decimal TROCO { get; set; } = 0;
        public decimal SALDORESTANTE
        {
            get
            {
                if (PAGAMENTOS_FEITOS is null) return VALOR_DA_VENDA;
                else return VALOR_DA_VENDA - PAGAMENTOS_FEITOS.Sum(x => x.VLR_PAGTO);
            }
        }

        public FECHAMENTOCUPOMViewModel()
        {

        }
        public FECHAMENTOCUPOMViewModel(VENDA vendaEmCurso, bool _fiscal)
        {
            VENDA_EM_CURSO = vendaEmCurso;
            fiscal = _fiscal;
            using AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            FORMASATIVAS = new List<FORMAPAGAMENTO>(_context
                .FORMAPAGAMENTOs
                .Select(x => x)
                .Include(x => x.PARCELAMENTO)
                .Where(x =>
                    x.STATUS == Status.Ativo &&
                    (x.UTILIZACAO == UsoFMAPAGTO.Ambos || x.UTILIZACAO == UsoFMAPAGTO.CupomFiscal)
                    )
                .ToArray()
                );
            VALORPAGTO = VALOR_DA_VENDA;
        }

        public string ValidaFormaEscolhida()
        {
            if (FORMASELECIONADA is null)
            {
                return "Código digitado inválido";
            }
            else return "0";
        }
        public bool ProcessaMetodoAtual()
        {
            VENDA_PAGAMENTO pagamento = new VENDA_PAGAMENTO();
            pagamento.FORMAPAGAMENTO = FORMASELECIONADA;
            pagamento.VLR_PAGTO = VALORPAGTO;
            pagamento.PLANO_CONTA = pagamento.PLANO_CONTA;
            VENDA_EM_CURSO.VENDA_PAGAMENTOs.Add(pagamento);
            OnPropertyChanged(null);
            FORMASELECIONADA = null;
            if (FaltaPagarMaisCoisa())
            {
                return false;
            }
            else
            {
                try
                {
                    FinalizaProcessoDeVenda();
                }
                catch (Exception)
                {
                    throw;
                }
                return true;
            }
        }

        private void FinalizaProcessoDeVenda()
        {
            if (fiscal)
            {
                Services.SAT.FuncoesGerais FuncoesSAT = new Services.SAT.FuncoesGerais();
               // FuncoesSAT.EnviaDadosParaSAT();
            }
            SalvaVendaNaBase();
            //ImprimeVendaNoCupom();
        }

        private void SalvaVendaNaBase()
        {
            using AmbiStoreDbContext context = new AmbiStoreDbContextFactory().CreateDbContext();
            context.Update(VENDA_EM_CURSO);
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool FaltaPagarMaisCoisa()
        {
            return (!(SALDORESTANTE <= 0));
        }
        private bool ExecutaOperacaoTEF()
        {
            return true;
        }
    }
}
