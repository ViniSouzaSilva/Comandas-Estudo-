using AmbiSetup.Models;
using AmbiSetup.Telas;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace AmbiSetup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SetupWindow : Window
    {
        //Progress<(string, double)> progressIndicator;
        static AUX_SETUP_CLASS aUX = new AUX_SETUP_CLASS();
        Dictionary<int, Page> paginas = new Dictionary<int, Page>() {
            {1, new Intro() },
            {2, new EULA(ref aUX) { }  },
            {3, new Seriais(ref aUX) },
            {4, new InstallDir(ref aUX) },
            {5, new Componentes(ref aUX) },
            {6, new Installation(ref aUX) },
            {7, new Finalization() }
                    };
        int currentPage = 1;
        public SetupWindow()
        {
            InitializeComponent();
            but_Retornar.Visibility = Visibility.Hidden;
            ((EULA)paginas[2]).ComboChanged += EventDetected;

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("O AmbiStore não foi instalado. Deseja realmente cancelar a instalação?", "Instalação do AmbiStore", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void EventDetected(object sender, EventArgs e)
        {
            if (((EULA)sender).AUTORIZADO)
            {
                but_Avancar.IsEnabled = true;
            }
            else if (!((EULA)sender).AUTORIZADO)
            {
                but_Avancar.IsEnabled = false;
            }
        }

        private void Avancar_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 7)
            {
                Application.Current.Shutdown();
                return;
            }
            if (currentPage == 4)
            {
                try
                {
                    Path.GetFullPath(aUX.INSTALL_LOCATION);
                }
                catch (Exception)
                {
                    MessageBox.Show("O caminho digitado não é válido");
                    return;
                }
            }
            if (currentPage == 3)
            {
                if (!((Seriais)paginas[3]).ValidarCampos())
                {
                    MessageBox.Show("Um ou mais seriais estão incorretos. Por favor, verifique e tente novamente",
                        "Instalação do AmbiStore",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
            }
            //else
            {
                frame_Frame.Navigate(paginas[currentPage + 1]);
                currentPage++;
            }
        }

        private void Retornar_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage == 1)
            {

            }
            else
            {
                frame_Frame.Navigate(paginas[currentPage - 1]);
                currentPage--;
            }
        }

        private void frame_Frame_Navigated(object sender, NavigationEventArgs e)
        {
            switch (currentPage)
            {
                case 1: //Intro
                    but_Cancelar.Visibility = Visibility.Visible;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Collapsed;
                    but_Cancelar.IsEnabled = true;
                    but_Avancar.IsEnabled = true;
                    but_Retornar.IsEnabled = false;
                    break;
                case 2: //EULA
                    but_Cancelar.Visibility = Visibility.Visible;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Visible;
                    but_Cancelar.IsEnabled = true;
                    but_Retornar.IsEnabled = true;
                    but_Avancar.IsEnabled = aUX.EULA_ACEITO switch
                    {
                        true => true,
                        _ => false
                    };
                    break;
                case 3: //Seriais
                    but_Cancelar.Visibility = Visibility.Visible;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Visible;
                    but_Cancelar.IsEnabled = true;
                    but_Avancar.IsEnabled = true;
                    but_Retornar.IsEnabled = true;
                    break;
                case 4: //InstallDir
                    but_Cancelar.Visibility = Visibility.Visible;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Visible;
                    but_Cancelar.IsEnabled = true;
                    but_Avancar.IsEnabled = true;
                    but_Retornar.IsEnabled = true;
                    break;
                case 5: //Componentes
                    but_Cancelar.Visibility = Visibility.Visible;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Visible;
                    but_Cancelar.IsEnabled = true;
                    but_Avancar.IsEnabled = true;
                    but_Retornar.IsEnabled = true;
                    break;
                case 6: //Installation
                    but_Cancelar.Visibility = Visibility.Collapsed;
                    but_Avancar.Visibility = Visibility.Collapsed;
                    but_Retornar.Visibility = Visibility.Collapsed;
                    but_Cancelar.IsEnabled = false;
                    but_Avancar.IsEnabled = false;
                    but_Retornar.IsEnabled = false;
                    break;
                case 7: //Finalization
                    but_Cancelar.Visibility = Visibility.Collapsed;
                    but_Avancar.Visibility = Visibility.Visible;
                    but_Retornar.Visibility = Visibility.Collapsed;
                    but_Cancelar.IsEnabled = false;
                    but_Avancar.IsEnabled = true;
                    but_Retornar.IsEnabled = false;
                    but_Avancar.Content = "Finalizar";
                    break;
                default:
                    break;
            }
        }
    }
}
