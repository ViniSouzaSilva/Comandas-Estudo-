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
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AmbiStore.Telas;
using AmbiStore.Shared.EFCore.Services;
using Renci.SshNet.Messages;
using System.Windows;

namespace AmbiStore.ViewModels
{
    public class FUNCIONARIOCadastroVM : ViewModelBase
    {
        private FUNCIONARIO fUNCIONARIO;

        public FUNCIONARIO FUNCIONARIO
        {
            get { return fUNCIONARIO; }
            set
            {
                var eMITENTE = _context.EMITENTEs.Select(e => e).Include(s => s.END_MUNICIPIO).Where(e => e.ID == 1).FirstOrDefault();//First();
                fUNCIONARIO = value;
                                
                if (fUNCIONARIO.CONTATO_FUNCIONARIO is null || fUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO is null)
                    MUNICIPIOs = _context.MUNICIPIOs.
                        Select(s => s).
                        Where(s => s.UF == (eMITENTE.END_MUNICIPIO.UF)).OrderBy(x => x.MUN_DESC).ToList();
                else
                    MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == fUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO.UF).OrderBy(x => x.MUN_DESC).ToList();
            }
        }
        public List<string> UFs { get; set; }
        public List<MUNICIPIO> MUNICIPIOs { get; set; }
        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();

        public FUNCIONARIOCadastroVM()
        {
            UFs = _context.MUNICIPIOs.Select(p => p.UF).Distinct().OrderBy(y => y).ToList();
            if (FUNCIONARIO is null) FUNCIONARIO = new FUNCIONARIO();
            if (FUNCIONARIO.CONTATO_FUNCIONARIO is null) FUNCIONARIO.CONTATO_FUNCIONARIO = new CONTATO();
        }

        public string UFSelecionado
        {
            get
            {
                if (FUNCIONARIO is null || FUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO is null) return null;
                else return FUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO.UF;
            }
            set
            {
                MUNICIPIOs = _context.MUNICIPIOs.Select(s => s).Where(s => s.UF == value).OrderBy(x => x.MUN_DESC).ToList();
                OnPropertyChanged("MUNICIPIOs");
            }
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
                XmlRootAttribute xRoot = new XmlRootAttribute
                {
                    ElementName = "xmlcep"
                };
                //xRoot.IsNullable = true;
                XmlSerializer serializer = new XmlSerializer(typeof(xmlcep), xRoot);
                StringReader sr = new StringReader(textobaixado);
                xmlcep infoCEP = (xmlcep)serializer.Deserialize(sr);
                if (infoCEP.erro == true)
                {
                    MessageBox.Show("Erro ao obter informações do CEP. CEP inválido ou falha de comunicação.");
                    return;
                }
                FUNCIONARIO.CONTATO_FUNCIONARIO.ENDERECO_BAIRRO = infoCEP.bairro;
                FUNCIONARIO.CONTATO_FUNCIONARIO.ENDERECO_LOGRAD = infoCEP.logradouro.Substring(infoCEP.logradouro.IndexOf(' ') + 1);
                //OnPropertyChanged("ENDERECO_LOGRAD");
                if (Enum.IsDefined(typeof(TipoLograd), infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' '))))
                    FUNCIONARIO.CONTATO_FUNCIONARIO.ENDERECO_TIPO = Enum.Parse<TipoLograd>(infoCEP.logradouro.Substring(0, infoCEP.logradouro.IndexOf(' ')), true);
                else FUNCIONARIO.CONTATO_FUNCIONARIO.ENDERECO_TIPO = TipoLograd.Outros;
                //OnPropertyChanged("ENDERECO_TIPO");
                FUNCIONARIO.CONTATO_FUNCIONARIO.DDD_CELULAR1 = infoCEP.ddd;
                FUNCIONARIO.CONTATO_FUNCIONARIO.DDD_CELULAR2 = infoCEP.ddd;
                FUNCIONARIO.CONTATO_FUNCIONARIO.DDD_COMERCIAL = infoCEP.ddd;
                OnPropertyChanged("MUNICPIOs");

                var xx = _context.MUNICIPIOs.Select(m => m).Where(m => m.ID_MUNICIPIO == int.Parse(infoCEP.ibge)).FirstOrDefault();

                FUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO = xx;
                FUNCIONARIO.CONTATO_FUNCIONARIO.END_MUNICIPIO_ID = xx.ID_MUNICIPIO;
                UFSelecionado = infoCEP.uf;

                OnPropertyChanged("FUNCIONARIO");
                OnPropertyChanged("UFSelecionado");

            }
            catch (Exception)
            {
                throw;
            }
            return;
        }

        public string IDENTIF_1
        {
            get
            {
                if (!(fUNCIONARIO is null))
                {
                    if (!(fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF is null))
                    {
                        return fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF.CPF;
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
                    fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF.CPF = value.ParseToCPF();
                    OnPropertyChanged("CONTATO.CONTATO_PF.CPF");
                    OnPropertyChanged("CAMPO1");
                    OnPropertyChanged("CAMPO2");
                    OnPropertyChanged("CAMPO3");
                }
                else if (value.TiraPont().Length == 14)
                {
                    MessageBox.Show("Funcionários apenas podem ser pessoas físicas. Para terceirizados, utilize o cadastro de fornecedores.");
                    return;
                }
            }
        }
        public string IDENTIF_2
        {
            get
            {
                if (!(fUNCIONARIO is null && !(fUNCIONARIO.CONTATO_FUNCIONARIO is null)))
                {

                    if (!(fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF is null))
                    {
                        return fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF.RG;
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
                fUNCIONARIO.CONTATO_FUNCIONARIO.CONTATO_PF.RG = value;
            }
        }

        public async Task<bool> SaveChanges()
        {
            _context.Update(FUNCIONARIO);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
