using AmbiPad.ViewModels;
using AmbiStore.Shared.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AmbiPad.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ComandaView : Window
    {
        public ComandaView()
        {
            InitializeComponent();
        }

        KeyConverter key = new KeyConverter();

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Comanda_txb_KeyDown(object sender, KeyEventArgs e)

        {

            if (e.Key == Key.Tab || e.Key == Key.NumPad0 || e.Key == Key.NumPad1|| e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8|| e.Key == Key.NumPad9) return;
            if ((char.IsNumber((string)key.ConvertTo(e.Key, typeof(string)), 0) == false)) { e.Handled = true; }
                
            if (e.Key == Key.Enter) 
            {
                ((ComandaVM)DataContext).PesquisaComanda();
                Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                ID_produto_txb.Focus();

            }
        }

        private void Add_comanda_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ((ComandaVM)DataContext).AdicionarComanda();
                

            }
            catch(Exception ex) 
            {
            
            
            }
        }

        private void Pesquisar_btn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SalvarItemComanda_btn_Click(object sender, RoutedEventArgs e)

        {
            if (ID_produto_txb.Text.Equals("")|| Comanda_txb.Text.Equals("")||Quantidade_txb.Text.Equals(""))
            {
                MessageBox.Show("Preencha os dados de Id do produto e quantidade","ATENÇÃO", MessageBoxButton.OK);
            }
            else
            {
                ((ComandaVM)DataContext).AdicionaItemComanda(Quantidade_txb.Text);
                Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (System.Windows.MessageBox.Show("Você deseja realmente deletar esse produto?", "ATENÇÃO", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                COMANDA_HISTORICO ID = (COMANDA_HISTORICO)Comanda_grid.SelectedItem;
                ((ComandaVM)DataContext).DeletaItemComanda(ID.ID);
                Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
            }
            else 
            {
            
            }
        }

        private void Fechamneto_btn_Click(object sender, RoutedEventArgs e)
        {
            Comanda_txb.Focus();
            if (Comanda_txb.Text.Equals("") || Comanda_txb.Text.Equals("0"))
            {
                MessageBox.Show("Preencha o ID da comanda", "ATENÇÃO", MessageBoxButton.OK);
            }
            else
            {
                ((ComandaVM)DataContext).FechaComanda(Comanda_txb.Text);
                LimpaCampos();


            }
            
        }

        private void ExibirComanda_btn_Click(object sender, RoutedEventArgs e)
        {
            ((ComandaVM)DataContext).PesquisaComanda();
            Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
        }

        private void ID_produto_txb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9) return;
            if ((char.IsNumber((string)key.ConvertTo(e.Key, typeof(string)), 0) == false)) { e.Handled = true; }
            if (e.Key == Key.Enter)
            {
                if (ID_produto_txb.Text.Equals("") || Comanda_txb.Text.Equals(""))
                {
                    MessageBox.Show("Preencha os dados de Id do produto e quantidade", "ATENÇÃO", MessageBoxButton.OK);
                }
                else
                {
                    ((ComandaVM)DataContext).AdicionaItemComanda(Quantidade_txb.Text);
                    Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                }
            }
        }

        
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                COMANDA ID = (COMANDA)Coman_datagrid.SelectedItem;
                ((ComandaVM)DataContext).InativaComanda(ID.ID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            
            {
                if (e.Key == Key.F2) 
                {
                    Comanda_txb.Focus();
                    ((ComandaVM)DataContext).PesquisaComanda();
                    Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                    ID_produto_txb.Focus();
                }

                if (e.Key == Key.F3)
                {
                    if (ID_produto_txb.Text.Equals("") || Comanda_txb.Text.Equals("") || Quantidade_txb.Text.Equals(""))
                    {
                        MessageBox.Show("Preencha os dados de Id do produto e quantidade", "ATENÇÃO", MessageBoxButton.OK);
                    }
                    else
                    {

                        ID_produto_txb.Focus();
                        ((ComandaVM)DataContext).AdicionaItemComanda(Quantidade_txb.Text);
                        Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                        Quantidade_txb.Focus();
                    }
                }
                if (e.Key == Key.F5)
                {
                    Comanda_txb.Focus();
                    if (Comanda_txb.Text.Equals("") || Comanda_txb.Text.Equals("0"))
                    {
                        MessageBox.Show("Preencha o ID da comanda", "ATENÇÃO", MessageBoxButton.OK);
                    }
                    else
                    {
                        ((ComandaVM)DataContext).FechaComanda(Comanda_txb.Text);
                        LimpaCampos();
                        //otal_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                    }
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void FecharComanda_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Comanda_txb.Text.Equals("")|| Comanda_txb.Text.Equals("0"))
                {
                    MessageBox.Show("Preencha o ID da comanda", "ATENÇÃO", MessageBoxButton.OK);
                }
                else
                {
                    ((ComandaVM)DataContext).FechaComanda(Comanda_txb.Text);
                    LimpaCampos();
                   // Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void Quantidade_txb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5
                || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9) return;
            if ((char.IsNumber((string)key.ConvertTo(e.Key, typeof(string)), 0) == false)) { e.Handled = true; }
            if (e.Key == Key.Enter)
            {
                if (ID_produto_txb.Text.Equals("") || Comanda_txb.Text.Equals("") || Quantidade_txb.Text.Equals(""))
                {
                    MessageBox.Show("Preencha os dados de Id do produto e quantidade", "ATENÇÃO", MessageBoxButton.OK);
                }
                else
                {
                    ((ComandaVM)DataContext).AdicionaItemComanda(Quantidade_txb.Text);
                    Total_txb.Text = ((ComandaVM)DataContext).SomaQuantidade().ToString("N2");
                }
            }
        }

        public void LimpaCampos() 
        {
            ID_produto_txb.Text = "";
            Quantidade_txb.Text = "";
            Total_txb.Text = "";
            Comanda_txb.Text = "";
        
        }

        private void Minimizar_txb_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Fechar_txb_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
