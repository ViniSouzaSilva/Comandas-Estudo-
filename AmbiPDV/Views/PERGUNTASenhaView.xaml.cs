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
    /// Interaction logic for PERGUNTASenhaView.xaml
    /// </summary>
    public partial class PERGUNTASENHAView : Window
    {
        public PERGUNTASENHAView()
        {
            InitializeComponent();
            txb_Password.Focus();
        }

        private async void txb_Porc_KeyDown(object sender, KeyEventArgs e)
        {
            bool result = false;
            if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(txb_Password.Password))
            {
                result = await ((PERGUNTASENHAViewModel)DataContext).VerificaSenha(txb_Password.Password);

                if (result == true)
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Senha digitada não é válida");
                    return;
                }
            }
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
            }
        }
    }
}
