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
    public partial class FUNCIONARIOSListView : Window
    {
        public FUNCIONARIOSListView()
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
            EditarSelecionado((FUNCIONARIO)dgv_Data.SelectedItem);
        }

        private void Novo_Click(object sender, RoutedEventArgs e)
        {
            CriarNovo();
        }

        private void EditarSelecionado(FUNCIONARIO funcionarioAEditar)
        {
            if (funcionarioAEditar is null) return;
            FUNCIONARIOCadastro cadastroview = new FUNCIONARIOCadastro() { DataContext = new FUNCIONARIOCadastroVM() { FUNCIONARIO = funcionarioAEditar } };
            cadastroview.ShowDialog();
            dgv_Data.Items.Refresh();

        }

        private void CriarNovo()
        {
            FUNCIONARIOCadastro funcionarioview = new FUNCIONARIOCadastro() { DataContext = new FUNCIONARIOCadastroVM() { FUNCIONARIO = new FUNCIONARIO() { CONTATO_FUNCIONARIO = new CONTATO() { CONTATO_PF = new CONTATO_PF()  } } } };
            funcionarioview.ShowDialog();
            dgv_Data.Items.Refresh();

        }

        private void Alterar_Click(object sender, RoutedEventArgs e)
        {
            EditarSelecionado((FUNCIONARIO)dgv_Data.SelectedItem);
        }
    }
}
