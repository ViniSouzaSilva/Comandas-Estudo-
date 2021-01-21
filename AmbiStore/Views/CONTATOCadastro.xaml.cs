using AmbiStore.Shared.EFCore.Models;
using AmbiStore.ViewModels;
using System;
using System.Windows;

namespace AmbiStore.Views

{
    /// <summary>
    /// Interaction logic for CadastroContato.xaml
    /// </summary>
    public partial class CONTATOCadastro : Window
    {
        public CONTATOCadastro()
        {
            InitializeComponent();
        }

        //private void CadastroContato_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "END_MUNICIPIO")
        //    {
        //        return;
        //    }
        //}

        private async void OK_Click(object sender, RoutedEventArgs e)
        {
            if (!VerificaDados())
            {

            }
            else
            {
               await ((CONTATOCadastroVW)DataContext).SaveChanges();
                MessageBox.Show("Contato salvo com sucesso");
            }
            DialogResult = true;
            Close();
        }

        private bool VerificaDados()
        {
            return true;
        }

        private void VerificaCEP_Botao(object sender, RoutedEventArgs e)
        {
            ((CONTATOCadastroVW)DataContext).ObtemDadosViaCEP(txb_CEP.Text);
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
