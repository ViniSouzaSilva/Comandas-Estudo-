using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace AmbiPDV.Controls
{
    public class ComboBoxF4 : ComboBox
    {
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                e.Handled = true;
                return;
            }

            base.OnPreviewKeyDown(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                e.Handled = true;
                return;
            }
            else if (e.Key == Key.Escape && (Text != "" || Text != String.Empty))
            {
                Text = "";
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }
    }
}
