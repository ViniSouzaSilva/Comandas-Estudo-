using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AmbiPDV.Auxiliares
{
    public class CortesiaVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case StatusCaixaEnum.Fechado:
                case StatusCaixaEnum.Livre:
                    return Visibility.Collapsed;
                case StatusCaixaEnum.EmVenda:
                case StatusCaixaEnum.Totalizacao:
                    return Visibility.Visible;
                case StatusCaixaEnum.EmDevolucao:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EmptyIfOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value == 1)
            {
                return String.Empty;
            }
            else
            {
                return ((decimal)value).ToString((string)parameter, new CultureInfo("pt-BR"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace((string)value))
            {
                return 1M;
            }
            else
            {
                return decimal.Parse((string)value, NumberStyles.AllowCurrencySymbol, new CultureInfo("pt-BR"));
            }
        }
    }
    public class EmptyIfZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value == 0)
            {
                return String.Empty;
            }
            else
            {
                return ((decimal)value).ToString((string)parameter, new CultureInfo("pt-BR"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace((string)value))
            {
                return 1M;
            }
            else
            {
                return decimal.Parse((string)value, NumberStyles.AllowCurrencySymbol, new CultureInfo("pt-BR"));
            }
        }
    }
    public class CollapseIfNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (String.IsNullOrWhiteSpace((string)value)) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class HiddenIfOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 1)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class HiddenIfZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((decimal)value == 0)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
