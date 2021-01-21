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
using System.Xml;
using System.Xml.Serialization;
using static AmbiStore.Telas.GroupManager;
using AmbiStore.Shared.SEFAZ.NF;
using AmbiStore.Shared.Serializador;
using AmbiStore.Shared.Serializador.NFe;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace AmbiStore.Funcoes
{
    public static class Static
    {
      public static List<int> lista = new List<int>();
       public static string cnpj;
        /// <summary>
        /// Gera ou retorna o ultimo token gerado 0 para devolver o antigo token e 1 para gerar um novo
        /// </summary>
        /// <param name="tipoToken"></param>
        /// <returns></returns>
        public static string RetornaToken(int tipoToken, string email = "artur.s@trilhatecnologia.com", string senha = "PFES2011")
        {

            try
            {
                var client = new RestClient("https://managersaas.tecnospeed.com.br:1337/api/v1/software-house/token");
                var request = new RestRequest(Method.POST);
                
                request.AddParameter("email", email, ParameterType.GetOrPost);
                request.AddParameter("senha", senha, ParameterType.GetOrPost);
                request.AddParameter("gerar", tipoToken, ParameterType.GetOrPost);


                object result = JObject.Parse(client.Execute(request).Content);
                Token post = JsonConvert.DeserializeObject<Token>(result.ToString());

                #region Código desativado
                /*
                string dadosPost = "email=artur.s@trilhatecnologia.com";
                dadosPost = dadosPost + "&senha=PFES2011";
                dadosPost = dadosPost + "&gerar=" + tipoToken.ToString();
                var dados = Encoding.UTF8.GetBytes(dadosPost);

                var requisicaoWeb = WebRequest.CreateHttp("https://managersaas.tecnospeed.com.br:1337/api/v1/software-house/token");
                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;

                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();

                }
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    string objResponse = reader.ReadToEnd();

                    Token post = JsonConvert.DeserializeObject<Token>(objResponse.ToString());
                    tokenRetornado = post.dados.token;
                    int a;
                    streamDados.Close();
                    resposta.Close();


                }

                return tokenRetornado;
             */
                #endregion
                return post.dados.token;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }
        public static string CadastroGrupo(string Token, string identificacao, string descricao, string senha, string confirmacaoSenha, string user = "artur.s@trilhatecnologia.com")
        {
            if (senha.Equals(confirmacaoSenha)) 
            {
                try
                {
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:1337/api/v1/grupo?token=" + Token);
                    var request = new RestRequest(Method.POST);

                    request.AddParameter("identificacao", identificacao, ParameterType.GetOrPost);
                    request.AddParameter("descricao", descricao, ParameterType.GetOrPost);
                    request.AddParameter("senha", senha, ParameterType.GetOrPost);
                    request.AddParameter("usuario", user, ParameterType.GetOrPost);


                    object result = JObject.Parse(client.Execute(request).Content);
                    GruposCadastrados.Grupos post = JsonConvert.DeserializeObject<GruposCadastrados.Grupos>(result.ToString());

                    #region Código desativado
                    /*
                    string dadosPost = identificacao+"&"+ descricao + "&" + senha + "&" + user;
                    //dadosPost = dadosPost + "&senha=PFES2011";
                    //dadosPost = dadosPost + "&gerar=" + tipoToken.ToString();
                    var dados = Encoding.UTF8.GetBytes(dadosPost);

                    var requisicaoWeb = WebRequest.CreateHttp("https://managersaas.tecnospeed.com.br:1337/api/v1/grupo?token="+Token+"/token");
                    requisicaoWeb.Method = "POST";
                    requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                    requisicaoWeb.ContentLength = dados.Length;

                    using (var stream = requisicaoWeb.GetRequestStream())
                    {
                        stream.Write(dados, 0, dados.Length);
                        stream.Close();

                    }
                    using (var resposta = requisicaoWeb.GetResponse())
                    {
                        var streamDados = resposta.GetResponseStream();
                        StreamReader reader = new StreamReader(streamDados);
                        string objResponse = reader.ReadToEnd();

                        GruposCadastrados.Grupos post = JsonConvert.DeserializeObject<GruposCadastrados.Grupos>(objResponse.ToString());

                        streamDados.Close();
                        resposta.Close();

                        */

                    #endregion
                    return post.mensagem;
                }
                catch (Exception ex)
                {

                    return ex.ToString();
                }
            }
            else 
            {
                return "As senhas não são iguais";
            }
        }

        /// <summary>
        /// Possibilita o retorno do Handler do grupo 
        /// </summary>
        /// <param name="tokenRetornado"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static string ConsultaGrupoCadastrado(string tokenRetornado, string sort)
        {
            try
            {
                var client = new RestClient("https://managersaas.tecnospeed.com.br:1337/api/v1/grupo?token=" + tokenRetornado);
                var request = new RestRequest(Method.GET);
                

                //request.AddParameter("token", tokenRetornado, ParameterType.GetOrPost);
                //request.AddParameter("limit", "10", ParameterType.GetOrPost);
                //request.AddParameter("sort", sort, ParameterType.GetOrPost);



                object result = JObject.Parse(client.Execute(request).Content);
                GruposCadastrados.Grupos post = JsonConvert.DeserializeObject<GruposCadastrados.Grupos>(result.ToString());
                int a = 0;
                foreach (var b in post.dados)
                {

                    lista.Add(post.dados[a].handle);
                    a++;
                }
                //var listaDeHandles = from dado in post.dados select dado.handle;
                //lista.Add(listaDeHandles);
                return post.dados[0].identificacao;
                //return post.dados[0].handle;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            #region Código desativado
            /*
            string dadosPost = "token=" + tokenRetornado;

       

            dadosPost = dadosPost + "10";
            dadosPost = dadosPost + "identificacao";
            var dados = Encoding.UTF8.GetBytes(dadosPost);

            

            var requisicaoWeb = WebRequest.CreateHttp("https://managersaas.tecnospeed.com.br:1337/api/v1/grupo"+ dados);

             requisicaoWeb.Method = "GET";            
             requisicaoWeb.UserAgent = "RequisicaoWebDemo";
             requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
             requisicaoWeb.ContentLength = dados.Length;

            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();

            }
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();

                GruposCadastrados.Grupos post = JsonConvert.DeserializeObject<GruposCadastrados.Grupos>(objResponse.ToString());
                
                

                int a;
                streamDados.Close();
                resposta.Close();

                return post;

            }*/

            #endregion

        }
        public static object ConsultaEmpresasCadastrado(string tokenRetornado, int Id_grupo)
        {
            try
            {

                var client = new RestClient("https://managersaas.tecnospeed.com.br:1337/api/v1/empresa?token=" + tokenRetornado + "&idgrupo=" + Id_grupo.ToString() + "&skip=0" + "&limit=1");
                var request = new RestRequest(Method.GET);

                object result = JObject.Parse(client.Execute(request).Content);
                EmpresasCadastradas.ConsultarEmpresasCadastradas post = JsonConvert.DeserializeObject<EmpresasCadastradas.ConsultarEmpresasCadastradas>(result.ToString());
                cnpj = post.dados[0].cnpj;
                return post;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
            #region Cód desativado
            // return null;
            /*
            string dadosPost = "?token=" + tokenRetornado;
            dadosPost = dadosPost + ResultadoConsultaGrupoCadastrado;
            dadosPost = dadosPost + "&10";
            dadosPost = dadosPost + "&identificacao";
            var dados = Encoding.UTF8.GetBytes(dadosPost);

            var requisicaoWeb = WebRequest.CreateHttp("https://managersaas.tecnospeed.com.br:1337/api/v1/grupo" + dados);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            requisicaoWeb.ContentLength = dados.Length;

            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();

            }
            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();

                EmpresasCadastradas.ConsultarEmpresasCadastradas post = JsonConvert.DeserializeObject<EmpresasCadastradas.ConsultarEmpresasCadastradas>(objResponse.ToString());

          



                int a;
                streamDados.Close();
                resposta.Close();

                return post;

            }

             */
            #endregion
        }

        public static void CancelaNota(string Grupo, string CNPJ, string ChaveNota, string Justificativa)
        {
            string result = "";
            string userSenha = "admin:1234";
            try
            {
                 
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/cancela");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                   
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
                    request.AddParameter("Justificativa", Justificativa, ParameterType.GetOrPost);

                     result = client.Execute(request).Content;
                    //string post = JsonConvert.DeserializeObject<string>(result.ToString());
                
            }
            catch (Exception ex)
            {

            }
        }
        public static string EnviaNota(string Grupo, string CNPJ)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/envia");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Arquivo", Leitura, ParameterType.GetOrPost);


                    result = client.Execute(request).Content;




                }
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string ValidaNota(string Grupo, string CNPJ)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/valida");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Arquivo", Leitura, ParameterType.GetOrPost);


                    result = client.Execute(request).Content;




                }
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }

        }

        public static string ConsultaNota(string Grupo, string CNPJ, string Filtro, string Campos, string Limite, string Inicio, string Ordem, string Origem, string Visao)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {
                
                    //TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/consulta");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    //string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Filtro", Filtro, ParameterType.GetOrPost);
                    request.AddParameter("Campos", Campos, ParameterType.GetOrPost);
                    request.AddParameter("Limite", Limite,ParameterType.GetOrPost);
                    request.AddParameter("Inicio", Inicio,ParameterType.GetOrPost);
                    request.AddParameter("Ordem", Ordem,ParameterType.GetOrPost);
                    request.AddParameter("Origem", Origem, ParameterType.GetOrPost);
                    request.AddParameter("Visao", Visao,ParameterType.GetOrPost);



                    result = client.Execute(request).Content;




                
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }
        }
        public static void AlteraEmpresasCadastrada(string tokenRetornado, string ResultadoConsultaGrupoCadastrado)
            {

                byte[] a;
                string dadosPost = ResultadoConsultaGrupoCadastrado;
                dadosPost = "?token=" + tokenRetornado;
                string url = "https://managersaas.tecnospeed.com.br:1337/api/v1/empresa/" + dadosPost;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    using (WebClient cliente = new WebClient())
                    {
                        cliente.BaseAddress = url;
                        a = cliente.UploadFile(openFileDialog.FileName, openFileDialog.Title);


                    }

                }

                var dados = Encoding.UTF8.GetBytes(dadosPost);

                var requisicaoWeb = WebRequest.CreateHttp("https://managersaas.tecnospeed.com.br:1337/api/v1/empresa/" + dadosPost);


                requisicaoWeb.Method = "POST";
                requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
                requisicaoWeb.ContentLength = dados.Length;

                using (var stream = requisicaoWeb.GetRequestStream())
                {
                    stream.Write(dados, 0, dados.Length);
                    stream.Close();

                }
                using (var resposta = requisicaoWeb.GetResponse())
                {
                    var streamDados = resposta.GetResponseStream();
                    StreamReader reader = new StreamReader(streamDados);
                    string objResponse = reader.ReadToEnd();

                    Token post = JsonConvert.DeserializeObject<Token>(objResponse.ToString());
                    tokenRetornado = post.dados.token;

                    streamDados.Close();
                    resposta.Close();

                    //return (object)a[0].ToString();
                }
            }

        public static string EmailNota( string Grupo, string CNPJ, string Email, string ChaveNota) 
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {

               
                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/email");
                var request = new RestRequest(Method.POST);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                //string Leitura = Leitor.ReadToEnd();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + resultado);
                request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                request.AddParameter("EmailDestinatario", Email, ParameterType.GetOrPost);
                request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
               



                result = client.Execute(request).Content;





                return result;
            }
            catch (Exception ex)
            {
                return "";

            }


        }

        public static string ExportaNotas(string Grupo, string CNPJ,DateTime Dt_inicial, DateTime Dt_final, string Tipo, string Origem, string NumeroInicial, string NumeroFinal, string URL, string SerieInicial,string SerieFinal,string Eventos, string GerarDanfe) 
        {
            IRestResponse result;
            string userSenha = "admin:1234";
           string a =  Dt_inicial.ToString("dd/MM/yyyy");
            string b = Dt_final.ToString("dd/MM/yyyy");
            try
            {

                //TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/exportaxml");
                var request = new RestRequest(Method.POST);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                //string Leitura = Leitor.ReadToEnd();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + resultado);
                request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                request.AddParameter("dtInicial",a , ParameterType.GetOrPost);
                request.AddParameter("dtFinal", b, ParameterType.GetOrPost);
                request.AddParameter("Tipo", Tipo, ParameterType.GetOrPost);
                request.AddParameter("Origem", Origem, ParameterType.GetOrPost);
                request.AddParameter("nInicial", NumeroInicial, ParameterType.GetOrPost);
                request.AddParameter("nFinal", NumeroFinal, ParameterType.GetOrPost);        
                request.AddParameter("Url", URL, ParameterType.GetOrPost);
                request.AddParameter("SerieInicial", SerieInicial, ParameterType.GetOrPost);
                request.AddParameter("SerieFinal", SerieFinal, ParameterType.GetOrPost);
                request.AddParameter("Eventos", Eventos, ParameterType.GetOrPost);
                request.AddParameter("GerarDanfe", GerarDanfe, ParameterType.GetOrPost);



                result = client.Execute(request);





                return result.Content;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string ImportaNotas(string Grupo, string CNPJ)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/importa");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Arquivo", Leitura, ParameterType.GetOrPost);


                    result = client.Execute(request).Content;




                }
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string VisualizarDanfe(string Grupo, string CNPJ,string URL)
        {
            string result = "";
            string userSenha = "admin:1234";
            
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/prever");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    string Leitura = Leitor.ReadToEnd();

                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Arquivo", Leitura, ParameterType.GetOrPost);

                    request.AddParameter("Url", URL, ParameterType.GetOrPost);




                    result = client.Execute(request).Content;

                }



                return result;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string ObterXml(string Grupo, string CNPJ, string ChaveNota,string Documento)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {


                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/xml?Grupo="+Grupo+"&CNPJ="+CNPJ+ "&ChaveNota="+ChaveNota);
                var request = new RestRequest(Method.GET);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                //string Leitura = Leitor.ReadToEnd();
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + resultado);
               // request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
               // request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);               
               // request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
               // request.AddParameter("Documento", Documento, ParameterType.GetOrPost);



                result = client.Execute(request).Content;

           
                XmlRootAttribute atrib = new XmlRootAttribute();
                // var atrib = new XmlRootAttribute("nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = true);
                atrib.ElementName = "nfeProc";
                atrib.Namespace = "http://www.portalfiscal.inf.br/nfe";
                atrib.IsNullable = true;
                XmlSerializer xml = new XmlSerializer(typeof(nfeproc),atrib);
                using (var reader = new StringReader(result))
                using (var Xreader = XmlReader.Create(reader))
                {
                    nfeproc a;
                    a = (nfeproc)xml.Deserialize(Xreader);
                    
                }
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }


        }

        public static string ConsultarCadastro(string Grupo, string CNPJ, string CNPJConsCad, string CPF,string IE,string UF)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {


                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/conscad?Grupo=" + Grupo + "&CNPJ=" + CNPJ + "&CNPJConsCad=" + CNPJConsCad+ "&CPF="+CPF+ "&IE="+ IE + "&UF=" + UF);
                var request = new RestRequest(Method.GET);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                //string Leitura = Leitor.ReadToEnd();
                //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");4783
                request.AddHeader("Authorization", "Basic " + resultado);
                // request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                // request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);               
                // request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
                // request.AddParameter("Documento", Documento, ParameterType.GetOrPost);



                result = client.Execute(request).Content;





                return result;
            }
            catch (Exception ex)
            {
                return "";

            }


        }

        public static string DescartaNota(string Grupo, string CNPJ,string ChaveNota,string NumeroLote)
        {
            IRestResponse result;
            string userSenha = "admin:1234";

            try
            {
                
                    //TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/descarta");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                   // string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
                    request.AddParameter("NumeroLote", NumeroLote, ParameterType.GetOrPost);


                result = client.Execute(request);




                
                return result.Content;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string ImprimirNota(string Grupo, string CNPJ,string ChaveNota, string URL,string Documento)
        {
            IRestResponse result;
            string userSenha = "admin:1234";

            try
            {
               
                   
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/imprime?Grupo="+Grupo+ "&CNPJ="+ CNPJ + "&ChaveNota="+ ChaveNota+ "&URL="+ URL);
                    var request = new RestRequest(Method.GET);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    
                    request.AddHeader("Authorization", "Basic " + resultado);
                    //request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    //request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                   // request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
                    //request.AddParameter("Url", URL, ParameterType.GetOrPost);
                    //request.AddParameter("Documento", Documento, ParameterType.GetOrPost);



                result = client.Execute(request);

                



                return result.Content;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string EnviarCartaCorreção(string Grupo, string CNPJ)
        {
            string result = "";
            string userSenha = "admin:1234";

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                    var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/envia");
                    var request = new RestRequest(Method.POST);
                    byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                    string resultado = System.Convert.ToBase64String(textoAsBytes);
                    string Leitura = Leitor.ReadToEnd();
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + resultado);
                    request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                    request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                    request.AddParameter("Arquivo", Leitura, ParameterType.GetOrPost);


                    result = client.Execute(request).Content;




                }
                return result;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string ResolveNota(string Grupo, string CNPJ, string ChaveNota)
        {
            IRestResponse result;
            string userSenha = "admin:1234";

            try
            {

                //TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/resolve");
                var request = new RestRequest(Method.POST);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                // string Leitura = Leitor.ReadToEnd();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + resultado);
                request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                request.AddParameter("ChaveNota", ChaveNota, ParameterType.GetOrPost);
                


                result = client.Execute(request);





                return result.Content;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static string Inutilizar(string Grupo, string CNPJ, string Ano, string Serie, string NFIni, string NFFin, string Justificativa)
        {
            IRestResponse result;
            string userSenha = "admin:1234";
           
            try
            {

                //TextReader Leitor = (TextReader)new StreamReader(openFileDialog.FileName);
                var client = new RestClient("https://managersaas.tecnospeed.com.br:8081/ManagerAPIWeb/nfe/inutiliza");
                var request = new RestRequest(Method.POST);
                byte[] textoAsBytes = Encoding.ASCII.GetBytes(userSenha);
                string resultado = System.Convert.ToBase64String(textoAsBytes);
                //string Leitura = Leitor.ReadToEnd();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Basic " + resultado);
                request.AddParameter("Grupo", Grupo, ParameterType.GetOrPost);
                request.AddParameter("CNPJ", CNPJ, ParameterType.GetOrPost);
                request.AddParameter("Ano", Ano, ParameterType.GetOrPost);
                request.AddParameter("Serie", Serie, ParameterType.GetOrPost);
                request.AddParameter("NFIni", NFIni, ParameterType.GetOrPost);
                request.AddParameter("NFFin", NFFin, ParameterType.GetOrPost);
                request.AddParameter("Justificativa", Justificativa, ParameterType.GetOrPost);
                



                result = client.Execute(request);





                return result.Content;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
        public static void Requisicao()
        {
            string dadosPost = "title=vini";
            dadosPost = dadosPost + "&body=teste de envio de post";
            dadosPost = dadosPost + "&userId=1";
            var dados = Encoding.UTF8.GetBytes(dadosPost);

            var requisicaoWeb = WebRequest.CreateHttp("http://jsonplaceholder.typicode.com/posts");

            // requisicaoWeb.Method = "GET";
            requisicaoWeb.Method = "POST";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            requisicaoWeb.ContentType = "application/x-www-form-urlencoded";
            requisicaoWeb.ContentLength = dados.Length;

            using (var stream = requisicaoWeb.GetRequestStream())
            {
                stream.Write(dados, 0, dados.Length);
                stream.Close();

            }

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                string objResponse = reader.ReadToEnd();

                object post = JsonConvert.DeserializeObject<Post>(objResponse.ToString());

                int a;
                streamDados.Close();
                resposta.Close();


            }
        }


        #region Constants

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        #endregion

        /// <summary>
        /// Activate a window from anywhere by attaching to the foreground window
        /// </summary>
        public static void GlobalActivate(this Window w)
        {
            //Get the process ID for this window's thread
            var interopHelper = new WindowInteropHelper(w);
            var thisWindowThreadId = GetWindowThreadProcessId(interopHelper.Handle, IntPtr.Zero);

            //Get the process ID for the foreground window's thread
            var currentForegroundWindow = GetForegroundWindow();
            var currentForegroundWindowThreadId = GetWindowThreadProcessId(currentForegroundWindow, IntPtr.Zero);

            //Attach this window's thread to the current window's thread
            AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, true);

            //Set the window position
            SetWindowPos(interopHelper.Handle, new IntPtr(0), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);

            //Detach this window's thread from the current window's thread
            AttachThreadInput(currentForegroundWindowThreadId, thisWindowThreadId, false);

            //Show and activate the window
            if (w.WindowState == WindowState.Minimized) w.WindowState = WindowState.Normal;
            w.Show();
            w.Activate();
        }

        #region Imports

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        #endregion
    }
}
