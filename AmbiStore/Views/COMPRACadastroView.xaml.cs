using AmbiStore.Shared.Libraries.Enums;
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

namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for COMPRACadastroView.xaml
    /// </summary>
    public partial class COMPRACadastroView : Window
    {
        public COMPRACadastroView()
        {
            InitializeComponent();
            exp_Fornecedor.IsExpanded = true;
        }

        private async void Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (await ((COMPRACadastroViewModel)this.DataContext).GravaCompraNaBase())
            {
                this.Close();
            }
        }

        private async void CFOP_CBB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await ((COMPRACadastroViewModel)this.DataContext).AtualizaDropDownCFOP(((ComboBox)sender).Text);
                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }

        private void CFOP_Pesq_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("CFOP_Pesq_Menu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private void CFOP_Comecando_Com_Click(object sender, RoutedEventArgs e)
        {
            ((COMPRACadastroViewModel)this.DataContext).TipoPesqCFOP = TipoPesquisaCBB.StartsWith;
        }

        private void CFOP_Contendo_Click(object sender, RoutedEventArgs e)
        {
            ((COMPRACadastroViewModel)this.DataContext).TipoPesqCFOP = TipoPesquisaCBB.Containing;
        }

        private void Importar_XML_Click(object sender, RoutedEventArgs e)
        {
            ImportaNotaCompra inc = new ImportaNotaCompra();
            if (inc.ShowDialog() == true)
            {
                ((COMPRACadastroViewModel)this.DataContext).ImportaCompra(((ImportaNotaViewModel)inc.DataContext).COMPRA_IMPORTADA);
            }
            dgv.Items.Refresh();
        }


        private void Fornec_Comecando_Com_Click(object sender, RoutedEventArgs e)
        {
            ((COMPRACadastroViewModel)this.DataContext).TipoPesqFornec = TipoPesquisaCBB.StartsWith;
        }

        private void Fornec_Contendo_Click(object sender, RoutedEventArgs e)
        {
            ((COMPRACadastroViewModel)this.DataContext).TipoPesqFornec = TipoPesquisaCBB.Containing;
        }

        private void Fornec_Pesq_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("Fornec_Pesq_Menu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        private async void Fornec_CBB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await((COMPRACadastroViewModel)this.DataContext).AtualizaDropDownFornec(((ComboBox)sender).Text);
                ((ComboBox)sender).IsDropDownOpen = true;
            }
        }
    }
}
