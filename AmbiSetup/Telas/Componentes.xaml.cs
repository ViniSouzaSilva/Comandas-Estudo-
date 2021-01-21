using AmbiSetup.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
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
    /// Interaction logic for Componentes.xaml
    /// </summary>
    public partial class Componentes : Page
    {
        private AUX_SETUP_CLASS AUX_SETUP;

        public Componentes(ref AUX_SETUP_CLASS aUX_SETUP)
        {
            AUX_SETUP = aUX_SETUP;
            InitializeComponent();
            aUX_SETUP.INSTALL_MYSQL = !IsMySQLInstalled();
            AUX_SETUP.INSTALL_NETCORE = !IsNetCore31Installed();
            this.DataContext = AUX_SETUP;
        }
        private bool IsMySQLInstalled()
        {
            if (
                Registry.CurrentUser.OpenSubKey(@"Software\MySQL AB\MySQL Server 8.0") == null
                &&
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\MySQL AB\MySQL Server 8.0") == null)
                return false;
            else return true;
        }

        private bool IsMySQLRunning()
        {
            ServiceController sc = new ServiceController("MySQL80");

            return sc.Status switch
            {
                ServiceControllerStatus.Running => true,
                _ => false
            };
        }

        private bool IsNetCore31Installed()
        {
            if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost").GetValue("Version") != null) return true;
            return false;
        }
    }
}
