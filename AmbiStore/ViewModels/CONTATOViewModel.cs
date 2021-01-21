using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Exceptions;
using static AmbiStore.Shared.Extension.StringExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using AmbiStore.Shared.EFCore.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using AmbiStore.Shared.Libraries.Validations;
using System.Xml.Serialization;
using System.Net;
using AmbiStore.Auxiliares;
using System.IO;
using AmbiStore.Shared.Libraries.Enums;
using AmbiStore.Shared.EFCore.Services;
using System.Threading.Tasks;

namespace AmbiStore.ViewModels
{

    public class CONTATOViewModel : ViewModelBase
    {
        private enum TipoCliente { FISICA, JURIDICA };
        private TipoCliente tipoCliente;
        private CONTATO cONTATO;

        public CONTATO CONTATO
        {
            get { return cONTATO; }
            set
            {
                var eMITENTE = _context.EMITENTEs.Select(e => e).Include(s => s.END_MUNICIPIO).Where(e => e.ID == 1).First();
                cONTATO = value;
                if (cONTATO.END_MUNICIPIO is null)
                    MUNICIPIOs = _context.MUNICIPIOs.
                        Select(s => s).
                        Where(s => s.UF == eMITENTE.END_MUNICIPIO.UF).OrderBy(x => x.MUN_DESC).ToList();
                else
                    MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == cONTATO.END_MUNICIPIO.UF).OrderBy(x => x.MUN_DESC).ToList();

                cONTATO.PropertyChanged += CONTATO_PropertyChanged;
            }
        }





        public bool EDITAVEL { get { if (CONTATO.CONTATO_PJ is null && CONTATO.CONTATO_PF is null) return true; else return false; } }

        [CPForCPNPJ]
        public string IDENTIF_1
        {
            get
            {
                if (!(CONTATO is null))
                {
                    if (!(CONTATO.CONTATO_PF is null) && !(CONTATO.CONTATO_PJ is null))
                    {
                        return tipoCliente switch
                        {
                            TipoCliente.FISICA => CONTATO.CONTATO_PF.CPF,
                            TipoCliente.JURIDICA => CONTATO.CONTATO_PJ.CNPJ,
                            _ => null
                        };

                    }
                    else if ((CONTATO.CONTATO_PF is null) && !(CONTATO.CONTATO_PJ is null))
                    {
                        tipoCliente = TipoCliente.JURIDICA;
                        return CONTATO.CONTATO_PJ.CNPJ;
                    }
                    else if (!(CONTATO.CONTATO_PF is null) && (CONTATO.CONTATO_PJ is null))
                    {
                        tipoCliente = TipoCliente.FISICA;
                        return CONTATO.CONTATO_PF.CPF;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else return null;
            }
            set
            {
                if (value.TiraPont().Length == 11)
                {
                    CONTATO.CONTATO_PF = new CONTATO_PF();
                    tipoCliente = TipoCliente.FISICA;
                    CONTATO.CONTATO_PF.CPF = value.ParseToCPF();
                    OnPropertyChanged("CONTATO.CONTATO_PF.CPF");
                    OnPropertyChanged("CAMPO1");
                    OnPropertyChanged("CAMPO2");
                }
                else if (value.TiraPont().Length == 14)
                {
                    CONTATO.CONTATO_PJ = new CONTATO_PJ();
                    tipoCliente = TipoCliente.JURIDICA;
                    CONTATO.CONTATO_PJ.CNPJ = value.ParseToCNPJ();
                    OnPropertyChanged("CONTATO.CONTATO_PF.CNPJ");
                    OnPropertyChanged("CAMPO1");
                    OnPropertyChanged("CAMPO2");
                }
                else
                {

                }
            }
        }
        public string IDENTIF_2
        {
            get
            {
                if (!(CONTATO is null))
                {

                    if (!(CONTATO.CONTATO_PF is null) && !(CONTATO.CONTATO_PJ is null))
                    {
                        return tipoCliente switch
                        {
                            TipoCliente.FISICA => CONTATO.CONTATO_PF.RG,
                            TipoCliente.JURIDICA => CONTATO.CONTATO_PJ.IE,
                            _ => null
                        };

                    }
                    else if ((CONTATO.CONTATO_PF is null) && !(CONTATO.CONTATO_PJ is null))
                    {
                        return CONTATO.CONTATO_PJ.IE;
                    }
                    else if (!(CONTATO.CONTATO_PF is null) && (CONTATO.CONTATO_PJ is null))
                    {
                        return CONTATO.CONTATO_PF.RG;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else return null;
            }
            set
            {
                switch (tipoCliente)
                {
                    case TipoCliente.FISICA:
                        CONTATO.CONTATO_PF.RG = value;
                        break;
                    case TipoCliente.JURIDICA:
                        CONTATO.CONTATO_PJ.IE = value;
                        break;
                    default:
                        break;
                }
            }
        }
        public string CAMPO2
        {
            get
            {
                return tipoCliente switch
                {
                    TipoCliente.FISICA => "RG: ",
                    TipoCliente.JURIDICA => "IE: ",
                    _ => "ERRO"
                };
            }
        }
        public string CAMPO1
        {
            get
            {
                return tipoCliente switch
                {
                    TipoCliente.FISICA => "CPF/CNPJ: ",
                    TipoCliente.JURIDICA => "CPF/CNPJ: ",
                    _ => "ERRO"
                };
            }
        }
        public List<string> UFs { get; set; }
        public List<MUNICIPIO> MUNICIPIOs { get; set; }
        public List<FUNCIONARIO> FUNCIONARIOs { get; set; }

        public string UFSelecionado
        {
            get
            {
                if (CONTATO is null || CONTATO.END_MUNICIPIO is null) return null;
                else return CONTATO.END_MUNICIPIO.UF;
            }
            set
            {
                MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == value).OrderBy(x => x.MUN_DESC).ToList();
                OnPropertyChanged("MUNICIPIOs");
            }
        }


        private AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public async Task<bool> SaveChanges()
        {
            _context.Update(CONTATO);
            await _context.SaveChangesAsync();
            return true;
        }


        public CONTATOViewModel()
        {
            cONTATO = new CONTATO();
            UFs = _context.MUNICIPIOs.Select(p => p.UF).Distinct().OrderBy(y => y).ToList();
            //FUNCIONARIOs =
            //    _context.FUNCIONARIOs
            //    .Include(x => x.CONTATO_FUNCIONARIO)
            //    .Select(s => s)
            //    .Where(s => s.CARGO.Contains(
            //        _context.CARGO_FUNCIONARIOs
            //        .Select(s => s)
            //        .Where(s => s.CARGO == Shared.Libraries.Enums.Cargo.Vendedor)
            //        .First())
            //    )
            //    .ToList();
        }

        private void CONTATO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

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
                CONTATO.ENDERECO_BAIRRO = infoCEP.bairro;
                CONTATO.ENDERECO_LOGRAD = infoCEP.logradouro.Substring(infoCEP.logradouro.IndexOf(' ') + 1);
                //OnPropertyChanged("ENDERECO_LOGRAD");
                if (Enum.IsDefined(typeof(TipoLograd), infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' '))))
                    CONTATO.ENDERECO_TIPO = Enum.Parse<TipoLograd>(infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' ')), true);
                else CONTATO.ENDERECO_TIPO = TipoLograd.Outros;
                //OnPropertyChanged("ENDERECO_TIPO");
                CONTATO.DDD_COMERCIAL = infoCEP.ddd;
                OnPropertyChanged("MUNICPIOs");

                var xx = _context.MUNICIPIOs.Select(m => m).Where(m => m.ID_MUNICIPIO == int.Parse(infoCEP.ibge)).FirstOrDefault();

                CONTATO.END_MUNICIPIO = xx;
                CONTATO.END_MUNICIPIO_ID = xx.ID_MUNICIPIO;
                UFSelecionado = infoCEP.uf;

                OnPropertyChanged("CONTATO");
            }
            catch (Exception)
            {
                throw;
            }
            return;
        }

    }
}
