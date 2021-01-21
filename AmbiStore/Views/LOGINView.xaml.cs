using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.EFCore.Services;
using AmbiStore.Shared.Libraries;
using AmbiStore.ViewModels;
using AmbiStore.Views;
using Microsoft.EntityFrameworkCore;
using static AmbiStore.Shared.Libraries.Static;

namespace AmbiStore.Views
{
    /// <summary>
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class LOGINView : Window
    {
        private Functions funcoes;
        public LOGINView()
        {
            InitializeComponent();
            funcoes = new Functions();
            DataContext = new LOGINViewModel();
            if (((LOGINViewModel)DataContext).VerificaConexao() is false)
            {
                MessageBox.Show("Servidor não foi encontrado, ou está desligado. Reinicie o aplicativo e tente novamente.");
                Login_btn.IsEnabled = false;
                Login_txb.IsEnabled = false;
                Senha_txb.IsEnabled = false;
            }
#if AUTOLOGIN
            AutoLogin();
#endif
        }

        private async void AutoLogin()
        {
            using var context = new AmbiStoreDbContextFactory().CreateDbContext();
            FUN_LOGADO = await context.FUNCIONARIOs.Select(x => x).Where(x => x.ID == -1).FirstAsync();
            this.Hide();
            (new MENUSTRIPView() { DataContext = new MENUStripVM() }).ShowDialog();

        }

        private async void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Login_txb.SelectedItem is null)
            {
                MessageBox.Show("Por favor, selecione um usuário");
                return;
            }
            if (string.IsNullOrWhiteSpace(Senha_txb.Password))
            {
                MessageBox.Show("Por favor, digite uma senha válida");
                return;
            }
            if (await funcoes.VerificaUsuarioESenha((FUNCIONARIO)Login_txb.SelectedItem, Senha_txb.Password))
            {
                FUN_LOGADO = ((LOGINViewModel)DataContext).FuncionarioSelecionado;
                this.Hide();
                (new MENUSTRIPView() { DataContext = new MENUStripVM() }).ShowDialog();
            }
            else
            {
                MessageBox.Show("Senha incorreta.");
                return;
            }

        }

        private async void Senha_txb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!(Login_txb.SelectedItem is null) && ((FUNCIONARIO)Login_txb.SelectedItem).ID == -1)
                if ((await funcoes.VerificaCadastroSenhaAdmin()) == false)
                {
                    using var context = new AmbiStoreDbContextFactory().CreateDbContext();
                    new CADASTRASENHAView(await context.FUNCIONARIOs.Select(x => x).Where(x => x.ID == -1).FirstAsync()).ShowDialog();
                }

        }

        private void Close_Click(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}