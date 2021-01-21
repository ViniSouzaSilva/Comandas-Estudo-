using AmbiPDV.Controls;
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
namespace AmbiPDV.Views
{
    /// <summary>
    /// Interaction logic for CAIXAView.xaml
    /// </summary>
    public partial class CAIXAView : Window
    {
        public CAIXAView()
        {
            InitializeComponent();
        }

        private void CAIXAView_InterfaceChanged(object sender, EventArgs e)
        {
            switch (((CAIXAViewModel)DataContext).MODO_CONSULTA)
            {
                case false:

                    break;
                case true:

                    break;
            }
        }


        private void CBB_Produto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ((CAIXAViewModel)this.DataContext).ProcessaPesquisa(CBB_Produto.Text);
                CBB_Produto.Text = String.Empty;
                scl_Produtos.ScrollToBottom();
            }
            if (e.Key == Key.Multiply || (e.KeyboardDevice.Modifiers == ModifierKeys.Shift && e.Key == Key.D8))
            {
                e.Handled = true;
                ((CAIXAViewModel)this.DataContext).AlteraQuantidade(CBB_Produto.Text);
                CBB_Produto.Text = String.Empty;
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!CBB_Produto.IsFocused)
            {
                TextBox textBox = CBB_Produto.Template.FindName("PART_EditableTextBox", CBB_Produto) as TextBox;
                textBox.Focus();
            }
            if (e.Key == Key.F2)
            {
                ((CAIXAViewModel)this.DataContext).TotalizaVenda(false);
            }
            if (e.Key == Key.F4)
            {
                ((CAIXAViewModel)this.DataContext).CancelaItemDaVendaAtual();
            }
            if (e.Key == Key.F5)
            {
                ((CAIXAViewModel)DataContext).AlternaModoConsulta();
            }
            if (e.Key == Key.F6)
            {
                ((CAIXAViewModel)DataContext).CancelaVenda();
            }
            if (e.Key == Key.F8)
            {
                ((CAIXAViewModel)DataContext).AplicaDescontoNoProduto();
            }
            if (e.Key == Key.Escape && e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
            {

            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
