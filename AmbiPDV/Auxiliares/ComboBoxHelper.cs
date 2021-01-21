using AmbiPDV.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AmbiPDV.Auxiliares
{
    public static class ComboBoxHelper
    {
        public static readonly DependencyProperty DisableF4HotKeyProperty =
            DependencyProperty.RegisterAttached("DisableF4HotKey", typeof(bool),
                typeof(ComboBoxHelper), new PropertyMetadata(false, OnDisableF4HotKeyChanged));

        public static bool GetDisableF4HotKey(DependencyObject obj)
        {
            return (bool)obj.GetValue(DisableF4HotKeyProperty);
        }

        public static void SetDisableF4HotKey(DependencyObject obj, bool value)
        {
            obj.SetValue(DisableF4HotKeyProperty, value);
        }

        private static void OnDisableF4HotKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as ComboBoxF4;
            if (d == null) return;

            box.PreviewKeyDown -= OnComboBoxKeyDown;
            box.PreviewKeyDown += OnComboBoxKeyDown;
        }

        private static void OnComboBoxKeyDown(object _, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F4)
            {
                e.Handled = true;
            }
        }
    }
}
