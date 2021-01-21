using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using AmbiStore.Shared.Extension;
using System.Configuration;

namespace AmbiStore.Controls
{
    class CNPJBox : TextBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool EnterToMoveNext { get; set; } = true;

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Text = Text.TiraPont();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (!Text.IsCnpj() && !string.IsNullOrWhiteSpace(Text)) MessageBox.Show("CNPJ inválido");
            else
            {
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    Text = Text.ParseToCNPJ();
                    OnPropertyChanged("Text");
                }
            }
            base.OnLostFocus(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!ContemSoNumeros(e.Text) || Text.Length >= 14)
            {
                e.Handled = true;
                return;
            }
            else base.OnPreviewTextInput(e);
        }

        public CNPJBox()
        {
            HorizontalContentAlignment = HorizontalAlignment.Left;
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
            base.OnPreviewKeyDown(e);
        }


        //private static readonly Regex _regex = 
        //    new Regex(@"\d{2}\.\d{3}\.\d{3}\-\d{4}\/\d{2}");

        //private static bool CNPJRegex(string texto)
        //{
        //    return !_regex.IsMatch(texto);
        //}

        private static readonly Regex _regex = new Regex(@"^\d+");

        private static bool ContemSoNumeros(string texto)
        {
            return _regex.IsMatch(texto);
        }

    }

}
