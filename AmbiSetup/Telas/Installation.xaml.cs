using System;
using System.Collections.Generic;
using System.IO.Compression;
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
using static AmbiSetup.Models.AUX_SETUP_CLASS;
using AmbiSetup.Models;
using System.Diagnostics;

namespace AmbiSetup.Telas
{
    /// <summary>
    /// Interaction logic for Installation.xaml
    /// </summary>
    public partial class Installation : Page
    {
        private Progress<(string textToReport, double percentage)> progressIndicator;
        private AUX_SETUP_CLASS AUX_SETUP;
        public Installation(ref AUX_SETUP_CLASS aUX_SETUP)
        {
            InitializeComponent();
            AUX_SETUP = aUX_SETUP;
        }

        

        private void InstalaStore()
        {
            progressIndicator = new Progress<(string textToReport, double percentage)>(AtualizaUI);
            Funcoes.ExtractFiles extractFiles = new Funcoes.ExtractFiles();
            string zipPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\Store.pack");
            //ZipArchive zip = ZipFile.OpenRead(zipPath);
            extractFiles.ExtraiArquivos(progressIndicator, zipPath, AUX_SETUP.INSTALL_LOCATION);
        }
        private void InstalaAnyDesk()
        {
            pgb_Progresso.IsIndeterminate = true;
            lbl_Progresso.Visibility = Visibility.Hidden;
            txb_Progresso.AppendText($"{Environment.NewLine}Instalando o AnyDesk - Programa de suporte remoto");
            string anyPack = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\AnyDesk-CM.exe");
            string anyDeskInstallDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"AnyDesk");
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = anyPack;
            psi.Arguments = $@"--install ""{anyDeskInstallDir}"" --start-with-win --create-dektop-icon --silent";
            Process.Start(psi);
            psi.FileName = "CMD.exe";
            psi.Arguments = $@"/c echo PFES2011 | ""{anyDeskInstallDir}\AnyDesk.exe"" --set-password";
            Process.Start(psi);
            txb_Progresso.AppendText($"{Environment.NewLine}AnyDesk Instalado com sucesso!");
        }

        private void AtualizaUI((string textToReport, double percentage) obj)
        {
            pgb_Progresso.Value = obj.percentage * 100;
            lbl_Progresso.Content = $"{(obj.percentage * 100):N1} %";
            txb_Progresso.AppendText($"{Environment.NewLine}{obj.textToReport}");
        }

        private void InstalaNETCore()
        {
            txb_Progresso.AppendText($"{Environment.NewLine}Instalando o .NET Core 3.1.8");
            ProcessStartInfo psi = new ProcessStartInfo();
            string NetCorePack = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\NETCore3.1.8.exe");
            psi.FileName = NetCorePack;
            Process processo = Process.Start(psi);
            while (processo.HasExited is false)
            {

            }
            txb_Progresso.AppendText($"{Environment.NewLine}.Net Core 3.1.8 Instalado com sucesso!");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (AUX_SETUP.INSTALL_STORE) InstalaStore();
            if (AUX_SETUP.INSTALL_ANYDESK) InstalaAnyDesk();
            if (AUX_SETUP.INSTALL_MYSQL) InstalaMySQL();
            if (AUX_SETUP.INSTALL_NETCORE) InstalaNETCore();
        }

        private void InstalaMySQL()
        {
            txb_Progresso.AppendText($"{Environment.NewLine}Instalando o MySQL 8");
            txb_Progresso.AppendText($"{Environment.NewLine}Siga as instruções referentes à instalação do MySQL fornecidas pelo seu revendedor.");

            ProcessStartInfo psi = new ProcessStartInfo();
            string NetCorePack = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"Resources\MySQL_Server-8.0.21.0.msi");
            psi.FileName = NetCorePack;
            Process processo = Process.Start(psi);
            while (processo.HasExited is false)
            {

            }
            txb_Progresso.AppendText($"{Environment.NewLine}MySQL 8 Instalado com sucesso!");
        }
    }
}
