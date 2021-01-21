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
    /// Interaction logic for DESCONTOView.xaml
    /// </summary>
    public partial class DESCONTOView : Window
    {
        public DESCONTOView()
        {
            InitializeComponent();
            txb_Porc.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((((DESCONTOViewModel)DataContext).ValidaDesconto()))
                {
                    DialogResult = true;
                }
            }
            if (e.Key == Key.Escape)
            {
                DialogResult = false;
            }
        }
    }
}
