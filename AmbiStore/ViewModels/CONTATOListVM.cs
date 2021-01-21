using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmbiStore.ViewModels
{
    public class CONTATOListVM
    {
        private readonly AmbiStoreDbContext _context = new AmbiStoreDbContextFactory().CreateDbContext();
        public List<CONTATO> ListaContatos
        {
            get { return _context.CONTATOs.Select(C => C)
                    .Include(C => C.CONTATO_PF)
                    .Include(C => C.CONTATO_PJ)
                    .Include(C=> C.VENDEDOR_PREF)
                    .ThenInclude(C=>C.CONTATO_FUNCIONARIO)
                    .Where(x=>x.ID > 0).ToList(); }
        }
        public bool IsFuncionario(CONTATO cONTATO)
        {
            return _context.FUNCIONARIOs.Select(x => x).Where(x => x.CONTATO_FUNCIONARIO == cONTATO).Any();
        }
    }
}