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
    /// Interaction logic for FUNCIONARIOCadastro.xaml
    /// </summary>
    public partial class FUNCIONARIOCadastro : Window
    {
        public FUNCIONARIOCadastro()
        {
            InitializeComponent();
        }

        private void VerificaCEP_Botao(object sender, RoutedEventArgs e)
        {
            ((FUNCIONARIOCadastroVM)DataContext).ObtemDadosViaCEP(txb_CEP.Text);

        }

        private async void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificaDados())
            {

            }
            else
            {
                await((FUNCIONARIOCadastroVM)DataContext).SaveChanges();
                MessageBox.Show("Contato salvo com sucesso");
            }
            DialogResult = true;
            Close();

        }
        private bool VerificaDados()
        {
            return true;
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();

        }
    }
}
