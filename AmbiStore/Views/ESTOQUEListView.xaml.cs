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

namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for StandardListView.xaml
    /// </summary>
    public partial class ESTOQUEListView : Window
    {
        public ESTOQUEListView()
        {
            InitializeComponent();
            InicializaComponentes();
        }

        private void InicializaComponentes()
        {
            double width = SystemParameters.PrimaryScreenWidth;
            this.Width = width + 12;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight - 125;
            this.Top = 115;
            this.Left = -6;
        }

        private void dgDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditarSelecionado((ESTOQUE)dgv_Data.SelectedItem);
        }

        private void Novo_Click(object sender, RoutedEventArgs e)
        {
            CriarNovo();
        }

        private void EditarSelecionado(ESTOQUE estoqueAEditar)
        {
            if (estoqueAEditar is null) return;
            ESTOQUECadastro cadastroview = new ESTOQUECadastro() { DataContext = new ESTOQUEViewModel() { ESTOQUE = estoqueAEditar } };
            cadastroview.ShowDialog();
            dgv_Data.Items.Refresh();
        }

        private void CriarNovo()
        {
            ESTOQUECadastro cadastroview = new ESTOQUECadastro() { DataContext = new ESTOQUEViewModel() { ESTOQUE = new ESTOQUE() } };
            cadastroview.ShowDialog();
            dgv_Data.Items.Refresh();
        }

        private void Alterar_Click(object sender, RoutedEventArgs e)
        {
            EditarSelecionado((ESTOQUE)dgv_Data.SelectedItem);
        }
    }
}
