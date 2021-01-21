using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using AmbiStore.Shared.EFCore.Services;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using AmbiStore.Views;

namespace AmbiStore.Telas
{
    /// <summary>
    /// Interaction logic for TesteLanding.xaml
    /// </summary>
    public partial class TesteLanding : Window
    {
        public TesteLanding()
        {
            InitializeComponent();
        }

        CONTATO cONTATO = new CONTATO()
        {
            NOME_FANTASIA = "Nome Fantasia",
            NOME_JURIDICO = "Nome Jurídico",
            CONTATO_PJ = new CONTATO_PJ()
            {
                CNPJ = "11.023.174/0055-89",
                IE = "123.456.789.012"
            },
            CONTATO_ESP = "CAIQUE VINICIUS JUAN",
            ENDERECO_CEP = "04001-000",
            ENDERECO_TIPO = Shared.Libraries.Enums.TipoLograd.Avenida,
            ENDERECO_LOGRAD = "PRESIDENTE ITAMAR FRANCO",
            DDD_COMERCIAL = "11",
            TEL_COMERCIAL = "32144796",
            ENDERECO_BAIRRO = "PARAÍSO",
            ENDERECO_NUMERO = "3600"
        };
        EMITENTE eMITENTE = new EMITENTE()
        {
            NOME_EMITENTE = "Nome Emitente",
            NOME_FANTASIA = "Nome Fantasia",
            CONTATO_RESPONSAVEL = "Contato Responsável",
            RAMO_ATIVIDADE = "Discos de Vinil",
            SIMPLES_NACIONAL = true,
            RT_EXCEDE_SUBLIMITE = false
        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AmbiStoreDbContext context = new AmbiStoreDbContextFactory().CreateDbContext();
            var saopaulo = context.MUNICIPIOs.Select(mun => mun).Where(mun => mun.MUN_DESC == "São Paulo").First();
            cONTATO.END_MUNICIPIO = saopaulo;
            CONTATOViewModel cvm = new CONTATOViewModel() { CONTATO = cONTATO };
            eMITENTE.END_MUNICIPIO = saopaulo;
            EMITENTEViewModel evm = new EMITENTEViewModel() { EMITENTE = eMITENTE };

            var janela = new EMITENTECadastro() { DataContext = evm };

            //var dataService = new GenericDataService<CONTATO>(new AmbiStoreDbContextFactory());
            //var janela2 = new CadastroEstoque(new ESTOQUE() { PRECO_VENDA = 499.90m, DESCRICAO = "Pulse Gel 3", PRECO_CUSTO = 175.58m, TIPO_ITEM = TipoItem.MercadoriaParaRevenda, PRODUTO_ESTOQUE = new PRODUTO() });
            //var janela3 = new CadastroEmitente(null);
            janela.Show();

            //NATUREZA_OPERACAO natoper = new NATUREZA_OPERACAO()
            //{
            //    CFOP_ID = 5102,
            //    DESCRICAO = "Minha Natoper",
            //    TAXA_UF = new TAXA_UF(),
            //    STATUS = Status.Ativo

            //};
            //context.Update(natoper);
            //context.SaveChanges();

            //VENDA venda = new VENDA();
            //venda.VALORVENDA = 19.90M;
            //venda.CLIENTE = context.CONTATOs.Select(x => x).Where(x => x.ID == 1).First();
            ////venda.NATUREZA_OPERACAO = context.NATUREZA_OPERACAOs.Select(x => x).Where(x => x.CFOP == 
            ////context.CFOP_SISs.Select(x=>x).Where(x=>x.CFOP == 5102).FirstOrDefault()
            ////).First();
            //venda.NATUREZA_OPERACAO = context.NATUREZA_OPERACAOs.Select(x => x).Where(x => x.CFOP.CFOP == 5102).First();
            //venda.VENDEDOR = context.FUNCIONARIOs.Select(x=>x).Where(x=>x.ID == -1).FirstOrDefault();
            //venda.PLANO_CONTA_ID = 1;
            //NFE nfe = new NFE();
            //nfe.CHAVE = "batata10";
            //venda.NFE = nfe;
            //context.Update(venda);
            //context.SaveChanges();

        }
    }
}
