using AmbiPDV.ViewModels;
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
using static AmbiStore.Shared.Extension.StringExtensions;

namespace AmbiPDV.Views
{
    /// <summary>
    /// Interaction logic for FECHAMENTOCUPOMView.xaml
    /// </summary>
    public partial class FECHAMENTOCUPOMView : Window
    {
        public FECHAMENTOCUPOMView()
        {
            InitializeComponent();
            txb_FormaPagto.Focus();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
            }
        }

        private void txb_FormaPagto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string verificaCodigo = ((FECHAMENTOCUPOMViewModel)this.DataContext).ValidaFormaEscolhida();
                if (verificaCodigo != "0")
                {
                    MessageBox.Show(verificaCodigo);
                    return;
                }
                if (((FECHAMENTOCUPOMViewModel)this.DataContext).ProcessaMetodoAtual() == true)
                {
                    //Finalizou a venda. Fecha, e corre pro abraço
                    DialogResult = true;
                }
                else
                {
                    //Limpa os campos e pede o próximo pagamento
                    txb_FormaPagto.Clear();
                    txb_FormaPagto.Focus();
                }
            }
        }
    }
}
