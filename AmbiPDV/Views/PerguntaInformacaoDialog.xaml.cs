using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AmbiPDV.Views
{
    /// <summary>
    /// Interaction logic for PerguntaInformacaoDialog.xaml
    /// </summary>
    public partial class PerguntaInformacaoDialog : Window, INotifyPropertyChanged
    {
        public string INFORMACAO_STR { get; set; }
        public int INFORMACAO_INT { get { return int.Parse(INFORMACAO_STR); } }
        public decimal INFORMACAO_DEC { get { return decimal.Parse(INFORMACAO_STR.Replace(',', '.'), CultureInfo.InvariantCulture); } }
        public PerguntaInformacaoDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void PerguntaSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) DialogResult = true;
            if (e.Key == Key.Escape) DialogResult = false;
        }

        public PerguntaInformacaoDialog(string tITULO, string sUBTITULO)
        {
            InitializeComponent();
            txb_Titulo.Text = tITULO;
            txb_Subtitulo.Text = sUBTITULO;

        }
    }
}
