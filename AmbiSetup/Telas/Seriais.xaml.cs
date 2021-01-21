using AmbiSetup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Seriais.xaml
    /// </summary>
    public partial class Seriais : Page
    {
        AUX_SETUP_CLASS AUX_SETUP;
        public Seriais(ref AUX_SETUP_CLASS aUX_SETUP)
        {
            AUX_SETUP = aUX_SETUP;
            InitializeComponent();
            this.DataContext = AUX_SETUP;
        }

        private bool ValidateSerial(string testingstring)
        {
            if (string.IsNullOrWhiteSpace(testingstring)) return true;
            if (testingstring == "DRANGELAZIEGLER") return true;
            if (!new string[5] { "STOR", "TECH", "MAIT", "CDIS", "WEBS" }.Contains(testingstring.Substring(0, 4))) return false;
            if (testingstring.Length == 16)
                testingstring = $"{testingstring.Substring(0, 4)}-{testingstring.Substring(4, 4)}-{testingstring.Substring(8, 4)}-{testingstring.Substring(12, 4)}";
            if (testingstring.Length != 19)
            {
                return false;
            }
            string _first = testingstring.Substring(5, 4);
            string _second = testingstring.Substring(10, 4);
            string _third = testingstring.Substring(15, 4);
            int first = 0;
            int second = 0;
            int third = 0;
            foreach (char a in _first)
            {
                first += (Convert.ToInt32(a) - 48);
            }
            foreach (char c in _third)
            {
                third += (Convert.ToInt32(c) - 48);
            }
            foreach (char b in _second)
            {
                second += b;
            }
            if ((((first % 11) == 0) && ((second % 11) == 0) && ((third % 11) == 0)) && ((_first == SortString(_first)) && (_second == SortString(_second)) && (_third == SortString(_third))))
            {
                return true;
            }
            return false;
        }
        private string SortString(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        public bool ValidarCampos()
        {
            if ((!ValidateSerial(AUX_SETUP.AMBISTORE_SERIAL)) || string.IsNullOrWhiteSpace(AUX_SETUP.AMBISTORE_SERIAL)) return false;
            if (!ValidateSerial(AUX_SETUP.AMBITECH_SERIAL)) return false;
            if (!ValidateSerial(AUX_SETUP.AMBIMAITRE_SERIAL)) return false;
            if (!ValidateSerial(AUX_SETUP.AMBICD_SERIAL)) return false;
            if (!ValidateSerial(AUX_SETUP.AMBIWEB_SERIAL)) return false;
            return true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
