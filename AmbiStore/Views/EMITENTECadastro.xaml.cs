using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Extension;
using AmbiStore.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Xml.Serialization;
using System.Windows.Controls;
using AmbiStore.Controls;

namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for CadastroEmitente.xaml
    /// </summary>
    public partial class EMITENTECadastro : Window
    {
        public EMITENTECadastro()
        {
            InitializeComponent();
        }

        private void VerificaCEP_Botao(object sender, RoutedEventArgs e)
        {
            ((EMITENTEViewModel)this.DataContext).ObtemDadosViaCEP(txb_CEP.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void CNAE_ConsultaBotao(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://cnae.ibge.gov.br/busca-online-cnae.html?view=estrutura");
        }

        private async void but_Registrar(object sender, RoutedEventArgs e)
        {
            await ((EMITENTEViewModel)DataContext).SaveChanges();
            MessageBox.Show("Registrado com sucesso!");
            this.Close();
        }

        private void TelefoneLostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
