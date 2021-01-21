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
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for CadastraSenhaAdminView.xaml
    /// </summary>
    public partial class CADASTRASENHAView : Window
    {
        private CADASTRASENHAViewModel _viewModel;
        public CADASTRASENHAView(FUNCIONARIO funcionario = null)
        {
            var viewModel = new CADASTRASENHAViewModel
            {
                funcionario = funcionario ?? new FUNCIONARIO()
            };

            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private async void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            if (
                (GetMD5Hash(txb_SenhaAntiga.Password) == _viewModel.funcionario.SENHA || _viewModel.funcionario.SENHA is null)
                &&
                txb_SenhaNova1.Password == txb_SenhaNova2.Password
                )
            {
                await _viewModel.GravaNovaSenha(txb_SenhaNova2.Password);
                DialogResult = true;
                this.Close();
            }
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}