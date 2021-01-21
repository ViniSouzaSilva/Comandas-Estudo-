using AmbiSetup.Models;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AmbiSetup.Telas
{
    /// <summary>
    /// Interaction logic for InstallDir.xaml
    /// </summary>
    public partial class InstallDir : Page
    {
        private AUX_SETUP_CLASS AUX_SETUP;

        public InstallDir(ref AUX_SETUP_CLASS aUX_SETUP)
        {
            AUX_SETUP = aUX_SETUP;
            InitializeComponent();
            this.DataContext = AUX_SETUP;
        }

        private void Procurar_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog folderBrowser = new VistaFolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.RootFolder = Environment.SpecialFolder.ProgramFiles;
            folderBrowser.SelectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Trilha Informatica\AmbiStore\");
            if (folderBrowser.ShowDialog() == true)
            {
                txb_InstallDir.Text = folderBrowser.SelectedPath;
                txb_InstallDir.Focus();
            }
        }
    }
}
