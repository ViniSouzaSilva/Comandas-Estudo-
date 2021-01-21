using AmbiStore.Controls;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.ViewModels;
using CurrencyTextBoxControl;
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
    /// Interaction logic for CadastroEstoque.xaml
    /// </summary>
    public partial class ESTOQUECadastro : Window
    {
        public ESTOQUECadastro()
        {
            InitializeComponent();
        }

        private void CadastroGrupo(object sender, RoutedEventArgs e)
        {
            ((ESTOQUEViewModel)DataContext).CadastraGrupos();
        }

        private void ConverteEmDolar(object sender, RoutedEventArgs e)
        {
            ((ESTOQUEViewModel)DataContext).PrecoEmDolar();

        }

        private void CalculaPrecoVenda(object sender, RoutedEventArgs e)
        {
            var bindingExpression = BindingOperations.GetBindingExpression(txb_Lucro, CurrencyTextBox.NumberProperty);
            bindingExpression.UpdateSource();
            ((ESTOQUEViewModel)DataContext).CalculaPrecoPeloLucro();
            bindingExpression = BindingOperations.GetBindingExpression(txb_PrecoVenda, CurrencyBox.ValueProperty);
            bindingExpression.UpdateTarget();
        }

        private void GeraCodigoBalanca(object sender, RoutedEventArgs e)
        {
            ((ESTOQUEViewModel)DataContext).GeraCodigoBalanca();
            var bindingExpression = BindingOperations.GetBindingExpression(txb_CodBarra, TextBox.TextProperty);
            bindingExpression.UpdateTarget();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private async void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificaDados())
            {

            }
            else
            {
                await ((ESTOQUEViewModel)DataContext).SaveChanges();
                MessageBox.Show("Estoque salvo com sucesso");
            }
            DialogResult = true;
            Close();
        }

        private bool VerificaDados()
        {
            return true;
        }

        private void CadastroUnimedida(object sender, RoutedEventArgs e)
        {
            ((ESTOQUEViewModel)DataContext).CadastraUniMedida();

        }
    }
}
