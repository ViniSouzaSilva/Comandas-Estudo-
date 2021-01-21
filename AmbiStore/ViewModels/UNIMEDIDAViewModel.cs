using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using AmbiStore.Shared.Libraries.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbiStore.ViewModels
{
    public class UNIMEDIDAViewModel : ViewModelBase
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<UNI_MEDIDA> UNIMEDIDAs
        {
            get
            {
                return _context.UNI_MEDIDAs.Select(x => x).Where(x => x.STATUS == Status.Ativo).ToList();
            }
        }

        private UNI_MEDIDA uniMedSelecionado;

        public UNI_MEDIDA UniMedSelecionado
        {
            get { return uniMedSelecionado; }
            set
            {
                uniMedSelecionado = value;
                OnPropertyChanged("UniMedSelecionado");
            }
        }


        public void SalvaSelecionado()
        {
            if (_context.UNI_MEDIDAs.Select(x => x).Where(x => x.ABREVIATURA == UniMedSelecionado.ABREVIATURA).Any())
            {
                _context.UNI_MEDIDAs.Update(UniMedSelecionado);
            }
            else
            {
                UniMedSelecionado.STATUS = Status.Ativo;
                _context.UNI_MEDIDAs.Add(UniMedSelecionado);
            }
            _context.SaveChanges();
            OnPropertyChanged("UNIMEDIDAs");
        }
    }
}
