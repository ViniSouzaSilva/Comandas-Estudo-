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
    class FoneBox : TextBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public bool EnterToMoveNext { get; set; } = true;

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            Text = Text.TiraPont(true);
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (!Text.IsTelefoneBR() && !string.IsNullOrWhiteSpace(Text)) MessageBox.Show("Telefone inválido");
            else
            {
                Text = Text.ParseToTelefone();
                OnPropertyChanged("Text");
            }
            base.OnLostFocus(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!ContemSoNumeros(e.Text) || Text.Length >= 11)
            {
                e.Handled = true;
                return;
            }
            else base.OnPreviewTextInput(e);
        }

        public FoneBox()
        {
            HorizontalContentAlignment = HorizontalAlignment.Left;
            VerticalContentAlignment = VerticalAlignment.Center;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
            base.OnPreviewKeyDown(e);
        }


        private static readonly Regex _regex = new Regex(@"^\d+");

        private static bool ContemSoNumeros(string texto)
        {
            return _regex.IsMatch(texto);
        }

    }

}
