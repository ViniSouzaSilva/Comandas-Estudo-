using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbiStore.ViewModels
{
    public class GRUPOSViewModel : ViewModelBase
    {
        AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<GRUPO> GRUPOs
        {
            get
            {
                return _context.GRUPOs.Select(x => x).ToList();
            }
        }

        private GRUPO grupoSelecionado;

        public GRUPO GrupoSelecionado
        {
            get { return grupoSelecionado; }
            set {
                grupoSelecionado = value;
                OnPropertyChanged("GrupoSelecionado");
            }
        }


        public void SalvaSelecionado()
        {
            _context.GRUPOs.Update(GrupoSelecionado);
            _context.SaveChanges();
            OnPropertyChanged("GRUPOs");
        }
    }
}
