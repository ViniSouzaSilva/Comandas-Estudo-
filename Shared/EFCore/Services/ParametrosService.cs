using AmbiStore.Shared.EFCore.Data;
using AmbiStore.Shared.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmbiStore.Shared.EFCore.Services
{
    public class ParametrosService
    {
        private readonly AmbiStoreDbContextFactory _contextFactory;

        public ParametrosService(AmbiStoreDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<string> Load(string chave)
        {
            using AmbiStoreDbContext context = _contextFactory.CreateDbContext();
            PARAMETRO entity = await context.PARAMETROs.Select(x => x).Where(x => x.CHAVE == chave).FirstOrDefaultAsync();
            return entity.VALOR;
        }

        public async Task Save((string chave, string valor) dados)
        {
            using AmbiStoreDbContext context = _contextFactory.CreateDbContext();
            PARAMETRO entity = new PARAMETRO() { CHAVE = dados.chave, VALOR = dados.valor };
            context.PARAMETROs.Update(entity);
            await context.SaveChangesAsync();
        }

    }
}
