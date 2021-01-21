using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.ViewModels
{
    public class FUNCIONARIOSListVM
    {
        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<FUNCIONARIO> ListaFuncionarios
        {
            get
            {
                return _context.FUNCIONARIOs.Select(x => x)
                  .Include(x => x.CONTATO_FUNCIONARIO)
                  .ThenInclude(y=>y.CONTATO_PF)
                  .Include(x=>x.CONTATO_FUNCIONARIO.END_MUNICIPIO)
                  .Where(x => x.ID > -1)
                  .ToList();
            }
        }
    }
}