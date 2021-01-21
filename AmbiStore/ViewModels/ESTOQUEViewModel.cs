using AmbiStore.Controls;
using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.EFCore.Services;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AmbiStore.ViewModels
{
    public class ESTOQUEViewModel : ViewModelBase
    {
        AmbiStoreDbContext _contexto = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<GRUPO> GRUPOs { get { return _contexto.GRUPOs.Select(x => x).ToList(); } }
        public List<UNI_MEDIDA> UNI_MEDIDAs { get { return _contexto.UNI_MEDIDAs.Select(x => x).ToList(); } }
        public ObservableCollection<CONTATO> FORNECEDORs { get; set; }
        public ObservableCollection<TAXA_UF> TAXA_UFs { get; set; }
        public ObservableCollection<CST> CSTs { get; set; }
        public ObservableCollection<CSOSN> CSOSNs { get; set; }
        public ObservableCollection<CFOP_SIS> CFOPs { get; set; }

        public ESTOQUEViewModel()
        {
            FORNECEDORs = new ObservableCollection<CONTATO>(_contexto.CONTATOs.Select(x => x));
            TAXA_UFs = new ObservableCollection<TAXA_UF>(_contexto.TAXA_UFs.Select(x => x));
            CSTs = new ObservableCollection<CST>(_contexto.CSTs.Select(x => x));
            CSOSNs = new ObservableCollection<CSOSN>(_contexto.CSOSNs.Select(x => x));
            CFOPs = new ObservableCollection<CFOP_SIS>(_contexto.CFOP_SISs.Select(x => x).Where(x=>x.CFOP>=5000 && x.CFOP<=5999));
            if (eSTOQUE is null) eSTOQUE = new ESTOQUE() { PRODUTO_ESTOQUE = new PRODUTO(), SERVICO_ESTOQUE = new SERVICO() };
        }

        public Visibility IsServico {
            get
            {
                if (eSTOQUE.TIPO_ITEM == TipoItem.Servico)
                {
                    return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            } 
        }

        internal void GeraCodigoBalanca()
        {
            //Para o produto 53, o código gerado será 2000530
            StringBuilder sb = new StringBuilder();
            sb.Append("2");
            sb.Append(ESTOQUE.ID.ToString().PadLeft(5, '0'));
            sb.Append("0");
            eSTOQUE.PRODUTO_ESTOQUE.COD_BARRA = sb.ToString();
        }

        public Visibility IsProduto { 
            get 
            {
                //return Visibility.Visible;
                if (eSTOQUE.TIPO_ITEM != TipoItem.Servico)
                {
                    if (eSTOQUE.TIPO_ITEM == 0) return Visibility.Collapsed;
                    else return Visibility.Visible;
                }
                else return Visibility.Collapsed;
            } 
        }

        internal void CadastraUniMedida()
        {
            (new UNIDADEMEDIDACadastroEListView() { DataContext = new UNIMEDIDAViewModel() }).ShowDialog();
            OnPropertyChanged("UNI_MEDIDAs");
        }

        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public async Task<bool> SaveChanges()
        {
            _context.Update(ESTOQUE);
            await _context.SaveChangesAsync();
            return true;
        }

        public decimal PrecoVenda
        {
            get 
            { 
                return eSTOQUE.PRECO_VENDA; 
            }
            set 
            { 
                eSTOQUE.PRECO_VENDA = value;
                OnPropertyChanged("LucroBruto");
                OnPropertyChanged("eSTOQUE");
            }
        }

        public DateTime? ULTIMA_COMPRA {
            get
            {
                var ultCompra =
                _context.VENDAs.
                    Select(x => x).
                    Where(x => x.VENDA_ITEMs.
                        Select(x => x).
                        Where(x => x.ESTOQUE.ID == eSTOQUE.ID)
                        .Any())
                    .FirstOrDefault();
                if (ultCompra is null) return null;
                else return ultCompra.DT_EMISSAO;
            }
        }

        public TipoItem tipoItem
        {
            get
            {
                return eSTOQUE.TIPO_ITEM;
            }
            set
            {
                eSTOQUE.TIPO_ITEM = value;
                OnPropertyChanged("IsProduto");
                OnPropertyChanged("IsServico");
            }
        }

        private ESTOQUE eSTOQUE;

        public ESTOQUE ESTOQUE
        {
            get { return eSTOQUE ?? new ESTOQUE(); }
            set { eSTOQUE = value; }
        }

        private decimal lucroBruto;
        public decimal LucroBruto
        {
            get
            {
                if (eSTOQUE.PRECO_VENDA == 0 || eSTOQUE.PRECO_CUSTO == 0) return 0;
                return ((eSTOQUE.PRECO_VENDA / eSTOQUE.PRECO_CUSTO) - 1.0000M);
            }
            set
            {
                lucroBruto = value;
            }
        }

        public void CalculaPrecoPeloLucro()
        {
            PrecoVenda = eSTOQUE.PRECO_CUSTO * (lucroBruto+1);
        }

        public decimal MediaCusto
        {
            get
            {
                return _contexto.COMPRA_ITEMs.Select(x => x.VALOR_ITEM).Average();
            }
        }

        public void PrecoEmDolar()
        {
            var CambioFunction = new Shared.Libraries.Functions();
            decimal valor = CambioFunction.ConverterDeReais(eSTOQUE.PRECO_VENDA, Moeda.USD);
            if (valor == -999M)
            {
                MessageBox.Show("Time out ao consultar câmbio. Tente novamente mais tarde");
                return;
            }
            else
            {
                eSTOQUE.PRECO_DOLAR = valor;
                OnPropertyChanged("eSTOQUE");
            }
        }

        public void CadastraGrupos()
        {
            (new GRUPOCadastroEListView() { DataContext = new GRUPOSViewModel() }).ShowDialog();
            OnPropertyChanged("GRUPOs");
        }
    }
}
