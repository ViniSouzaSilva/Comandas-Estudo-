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
    /// Interaction logic for StandardListView.xaml
    /// </summary>
    public partial class CONTATOSListViewDrawer : Window
    {
        public CONTATOSListViewDrawer()
        {
            InitializeComponent();
            InicializaComponentes();
        }

        private void InicializaComponentes()
        {
            double width = SystemParameters.PrimaryScreenWidth;
            this.Width = width + 12;
            this.Height = SystemParameters.MaximizedPrimaryScreenHeight - 125;
            this.Top = 115;
            this.Left = -6;
        }

        private void Certificado_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grupos_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleButton_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CONTATOCadastro cadastroview = new CONTATOCadastro() { DataContext = new CONTATOCadastroVW() { CONTATO = (CONTATO)dgv_Data.SelectedItem } };
            cadastroview.ShowDialog();
        }
    }
}
