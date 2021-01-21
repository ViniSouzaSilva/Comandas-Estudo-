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

namespace AmbiStore.Telas
{
    /// <summary>
    /// Lógica interna para TelaNF.xaml
    /// </summary>
    public partial class TelaNF : Window
    {
        public TelaNF()
        {
            InitializeComponent();
        }
        public int Mamao { get; set; }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Label_GotFocus(object sender, RoutedEventArgs e)
        {
            Descricao_txb.Focus();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Descricao_lbl.Visibility == Visibility.Visible ) {
                Descricao_lbl.Visibility = Visibility.Collapsed;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Descricao_lbl.Visibility == Visibility.Collapsed && Descricao_txb.Text.Equals("")) 
            {
                Descricao_lbl.Visibility = Visibility.Visible;
            
            }
        }

        private void Descricao_lbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Descricao_txb.Focus();
        }

        private void CPF_txb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CPF_lbl.Visibility == Visibility.Visible)
            {
                CPF_lbl.Visibility = Visibility.Collapsed;
            }
        }

        private void CPF_txb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CPF_lbl.Visibility == Visibility.Collapsed && CPF_txb.Text.Equals(""))
            {
                CPF_lbl.Visibility = Visibility.Visible;

            }

        }

        private void CPF_lbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CPF_txb.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Expander_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            if (Expander.IsExpanded == false)
            {
                Batata_grid.SetValue(Grid.RowProperty, 4);
            }
            if (Expander.IsExpanded == true)
            {
                Batata_grid.SetValue(Grid.RowProperty, 6);
                Produto_datagrid.SetValue(Grid.RowProperty, 6);
                IncluirProd_btn.SetValue(Grid.RowProperty,6);
                ExcluirProd_btn.SetValue(Grid.RowProperty, 6);
                EditProdu_btn.SetValue(Grid.RowProperty, 6);
                CalcST_btn.SetValue(Grid.RowProperty, 7);
                Contribuinte_check.SetValue(Grid.RowProperty, 8);
                LocalEntrega_btn.SetValue(Grid.RowProperty, 9);
            }
            if (Expander2.IsExpanded == true) 
            {
                Expander2.IsExpanded = false;
            }

        }

        private void Expander2_Expanded(object sender, RoutedEventArgs e)
        {
            if (Expander2.IsExpanded == true)
            {
                Expander.IsExpanded = false;
                if (Expander.IsExpanded == false)
                {
                    Batata_grid.SetValue(Grid.RowProperty, 4);
                }
                if (Expander.IsExpanded == true)
                {
                    Batata_grid.SetValue(Grid.RowProperty, 7);
                }
            }
        }
    }
}
