using AmbiPDV.Auxiliares;
using AmbiPDV.Controls;
using AmbiPDV.Views;
using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static AmbiStore.Shared.Libraries.Static;
namespace AmbiPDV.ViewModels
{
    public class CAIXAViewModel : ViewModelBase
    {
        #region Properties
        public string AVISOS { get; set; }
        public bool COMBOBOXOPEN { get; set; } = false;
        public string CORTESIA { get; set; }
        public bool MODO_CONSULTA { get; set; } = false;
        public ObservableCollection<CUPOMEntry> CUPOMGRIDEntryList { get; set; } = new ObservableCollection<CUPOMEntry>();
        public List<ESTOQUE> ESTOQUEList { get; set; }
        public List<ESTOQUE> Estoque_List { get; set; }
        public List<ESTOQUE> Estoque_List_Full { get; set; }
        public ESTOQUE ESTOQUE_Selecionado { get; set; }
        public string HORA_ATUAL
        {
            get { return DateTime.Now.ToShortTimeString(); }
        }
        public string MARQUEE { get; set; }
        public TipoPesquisaCBB PESQUISACBB { get; set; } //= TipoPesquisaCBB.StartsWith;
        public StatusCaixaEnum STATUSCAIXA { get; set; } = StatusCaixaEnum.Fechado;
        public string OPERADOR
        {
            get { return $"Operador: {FUN_LOGADO.CONTATO_FUNCIONARIO.NOME_FANTASIA}"; }
        }
        public decimal QUANTIDADE { get; set; } = 1M;
        public decimal SUBTOTAL { get; set; }
        public string TERMINAL
        {
            get { return $"TERMINAL: {terminal}"; }
        }
        public (decimal valor, TipoDesconto? tipo) DESCONTO { get; set; }

#if DEBUG
#pragma warning disable CS0649
#endif
        private int terminal;
#pragma warning restore CS0649 
        public VENDA VENDA_EM_CURSO { get; set; }
        public decimal VLR_UNIT { get; set; }
        public decimal VLR_TOTAL { get { return (QUANTIDADE * VLR_UNIT); } }
        #endregion Properties

        #region Construtor
        public CAIXAViewModel()
        {
            using AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            Estoque_List_Full = _context
                            .ESTOQUEs
                            .Select(x => x)
                            .Include(x => x.PRODUTO_ESTOQUE)
                            .Include(x => x.SERVICO_ESTOQUE)
                            .Include(x => x.UNI_MEDIDA)
                            .Where(x => x.STATUS == Status.Ativo)
                            .ToList();
            Estoque_List = new List<ESTOQUE>(Estoque_List_Full);
            VerificaStatusDoTurno();
            OnPropertyChanged(null);
        }
        #endregion Construtor

        #region Methods
        private void VerificaStatusDoTurno()
        {
            AVISOS = "CAIXA LIVRE";
            STATUSCAIXA = StatusCaixaEnum.Livre;
        }
        internal void AplicaDescontoNoProduto()
        {
            DESCONTOView janelaDesconto = new DESCONTOView();
            switch (janelaDesconto.ShowDialog())
            {
                case true:
                    DESCONTO = ((DESCONTOViewModel)janelaDesconto.DataContext).DescontoAAplicar();
                    break;
                default:
                    return;
            }
        }
        private void AbreNovoCupom()
        {
            VENDA_EM_CURSO = new VENDA();
            STATUSCAIXA = StatusCaixaEnum.EmVenda;
            MARQUEE = null;
            OnPropertyChanged(null);
        }
        private void AdicionaProdutoNaDevolução()
        {
            throw new NotImplementedException();
        }
        internal void AdicionaProdutoNaTela(int numItem, string ean13, string descr, decimal qtd, string unid, decimal vlrUnit)
        {
            CUPOMGRIDEntryList.Add(new CUPOMEntry(numItem, ean13, descr, qtd, unid, vlrUnit));
            return;
        }
        internal void AdicionaProdutoNaTela(VENDA_ITEM _item)
        {
            string infadic = string.Empty;
            if (_item.VLR_DESCONTO > 0)
            {
                infadic = $"--Desconto de {_item.VLR_DESCONTO:C2}";
            }
            if (_item.QTD_ITEM >= _item.ESTOQUE.QTD_ATACADO)
            {
                infadic = $"--Desconto por atacado aplicado";
            }
            CUPOMGRIDEntryList.Add(new CUPOMEntry(_item.NUM_ITEM, _item.ESTOQUE.PRODUTO_ESTOQUE.COD_BARRA ?? _item.ESTOQUE.ID.ToString(),
                _item.ESTOQUE.DESCRICAO, _item.QTD_ITEM, _item.UNI_MEDIDA.ABREVIATURA, _item.ESTOQUE.PRECO_VENDA, infadic));
            return;
        }
        private void AdicionaProdutoNaVenda()
        {
            VENDA_ITEM _item = new VENDA_ITEM();
            _item.ESTOQUE = ESTOQUE_Selecionado;
            _item.CFOP = ESTOQUE_Selecionado.CFOP_CFE;
            _item.NUM_ITEM = VENDA_EM_CURSO.VENDA_ITEMs.Count > 0 ? VENDA_EM_CURSO.VENDA_ITEMs.Max(x => x.NUM_ITEM) + 1 : 1;
            _item.QTD_ITEM = QUANTIDADE;
            _item.UNI_MEDIDA = ESTOQUE_Selecionado.UNI_MEDIDA;
            if (DESCONTO.valor > 0)
            {
                switch (DESCONTO.tipo)
                {
                    case TipoDesconto.Absoluto:
                        if (DESCONTO.valor >= _item.ESTOQUE.PRECO_VENDA)
                        {
                            MessageBox.Show("Desconto aplicado não pode ser maior que o valor de venda do produto.");
                            return;
                        }
                        else _item.VLR_DESCONTO = DESCONTO.valor;
                        break;
                    case TipoDesconto.Porcentual:
                        _item.VLR_DESCONTO = _item.ESTOQUE.PRECO_VENDA * DESCONTO.valor;
                        break;
                    default:
                        break;
                }
            }
            VENDA_EM_CURSO.VENDA_ITEMs.Add(_item);
            AdicionaProdutoNaTela(_item);
            CORTESIA = _item.ESTOQUE.DESCRICAO;
            if (_item.QTD_ITEM >= _item.ESTOQUE.QTD_ATACADO)
                VLR_UNIT = _item.ESTOQUE.PRECO_ATACADO;
            else
                VLR_UNIT = _item.ESTOQUE.PRECO_VENDA;
            SUBTOTAL += (VLR_UNIT - _item.VLR_DESCONTO) * _item.QTD_ITEM;
            OnPropertyChanged("CORTESIA");
            OnPropertyChanged("VLR_UNIT");
            OnPropertyChanged("VLR_TOTAL");
            OnPropertyChanged("SUBTOTAL");
            QUANTIDADE = 1;
            OnPropertyChanged("QUANTIDADE");
            DESCONTO = (0, null);
        }
        internal void AlteraQuantidade(string text)
        {
            if (decimal.TryParse(text.Replace(',', '.').Split('*')[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal qtd) == true)
            {
                QUANTIDADE = qtd;
                OnPropertyChanged("QUANTIDADE");
            }
        }
        internal void AtualizaDropDownEstoque(string text)
        {
            switch (PESQUISACBB)
            {
                case TipoPesquisaCBB.Containing:
                    Estoque_List = Estoque_List_Full.Select(x => x).Where(x => x.DESCRICAO.ToLower().Contains(text.ToLower())).ToList();
                    break;
                case TipoPesquisaCBB.StartsWith:
                default:
                    Estoque_List = Estoque_List_Full
                        .Select(x => x)
                        .Where(x => x.DESCRICAO.ToLower().StartsWith(text.ToLower()))
                        .ToList();
                    break;
            }
            if (Estoque_List.Count == 0) { MessageBox.Show("Nenhum produto correspondente encontrado."); return; }
            OnPropertyChanged("Estoque_List");
            COMBOBOXOPEN = true;
            OnPropertyChanged("COMBOBOXOPEN");
        }
        internal void CancelaItemDaVendaAtual()
        {
            PerguntaInformacaoDialog piDialog = new PerguntaInformacaoDialog("REMOVENDO ITEM DA VENDA", "Digite o número do item");
            if (piDialog.ShowDialog() == true)
            {
                VENDA_ITEM itemARemover = VENDA_EM_CURSO.VENDA_ITEMs.Select(x => x).Where(x => x.NUM_ITEM == piDialog.INFORMACAO_INT).FirstOrDefault();
                if (itemARemover is null)
                {
                    MessageBox.Show("Item não encontrado. Ele pode já ter sido removido da venda");
                    return;
                }
                else
                {
                    VENDA_EM_CURSO.VENDA_ITEMs.Remove(itemARemover);
                    AdicionaProdutoNaTela(0, "CANCELADO - ", itemARemover.ESTOQUE.DESCRICAO, itemARemover.QTD_ITEM, itemARemover.UNI_MEDIDA.ABREVIATURA, itemARemover.ESTOQUE.PRECO_VENDA);
                }
            }
        }
        internal void CancelaVenda()
        {
            switch (STATUSCAIXA)
            {
                case StatusCaixaEnum.Fechado:
                case StatusCaixaEnum.Livre:
                    CancelaVendaAnterior();
                    break;
                case StatusCaixaEnum.EmVenda:
                    CancelaVendaAtual();
                    break;
                case StatusCaixaEnum.Totalizacao:
                    break;
                case StatusCaixaEnum.EmDevolucao:
                    CancelaVendaAtual();
                    break;
                default:
                    break;
            }
        }
        private void CancelaVendaAnterior()
        {
            
        }
        private void CancelaVendaAtual()
        {
            PERGUNTASENHAView PSG = new PERGUNTASENHAView() { DataContext = new PERGUNTASENHAViewModel() { TITULO = "CANCELAR VENDA ATUAL", SUBTITULO = "Digite uma senha de gerente", CARGOS = new Cargo[] { Cargo.Gerente } } };
            if (PSG.ShowDialog() == true)
            {
                VENDA_EM_CURSO = null;
                STATUSCAIXA = StatusCaixaEnum.Livre;
                //MARQUEE = CORTESIA;
                SUBTOTAL = 0;
                CUPOMGRIDEntryList.Clear();
                VLR_UNIT = 0;
                QUANTIDADE = 1;
                CORTESIA = string.Empty;
                OnPropertyChanged(null);
            }
        }
        internal void TotalizaVenda(bool vendaFiscal)
        {
            if (STATUSCAIXA == StatusCaixaEnum.EmVenda)
            {
                STATUSCAIXA = StatusCaixaEnum.Totalizacao;
                FECHAMENTOCUPOMView fechamento = new FECHAMENTOCUPOMView()
                { DataContext = new FECHAMENTOCUPOMViewModel(VENDA_EM_CURSO, vendaFiscal) };
                switch (fechamento.ShowDialog())
                {
                    case true:

                        return;
                    case false:

                        STATUSCAIXA = StatusCaixaEnum.EmVenda;
                        break;
                }
                
            }

        }
        internal void ProcessaPesquisa(string text)
        {
            ESTOQUE estoqueTentativo;

            if (int.TryParse(text, out int idTentativo))
            {
                estoqueTentativo = Estoque_List_Full.Select(x => x).Where(x => x.ID == idTentativo).FirstOrDefault();
            }
            else estoqueTentativo = Estoque_List_Full.Select(x => x).Where(x => x.PRODUTO_ESTOQUE.COD_BARRA == text).FirstOrDefault();
            if (!(estoqueTentativo is null))
            {
                ESTOQUE_Selecionado = estoqueTentativo;
                ProcessaProduto();
                return;
            }
            if (!(ESTOQUE_Selecionado is null))
            {
                ProcessaProduto();
                return;
            }
            else
            {
                AtualizaDropDownEstoque(text);
                return;
            }
        }
        private void ProcessaProduto()
        {
            if (MODO_CONSULTA)
            {
                CORTESIA = ESTOQUE_Selecionado.DESCRICAO;
                VLR_UNIT = ESTOQUE_Selecionado.PRECO_VENDA;
                OnPropertyChanged("CORTESIA");
                OnPropertyChanged("VLR_UNIT");
                OnPropertyChanged("VLR_TOTAL");
            }
            else
            {
                switch (STATUSCAIXA)
                {
                    case StatusCaixaEnum.Fechado:
                        MessageBox.Show("O turno não foi aberto.\nAbra um novo turno com F12");
                        return;
                    case StatusCaixaEnum.Livre:
                        AbreNovoCupom();
                        AdicionaProdutoNaVenda();
                        break;
                    case StatusCaixaEnum.EmVenda:
                        AdicionaProdutoNaVenda();
                        break;
                    case StatusCaixaEnum.Totalizacao:
                        break;
                    case StatusCaixaEnum.EmDevolucao:
                        AdicionaProdutoNaDevolução();
                        break;
                    default:
                        throw new FlowException("O estado do caixa está inválido. Impossível continuar.");
                }
                ESTOQUE_Selecionado = null;
            }
        }
        internal void AlternaModoConsulta()
        {
            MODO_CONSULTA = !MODO_CONSULTA;
            CORTESIA = "";
            VLR_UNIT = 0M;
            QUANTIDADE = 1M;
            OnPropertyChanged("QUANTIDADE");
            OnPropertyChanged("CORTESIA");
            OnPropertyChanged("VLR_UNIT");
            OnPropertyChanged("VLR_TOTAL");
            switch (MODO_CONSULTA)
            {
                case true:
                    AVISOS = "MODO DE CONSULTA";
                    break;
                case false:
                    switch (STATUSCAIXA)
                    {
                        case StatusCaixaEnum.Fechado:
                            AVISOS = "CAIXA FECHADO";
                            break;
                        case StatusCaixaEnum.Livre:
                            AVISOS = "CAIXA LIVRE";
                            break;
                        case StatusCaixaEnum.EmVenda:
                        case StatusCaixaEnum.Totalizacao:
                            AVISOS = String.Empty;
                            break;
                        case StatusCaixaEnum.EmDevolucao:
                            AVISOS = "MODO DE DEVOLUÇÃO";
                            break;
                        default:
                            AVISOS = "¿";
                            break;
                    }
                    break;
            }
            OnPropertyChanged("AVISOS");
            OnPropertyChanged("MODO_CONSULTA");

        }

        #endregion Methods
    }
}
