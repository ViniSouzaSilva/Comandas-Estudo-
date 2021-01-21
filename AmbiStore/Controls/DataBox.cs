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
    class DataBox : DatePicker
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (Template.FindName("PART_Button", this) is Button button)
            {
                button.Visibility = Visibility.Collapsed;
            }
        }
        public DataBox()
        {
            this.AddHandler(GotFocusEvent, new RoutedEventHandler(GotFocusExtra), true);
            //this.AddHandler(LostFocusEvent, new RoutedEventHandler(LostFocusExtra), true);

        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
        }
        private void GotFocusExtra(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = true;
        }
        //private void LostFocusExtra(object sender, RoutedEventArgs e)
        //{
        //    this.IsDropDownOpen = false;
        //}
        //protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        //{
        //    this.IsDropDownOpen = true;
        //    base.OnGotKeyboardFocus(e);
        //}
    }
}
