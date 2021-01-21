using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.ViewModels
{
    public class NOTAS_ENTRADAListVM
    {
        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<COMPRA> ListaEntradas
        {
            get { return _context.COMPRAs.Select(x => x).Include(x => x.FORNECEDOR).ToList(); }
        }
    }
}