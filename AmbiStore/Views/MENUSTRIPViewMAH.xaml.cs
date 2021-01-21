using AmbiStore.Shared.EFCore.Data;
using AmbiStore.ViewModels;
using AmbiStore.Views;
using MaterialDesignThemes.Wpf;
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
using Theme = MaterialDesignThemes.Wpf.Theme;
namespace AmbiStore.Views
{
    /// <summary>
    /// Interaction logic for MenuStrip.xaml
    /// </summary>
    public partial class MENUSTRIPViewMAH : Window
    {





        private Window openWindow;
        public MENUSTRIPViewMAH()
        {
            InitializeComponent();
            InicializaComponentes();
            VerificaEmitente();
        }

        private void VerificaEmitente()
        {
            AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
            if (_context.EMITENTEs.Select(x => x).Any() is false)
            {
                EMITENTECadastro cadastroEmitente = new EMITENTECadastro() { DataContext = new EMITENTEViewModel() { EMITENTE = new Shared.EFCore.Models.EMITENTE() } };
                if (!(cadastroEmitente.ShowDialog() is true))
                    Application.Current.Shutdown();

            }
        }

        public void InicializaComponentes()
        {
            AjustaTamanhoDaJanela();
        }

        private void AjustaTamanhoDaJanela()
        {
            double width = SystemParameters.PrimaryScreenWidth;
            this.Width = width + 12;
            this.Height = 121;
            this.Top = 0;
            this.Left = -6;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(openWindow is null))
                openWindow.Close();
            Application.Current.Shutdown();
        }

        private void Emitente_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Contatos_Click(object sender, RoutedEventArgs e)
        {
            if (!(openWindow is null))
                openWindow.Close();
            openWindow = new CONTATOSListView() { DataContext = new CONTATOListVM() };
            openWindow.Show();
        }
    }
}
