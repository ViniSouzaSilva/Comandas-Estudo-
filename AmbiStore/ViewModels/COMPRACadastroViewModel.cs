using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Extension;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Shared.SEFAZ.NF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.ViewModels
{
    public class COMPRACadastroViewModel : ViewModelBase
    {
        private bool fornecedorExpandido = true;
        public CONTATO FornecSelecionado { get; set; }
        public bool FornecedorExpandido
        {
            get { return fornecedorExpandido; }
            set
            {
                fornecedorExpandido = value;
                if (!fornecedorExpandido)
                {
                    impostosExpandido = true;
                    OnPropertyChanged("impostosExpandido");
                }
                else
                {
                    impostosExpandido = transporteExpandido = faturamentoExpandido = false;
                    OnPropertyChanged("impostosExpandido");
                    OnPropertyChanged("transporteExpandido");
                    OnPropertyChanged("faturamentoExpandido");
                }
            }
        }

        internal async Task<bool> GravaCompraNaBase()
        {
            DateTime entrada = DateTime.Now;
            if (COMPRA.FORNECEDOR is null)
            {
                MessageBox.Show("Fornecedor não pode ser nulo");
                return false;
            }
            if (COMPRA.COMPRA_ITEMs is null || COMPRA.COMPRA_ITEMs.Count == 0)
            {
                MessageBox.Show("Lista de itens inválida. Verifique se os itens foram adicionados");
                return false;
            }
            if (COMPRA.COMPRA_PAGAMENTOs is null || COMPRA.COMPRA_PAGAMENTOs.Count == 0)
            {
                MessageBox.Show("Lista de formas de pagamento inválida. Mas por enquanto n tem problema.\nVai lá filhão!");
            }
            using AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

            foreach (COMPRA_ITEM compraItem in COMPRA.COMPRA_ITEMs)
            {
                compraItem.ESTOQUE.QUANTIDADE += compraItem.QUANTIDADE_ITEM;
                compraItem.ESTOQUE.ULTIMO_FORNECEDOR = COMPRA.FORNECEDOR;
                compraItem.ESTOQUE.ULTIMA_COMPRA = entrada;
                compraItem.CFOP = await _context.CFOP_SISs.Select(x => x).Where(x => x.CFOP == 1102).FirstAsync();
                //_context.Update(compraItem.ESTOQUE);
            }
            _context.Update(COMPRA);
            //_context.UpdateRange(COMPRA.COMPRA_ITEMs);
            //_context.UpdateRange(COMPRA.COMPRA_PAGAMENTOs);
            await _context.SaveChangesAsync();
            return true;
        }

        internal void ImportaCompra(COMPRA cOMPRA_IMPORTADA)
        {
            COMPRA = cOMPRA_IMPORTADA;
            COMPRA.COMPRADOR = FUN_LOGADO;
            OnPropertyChanged("COMPRA");
        }

        private bool impostosExpandido;

        public bool ImpostosExpandido
        {
            get { return impostosExpandido; }
            set
            {
                impostosExpandido = value;
                if (!impostosExpandido)
                {
                    transporteExpandido = true;
                    OnPropertyChanged("transporteExpandido");
                }
                else
                {
                    fornecedorExpandido = transporteExpandido = faturamentoExpandido = false;
                    OnPropertyChanged("fornecedorExpandido");
                    OnPropertyChanged("transporteExpandido");
                    OnPropertyChanged("faturamentoExpandido");
                }

            }
        }

        internal async Task AtualizaDropDownFornec(string text)
        {
            switch (tipoPesqFornec)
            {
                case TipoPesquisaCBB.Containing:
                    fornecedores_list = fornecedores_list_total.Select(x => x).Where(x => x.NOME_JURIDICO.Contains(text)).ToList();
                    break;
                case TipoPesquisaCBB.StartsWith:
                    fornecedores_list = fornecedores_list_total.Select(x => x).Where(x => x.NOME_JURIDICO.StartsWith(text)).ToList();
                    break;
                default:
                    fornecedores_list = fornecedores_list_total.Select(x => x).Where(x => x.NOME_JURIDICO.StartsWith(text)).ToList();
                    break;
            }
            OnPropertyChanged("CFOP_SIS_List");
        }

        internal async Task AtualizaDropDownCFOP(string strPesquisada)
        {
            if (strPesquisada.Length == 4 && strPesquisada.IsNumbersOnly())
                cfop_sis_list = cfop_sis_list_total.Select(x => x).Where(x => x.CFOP == int.Parse(strPesquisada)).ToList();

            else
                switch (tipoPesqCFOP)
                {
                    case TipoPesquisaCBB.Containing:
                        cfop_sis_list = cfop_sis_list_total.Select(x => x).Where(x => x.DESCRICAO.Contains(strPesquisada)).ToList();
                        break;
                    case TipoPesquisaCBB.StartsWith:
                        cfop_sis_list = cfop_sis_list_total.Select(x => x).Where(x => x.DESCRICAO.StartsWith(strPesquisada)).ToList();
                        break;
                    default:
                        cfop_sis_list = cfop_sis_list_total.Select(x => x).Where(x => x.DESCRICAO.StartsWith(strPesquisada)).ToList();
                        break;
                }
            OnPropertyChanged("CFOP_SIS_List");
        }

        internal async void ImportaTNFe(COMPRA COMPRA_IMPORTADA)
        {
            //COMPRA.CHAVE = COMPRA_IMPORTADA;
            //COMPRA.NUMERO_NF = int.Parse(importaNotaVM.NUMERO);
            //COMPRA.NF_MODELO = "55";
            //COMPRA.NUMERO_SERIE = importaNotaVM.SERIE;
            //COMPRA.DATA_EMISSAO = DateTime.Parse(importaNotaVM.TNFE.infNFe.ide.dhEmi);
            //COMPRA.FORNECEDOR = importaNotaVM.CONTATO_DA_NOTA;
            //FornecSelecionado = await _context.CONTATOs.Select(x => x).Where(x => x.ID == COMPRA.FORNECEDOR.ID).FirstOrDefaultAsync();
            //foreach (var item in importaNotaVM.ITEMS)
            //{
            //    COMPRA.COMPRA_ITEMs.Add(new COMPRA_ITEM()
            //    {
            //        ESTOQUE = await _context.ESTOQUEs.Select(x => x).Where(x => x.ID == (int)item.CodEstoque).FirstOrDefaultAsync(),
            //        QUANTIDADE_ITEM = item.Quant,
            //        CFOP_ID = 5102
            //    });

            //}
            //OnPropertyChanged(null);
        }

        private bool transporteExpandido;

        public bool TransporteExpandido
        {
            get { return transporteExpandido; }
            set
            {
                transporteExpandido = value;
                if (!transporteExpandido)
                {
                    faturamentoExpandido = true;
                    OnPropertyChanged("faturamentoExpandido");
                }
                else
                {
                    fornecedorExpandido = faturamentoExpandido = impostosExpandido = false;
                    OnPropertyChanged("fornecedorExpandido");
                    OnPropertyChanged("faturamentoExpandido");
                    OnPropertyChanged("impostosExpandido");
                }
            }
        }
        private bool faturamentoExpandido;

        public bool FaturamentoExpandido
        {
            get { return faturamentoExpandido; }
            set
            {
                faturamentoExpandido = value;
                if (!faturamentoExpandido)
                {
                    fornecedorExpandido = true;
                    OnPropertyChanged("fornecedorExpandido");
                }
                else
                {
                    fornecedorExpandido = transporteExpandido = impostosExpandido = false;
                    OnPropertyChanged("fornecedorExpandido");
                    OnPropertyChanged("transporteExpandido");
                    OnPropertyChanged("impostosExpandido");
                }
            }
        }

        private List<CFOP_SIS> cfop_sis_list;
        private List<CFOP_SIS> cfop_sis_list_total;
        public List<FUNCIONARIO> compradores;

        private List<CONTATO> fornecedores_list;

        public List<CONTATO> Fornecedores_List
        {
            get { return fornecedores_list; }
            set { fornecedores_list = value; }
        }

        public List<CONTATO> fornecedores_list_total;
        public List<CFOP_SIS> CFOP_SIS_List
        {
            get { return cfop_sis_list; }
        }
        public COMPRA COMPRA { get; set; } = new COMPRA() { COMPRA_ITEMs = new List<COMPRA_ITEM>() };

        private CFOP_SIS cfop_sis_selecionado;

        public CFOP_SIS CFOP_SIS_Selecionado
        {
            get { return cfop_sis_selecionado; }
            set
            {
                cfop_sis_selecionado = value;
                OnPropertyChanged("CFOP_SIS_Selecionado");
            }
        }

        public COMPRACadastroViewModel()
        {
            using AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

            cfop_sis_list_total = _context.CFOP_SISs.Select(x => x).Where(x => x.CFOP <= 1999).ToList();
            cfop_sis_list = new List<CFOP_SIS>(cfop_sis_list_total);
            compradores = _context
                .FUNCIONARIOs
                .Select(x => x)
                .Include(x => x.CONTATO_FUNCIONARIO)
                .Include(x => x.CARGOS)
                .Where(x => x.CARGOS.Select(x => x).Any() == true && x.STATUS == Status.Ativo).ToList();
            fornecedores_list_total = _context
                .CONTATOs
                .Select(x => x)
                .AsNoTracking()
                .Where(x => x.STATUS == Status.Ativo)
                .ToList();
            fornecedores_list = new List<CONTATO>(fornecedores_list_total);
        }
        private TipoPesquisaCBB tipoPesqCFOP;

        public TipoPesquisaCBB TipoPesqCFOP
        {
            set
            {
                tipoPesqCFOP = value;
                OnPropertyChanged("CFOPContendoSelected");
                OnPropertyChanged("CFOPComecandoSelected");
            }
        }


        public Visibility CFOPContendoSelected
        {
            get
            {
                return tipoPesqCFOP switch
                {
                    TipoPesquisaCBB.Containing => Visibility.Visible,
                    _ => Visibility.Collapsed
                };
            }
        }
        public Visibility CFOPComecandoSelected
        {
            get
            {
                return tipoPesqCFOP switch
                {
                    TipoPesquisaCBB.StartsWith => Visibility.Visible,
                    _ => Visibility.Collapsed
                };
            }
        }
        private TipoPesquisaCBB tipoPesqFornec;

        public TipoPesquisaCBB TipoPesqFornec
        {
            set
            {
                tipoPesqFornec = value;
                OnPropertyChanged("FornecContendoSelected");
                OnPropertyChanged("FornecComecandoSelected");
            }
        }


        public Visibility FornecContendoSelected
        {
            get
            {
                return tipoPesqFornec switch
                {
                    TipoPesquisaCBB.Containing => Visibility.Visible,
                    _ => Visibility.Collapsed
                };
            }
        }
        public Visibility FornecComecandoSelected
        {
            get
            {
                return tipoPesqFornec switch
                {
                    TipoPesquisaCBB.StartsWith => Visibility.Visible,
                    _ => Visibility.Collapsed
                };
            }
        }

    }
}
