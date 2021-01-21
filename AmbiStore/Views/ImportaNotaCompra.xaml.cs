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
using AmbiStore.Shared.Libraries;
using AmbiStore.ViewModels;
using Microsoft.Win32;

namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for ImportaNotaCompra.xaml
    /// </summary>
    public partial class ImportaNotaCompra : Window //TODO: Estilo da linha é perdido ao selecionar a linha
    {
        public ImportaNotaCompra()
        {
            InitializeComponent();
        }

        private async void Importar_Via_Xml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "NFe em XML (.xml)|*.xml";
            ofd.DefaultExt = ".xml";
            if (ofd.ShowDialog() is true)
            {
                await ((ImportaNotaViewModel)DataContext).ImportaNFE(ofd.FileName);

                //Verificação de dados do XML importado

                //Agora é verificado o status de parametrização de cada um dos
                //Produtos listados na xml de entrada
                //await ((ImportaNotaViewModel)DataContext).VerificaProdutos();
            }
            else
            {
                return;
            }

        }

        private void Localizar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Vincular_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            ((ImportaNotaViewModel)DataContext).EditaEstoqueParametrizaco();
        }

        private void Cadastrar_Click(object sender, RoutedEventArgs e)
        {
            ((ImportaNotaViewModel)DataContext).CadastraItemNovo();
        }

        private void Importar_Via_Site_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Escaneie o código de barras, ou digite a chave da nota na página da Receita que será aberta.\n**ATENÇÃO** será necessário o uso de certificado digital válido.\nEm seguida, use o botão \"Importar via XML\" para abrir o arquivo baixado.");
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("http://www.nfe.fazenda.gov.br/portal/consultaRecaptcha.aspx?tipoConsulta=resumo&tipoConteudo=d09fwabTnLk=") { UseShellExecute = true });
        }

        private void Importar_Click(object sender, RoutedEventArgs e)
        {
            ((ImportaNotaViewModel)DataContext).ConfirmaItems();
            DialogResult = true;
            this.Close();
        }
    }
}
