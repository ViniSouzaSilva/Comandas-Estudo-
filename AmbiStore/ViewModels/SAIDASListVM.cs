using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.ViewModels
{
    public class NOTAS_SAIDAListVM
    {
        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<VENDA> ListaSaidas
        {
            get { return _context.VENDAs.Select(x => x).Include(x => x.CLIENTE).ToList(); }
        }
    }
}