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
    /// Interaction logic for GRUPOCadastroEListView.xaml
    /// </summary>
    public partial class UNIDADEMEDIDACadastroEListView : Window
    {
        public UNIDADEMEDIDACadastroEListView()
        {
            InitializeComponent();
        }

        private void Novo_Click(object sender, RoutedEventArgs e)
        {
            ((UNIMEDIDAViewModel)DataContext).UniMedSelecionado = new UNI_MEDIDA();
            this.Height = 296.08 + 123.84;
            grd_Collapsable.Visibility = Visibility.Visible;
            dgv_Grupos.IsEnabled = false;
            grp_Buttons.IsEnabled = false;

        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 296.08 + 123.84;
            grd_Collapsable.Visibility = Visibility.Visible;
            dgv_Grupos.IsEnabled = false;
            grp_Buttons.IsEnabled = false;
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Confirmar_Click(object sender, RoutedEventArgs e)
        {
            ((UNIMEDIDAViewModel)DataContext).SalvaSelecionado();
            this.Height = 296.08;
            grd_Collapsable.Visibility = Visibility.Collapsed;
            dgv_Grupos.Items.Refresh();
            dgv_Grupos.IsEnabled = true;
            grp_Buttons.IsEnabled = true;

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 296.084;
            grd_Collapsable.Visibility = Visibility.Collapsed;
            dgv_Grupos.IsEnabled = true;
            grp_Buttons.IsEnabled = true;

        }
    }
}
