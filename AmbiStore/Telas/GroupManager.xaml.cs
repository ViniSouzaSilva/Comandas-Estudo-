using AmbiStore.Objetos;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
using static AmbiStore.Funcoes.Static;
namespace AmbiStore.Telas
{
    /// <summary>
    /// Lógica interna para HTTPTESTE.xaml
    /// </summary>
    public partial class GroupManager : Window
    {
        
        
        public GroupManager()
        {
            InitializeComponent();
            
           var tkreturn =  RetornaToken(1);
            
            //CadastroGrupo(tkreturn,"Grupo-teste","teste","1234","1234");
           object result = ConsultaGrupoCadastrado(tkreturn,"x");
            
            foreach (var lista in lista)
            {
                ConsultaEmpresasCadastrado(tkreturn, lista);
            }
            // 148536685116
            //DescartaNota(result.ToString(),cnpj,"41201010708829000105550830001000421000000109","0");
            //ConsultarCadastro(result.ToString(),cnpj,cnpj,"","","SP");
            ObterXml(result.ToString(),cnpj, "35201010708829000105550830001000431000000109", "");
            Inutilizar(result.ToString(),cnpj,"20","83","01","10000","Teste, muito testado de certa forma o teste é sempre um grande teste, e hoje não é diferente, a vida é um teste, toda hora você precisa passar por teste, e eles pensam que esses testes bobos dizem quem você é, e isso é o pior tipo de teste.");
            ResolveNota(result.ToString(),cnpj,"35201010708829000105550830001000431000000109");
            EnviarCartaCorreção(result.ToString(),cnpj);
            ImprimirNota(result.ToString(),cnpj, "35201010708829000105550830001000431000000109","1","CCe");
            ImportaNotas(result.ToString(), cnpj);
            string XML = "";
            //EnviaNota(result.ToString(),cnpj);
            //string url = VisualizarDanfe(result.ToString(), cnpj,"1");
            //DanfeVisualizer danfe = new DanfeVisualizer(url);
            //danfe.ShowDialog();
            // CancelaNota(result.ToString(),cnpj,"35201010708829000105550830001000421000000101","cancelamento teste");
            //EmailNota(result.ToString(),cnpj,"artur.s@trilhatecnologia.com", "35201010708829000105550830001000421000000101");
           
            ExportaNotas(result.ToString(), cnpj, DateTime.Today.AddDays(-30), DateTime.Now, "DENEGADA", "0", "0", "100", "1", "100040", "100045", "0", "1");
            ConsultaNota(result.ToString(),cnpj, "situacao = AUTORIZADA and dtautorizacao >= '02/08/2017 10:59:41'", "chave","100","10","4","","");
            string retur = ValidaNota(result.ToString(),cnpj);
            try
            {

                   EnvioNFE envio = new EnvioNFE(retur);
                var a = envio.CStat;
            }
            catch (Exception ex) 
            {
               
                    
                ErroNFE envio = new ErroNFE(retur);
                var a = envio.MSGException;
                    
            }
            //Requisicao();


        }
     


        


        

        
        public class Post 
        {
            public int Id { get; set; }
            public int userId { get; set; }
            public string title { get; set; }
            public string body { get; set; }


        }

        
        


        private void Certificado_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grupos_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Icone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            //file.OpenFile();
            AddCertificado_btn.Visibility = Visibility.Visible;
            Icone_ok.Visibility = Visibility.Visible;
            Icone.Visibility = Visibility.Collapsed;

        }
    }
}
