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
    /// Lógica interna para DanfeVisualizer.xaml
    /// </summary>
    public partial class DanfeVisualizer : Window
    {
        public DanfeVisualizer(string url)
        {
            InitializeComponent();
            //PdfViewerControl.Load(url);
            WebBrowser.Navigate(url);
        }

        private void WebBrowser_OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
