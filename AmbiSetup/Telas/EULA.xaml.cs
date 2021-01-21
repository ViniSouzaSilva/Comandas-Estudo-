using AmbiSetup.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AmbiSetup.Telas
{
    /// <summary>
    /// Interaction logic for EULA.xaml
    /// </summary>
    public partial class EULA : Page
    {
        private AUX_SETUP_CLASS AUX_SETUP;

        private bool _autorizado;

        public event EventHandler ComboChanged;

        public bool AUTORIZADO
        {
            get { return _autorizado; }
            set { 
                _autorizado = value;
                ComboChanged?.Invoke(this, new EventArgs());
            }
        }



        public EULA(ref AUX_SETUP_CLASS aUX_SETUP_CLASS)
        {
            AUX_SETUP = aUX_SETUP_CLASS;
            InitializeComponent();
        }

        private void Accept_EULA(object sender, RoutedEventArgs e)
        {
            AUX_SETUP.EULA_ACEITO = true;
            AUTORIZADO = true;
        }

        private void Decline_EULA(object sender, RoutedEventArgs e)
        {
            AUX_SETUP.EULA_ACEITO = false;
            AUTORIZADO = false;
        }

    }
}
