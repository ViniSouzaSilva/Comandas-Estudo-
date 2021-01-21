using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AmbiStore.Shared.Extension;
using System.Net;
using System.Xml.Serialization;
using System.IO;
using AmbiStore.Auxiliares;
using System.Windows.Data;
using System.Windows.Input;
using AmbiStore.Shared.EFCore.Services;
using System.Threading.Tasks;
using AmbiStore.Shared.Libraries.Validations;

namespace AmbiStore.ViewModels
{
    public class EMITENTEViewModel : ViewModelBase
    {
        public AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        private EMITENTE eMITENTE;

        public EMITENTE EMITENTE
        {
            get { return eMITENTE; }
            set
            {
                eMITENTE = value;
                if (eMITENTE is null) eMITENTE = new EMITENTE();
                if (eMITENTE.DT_COMPRA_SISTEMA == DateTime.MinValue) eMITENTE.DT_COMPRA_SISTEMA = DateTime.Today;
                if (!(eMITENTE.END_MUNICIPIO is null))
                    MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == eMITENTE.END_MUNICIPIO.UF).OrderBy(x => x.MUN_DESC).ToList();

                eMITENTE.PropertyChanged += EMITENTE_PropertyChanged;
            }
        }

        private void EMITENTE_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SIMPLES_NACIONAL") return;
        }

        public bool SNSIMChecado
        {
            get
            {
                if (EMITENTE is null) return false;
                else return EMITENTE.SIMPLES_NACIONAL;
            }
            set
            {
                if (value is true)
                {
                    EMITENTE.SIMPLES_NACIONAL = true;
                    SNNAOChecado = false;
                }
                OnPropertyChanged("SNSIMChecado");
            }
        }

        internal void AlteraEmitente()
        {
            
       
        }

        public bool SNNAOChecado
        {
            get
            {
                if (EMITENTE is null) return false;
                else return !EMITENTE.SIMPLES_NACIONAL;
            }
            set
            {
                if (value is true)
                {
                    EMITENTE.SIMPLES_NACIONAL = false;
                    SNSIMChecado = false;

                }
                OnPropertyChanged("SNNAOChecado");

            }
        }
        public bool RTNORMChecado
        {
            get { return !EMITENTE.RT_EXCEDE_SUBLIMITE; }
            set { }
        }
        public bool RTEXCEChecado
        {
            get { return EMITENTE.RT_EXCEDE_SUBLIMITE; }
            set { EMITENTE.RT_EXCEDE_SUBLIMITE = value; }
        }

        public string UFSelecionado
        {
            get
            {
                if (EMITENTE is null || EMITENTE.END_MUNICIPIO is null) return null;
                else return EMITENTE.END_MUNICIPIO.UF;
            }
            set
            {
                MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == value).OrderBy(x => x.MUN_DESC).ToList();
                OnPropertyChanged("UFSelecionado");
                OnPropertyChanged("MUNICIPIOs");
            }
        }

        public List<string> RAMOS_ATIV { get; set; }
        public List<string> UFs { get; set; }
        public List<MUNICIPIO> MUNICIPIOs { get; set; }

        public async Task<bool> SaveChanges()
        {
            _context.Update(EMITENTE);
            await _context.SaveChangesAsync();
            return true;
        }

        public EMITENTEViewModel()
        {
            UFs = _context.MUNICIPIOs.Select(p => p.UF).Distinct().OrderBy(y => y).ToList();
            RAMOS_ATIV = new List<string>();
            string[] textobaixado;
            try
            {
                using var client = new WebClient();
                textobaixado = client.DownloadString("https://gist.githubusercontent.com/prodis/857240/raw/482c09052fbd44142962dd35097a33841e896690/ramos_de_atividade.txt").Split("\n");

            }
            catch (Exception)
            {
                throw;
            }
            RAMOS_ATIV.AddRange(textobaixado);
        }



        public void ObtemDadosViaCEP(string cep)
        {
            cep = cep.TiraPont();
            if (cep.Length != 8) return;
            string textobaixado;
            try
            {
                using var client = new WebClient();
                textobaixado = client.DownloadString("https://viacep.com.br/ws/" + cep + "/xml/");

            }
            catch (Exception)
            {
                throw;
            }
            try
            {
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "xmlcep";
                //xRoot.IsNullable = true;
                XmlSerializer serializer = new XmlSerializer(typeof(xmlcep), xRoot);
                StringReader sr = new StringReader(textobaixado);
                xmlcep infoCEP = (xmlcep)serializer.Deserialize(sr);
                EMITENTE.ENDERECO_BAIRRO = infoCEP.bairro;
                //OnPropertyChanged("ENDERECO_BAIRRO");
                EMITENTE.ENDERECO_LOGRAD = infoCEP.logradouro.Substring(infoCEP.logradouro.IndexOf(' ') + 1);
                //OnPropertyChanged("ENDERECO_LOGRAD");
                if (Enum.IsDefined(typeof(TipoLograd), infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' '))))
                    EMITENTE.ENDERECO_TIPO = Enum.Parse<TipoLograd>(infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' ')), true);
                else EMITENTE.ENDERECO_TIPO = TipoLograd.Outros;
                //OnPropertyChanged("ENDERECO_TIPO");
                EMITENTE.DDD_COMERCIAL = infoCEP.ddd;
                OnPropertyChanged("MUNICPIOs");

                var xx = _context.MUNICIPIOs.Select(m => m).Where(m => m.ID_MUNICIPIO == int.Parse(infoCEP.ibge)).FirstOrDefault();

                EMITENTE.END_MUNICIPIO = xx;
                EMITENTE.END_MUNICIPIO_ID = xx.ID_MUNICIPIO;
                UFSelecionado = infoCEP.uf;

                OnPropertyChanged("EMITENTE");
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }

#if ICOMMAND
        private ICommand uCommand;

        public ICommand Update
        {
            get
            {
                if (uCommand == null) uCommand = new Updater(this);
                return uCommand;
            }
            set { uCommand = value; }
        }
#endif

    }

#if ICOMMAND
    public class Updater : ICommand
    {
        private readonly EMITENTEViewModel _evm;

        public Updater(EMITENTEViewModel evm)
        {
            _evm = evm;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _evm._context.SaveChangesAsync();
        }
    }
#endif

}
