using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace AmbiStore.Controls
{
    public class PercentageBox : TextBox/*, INotifyPropertyChanged*/
    {
        public static readonly CultureInfo ptBR = CultureInfo.GetCultureInfo("pt-BR");
        private static readonly System.Text.RegularExpressions.Regex _regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
        private bool caretIsOnDecimal = false;

        public PercentageBox()
        {
            //this.DataContext = this;
            //Text = $"{moeda}0,00";
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            HorizontalContentAlignment = HorizontalAlignment.Right;
            VerticalContentAlignment = VerticalAlignment.Center;
        }
        public decimal Value
        {
            get
            {
                return (decimal)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(decimal), typeof(PercentageBox), new UIPropertyMetadata(MyPropertyChangedHandler));

        public static void MyPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //// Get instance of current control from sender
            //// and property value from e.NewValue

            // Set public property on TaregtCatalogControl, e.g.
            ((PercentageBox)sender).Text = ((decimal)e.NewValue).ToString("P2");
            ((PercentageBox)sender).Value = (decimal)e.NewValue;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            Text = Value.ToString("P2", ptBR);
            base.OnLostFocus(e);
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //public void OnPropertyChanged(string name)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}

        public bool EnterToMoveNext { get; set; } = true;


        private int decimalCaretIndex()
        {
            if (!String.IsNullOrEmpty(Text))
            {
                return Text.Length - 3;
            }
            else return 0;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (CaretIndex <= Text.Length - 4)
            {
                caretIsOnDecimal = false;
            }
            else
            {
                caretIsOnDecimal = true;
            }

            if (e.Key == Key.Delete)
            {
                e.Handled = true;
                Value = 0;
                caretIsOnDecimal = false;
            }

            if (e.Key == Key.Back)
            {
                if (!caretIsOnDecimal)
                {
                    if (Text.Length == 5)
                    {
                        Value = decimal.Parse("0," + Text.Substring(decimalCaretIndex(), 2), ptBR)/100;
                        e.Handled = true;
                        CaretIndex = Text.Length - 5;

                    }
                    else
                    {
                        Value = decimal.Parse($"{Text.Substring(0, Text.Length - (5 + 0))},{Text.Substring(decimalCaretIndex(), 2)}", ptBR)/100;
                        e.Handled = true;
                        CaretIndex = Text.Length - 5;

                    }
                }
                else
                {
                    Value = decimal.Parse($"{Text.Substring(0, Text.Length - 4)},0{Text.Substring(Text.Length - 3, 1)}", ptBR)/100;
                    e.Handled = true;
                    CaretIndex = Text.Length-1;
                }
            }

            else if (e.Key == Key.Right || e.Key == Key.Left || e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {

            }
            else if (e.Key == Key.Space) e.Handled = true;
            else
            {
                if (this.SelectionLength == Text.Length)
                {
                    Value = 0;
                }
            }
            base.OnPreviewKeyDown(e);
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (e.Text == "," || e.Text == ".")
            {
                caretIsOnDecimal = true;
                CaretIndex = Text.Length;
            }
            string inteiros = Text.Substring(0, Text.Length - 4);

            string decimais = Text.Substring(decimalCaretIndex(), 2);
            base.OnPreviewTextInput(e);

            if (ContemSoNumeros(e.Text))
            {
                if (!caretIsOnDecimal)
                {
                    if (decimal.Parse(inteiros) == 0)
                    {
                        Value = decimal.Parse($"{e.Text},{decimais}", ptBR)/100;
                        //Text = $"R$ {e.Text},{decimais}";
                    }
                    else
                    {
                        Value = decimal.Parse($"{inteiros}{e.Text},{decimais}", ptBR)/100;
                        //Text = $"R$ {inteiros}{e.Text},{decimais}";
                    }
                    CaretIndex = Text.Length - 5;
                }
                else
                {
                    Value = decimal.Parse($"{inteiros},{decimais.Substring(1)}{e.Text}", ptBR)/100;
                    //Text = $"R$ {inteiros},{decimais.Substring(1)}{e.Text}";
                    CaretIndex = Text.Length-1;
                }
            }
            //Value = decimal.Parse(Text.Substring(3), ptBR);
            e.Handled = true;
        }


        protected override void OnGotFocus(RoutedEventArgs e)
        {
            caretIsOnDecimal = false;
            SelectAll();
            e.Handled = true;
            base.OnGotFocus(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (!this.IsFocused)
            {
                this.Focus();
                e.Handled = true;
            }
            else
            {
                if (CaretIndex <= Text.Length - 3)
                {
                    caretIsOnDecimal = false;
                }
                else
                {
                    caretIsOnDecimal = true;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var element = this as UIElement;
            if (e.Key == Key.Enter && EnterToMoveNext)
            {
                element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            base.OnKeyDown(e);
        }


        private static bool ContemSoNumeros(string texto)
        {
            return !_regex.IsMatch(texto);
        }
    }

}
