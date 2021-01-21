using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Extension;
using AmbiStore.Shared.Libraries;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Shared.SEFAZ.NF;
using AmbiStore.Views;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Messages.Transport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AmbiStore.ViewModels
{
    public class ImportaNotaViewModel : ViewModelBase
    {
        public AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        NFeEntradaFunctions funcoes = new NFeEntradaFunctions();
        public COMPRA COMPRA_IMPORTADA { get; set; }
        public bool PodeImportar
        {
            get
            {
                return !(ITEMS.Select(x => x).Where(x => x.CodEstoque is null).Any());
            }
        }
        public ItemImportado SelectedItemI { get; set; }
        public TNFe TNFE { get; private set; }
        public string TRANSPORTADORA { get; private set; }
        public string CPFCNPJTRANSPORTADORA { get; private set; }
        public decimal BASEICMS { get; private set; }
        public decimal VLRICMS { get; private set; }
        public decimal IPI { get; private set; }
        public decimal DESCASC { get; private set; }
        public decimal SEGURO { get; private set; }
        public decimal DESPESAS { get; private set; }
        public decimal FCP_ST { get; private set; }
        public decimal FRETE { get; private set; }
        public decimal TOTPROD { get; private set; }
        public decimal TOTSERV { get; private set; }
        public decimal TOTNOTA { get { return TOTPROD + TOTSERV; } }
        public string INFOCMPL { get; private set; }
        public List<ItemImportado> ITEMS { get; set; }
        public async Task<bool> ImportaNFE(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            else
            {
                string xmlLido = await File.ReadAllTextAsync(path);
                TNFE = funcoes.Deserializa(xmlLido);
                COMPRA_IMPORTADA = new COMPRA();
                COMPRA_IMPORTADA.NUMERO_NF = TNFE.infNFe.ide.nNF.Safeint();
                COMPRA_IMPORTADA.NUMERO_SERIE = TNFE.infNFe.ide.serie;
                COMPRA_IMPORTADA.NF_MODELO = TNFE.infNFe.ide.mod switch
                {
                    TMod.Item65 => "65",
                    _ => "55"
                };
                COMPRA_IMPORTADA.CHAVE = TNFE.infNFe.Id.Substring(3);
                COMPRA_IMPORTADA.DATA_EMISSAO = DateTime.Parse(TNFE.infNFe.ide.dhEmi);
                COMPRA_IMPORTADA.COMPRA_ITEMs = new List<COMPRA_ITEM>();
                TRANSPORTADORA = TNFE.infNFe.transp?.transporta?.xNome;
                CPFCNPJTRANSPORTADORA = TNFE.infNFe.transp?.transporta?.Item;
                //Abaixo daqui é remendo
                BASEICMS = decimal.Parse(TNFE.infNFe.total.ICMSTot.vBC, CultureInfo.InvariantCulture);
                VLRICMS = decimal.Parse(TNFE.infNFe.total.ICMSTot.vICMS, CultureInfo.InvariantCulture);
                IPI = decimal.Parse(TNFE.infNFe.total.ICMSTot.vIPI, CultureInfo.InvariantCulture);
                DESCASC = decimal.Parse(TNFE.infNFe.total.ICMSTot.vDesc, CultureInfo.InvariantCulture);
                SEGURO = decimal.Parse(TNFE.infNFe.total.ICMSTot.vSeg, CultureInfo.InvariantCulture);
                DESPESAS = decimal.Parse(TNFE.infNFe.total.ICMSTot.vOutro, CultureInfo.InvariantCulture);
                FCP_ST = decimal.Parse(TNFE.infNFe.total.ICMSTot.vFCPST, CultureInfo.InvariantCulture);
                FRETE = decimal.Parse(TNFE.infNFe.total.ICMSTot.vFrete, CultureInfo.InvariantCulture);
                TOTPROD = decimal.Parse(TNFE.infNFe.total.ICMSTot.vProd, CultureInfo.InvariantCulture);
                if (!(TNFE.infNFe.total.ISSQNtot is null))
                {
                    TOTSERV = decimal.Parse(TNFE.infNFe.total.ISSQNtot.vServ, CultureInfo.InvariantCulture);
                }
                INFOCMPL = TNFE.infNFe.infAdic.infCpl;
                //Fim do remendo
                COMPRA_IMPORTADA.FORNECEDOR = await VerificaFornecedor();
                await VerificaProdutos();
                OnPropertyChanged(null);
                return true;
            }
        }

        internal async void ConfirmaItems()
        {
            foreach (ItemImportado item in ITEMS)
            {
                COMPRA_IMPORTADA.COMPRA_ITEMs.Add(new COMPRA_ITEM()
                {
                    ESTOQUE = _context.ESTOQUEs.Select(x => x).Include(x => x.PRODUTO_ESTOQUE).Include(x => x.SERVICO_ESTOQUE).Include(x => x.UNI_MEDIDA).Where(x => x.ID == item.CodEstoque).First(),
                    QUANTIDADE_ITEM = item.Quant,
                    NUMERO_ITEM = item.NItem,
                    UNIDADE_MEDIDA = _context.UNI_MEDIDAs.Select(x => x).Where(x => x.ABREVIATURA == item.UniMedida).FirstOrDefault() ??
                                     _context.UNI_MEDIDAs.Select(x => x).Where(x => x.ABREVIATURA == "UN").First()
                }
                    );
            }
        }

        private async Task<CONTATO> VerificaFornecedor()
        {
            bool jaCadastrado = await _context.CONTATOs.Select(x => x).AsNoTracking()
                                            .Include(x => x.CONTATO_PF)
                                            .Include(x => x.CONTATO_PJ)
                                            .Where(x => x.CONTATO_PF.CPF == TNFE.infNFe.emit.Item || x.CONTATO_PJ.CNPJ == TNFE.infNFe.emit.Item)
                                            .AnyAsync();
            if (jaCadastrado)
            {
                return await _context.CONTATOs.Select(x => x).AsNoTracking()
                                            .Include(x => x.CONTATO_PF)
                                            .Include(x => x.CONTATO_PJ)
                                            .Include(x => x.END_MUNICIPIO)
                                            .Where(x => x.CONTATO_PF.CPF == TNFE.infNFe.emit.Item || x.CONTATO_PJ.CNPJ == TNFE.infNFe.emit.Item)
                                            .FirstAsync();
            }
            else
            {
                if (MessageBox.Show("Fornecedor não cadastrado. Deseja cadastrá-lo agora?", "Novo Fornecedor", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    return await CadastraNovoContato();

                }
                else
                {
                    return await _context.CONTATOs.Select(x => x).Where(x => x.ID == -2).FirstAsync();
                }
            }
        }

        internal async void EditaEstoqueParametrizaco()
        {
            //if (selectedItem.CodEstoque is null)
            //{
            //    MessageBox.Show("Cadstre ou vincule um produto antes de tentar editá-lo.");
            //    return;
            //}
            //ESTOQUE estoqueAEditar = await _context.ESTOQUEs.Select(x => x).Where(x => x.ID == selectedItem.CodEstoque).FirstOrDefaultAsync();
            //if (estoqueAEditar is null) { MessageBox.Show("Erro ao editar o cadastro vinculado.", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            //ESTOQUECadastro cadastroview = new ESTOQUECadastro() { DataContext = new ESTOQUEViewModel() { ESTOQUE = estoqueAEditar } };
            //cadastroview.ShowDialog();
            //OnPropertyChanged("ITEMS");
        }

        internal async Task VerificaProdutos()
        {
            ITEMS = new List<ItemImportado>();
            foreach (TNFeInfNFeDet item in TNFE.infNFe.det)
            {
                {
                    ESTOQUE estoqueTentativo = await _context.ESTOQUEs.Select(x => x).Include(x => x.PARAMETRIZACAO_ESTOQUE).Where(x =>
                                                x.PARAMETRIZACAO_ESTOQUE
                                                        .Select(y => y)
                                                        .Where(y => y.FORNECEDOR == COMPRA_IMPORTADA.FORNECEDOR && y.COD_FORNECEDOR == item.prod.cProd)
                                                        .Any() == true)
                                                .FirstOrDefaultAsync();
                    ItemImportado ii = new ItemImportado()
                    {
                        CodFornec = item.prod.cProd,
                        DescFornec = item.prod.xProd,
                        CodEstoque = estoqueTentativo?.ID,
                        DescEstoque = estoqueTentativo?.DESCRICAO,
                        Quant = decimal.Parse(item.prod.qCom),
                        UniMedida = item.prod.uCom.ToUpper(),
                        NItem = item.nItem.Safeint(),
                        Detalhamento = item
                    };
                    ITEMS.Add(ii);
                }

            }
        }

        public async Task<CONTATO> CadastraNovoContato()
        {
            CONTATO novoContato = new CONTATO();
            novoContato.NOME_JURIDICO = TNFE.infNFe.emit.xNome;
            if (TNFE.infNFe.emit.Item.Length == 11) //CPF
            {
                CONTATO_PF novoContatoPf = new CONTATO_PF()
                {
                    CPF = TNFE.infNFe.emit.Item
                };
                novoContato.CONTATO_PF = novoContatoPf;
            }
            else if (TNFE.infNFe.emit.Item.Length == 14) //CNPJ
            {
                CONTATO_PJ novoContatoPj = new CONTATO_PJ()
                {
                    CNPJ = TNFE.infNFe.emit.Item,
                    IE = TNFE.infNFe.emit.IE
                };
                novoContato.CONTATO_PJ = novoContatoPj;
            }
            novoContato.ENDERECO_LOGRAD = TNFE.infNFe.emit.enderEmit.xLgr;
            novoContato.ENDERECO_NUMERO = TNFE.infNFe.emit.enderEmit.nro;
            novoContato.ENDERECO_BAIRRO = TNFE.infNFe.emit.enderEmit.xBairro;
            novoContato.ENDERECO_CEP = TNFE.infNFe.emit.enderEmit.CEP;
            novoContato.END_MUNICIPIO_ID = int.Parse(TNFE.infNFe.emit.enderEmit.cMun);
            TipoLograd parsedLograd;
            if (Enum.TryParse(TNFE.infNFe.emit.enderEmit.xLgr.Split(' ')[0], true, out parsedLograd))
                if (Enum.IsDefined(typeof(TipoLograd), parsedLograd))
                    novoContato.ENDERECO_TIPO = parsedLograd;
            novoContato.DDD_CELULAR1 = TNFE.infNFe.emit.enderEmit.fone?.Substring(0, 2);
            novoContato.TEL_CELULAR1 = TNFE.infNFe.emit.enderEmit.fone?.Substring(2);
            novoContato.STATUS = Status.Ativo;
            _context.Update(novoContato);
            await _context.SaveChangesAsync();
            return novoContato;
        }
        public async void CadastraItemNovo()
        {
            if (!(SelectedItemI.CodEstoque is null)) { MessageBox.Show("Item já vinculado. Edite o item, caso necessário"); return; }
            ESTOQUE novoEstoque = new ESTOQUE();
            PRODUTO novoProduto = new PRODUTO();
            novoEstoque.DESCRICAO = SelectedItemI.DescFornec;
            novoEstoque.STATUS = Status.Ativo;
            novoEstoque.TIPO_ITEM = TipoItem.MercadoriaParaRevenda;
            novoEstoque.UNI_MEDIDA_ID = "UN";
            novoEstoque.TIPOITEMCADASTRO = TipoItemCadastro.Nenhum;
            novoEstoque.PARAMETRIZACAO_ESTOQUE = new List<PARAMETRIZACAO>();
            novoEstoque.PARAMETRIZACAO_ESTOQUE.Add(new PARAMETRIZACAO() { COD_FORNECEDOR = SelectedItemI.CodFornec, FORNECEDOR = COMPRA_IMPORTADA.FORNECEDOR });
            novoProduto.COD_BARRA = SelectedItemI.Detalhamento.prod.cEAN;
            novoEstoque.CFOP_NF_ID = novoEstoque.CFOP_CFE_ID = int.Parse($"5{SelectedItemI.Detalhamento.prod.CFOP.Substring(1)}");
            novoEstoque.PRODUTO_ESTOQUE = novoProduto;
            _context.Update(novoEstoque);
            await _context.SaveChangesAsync();
            SelectedItemI.CodEstoque = novoEstoque.ID;
            SelectedItemI.DescEstoque = novoEstoque.DESCRICAO;
            OnPropertyChanged("PodeImportar");
            OnPropertyChanged("ITEMS");
        }
    }
    public class ItemImportado : INotifyPropertyChanged
    {
        public int NItem { get; set; }
        public string CodFornec { get; set; }
        private int? codEstoque;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int? CodEstoque
        {
            get { return codEstoque; }
            set { codEstoque = value; OnPropertyChanged("Param"); }
        }

        public string DescFornec { get; set; }
        private string descEstoque;

        public string DescEstoque
        {
            get { return descEstoque; }
            set { descEstoque = value; OnPropertyChanged(null); }
        }

        public decimal Quant { get; set; }
        public string UniMedida { get; set; }
        public decimal VlrUnit { get; set; }
        public TNFeInfNFeDet Detalhamento { get; set; }
        public EstoqueParam Param
        {
            get
            {
                if (codEstoque is null) return EstoqueParam.Inexistente;
                else if (DescFornec != DescEstoque) return EstoqueParam.Achado;
                else return EstoqueParam.Conferido;
            }
        }

    }

}
